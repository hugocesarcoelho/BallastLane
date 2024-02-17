using BallastLane.ApplicationService.Dto;
using Domain.ValueObjects;

namespace BallastLane.ApplicationService.Interface
{
    public interface IApplicationAppService
    {
        Task<Result<ApplicationOutputDto>> CreateAsync(ApplicationCreateInputDto inputDto);
        Task<Result> UpdateAsync(string id, ApplicationUpdateInputDto inputDto);
        Task DeleteAsync(string id);
        Task<ApplicationOutputDto> GetByIdAsync(string id);
        Task<IEnumerable<ApplicationOutputDto>> GetAllAsync(int offset, int fetch);
    }
}
