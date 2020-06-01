namespace RPGCalendar.Api.Controllers
{
    using System.Threading.Tasks;
    using Core.Dto;
    using Core.Services;
    using Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;


        public RegistrationController(IAuthenticationService authenticationService,
                                      IUserService userRepository)
        {
            _authenticationService = authenticationService;
            _userService = userRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Post(RegistrationModel model)
        {
            var result = await _authenticationService.Register(model);
            if (result is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var user = await _userService.RegisterUser(new UserInput
            {
                Username = model.Username!,
                Email = model.Email!,
                AuthId = result
            });
            return Ok(user);

        }

        [HttpPost("change/{userEmail},{currentPassword},{newPassword},{confirmPassword}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangePassword(string userEmail, string currentPassword, string newPassword, string confirmPassword)
        {
            if(currentPassword.Equals(newPassword) || !newPassword.Equals(confirmPassword))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var result = await _authenticationService.ChangePassword(userEmail, currentPassword, newPassword);
            if( result is null )
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return Ok(result);
        }

    }
}