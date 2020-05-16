
namespace RPGCalendar.Controllers
{
    using System.Threading.Tasks;
    using Core.Dto;
    using Core.Services;
    using Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userRepository;



        public LoginController(
            IAuthenticationService authenticationService,
            IUserService userRepository)
        {
            _authenticationService = authenticationService;
            _userRepository = userRepository;
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

            var user = await _userRepository.LoginUser(result);
            return Ok(user);

        }


    }
}