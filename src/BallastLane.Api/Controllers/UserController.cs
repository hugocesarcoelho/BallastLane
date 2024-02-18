using BallastLane.ApplicationService.Dto.User;
using BallastLane.ApplicationService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BallastLane.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPost]
        public async Task<ActionResult<UserOutputDto>> CreateAsync(UserCreateInputDto userCreateInputDto)
        {
            var response = await _userAppService.CreateAsync(userCreateInputDto);
            if (response.HasErrors())
            {
                return BadRequest(response);
            }

            return CreatedAtRoute(nameof(GetUserByIdAsync), new { id = response.Content.Id }, response.Content);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserOutputDto>> UpdateAsync([Required] string id, UserUpdateInputDto userUpdateInputDto)
        {
            var response = await _userAppService.UpdateAsync(id, userUpdateInputDto);

            if (response.HasErrors())
            {
                return BadRequest(response);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserOutputDto>> DeleteAsync(string id)
        {
            await _userAppService.DeleteAsync(id);
            return Accepted();
        }

        [HttpGet("{id}", Name = nameof(GetUserByIdAsync))]
        public async Task<ActionResult<UserOutputDto>> GetUserByIdAsync([Required] string id)
            => Ok(await _userAppService.GetByIdAsync(id));

        [HttpGet]
        public async Task<ActionResult<UserOutputDto>> GetAllAsync([FromQuery] int offset = 0, [FromQuery] int fetch = 100)
            => Ok(await _userAppService.GetAllAsync(offset, fetch));
    }
}
