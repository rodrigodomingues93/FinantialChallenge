using DocumentAPI.Controllers;
using DocumentApp.Interfaces;
using DocumentDomain.DTO;
using DocumentTests.AutoFakers;
using Infrastructure.Pagination;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace DocumentTests
{
    public class DocumentTest
    {
        private readonly AutoMocker _mocker;
        public DocumentTest()
        {
            _mocker = new AutoMocker();
        }

        [Fact(DisplayName = "AddDocument Test")]
        public async Task AddDocumentTest()
        {
            var documentDto = DocumentFaker.GenerateDocumentDto();

            var documentService = _mocker.GetMock<IDocumentApplication>();
            documentService.Setup(d => d.AddDocument(documentDto));

            var documentController = _mocker.CreateInstance<DocumentController>();

            await documentController.AddDocument(documentDto);

            documentService.Verify(d => d.AddDocument(It.IsAny<DocumentDto>()), Times.Once());
        }

        [Fact(DisplayName = "GetAllDocuments Test")]
        public async Task GetAllDocumentsTest()
        {
            var documentService = _mocker.GetMock<IDocumentApplication>();
            PageParameters page = new PageParameters();
            documentService.Setup(d => d.GetAllDocuments(page));

            var documentController = _mocker.CreateInstance<DocumentController>();

            await documentController.GetAllDocuments(page);

            documentService.Verify(d => d.GetAllDocuments(page), Times.Once());
        }

        [Fact(DisplayName = "GetDocumentById Test")]
        public async Task GetDocumentByIdTest()
        {
            var document = DocumentFaker.GenerateDocument();

            var documentService = _mocker.GetMock<IDocumentApplication>();
            documentService.Setup(x => x.GetDocumentById(document.Id));

            var buyReqController = _mocker.CreateInstance<DocumentController>();

            await buyReqController.GetDocumentById(document.Id);

            documentService.Verify(x => x.GetDocumentById(document.Id), Times.Once());
        }

        [Fact(DisplayName = "UpdateDocument Test")]
        public async Task UpdateDocumentTest()
        {
            var document = DocumentFaker.GenerateDocument();
            var documentDto = DocumentFaker.GenerateDocumentDto();

            var documentService = _mocker.GetMock<IDocumentApplication>();

            documentService.Setup(d => d.UpdateDocument(document.Id, documentDto));

            var documentController = _mocker.CreateInstance<DocumentController>();

            await documentController.UpdateDocument(document.Id, documentDto);

            documentService.Verify(d => d.UpdateDocument(document.Id, It.IsAny<DocumentDto>()), Times.Once());
        }

        [Fact(DisplayName = "DeleteDocument Test")]
        public async Task DeleteDocumentTest()
        {
            var document = DocumentFaker.GenerateDocument();

            var documentService = _mocker.GetMock<IDocumentApplication>();
            documentService.Setup(d => d.DeleteDocument(document.Id));

            var documentController = _mocker.CreateInstance<DocumentController>();

            await documentController.DeleteDocument(document.Id);

            documentService.Verify(d => d.DeleteDocument(document.Id), Times.Once());
        }
    }
}
