using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using RequestData.Repository.Interfaces;
using RequestDomain.Entities;

namespace RequestData.Repository
{
	public class RequestRepository : GenericRepository<Request>, IRequestRepository
	{
		public RequestRepository(RequestContext context, IProductRequestRepository productRepository) : base(context)
		{
			SetInclude(r => r.Include(i => i.Products));
		}
	}
}
