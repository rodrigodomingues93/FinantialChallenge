using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
	public class SqlContext : DbContext
	{
		public SqlContext(DbContextOptions options) : base(options)
		{

		}
	}
}