namespace Control.Web.Helpers
{
    using System.Threading.Tasks;
    using Control.Web.Models;
    using Data.Entities;
    using Microsoft.AspNetCore.Identity;

    public class UserHelper : IUserHelper //hereda la interface
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserHelper(
            UserManager<User> userManager,      //esta clase userhelper es la unica que inyecta el usermanager
            SignInManager<User> signInManager)  //esta propiedad permite login - logout
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await this.userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
           return await this.userManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await this.signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);//en true puede bloquear la cuenta
        }

        public async Task LogoutAsync()
        {
            await this.signInManager.SignOutAsync();
        }
    }

}
