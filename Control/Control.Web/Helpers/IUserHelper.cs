﻿namespace Control.Web.Helpers
{
    using Control.Web.Models;
    using Data.Entities;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public interface IUserHelper//es la interface de UserHelper contiene los metodos para la administracion de usuarios
    {
        Task<User> GetUserByEmailAsync(string email); //llama el usuario

        Task<IdentityResult> AddUserAsync(User user, string password); 

        Task<SignInResult> LoginAsync(LoginViewModel model);//login

        Task LogoutAsync();//logout

        Task<IdentityResult> UpdateUserAsync(User user);//modifica datos del usuario

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

    }
}