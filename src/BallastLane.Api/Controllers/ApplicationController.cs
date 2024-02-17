using BallastLane.ApplicationService.Dto;
using BallastLane.ApplicationService.Interface;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BallastLane.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ApplicationController : Controller
    {
        private readonly IApplicationAppService _applicationAppService;

        public ApplicationController(IApplicationAppService applicationAppService)
        {
            _applicationAppService = applicationAppService;
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationOutputDto>> CreateAsync(ApplicationCreateInputDto applicationCreateInputDto)
        {
            var response = await _applicationAppService.CreateAsync(applicationCreateInputDto);
            if (response.HasErrors())
            {
                return BadRequest(response);
            }

            return CreatedAtRoute(nameof(GetByIdAsync), new { id = response.Content.Id }, response.Content);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationOutputDto>> UpdateAsync([Required] string id, ApplicationUpdateInputDto applicationUpdateInputDto)
        {
            var response = await _applicationAppService.UpdateAsync(id, applicationUpdateInputDto);

            if (response.HasErrors())
            {
                return BadRequest(response);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApplicationOutputDto>> DeleteAsync(string id)
        {
            await _applicationAppService.DeleteAsync(id);
            return Accepted();
        }

        [HttpGet("{id}", Name = nameof(GetByIdAsync))]
        public async Task<ActionResult<ApplicationOutputDto>> GetByIdAsync([Required] string id)
            => Ok(await _applicationAppService.GetByIdAsync(id));

        [HttpGet]
        public async Task<ActionResult<ApplicationOutputDto>> GetAllAsync([FromQuery] int offset = 0, [FromQuery] int fetch = 100)
            => Ok(await _applicationAppService.GetAllAsync(offset, fetch));
    }
}
