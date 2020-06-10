namespace RPGCalendar.Identity
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public interface IAuthenticationService
    {
        Task<string?> Login(LoginModel model);
        Task<string?> Register(RegistrationModel model);
        Task Logout();
        Task<string?> ChangePassword(string userEmail, string currentPassword, string newPassword);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<string?> Login(LoginModel model)
        {
            if (model.Email is null)
                return null;
            var user = await _userManager.Users.FirstAsync(ur => ur.Email == model.Email);
            var result = await _signInManager.PasswordSignInAsync(user.UserName,
                model.Password, model.RememberMe, lockoutOnFailure: false);
            return result.Succeeded
                ? user.Id
                : null;
        }

        public async Task<string?> Register(RegistrationModel model)
        {
            var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return null;
            

            await _signInManager.SignInAsync(user, isPersistent: false);
            return user.Id;


        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string?> ChangePassword(string userEmail, string currentPassword, string newPassword)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(userEmail);
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result is null)
                return null;
            if (!result.Succeeded)
                return "Error: " + result.ToString();
            return result.ToString();
        }
    }
}
