using DocumentDomain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DocumentData
{
	public class DocumentContext : SqlContext
	{
		public DocumentContext(DbContextOptions<DocumentContext> options) : base(options)
		{

		}

		public DbSet<Document> DbDocuments { get; set; }
	}
}
