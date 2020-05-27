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
        private readonly IUserService _userRepository;


        public RegistrationController(IAuthenticationService authenticationService,
                                      IUserService userRepository)
        {
            _authenticationService = authenticationService;
            _userRepository = userRepository;
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
            var user = await _userRepository.RegisterUser(new UserInput
            {
                Username = model.Username!,
                Email = model.Email!,
                AuthId = result
            });
            return Ok(user);

        }

    }
}