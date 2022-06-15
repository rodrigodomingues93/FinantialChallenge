using Infrastructure.Repository;
using RequestData.Repository.Interfaces;
using RequestDomain.Entities;

namespace RequestData.Repository
{
	public class ProductRequestRepository : GenericRepository<ProductRequest>, IProductRequestRepository
	{
		public ProductRequestRepository(RequestContext context) : base(context)
		{
		}
	}
}
