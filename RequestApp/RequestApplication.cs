using AutoMapper;
using CashBookAPIClient.Interfaces;
using CashBookDomain.DTO;
using CashBookDomain.Entities.Enums;
using Infrastructure.ErrorMessages;
using Infrastructure.Pagination;
using RequestApp.Interfaces;
using RequestData.Repository.Interfaces;
using RequestDomain.DTO;
using RequestDomain.Entities;
using RequestDomain.Entities.Enums;
using RequestDomain.Validators;
using System.Net;

namespace RequestApp
{
	public class RequestApplication : IRequestApplication
	{
		private readonly IRequestRepository _requestRepository;
		private readonly ICashBookClient _cashBookClient;
		private readonly IProductRequestRepository _productRepository;
		private readonly IProductRequestApplication _productApplication;
		private readonly IMapper _mapper;

		public RequestApplication(IRequestRepository requestRepository, ICashBookClient cashBookClient, IProductRequestRepository productRepository, IProductRequestApplication productApplication, IMapper mapper)
		{
			_requestRepository = requestRepository;
			_cashBookClient = cashBookClient;
			_productRepository = productRepository;
			_productApplication = productApplication;
			_mapper = mapper;
		}
		public async Task<Request> AddRequest(RequestDto requestDto)
		{
			var request = _mapper.Map<Request>(requestDto);

			var validator = new RequestValidator();
			var result = validator.Validate(request);

			if (request.Status == EnumStatus.Received)
				request.DeliveryDate = DateTimeOffset.MinValue;

			if (!result.IsValid)
			{ 
				var error = new Error<RequestDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(),
						result.Errors.ConvertAll(r => r.ErrorMessage.ToString()), requestDto);
				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}

			await _requestRepository.AddAsync(request);
			return request;
		}
		public async Task<Request> DeleteRequest(Guid id)
		{
			var request = await _requestRepository.GetByIdAsync(id);
			var requestDto = _mapper.Map<RequestDto>(request);

			if (request == null)
			{
				var error = NotFound(requestDto);
				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}

			await _requestRepository.DeleteAsync(request);

			if (request.Status == EnumStatus.Finished)
			{
				var response = await _cashBookClient.AddCashBook(EnumOrigin.BuyRequest, id, $"Revert BuyRequest with id: {request.Id}", EnumType.Reversal, -request.TotalValue);

				if (response == false)
				{
					var error = BadRequest(requestDto, "Error in the communication with the CashBookAPI.");
					var errors = ListOfErrors(error);
					throw new Exception(errors);
				}
			}

			return request;
		}
		public async Task<IEnumerable<Request>> GetAllRequests(PageParameters page)
		{
			var requestDto = new RequestDto();			
			var requests = await _requestRepository.GetAllWithPaging(page);

			if (requests.Count() == 0)
			{
				var error = NotFound(requestDto);
				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}

			return requests;
		}
		public async Task<Request> GetRequestByClientId(Guid clientId)
		{
			var requestDto = new RequestDto();
			var request = await _requestRepository.GetAsync(r => r.Client == clientId);
			
			if (request == null)
			{
				var error= NotFound(requestDto);
				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}

			return request;
		}
		public async Task<Request> GetRequestById(Guid id)
		{
			var requestDto = new RequestDto();
			var request= await _requestRepository.GetByIdAsync(id);

			if(request == null)
			{
				var error= NotFound(requestDto);
				var errors= ListOfErrors(error);
				throw new Exception(errors);
			}
			
			return request;
		}
		public async Task<Request> UpdateRequest(RequestDto requestDto)
		{
			var request = await _requestRepository.GetByIdAsync(requestDto.Id);

			if (request == null)
			{
				var error1 = NotFound(requestDto);
				var errors1= ListOfErrors(error1);
				throw new Exception(errors1);
			}

			if (request.Status == EnumStatus.Finished && requestDto.Status != EnumStatus.Finished)
			{
				var error2 = BadRequest(requestDto, "Request status is Finished, you can only delete it");
				var errors2 = ListOfErrors(error2);
				throw new Exception(errors2);
			}

			var requestMap = _mapper.Map<Request>(requestDto);

			var validator = new RequestValidator();
			var result = validator.Validate(requestMap);
			
			if (!result.IsValid)
			{
				var errors3 = new Error<RequestDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(),result.Errors.ConvertAll(r => r.ErrorMessage.ToString()), requestDto);
				var error3 = ListOfErrors(errors3);
				throw new Exception(error3);
			}
			else
			{
				var productId = requestDto.Products.Select(r => r.Id).ToList();
				var products = _productApplication.GetAllProducts().Result.Where(p => p.RequestId == requestDto.Id);
				var productDelete = products.Where(p => !productId.Contains(p.Id)).ToList();

				foreach (var prods in productDelete)
				{
					var prod = _productRepository.GetByIdAsync(prods.Id).Result;
					await _productRepository.DeleteAsync(prod);
				}

				await _requestRepository.UpdateAsync(requestMap);
			}

			if (requestMap.Status == EnumStatus.Finished)
			{
				var type = EnumType.Receipt;
				var value = requestMap.TotalValue;
				string description = $"Finantial transaction RequestId: {request.Id}";
				var status = request.Status;

				if(requestMap.Status == status && requestMap.Status == EnumStatus.Finished && value > request.TotalValue)
				{
					description = $"Difference purchase RequestId: {request.Id}";
					value = requestMap.TotalValue - request.TotalValue;
					type = EnumType.Receipt;
				}
				else if (requestMap.Status == status && requestMap.Status == EnumStatus.Finished && request.TotalValue > value)
				{
					description = $"Difference purchase RequestId: {request.Id}";
					value = requestMap.TotalValue - request.TotalValue;
					type = EnumType.Payment;
				}
				else
				{
					var error4 = BadRequest(requestDto, "The TotalValue was not changed!");
					var errors4= ListOfErrors(error4);
					throw new Exception(errors4);
				}

				var response = await _cashBookClient.AddCashBook(EnumOrigin.BuyRequest, requestMap.Id, description, type, value);

				if(response == false)
				{
					var error5 = BadRequest(requestDto, "Error in the communication with the CashBookAPI.");
					var errors5 = ListOfErrors(error5);
					throw new Exception(errors5);
				}
			}
			
			return requestMap;
		}
		public async Task<Request> UpdateRequestStatus(Guid id, EnumStatus status)
		{
			var request = await _requestRepository.GetByIdAsync(id);
			var requestDto = _mapper.Map<RequestDto>(request);

			if (request == null)
			{
				var error1 = NotFound(requestDto);
				var errors1 = ListOfErrors(error1);
				throw new Exception(errors1);
			}

			if (request.Status == EnumStatus.Finished)
			{
				var error2 = BadRequest(requestDto, "Request status is Finished, you can only delete it");
				var errors2 = ListOfErrors(error2);
				throw new Exception(errors2);
			}

			request.Status = status;
			request.DeliveryDate = DateTimeOffset.UtcNow;

			await _requestRepository.UpdateAsync(request);

			if (request.Status == EnumStatus.Finished)
			{
				var response = await _cashBookClient.AddCashBook(EnumOrigin.BuyRequest, request.Id, $"RequestId: {request.Id}", EnumType.Receipt, request.TotalValue);

				if(response == false)
				{
					var error3 = BadRequest(requestDto, "Error in the communication with the CashBookAPI.");
					var errors3 = ListOfErrors(error3);
					throw new Exception(errors3);
				}
			}

			return request;
		}

		public string ListOfErrors(Error<RequestDto> error)
		{
			var errors = string.Empty;

			foreach (var item in error.Message)
				errors += item.ToString() + " ";

			return errors;
		}
		public Error<RequestDto> NotFound(RequestDto requestDto)
		{
			var errors = new List<string>();
			errors.Add("The Request was not found in the DataBase!");

			var error = new Error<RequestDto>(HttpStatusCode.NoContent.GetHashCode().ToString(), errors, requestDto);
			return error;
		}
		public Error<RequestDto> BadRequest(RequestDto requestDto, string message)
		{
			var errors = new List<string>();
			errors.Add(message);

			var error = new Error<RequestDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), errors, requestDto);
			return error;
		}
	}
}
