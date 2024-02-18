using AutoFixture;
using AutoMapper;
using BallastLane.ApplicationService;
using BallastLane.ApplicationService.Dto.User;
using BallastLane.ApplicationService.Interface;
using BallastLane.Domain.Model;
using BallastLane.Domain.Settings;
using BallastLane.Infrastructure.Data.Repository.Interface;
using Moq;

namespace BallastLane.UnitTest.ApplicationService
{
    public class UserAppServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IUserAppService _userAppService;
        private Fixture _fixture;

        public UserAppServiceTest()
        {
            var projectSettings = new Mock<IProjectSettings>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();

            _userAppService = new UserAppService(_userRepositoryMock.Object, projectSettings.Object, _mapperMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task CreateAsync_ReturnsSuccess()
        {
            // arrange
            var inputDto = _fixture.Create<UserCreateInputDto>();
            var user = _fixture.Create<User>();
            var users = new List<User>();
            _mapperMock.Setup(x => x.Map<UserCreateInputDto, User>(inputDto)).Returns(user);

            // act
            var result = await _userAppService.CreateAsync(inputDto);

            // assert
            Assert.False(result.HasErrors());
        }

        [Fact]
        public async Task DeleteAsync_ReturnsNoException()
        {
            // arrange
            var id = _fixture.Create<string>();
            Exception exception = null;

            // act
            try
            {
                await _userAppService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetAllAsync_WhenUsersExists_ReturnsSuccess()
        {
            // arrange
            var offset = 0;
            var fetch = 10;
            var users = _fixture.CreateMany<User>();
            var userOutputDtos = _fixture.CreateMany<UserOutputDto>();

            _userRepositoryMock.Setup(x => x.GetAllAsync(offset, fetch)).ReturnsAsync(users);
            _mapperMock.Setup(x => x.Map<IEnumerable<User>, IEnumerable<UserOutputDto>>(users)).Returns(userOutputDtos);

            // act
            var result = await _userAppService.GetAllAsync(offset, fetch);

            // assert
            Assert.Equal(userOutputDtos, result);
        }

        [Fact]
        public async Task GetByIdAsync_WhenUserExists_ReturnsSuccess()
        {
            // arrange
            var id = _fixture.Create<string>();
            var user = _fixture.Create<User>();
            var userOutputDto = _fixture.Create<UserOutputDto>();

            _userRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(user);
            _mapperMock.Setup(x => x.Map<User, UserOutputDto>(user)).Returns(userOutputDto);

            // act
            var result = await _userAppService.GetByIdAsync(id);

            // assert
            Assert.Equal(userOutputDto, result);
        }

        [Fact]
        public async Task UpdateAsync_WhenUserExists_ReturnsSuccess()
        {
            // arrange
            var id = _fixture.Create<string>();
            var inputDto = _fixture.Create<UserUpdateInputDto>();
            var user = _fixture.Create<User>();

            _userRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(user);
            _mapperMock.Setup(x => x.Map<UserUpdateInputDto, User>(inputDto)).Returns(user);

            // act
            var result = await _userAppService.UpdateAsync(id, inputDto);

            // assert
            Assert.False(result.HasErrors());
        }

        [Fact]
        public async Task UpdateAsync_WhenUserDoesNotExists_ReturnsError()
        {
            // arrange
            var id = _fixture.Create<string>();
            var inputDto = _fixture.Create<UserUpdateInputDto>();
            _userRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((User)null);

            // act
            var result = await _userAppService.UpdateAsync(id, inputDto);

            // assert
            Assert.True(result.HasErrors());
        }
    }
}
