using System.Net;
using AutoFixture;
using BallastLane.ApplicationService.Dto.User;
using BallastLane.ApplicationService.Interface;
using BallastLane.WebApi.Controllers;
using Domain.ValueObjects;
using Moq;

namespace BallastLane.UnitTest.Api
{
    public class UserControllerTest: BaseControllerTest<UserController>
    {
        private readonly Mock<IUserAppService> _userAppServiceMock;
        private readonly UserController _userController;
        private Fixture _fixture;

        public UserControllerTest()
        {
            _userAppServiceMock = new Mock<IUserAppService>();
            _userController = new UserController(_userAppServiceMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task CreateAsync_ReturnCreated()
        {
            // arrange
            var userCreateInputDto = _fixture.Create<UserCreateInputDto>();
            var userOutputDto = _fixture.Create<UserOutputDto>();
            var userOutputDtoResult = new Result<UserOutputDto>(userOutputDto);

            _userAppServiceMock.Setup(x => x.CreateAsync(userCreateInputDto)).ReturnsAsync(userOutputDtoResult);

            // act
            var response = await _userController.CreateAsync(userCreateInputDto);

            // assert
            var statusCode = GetStatusCode(response);
            Assert.Equal(statusCode, (int)HttpStatusCode.Created);
        }


        [Fact]
        public async Task CreateAsync_WhenInvalidParameters_ReturnBadRequest()
        {
            // arrange
            var userCreateInputDto = _fixture.Create<UserCreateInputDto>();
            var userOutputDtoResult = new Result<UserOutputDto>();
            userOutputDtoResult.AddError(_fixture.Create<string>(), _fixture.Create<string>());

            _userAppServiceMock.Setup(x => x.CreateAsync(userCreateInputDto)).ReturnsAsync(userOutputDtoResult);

            // act
            var response = await _userController.CreateAsync(userCreateInputDto);

            // assert
            var statusCode = GetStatusCode(response);
            Assert.Equal(statusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateAsync_ReturnNoContent()
        {
            // arrange
            var id = _fixture.Create<string>();
            var userUpdateInputDto = _fixture.Create<UserUpdateInputDto>();
            var userOutputDto = _fixture.Create<UserOutputDto>();

            _userAppServiceMock.Setup(x => x.UpdateAsync(id, userUpdateInputDto)).ReturnsAsync(new Result());

            // act
            var response = await _userController.UpdateAsync(id, userUpdateInputDto);

            // assert
            var statusCode = GetStatusCode(response);
            Assert.Equal(statusCode, (int)HttpStatusCode.NoContent);
        }


        [Fact]
        public async Task UpdateAsync_WhenInvalidParameters_ReturnBadRequest()
        {
            // arrange
            var id = _fixture.Create<string>();
            var userUpdateInputDto = _fixture.Create<UserUpdateInputDto>();
            var userOutputDto = _fixture.Create<UserOutputDto>();
            var result = new Result(_fixture.Create<string>(), _fixture.Create<string>());

            _userAppServiceMock.Setup(x => x.UpdateAsync(id, userUpdateInputDto)).ReturnsAsync(result);

            // act
            var response = await _userController.UpdateAsync(id, userUpdateInputDto);

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
            var response = await _userController.DeleteAsync(id);

            // assert
            var statusCode = GetStatusCode(response);
            Assert.Equal(statusCode, (int)HttpStatusCode.Accepted);
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnOk()
        {
            // arrange
            var id = _fixture.Create<string>();

            // act
            var response = await _userController.GetUserByIdAsync(id);

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
            var response = await _userController.GetAllAsync(offset, fetch);

            // assert
            var statusCode = GetStatusCode(response);
            Assert.Equal(statusCode, (int)HttpStatusCode.OK);
        }
    }
}
