﻿namespace Control.Web.Helpers
{
    using Control.Web.Models;
    using Data.Entities;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserHelper//es la interface de UserHelper contiene los metodos para la administracion de usuarios
    {
        Task<User> GetUserByEmailAsync(string email); //llama el usuario

        Task<IdentityResult> AddUserAsync(User user, string password); 

        Task<SignInResult> LoginAsync(LoginViewModel model);//login

        Task LogoutAsync();//logout

        Task<IdentityResult> UpdateUserAsync(User user);//modifica datos del usuario

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        Task<SignInResult> ValidatePasswordAsync(User user, string password);//valida el usuario para acceso al API

        Task CheckRoleAsync(string roleName); //metodo para verificar los roles de los usuarios

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        //metodos para la confirmacion de usuarios por medio de correo
        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<User> GetUserByIdAsync(string userId);

        //metodos de recuperacion de password
        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);

        //administracion de usuarios
        Task<List<User>> GetAllUsersAsync();

        Task RemoveUserFromRoleAsync(User user, string roleName);//quitq el permiso del role

        Task DeleteUserAsync(User user);//elimina usuarios registrados



    }
}