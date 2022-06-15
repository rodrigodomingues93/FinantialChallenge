using DocumentDomain.DTO;
using DocumentDomain.Entities;
using Infrastructure.Pagination;

namespace DocumentApp.Interfaces
{
	public interface IDocumentApplication
	{
		Task<Document> AddDocument(DocumentDto documentDto);
		Task<List<Document>> GetAllDocuments(PageParameters page);
		Task<Document> GetDocumentById(Guid id);
		Task<Document> UpdateDocument(Guid id, DocumentDto documentDto);
		Task<Document> UpdatePaymentStatus(Guid id, bool status);
		Task<Document> DeleteDocument(Guid id);
	}
}
