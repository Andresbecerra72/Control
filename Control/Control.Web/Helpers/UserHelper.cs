namespace Control.Web.Helpers
{
    using Control.Web.Models;
    using Data.Entities;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

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

        public async Task<IdentityResult> UpdateUserAsync(User user) //modifica el usuario
        {
            return await this.userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)//modifica el password
        {
            return await this.userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<SignInResult> ValidatePasswordAsync(User user, string password)//metodo que valida el usuario logeado para acceso al api
        {
            return await this.signInManager.CheckPasswordSignInAsync(
         user,
         password,
         false);

        }
    }

}
