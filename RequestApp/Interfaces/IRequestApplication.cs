using Infrastructure.Pagination;
using RequestDomain.DTO;
using RequestDomain.Entities;
using RequestDomain.Entities.Enums;

namespace RequestApp.Interfaces
{
	public interface IRequestApplication
	{
		Task<Request> AddRequest(RequestDto requestDto);
		Task<IEnumerable<Request>> GetAllRequests(PageParameters page);
		Task<Request> GetRequestById(Guid id);
		Task<Request> GetRequestByClientId(Guid clientId);
		Task<Request> UpdateRequest(RequestDto requestDto);
		Task<Request> UpdateRequestStatus(Guid id, EnumStatus status);
		Task<Request> DeleteRequest(Guid id);
	}
}
