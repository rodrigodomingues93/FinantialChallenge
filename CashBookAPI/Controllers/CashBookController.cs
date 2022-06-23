using CashBookApp.Interfaces;
using CashBookDomain.DTO;
using CashBookDomain.Entities;
using Infrastructure.ErrorMessages;
using Infrastructure.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CashBookAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CashBookController : ControllerBase
	{
		private readonly ICashBookApplication _cashBookApplication;

		public CashBookController(ICashBookApplication cashBookApplication)
		{
			_cashBookApplication = cashBookApplication;
		}

		[HttpPost]
		public async Task<IActionResult> AddCashBook([FromBody] CashBookDto cashBookDto)
		{
			try
			{
				await _cashBookApplication.AddCashBook(cashBookDto);
				return Ok(cashBookDto);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<CashBookDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, cashBookDto));
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCashBooks([FromQuery] PageParameters page)
		{
			try
			{
				var result= await _cashBookApplication.GetAllCashBooks(page);
				return Ok(result);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<CashBookDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, new CashBookDto()));
			}
		}

		[HttpGet("Origin/{originId}")]
		public async Task<IActionResult> GetCashBookByOriginId(Guid originId)
		{
			try
			{
				var cashBook = await _cashBookApplication.GetCashBookByOriginId(originId);
				return Ok(cashBook);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<CashBookDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, new CashBookDto()));
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCashBookById(Guid id)
		{
			try
			{
				var cashBook = await _cashBookApplication.GetCashBookById(id);
				return Ok(cashBook);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<CashBookDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, new CashBookDto()));
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCashBook(Guid id, [FromBody] CashBookDto cashBookDto)
		{
			try
			{
				var cashBook= await _cashBookApplication.UpdateCashBook(id, cashBookDto);
				return Ok(cashBook);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<CashBookDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, cashBookDto));
			}
		}
	}
}
