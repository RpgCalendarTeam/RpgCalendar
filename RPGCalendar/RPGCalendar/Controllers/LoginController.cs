
namespace RPGCalendar.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Core.Dto;
    using Core.Extensions;
    using Core.Services;
    using Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;



        public LoginController(
            IAuthenticationService authenticationService,
            IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Login(LoginModel model)
        {
            var result = await _authenticationService.Login(model);
            if (result is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var user = await _userService.LoginUser(result);
            return Ok(user);

        }


    }
}