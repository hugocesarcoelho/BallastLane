using AutoMapper;
using BallastLane.ApplicationService.Base;
using BallastLane.ApplicationService.Dto;
using BallastLane.ApplicationService.Interface;
using BallastLane.Domain.Model;
using BallastLane.Infrastructure.Data.Repository.Interface;
using Domain.ValueObjects;

namespace BallastLane.ApplicationService
{
    public class ApplicationAppService : IApplicationAppService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        public ApplicationAppService(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        public async Task<Result<ApplicationOutputDto>> CreateAsync(ApplicationCreateInputDto inputDto)
        {
            var model = _mapper.Map<ApplicationCreateInputDto, Application>(inputDto);
            
            var response = await _applicationRepository.CreateAsync(model);

            var outputDto = _mapper.Map<Application, ApplicationOutputDto>(response);

            return new Result<ApplicationOutputDto>(outputDto);
        }

        public async Task DeleteAsync(string id)
        {
            await _applicationRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ApplicationOutputDto>> GetAllAsync(int offset, int fetch)
        {
            var response = await _applicationRepository.GetAllAsync(offset, fetch);
            return _mapper.Map<IEnumerable<Application>, IEnumerable<ApplicationOutputDto>>(response);
        }

        public async Task<ApplicationOutputDto> GetByIdAsync(string id)
        {
            var response = await _applicationRepository.GetByIdAsync(id);
            return _mapper.Map<Application, ApplicationOutputDto>(response);
        }

        public async Task<Result> UpdateAsync(string id, ApplicationUpdateInputDto inputDto)
        {
            var model = _mapper.Map<ApplicationUpdateInputDto, Application>(inputDto);

            var application = await _applicationRepository.GetByIdAsync(id);

            if (application is null)
            {
                return new Result(MessageUtils.NotFoundCode, MessageUtils.NotFoundMessage);
            }
            
            await _applicationRepository.UpdateAsync(id, model);

            return new Result();
        }
    }
}
