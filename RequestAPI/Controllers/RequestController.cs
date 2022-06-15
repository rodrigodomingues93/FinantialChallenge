using Infrastructure.ErrorMessages;
using Infrastructure.Pagination;
using Microsoft.AspNetCore.Mvc;
using RequestApp.Interfaces;
using RequestDomain.DTO;
using RequestDomain.Entities;
using RequestDomain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace RequestAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RequestController : ControllerBase
	{
		private readonly IRequestApplication _requestApplication;

		public RequestController(IRequestApplication requestApplication)
		{
			_requestApplication = requestApplication;
		}

		[HttpPost]
		public async Task<IActionResult> AddRequest([FromBody] RequestDto requestDto)
		{
			try
			{
				var request = await _requestApplication.AddRequest(requestDto);
				return Ok(request);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<RequestDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, requestDto));
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetAllRequests([FromQuery] PageParameters page)
		{
			try
			{
				var requests = await _requestApplication.GetAllRequests(page);
				return Ok(requests);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<RequestDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, new RequestDto()));
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetRequestById(Guid id)
		{
			try
			{
				var request = await _requestApplication.GetRequestById(id);
				return Ok(request);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<RequestDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, new RequestDto()));
			}
		}

		[HttpGet("Client/{clientId}")]
		public async Task<IActionResult> GetRequestByClientId(Guid clientId)
		{
			try
			{
				var request = await _requestApplication.GetRequestByClientId(clientId);
				return Ok(request);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<RequestDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, new RequestDto()));
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateRequest([FromBody] RequestDto requestDto)
		{
			try
			{
				var request = await _requestApplication.UpdateRequest(requestDto);
				return Ok(request);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<RequestDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, requestDto));
			}
		}

		[HttpPut("status/{id}")]
		public async Task<IActionResult> UpdateRequestStatus(Guid id, EnumStatus status)
		{
			try
			{
				var request = await _requestApplication.UpdateRequestStatus(id, status);
				return Ok(request);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<RequestDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, null));
			}
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteRequest(Guid id)
		{
			try
			{
				var request = await _requestApplication.DeleteRequest(id);
				return Ok(request);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<Request>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, null));
			}
		}
	}
}
