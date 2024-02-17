using AutoFixture;
using AutoMapper;
using BallastLane.ApplicationService;
using BallastLane.ApplicationService.Dto;
using BallastLane.ApplicationService.Interface;
using BallastLane.Domain.Model;
using BallastLane.Infrastructure.Data.Repository.Interface;
using Moq;

namespace BallastLane.UnitTest.ApplicationService
{
    public class ApplicationAppServiceTest
    {
        private readonly Mock<IApplicationRepository> _applicationRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IApplicationAppService _applicationAppService;
        private Fixture _fixture;

        public ApplicationAppServiceTest()
        {
            _applicationRepositoryMock = new Mock<IApplicationRepository>();
            _mapperMock = new Mock<IMapper>();

            _applicationAppService = new ApplicationAppService(_applicationRepositoryMock.Object, _mapperMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task CreateAsync_ReturnsSuccess()
        {
            // arrange
            var inputDto = _fixture.Create<ApplicationCreateInputDto>();
            var application = _fixture.Create<Application>();
            var applications = new List<Application>();
            _mapperMock.Setup(x => x.Map<ApplicationCreateInputDto, Application>(inputDto)).Returns(application);

            // act
            var result = await _applicationAppService.CreateAsync(inputDto);

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
                await _applicationAppService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetAllAsync_WhenApplicationsExists_ReturnsSuccess()
        {
            // arrange
            var offset = 0;
            var fetch = 10;
            var applications = _fixture.CreateMany<Application>();
            var applicationOutputDtos = _fixture.CreateMany<ApplicationOutputDto>();

            _applicationRepositoryMock.Setup(x => x.GetAllAsync(offset, fetch)).ReturnsAsync(applications);
            _mapperMock.Setup(x => x.Map<IEnumerable<Application>, IEnumerable<ApplicationOutputDto>>(applications)).Returns(applicationOutputDtos);

            // act
            var result = await _applicationAppService.GetAllAsync(offset, fetch);

            // assert
            Assert.Equal(applicationOutputDtos, result);
        }

        [Fact]
        public async Task GetByIdAsync_WhenApplicationExists_ReturnsSuccess()
        {
            // arrange
            var id = _fixture.Create<string>();
            var application = _fixture.Create<Application>();
            var applicationOutputDto = _fixture.Create<ApplicationOutputDto>();

            _applicationRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(application);
            _mapperMock.Setup(x => x.Map<Application, ApplicationOutputDto>(application)).Returns(applicationOutputDto);

            // act
            var result = await _applicationAppService.GetByIdAsync(id);

            // assert
            Assert.Equal(applicationOutputDto, result);
        }

        [Fact]
        public async Task UpdateAsync_WhenApplicationExists_ReturnsSuccess()
        {
            // arrange
            var id = _fixture.Create<string>();
            var inputDto = _fixture.Create<ApplicationUpdateInputDto>();
            var application = _fixture.Create<Application>();

            _applicationRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(application);
            _mapperMock.Setup(x => x.Map<ApplicationUpdateInputDto, Application>(inputDto)).Returns(application);

            // act
            var result = await _applicationAppService.UpdateAsync(id, inputDto);

            // assert
            Assert.False(result.HasErrors());
        }

        [Fact]
        public async Task UpdateAsync_WhenApplicationDoesNotExists_ReturnsError()
        {
            // arrange
            var id = _fixture.Create<string>();
            var inputDto = _fixture.Create<ApplicationUpdateInputDto>();
            _applicationRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Application)null);

            // act
            var result = await _applicationAppService.UpdateAsync(id, inputDto);

            // assert
            Assert.True(result.HasErrors());
        }
    }
}
