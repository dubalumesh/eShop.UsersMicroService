using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UsersMicroService.Core.DTOs;
using UsersMicroService.Core.Services;
using UsersMicroService.Core.Validators;

namespace UsersMicroService.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        [Route("Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request, [FromServices] RegisterUserRequestValidator validator)
        {
            if (request == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            var validationResult = await validator.ValidateAsync(request);
            if ((validationResult.IsValid))
            {
                return Ok(await userService.RegisterUserAsync(request));
            }
            else
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(string.Join(", ", errors));
            }


        }

    }
}
