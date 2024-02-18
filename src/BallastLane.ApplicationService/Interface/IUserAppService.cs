using BallastLane.ApplicationService.Dto.User;
using Domain.ValueObjects;

namespace BallastLane.ApplicationService.Interface
{
    public interface IUserAppService
    {
        Task<Result<UserOutputDto>> CreateAsync(UserCreateInputDto inputDto);
        Task<Result> UpdateAsync(string id, UserUpdateInputDto inputDto);
        Task DeleteAsync(string id);
        Task<UserOutputDto> GetByIdAsync(string id);
        Task<UserOutputDto> GetByUsernameAsync(string username);
        Task<IEnumerable<UserOutputDto>> GetAllAsync(int offset, int fetch);
        Task<bool> ValidateAsync(string username, string password);
    }
}
