namespace Control.Web.Helpers
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Microsoft.AspNetCore.Identity;

    public class UserHelper : IUserHelper //hereda la interface
    {
        private readonly UserManager<User> userManager;

        public UserHelper(UserManager<User> userManager)//esta clase userhelper es la unica que inyecta el usermanager
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await this.userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
           return await this.userManager.FindByEmailAsync(email);
        }
    }

}
