using AutoMapper;
using CashBookApp.Interfaces;
using CashBookData.Repositories.Interfaces;
using CashBookDomain.DTO;
using CashBookDomain.Entities;
using CashBookDomain.Entities.Enums;
using CashBookDomain.Validators;
using CashBookDomain.Models;
using Infrastructure.ErrorMessages;
using Infrastructure.Pagination;
using System.Net;

namespace CashBookApp
{
	public class CashBookApplication : ICashBookApplication
	{
		private readonly ICashBookRepository _cashBookRepository;
		private readonly IMapper _mapper;

		public CashBookApplication(ICashBookRepository cashBookRepository, IMapper mapper)
		{
			_cashBookRepository = cashBookRepository;
			_mapper = mapper;
		}

		public async Task AddCashBook(CashBookDto cashBookDto)
		{
			var cashBook = _mapper.Map<CashBook>(cashBookDto);

			var validator = new CashBookValidator();
			var result = validator.Validate(cashBook);

			if (!result.IsValid)
			{
				var error = new Error<CashBookDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(),
					result.Errors.ConvertAll(c => c.ErrorMessage.ToString()), cashBookDto);

				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}

			await _cashBookRepository.AddAsync(cashBook);
		}
		public async Task<CashBookModel> GetAllCashBooks(PageParameters page)
		{
			CashBookDto cashBookDto = new CashBookDto();
			CashBookModel cashBookModel= new CashBookModel();

			cashBookModel.Models = await _cashBookRepository.GetAllWithPaging(page);

			if (cashBookModel.Models.Count() == 0)
			{
				var error = NotFound(cashBookDto);
				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}
			
			cashBookModel.Total= cashBookModel.Models.Sum(c => c.Value);
			return cashBookModel;
		}
		public async Task<CashBook> GetCashBookById(Guid id)
		{
			CashBookDto cashBookDto = new CashBookDto();
			var cashBook = await _cashBookRepository.GetByIdAsync(id);

			if (cashBook == null)
			{
				var error = NotFound(cashBookDto);
				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}

			return cashBook;
		}
		public async Task<CashBook> GetCashBookByOriginId(Guid originId)
		{
			CashBookDto cashBookDto = new CashBookDto();
			var cashBook= await _cashBookRepository.GetAsync(c => c.OriginId == originId);
			
			if (cashBook == null)
			{
				var error = NotFound(cashBookDto);
				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}

			return cashBook;
		}
		public async Task<CashBook> UpdateCashBook(Guid id, CashBookDto cashBookDto)
		{
			var cashBook = await _cashBookRepository.GetByIdAsync(id);

			if (cashBook == null)
			{
				var error1 = NotFound(cashBookDto);
				var errors1 = ListOfErrors(error1);
				throw new Exception(errors1);
			}

			if (cashBook.OriginId != null)
			{
				var error2 = BadRequest(cashBookDto, "This CashBook was integrated, so it can't be changed.");
				var errors2 = ListOfErrors(error2);
				throw new Exception(errors2);
			}

			var cashBookUpdate = _mapper.Map<CashBook>(cashBookDto);
			cashBookUpdate.Id = id;

			var validator = new CashBookValidator();
			var result = validator.Validate(cashBookUpdate);
			if (!result.IsValid)
			{
				var error3 = new Error<CashBookDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(),
					result.Errors.ConvertAll(c => c.ErrorMessage.ToString()), cashBookDto);

				var errors3 = ListOfErrors(error3);
				throw new Exception(errors3);
			}
			
			await _cashBookRepository.UpdateAsync(cashBookUpdate);
			return cashBookUpdate;
		}

		public string ListOfErrors(Error<CashBookDto> error)
		{
			var errors = string.Empty;

			foreach (var item in error.Message)
				errors += item.ToString() + " ";

			return errors;
		}
		public Error<CashBookDto> NotFound(CashBookDto cashBookDto)
		{
			var errors = new List<string>();
			errors.Add("The CashBook was not found in the DataBase!");

			var error = new Error<CashBookDto>(HttpStatusCode.NoContent.GetHashCode().ToString(), errors, cashBookDto);
			return error;
		}
		public Error<CashBookDto> BadRequest(CashBookDto cashBookDto, string message)
		{
			var errors = new List<string>();
			errors.Add(message);

			var error = new Error<CashBookDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), errors, cashBookDto);
			return error;
		}
	}
}
