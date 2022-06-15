using Infrastructure.Pagination;
using Moq;
using Moq.AutoMock;
using RequestAPI.Controllers;
using RequestApp.Interfaces;
using RequestDomain.DTO;
using RequestDomain.Entities;
using RequestTests.AutoFakers;
using Xunit;

namespace RequestTests
{
    public class RequestTest
    {
        private readonly AutoMocker _mocker;
        public RequestTest()
        {
            _mocker = new AutoMocker();
        }

        [Fact(DisplayName = "AddRequest Test")]
        public async Task AddRequestTest()
        {
            var requestDto = RequestFaker.GenerateRequestDto();

            var requestService = _mocker.GetMock<IRequestApplication>();
            requestService.Setup(r => r.AddRequest(requestDto));

            var requestController = _mocker.CreateInstance<RequestController>();

            await requestController.AddRequest(requestDto);

            requestService.Verify(r => r.AddRequest(It.IsAny<RequestDto>()), Times.Once());
        }

        [Fact(DisplayName = "GetAllRequests Test")]
        public async Task GetAllRequestsTest()
        {
            var requestService = _mocker.GetMock<IRequestApplication>();
            PageParameters page = new PageParameters() { PageNumber = 1, PageSize = 100 };
            requestService.Setup(r => r.GetAllRequests(page));

            var requestController = _mocker.CreateInstance<RequestController>();

            await requestController.GetAllRequests(page);

            requestService.Verify(r => r.GetAllRequests(page), Times.Once());
        }

        [Fact(DisplayName = "GetRequestByClientId Test")]
        public async Task GetRequestByClientIdTest()
        {
            var request = new Request();

            var requestService = _mocker.GetMock<IRequestApplication>();
            requestService.Setup(r => r.GetRequestByClientId(request.Client));

            var requestController = _mocker.CreateInstance<RequestController>();

            await requestController.GetRequestByClientId(request.Client);

            requestService.Verify(r => r.GetRequestByClientId(request.Client), Times.Once());
        }

        [Fact(DisplayName = "UpdateRequest Test")]
        public async Task UpdateRequestTest()
        {
            var requestDto = RequestFaker.GenerateRequestDto();

            var requestService = _mocker.GetMock<IRequestApplication>();
            requestService.Setup(r => r.GetRequestByClientId(requestDto.Client));
            requestService.Setup(r => r.UpdateRequest(requestDto));

            var requestController = _mocker.CreateInstance<RequestController>();

            await requestController.UpdateRequest(requestDto);

            requestService.Verify(r => r.UpdateRequest(It.IsAny<RequestDto>()), Times.Once());
        }

        [Fact(DisplayName = "DeleteRequest Test")]
        public async Task DeleteRequestTest()
        {
            var requestDto = RequestFaker.GenerateRequestDto();

            var requestService = _mocker.GetMock<IRequestApplication>();
            requestService.Setup(r => r.DeleteRequest(requestDto.Id));

            var requestController = _mocker.CreateInstance<RequestController>();

            await requestController.DeleteRequest(requestDto.Id);

            requestService.Verify(r => r.DeleteRequest(requestDto.Id), Times.Once());
        }

    }
}
