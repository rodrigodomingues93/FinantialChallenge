using RequestDomain.DTO;
using RequestDomain.Entities;

namespace RequestApp.Interfaces
{
	public interface IProductRequestApplication
	{
		Task<IEnumerable<ProductRequest>> GetAllProducts();
	}
}
