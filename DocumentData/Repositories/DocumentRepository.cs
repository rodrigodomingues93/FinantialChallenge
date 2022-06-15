using DocumentData.Repositories.Interfaces;
using DocumentDomain.Entities;
using Infrastructure.Repository;

namespace DocumentData.Repositories
{
	public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
	{
		private readonly DocumentContext _context;
		public DocumentRepository(DocumentContext context) : base(context)
		{
			_context = context;
		}
	}
}
