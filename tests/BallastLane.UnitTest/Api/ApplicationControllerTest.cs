using System.Net;
using AutoFixture;
using BallastLane.ApplicationService.Dto.Application;
using BallastLane.ApplicationService.Interface;
using BallastLane.WebApi.Controllers;
using Domain.ValueObjects;
using Moq;

namespace BallastLane.UnitTest.Api
{
    public class ApplicationControllerTest: BaseControllerTest<ApplicationController>
    {
        private readonly Mock<IApplicationAppService> _applicationAppServiceMock;
        private readonly ApplicationController _applicationController;
        private Fixture _fixture;

        public ApplicationControllerTest()
        {
            _applicationAppServiceMock = new Mock<IApplicationAppService>();
            _applicationController = new ApplicationController(_applicationAppServiceMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task CreateAsync_ReturnCreated()
        {
            // arrange
            var applicationCreateInputDto = _fixture.Create<ApplicationCreateInputDto>();
            var applicationOutputDto = _fixture.Create<ApplicationOutputDto>();
            var applicationOutputDtoResult = new Result<ApplicationOutputDto>(applicationOutputDto);

            _applicationAppServiceMock.Setup(x => x.CreateAsync(applicationCreateInputDto)).ReturnsAsync(applicationOutputDtoResult);

            // act
            var response = await _applicationController.CreateAsync(applicationCreateInputDto);

            // assert
            var statusCode = GetStatusCode(response);
            Assert.Equal(statusCode, (int)HttpStatusCode.Created);
        }


        [Fact]
        public async Task CreateAsync_WhenInvalidParameters_ReturnBadRequest()
        {
            // arrange
            var applicationCreateInputDto = _fixture.Create<ApplicationCreateInputDto>();
            var applicationOutputDtoResult = new Result<ApplicationOutputDto>();
            applicationOutputDtoResult.AddError(_fixture.Create<string>(), _fixture.Create<string>());

            _applicationAppServiceMock.Setup(x => x.CreateAsync(applicationCreateInputDto)).ReturnsAsync(applicationOutputDtoResult);

            // act
            var response = await _applicationController.CreateAsync(applicationCreateInputDto);

            // assert
            var statusCode = GetStatusCode(response);
            Assert.Equal(statusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateAsync_ReturnNoContent()
        {
            // arrange
            var id = _fixture.Create<string>();
            var applicationUpdateInputDto = _fixture.Create<ApplicationUpdateInputDto>();
            var applicationOutputDto = _fixture.Create<ApplicationOutputDto>();

            _applicationAppServiceMock.Setup(x => x.UpdateAsync(id, applicationUpdateInputDto)).ReturnsAsync(new Result());

            // act
            var response = await _applicationController.UpdateAsync(id, applicationUpdateInputDto);

            // assert
            var statusCode = GetStatusCode(response);
            Assert.Equal(statusCode, (int)HttpStatusCode.NoContent);
        }


        [Fact]
        public async Task UpdateAsync_WhenInvalidParameters_ReturnBadRequest()
        {
            // arrange
            var id = _fixture.Create<string>();
            var applicationUpdateInputDto = _fixture.Create<ApplicationUpdateInputDto>();
            var applicationOutputDto = _fixture.Create<ApplicationOutputDto>();
            var result = new Result(_fixture.Create<string>(), _fixture.Create<string>());

            _applicationAppServiceMock.Setup(x => x.UpdateAsync(id, applicationUpdateInputDto)).ReturnsAsync(result);

            // act
            var response = await _applicationController.UpdateAsync(id, applicationUpdateInputDto);

            // assert
            var statusCode = GetStatusCode(response);
            Assert.Equal(statusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAsync_ReturnAccepted()
        {
            // arrange
            var id = _fixture.Create<string>();

            // act
            var response = await _applicationController.DeleteAsync(id);

            // assert
            var statusCode = GetStatusCode(response);
            Assert.Equal(statusCode, (int)HttpStatusCode.Accepted);
        }

        [Fact]
        public async Task GetApplicationByIdAsync_ReturnOk()
        {
            // arrange
            var id = _fixture.Create<string>();

            // act
            var response = await _applicationController.GetApplicationByIdAsync(id);

            // assert
            var statusCode = GetStatusCode(response);
            Assert.Equal(statusCode, (int)HttpStatusCode.OK);
        }


        [Fact]
        public async Task GetAllAsync_ReturnOk()
        {
            // arrange
            var offset = 0;
            var fetch = 10;

            // act
            var response = await _applicationController.GetAllAsync(offset, fetch);

            // assert
            var statusCode = GetStatusCode(response);
            Assert.Equal(statusCode, (int)HttpStatusCode.OK);
        }
    }
}
