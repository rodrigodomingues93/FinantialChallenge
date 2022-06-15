using AutoMapper;
using CashBookAPIClient.Interfaces;
using CashBookDomain.DTO;
using CashBookDomain.Entities.Enums;
using DocumentApp.Interfaces;
using DocumentData.Repositories.Interfaces;
using DocumentDomain.DTO;
using DocumentDomain.Entities;
using DocumentDomain.Entities.Enums;
using DocumentDomain.Validators;
using Infrastructure.ErrorMessages;
using Infrastructure.Pagination;
using System.Net;

namespace DocumentApp
{
	public class DocumentApplication : IDocumentApplication
	{
		private readonly IDocumentRepository _documentRepository;
		private readonly ICashBookClient _cashBookClient;
		private readonly IMapper _mapper;

		public DocumentApplication(IDocumentRepository documentRepository, ICashBookClient cashBookClient, IMapper mapper)
		{
			_documentRepository = documentRepository;
			_cashBookClient = cashBookClient;
			_mapper = mapper;
		}

		public async Task<Document> AddDocument(DocumentDto documentDto)
		{
			var document = _mapper.Map<Document>(documentDto);

			var validator = new DocumentValidator();
			var result = validator.Validate(document);

			if (result.IsValid)
			{
				if (document.Paid == true)
				{
					var type = new EnumType();

					if (document.Operation == EnumOperation.Entry)
					{
						type = EnumType.Receipt;
					}
					else
					{
						type = EnumType.Payment;
					}

					await _documentRepository.AddAsync(document);

					var response = await _cashBookClient.AddCashBook(EnumOrigin.Document, document.Id, $"Finantial transaction DocumentId: {document.Id}", type, document.Total);

					if (response == false)
					{
						var error1 = BadRequest(documentDto, "Error in the communication with the CashBookAPI.");
						var errors1 = ListOfErrors(error1);
						throw new Exception(errors1);
					}
				}
				else
				{
					await _documentRepository.AddAsync(document);
				}
			}
			else
			{
				var error2 = new Error<DocumentDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(),
					result.Errors.ConvertAll(r => r.ErrorMessage.ToString()), documentDto);
				var errors2 = ListOfErrors(error2);
				throw new Exception(errors2);
			}

			return document;
		}
		public async Task<Document> DeleteDocument(Guid id)
		{
			var document = await _documentRepository.GetByIdAsync(id);
			var documentDto = _mapper.Map<DocumentDto>(document);

			if (document == null)
			{
				var error1 = NotFound(documentDto);
				var errors1 = ListOfErrors(error1);
				throw new Exception(errors1);
			}
			else
			{
				await _documentRepository.DeleteAsync(document);
			}
			
			if (document.Paid == true)
			{
				var response = await _cashBookClient.AddCashBook(EnumOrigin.Document, document.Id, $"Reversal transaction Id: {document.Id}", EnumType.Reversal, -document.Total);

				if (response == false)
				{
					var error2 = BadRequest(documentDto, "Error in the communication with the CashBookAPI.");
					var errors2 = ListOfErrors(error2);
					throw new Exception(errors2);
				}
			}

			return document;
		}
		public async Task<List<Document>> GetAllDocuments(PageParameters page)
		{
			DocumentDto documentDto = new DocumentDto();
			var documents = await _documentRepository.GetAllWithPaging(page);
			
			if (documents.Count() == 0)
			{
				var error = NotFound(documentDto);
				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}

			return documents;
		}
		public async Task<Document> GetDocumentById(Guid id)
		{
			DocumentDto documentDto = new DocumentDto();
			var document = await _documentRepository.GetByIdAsync(id);

			if (document == null)
			{
				var error = NotFound(documentDto);
				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}

			return document;
		}
		public async Task<Document> UpdateDocument(Guid id, DocumentDto documentDto)
		{
			var document = await _documentRepository.GetByIdAsync(id);
			var oldTotal = document.Total;

			if (document == null)
			{
				var error1 = NotFound(documentDto);
				var errors1 = ListOfErrors(error1);
				throw new Exception(errors1);
			}

			if (document.Paid == true && documentDto.Paid == false)
			{
				var error2 = BadRequest(documentDto, "The Document is already paid.");
				var errors2 = ListOfErrors(error2);
				throw new Exception(errors2);
			}

			var docUpdate = _mapper.Map<Document>(documentDto);
			var updatedTotal= docUpdate.Total - oldTotal;

			var validator = new DocumentValidator();
			var result = validator.Validate(docUpdate);
			if (!result.IsValid)
			{
				var error3 = new Error<DocumentDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(),
					result.Errors.ConvertAll(d => d.ErrorMessage.ToString()), documentDto);

				var errors3 = ListOfErrors(error3);
				throw new Exception(errors3);
			}
			else
			{
				if (document.Paid == false && documentDto.Paid == true || updatedTotal != oldTotal && document.Paid == true)
				{
					string description = $"Diference transaction in DocumentId: {document.Id}";
					var type = EnumType.Reversal;
					decimal total = updatedTotal;

					if(document.Paid == false && documentDto.Paid == true)
					{
						description = $"Finantial transaction in DOcumentId: {document.Id}";
						type = EnumType.Receipt;
						total = documentDto.Total;
					}

					var response = await _cashBookClient.AddCashBook(EnumOrigin.Document, document.Id, description, type, total);

					if (response == false)
					{
						var error4 = BadRequest(documentDto, "Error in the communication with the CashBookAPI.");
						var errors4 = ListOfErrors(error4);
						throw new Exception(errors4);
					}
				}


			}

			await _documentRepository.UpdateAsync(docUpdate);
			return docUpdate;
		}
		public async Task<Document> UpdatePaymentStatus(Guid id, bool status)
		{
			var document = await _documentRepository.GetByIdAsync(id);
			var documentDto= _mapper.Map<DocumentDto>(document);

			if (document == null)
			{
				var error1 = NotFound(documentDto);
				var errors1 = ListOfErrors(error1);
				throw new Exception(errors1);
			}

			if (document.Paid == true)
			{
				var error2 = BadRequest(documentDto, "This Document is already paid and can only be deleted.");
				var errors2 = ListOfErrors(error2);
				throw new Exception(errors2);
			}

			document.Paid = status;
			await _documentRepository.UpdateAsync(document);

			if (document.Paid == true)
			{
				var type = EnumType.Receipt;
				if (document.Operation == EnumOperation.Exit)
				{
					type = EnumType.Payment;
				}

				var response = await _cashBookClient.AddCashBook(EnumOrigin.Document, document.Id, $"Finantial transaction DocumentId: {document.Id}", type, document.Total);

				if (response == false)
				{
					var error3 = BadRequest(documentDto, "Error in the communication with the CashBookAPI.");
					var errors3 = ListOfErrors(error3);
					throw new Exception(errors3);
				}
			}

			return document;
		}

		public string ListOfErrors(Error<DocumentDto> error)
		{
			var errors = string.Empty;

			foreach (var item in error.Message)
				errors += item.ToString() + " ";

			return errors;
		}
		public Error<DocumentDto> NotFound(DocumentDto documentDto)
		{
			var errors = new List<string>();
			errors.Add("The Document was not found in the DataBase!");

			var error = new Error<DocumentDto>(HttpStatusCode.NoContent.GetHashCode().ToString(), errors, documentDto);
			return error;
		}
		public Error<DocumentDto> BadRequest(DocumentDto documentDto, string message)
		{
			var errors = new List<string>();
			errors.Add(message);

			var error = new Error<DocumentDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), errors, documentDto);
			return error;
		}
	}
}
