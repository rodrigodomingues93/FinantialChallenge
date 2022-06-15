using CashBookAPI.Controllers;
using CashBookApp.Interfaces;
using CashBookDomain.DTO;
using CashBookDomain.Entities;
using CashBookTests.AutoFakers;
using Infrastructure.Pagination;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace CashBookTests
{
    public class CashBookTest
    {
        private readonly AutoMocker _mocker;
        public CashBookTest()
        {
            _mocker = new AutoMocker();
        }

        [Fact(DisplayName = "AddCashBook Test")]
        public async Task AddCashBookTest()
        {
            var cashBookDto = CashBookFaker.GenerateCashBookDto();

            var cashBookService = _mocker.GetMock<ICashBookApplication>();
            cashBookService.Setup(c => c.AddCashBook(cashBookDto));

            var cashBookController = _mocker.CreateInstance<CashBookController>();

            await cashBookController.AddCashBook(cashBookDto);

            cashBookService.Verify(c => c.AddCashBook(It.IsAny<CashBookDto>()), Times.Once());
        }

        [Fact(DisplayName = "GetAllCashBooks Test")]
        public async Task GetAllCashBooksTest()
        {
            var cashBookService = _mocker.GetMock<ICashBookApplication>();
            PageParameters page = new PageParameters() { PageNumber = 1, PageSize = 100 };
            cashBookService.Setup(c => c.GetAllCashBooks(page));

            var cashBookController = _mocker.CreateInstance<CashBookController>();

            await cashBookController.GetAllCashBooks(page);

            cashBookService.Verify(c => c.GetAllCashBooks(page), Times.Once());
        }

        [Fact(DisplayName = "GetCashBookById Test")]
        public async Task GetCashBookByIdTest()
        {
            var cashBook = new CashBook();

            var cashBookService = _mocker.GetMock<ICashBookApplication>();
            cashBookService.Setup(c => c.GetCashBookById(cashBook.Id));

            var cashBookController = _mocker.CreateInstance<CashBookController>();

            await cashBookController.GetCashBookById(cashBook.Id);

            cashBookService.Verify(c => c.GetCashBookById(cashBook.Id), Times.Once());
        }

        [Fact(DisplayName = "GetCashBookByOriginId Test")]
        public async Task GetCashBookByOriginIdTest()
        {
            var id = Guid.NewGuid();

            var cashBook = new CashBook();

            var cashBookService = _mocker.GetMock<ICashBookApplication>();
            cashBookService.Setup(c => c.GetCashBookByOriginId(id));

            var cashBookController = _mocker.CreateInstance<CashBookController>();

            await cashBookController.GetCashBookByOriginId(id);

            cashBookService.Verify(c => c.GetCashBookByOriginId(id), Times.Once());
        }

        [Fact(DisplayName = "UpdateCashBook Test")]
        public async Task UpdateCashBookTest()
        {
            var cashBook = CashBookFaker.GenerateCashBook();

            var cashBookService = _mocker.GetMock<ICashBookApplication>();
            cashBookService.Setup(c => c.GetCashBookById(cashBook.Id)).ReturnsAsync(cashBook);
            cashBookService.Setup(c => c.UpdateCashBook(cashBook.Id, new CashBookDto()));

            var cashBookController = _mocker.CreateInstance<CashBookController>();

            await cashBookController.UpdateCashBook(cashBook.Id, new CashBookDto());

            cashBookService.Verify(c => c.UpdateCashBook(cashBook.Id, It.IsAny<CashBookDto>()), Times.Once());
        }
    }
}
