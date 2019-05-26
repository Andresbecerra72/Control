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
        public readonly RoleManager<IdentityRole> roleManager; //IdentityRol es una clase predefinida

        public UserHelper(
            UserManager<User> userManager,      //esta clase userhelper es la unica que inyecta el usermanager
            SignInManager<User> signInManager,  //esta propiedad permite login - logout
        RoleManager<IdentityRole> roleManager)  //permite asignacion de roles a los usuarios 
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
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

        public async Task CheckRoleAsync(string roleName)//metodo que verifica el role y si no exite lo crea
        {
            var roleExists = await this.roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await this.roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }

        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await this.userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await this.userManager.IsInRoleAsync(user, roleName);
        }
    }

}
