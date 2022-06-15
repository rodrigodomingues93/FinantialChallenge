using DocumentApp.Interfaces;
using DocumentDomain.DTO;
using DocumentDomain.Entities;
using Infrastructure.ErrorMessages;
using Infrastructure.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace DocumentAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DocumentController : ControllerBase
	{
		private readonly IDocumentApplication _documentApplication;
		public DocumentController(IDocumentApplication documentApplication)
		{
			_documentApplication = documentApplication;
		}

		[HttpPost]
		public async Task<IActionResult> AddDocument([FromBody] DocumentDto documentDto)
		{
			try
			{
				var document = await _documentApplication.AddDocument(documentDto);
				return Ok(document);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<DocumentDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, documentDto));
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetAllDocuments([FromQuery] PageParameters page)
		{
			try
			{
				var documents= await _documentApplication.GetAllDocuments(page);
				return Ok(documents);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<DocumentDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, new DocumentDto()));
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetDocumentById(Guid id)
		{
			try
			{
				var document = await _documentApplication.GetDocumentById(id);
				return Ok(document);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<DocumentDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, new DocumentDto()));
			}
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdateDocument(Guid id, [FromBody] DocumentDto documentDto)
		{
			try
			{
				var document= await _documentApplication.UpdateDocument(id, documentDto);
				return Ok(document);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<DocumentDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, documentDto));
			}
		}

		[HttpPut("status/{id}")]
		public async Task<IActionResult> UpdateDocumentStatus(Guid id, bool status)
		{
			try
			{
				var document= await _documentApplication.UpdatePaymentStatus(id, status);
				return Ok(document);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<DocumentDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, null));
			}
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteDocument(Guid id)
		{
			try
			{
				var document= await _documentApplication.DeleteDocument(id);
				return Ok(document);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<Document>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, null));
			}
		}
	}
}
