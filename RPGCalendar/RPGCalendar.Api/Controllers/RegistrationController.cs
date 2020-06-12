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
        private readonly IEmailService _emailService;


        public RegistrationController(IAuthenticationService authenticationService,
                                      IUserService userRepository,
                                      IEmailService emailService)
        {
            _authenticationService = authenticationService;
            _userService = userRepository;
            _emailService = emailService;
        }

        //testing sendemail
        //[HttpGet]
        //public async Task Get()
        //{
        //    var message = new Message(new string[] { "example@gmail.com" }, "Test email", "This is the content from our email.");
        //    await _emailService.SendEmailAsync(message);

        //}


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

        [HttpPost("changepassword/{userEmail},{currentPassword},{newPassword},{confirmPassword}")]
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


        [HttpPost("forgotpassword")]
        public async Task<ActionResult> ForgotPassword(string email)
        {
            var user = await _authenticationService.FindByEmailAsync(email);
            if (user == null)
                return StatusCode(404);

            var token = await _authenticationService.GeneratePasswordResetTokenAsync(user);

            var message = new Message(new string[] { user.Email }, "Reset password token", "Copy and paste this token: \n"+token);
            await _emailService.SendEmailAsync(message);

            return StatusCode(200);
        }

        [HttpPost("resetpassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (model.Password is null || model.Email is null || model.Token is null || !model.Password.Equals(model.ConfirmPassword))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var user = await _authenticationService.FindByEmailAsync(model.Email);
            if (user is null)
                return NoContent();
            var resetPassResult = await _authenticationService.ResetPasswordAsync(user, model.Token, model.Password);
            if (!resetPassResult.Succeeded)
                return NoContent();
            return NoContent();
        }
    }
}