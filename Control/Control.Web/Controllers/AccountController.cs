﻿namespace Control.Web.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Entities;
    using Helpers;
    using Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Control.Web.Data.Repositories;
    using Microsoft.AspNetCore.Authorization;

    public class AccountController : Controller
    {
        //inyecciones
        private readonly IUserHelper userHelper;
        private readonly IMailHelper mailHelper;
        private readonly ICountryRepository countryRepository;
        private readonly IConfiguration configuration;

        public AccountController(
                IUserHelper userHelper,
                IMailHelper mailHelper,
                ICountryRepository countryRepository,
                IConfiguration configuration)
        {
            this.userHelper = userHelper;
            this.mailHelper = mailHelper;
            this.countryRepository = countryRepository;
            this.configuration = configuration;

        }

        //LOGIN USER
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        //POST LOGIN USER
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to login.");
            return this.View(model);
        }

        //LOGOUT
        public async Task<IActionResult> Logout()
        {
            await this.userHelper.LogoutAsync();
            return this.RedirectToAction("Index", "Home");
        }

        //REGISTER NEW USER
        public IActionResult Register()
        {
            var model = new RegisterNewUserViewModel
            {
                Countries = this.countryRepository.GetComboCountries(),
                Cities = this.countryRepository.GetComboCities(0)
            };

            return this.View(model);
        }

        //POST REGISTER
        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.Username);
                if (user == null)
                {
                    var city = await this.countryRepository.GetCityAsync(model.CityId);

                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Document = model.Document,
                        Email = model.Username,
                        UserName = model.Username,
                        Address = model.Address,
                        PhoneNumber = model.PhoneNumber,
                        CityId = model.CityId,
                        City = city
                    };

                    var result = await this.userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);
                    }

                    var myToken = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
                    var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    this.mailHelper.SendMail(model.Username, "SATENA TCP App Email confirmation", $"<h1>TCP App Email Confirmation</h1>" +
                        $"To allow the user, " +
                        $"please click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                    this.ViewBag.Message = "The instructions to allow your user has been sent to email.";
                    return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "The username is already registered.");
            }

            return this.View(model);
        }


        //CHANGE USER GET
        public async Task<IActionResult> ChangeUser()
        {
            var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new ChangeUserViewModel();

            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Document = user.Document;
                model.Address = user.Address;
                model.PhoneNumber = user.PhoneNumber;

                var city = await this.countryRepository.GetCityAsync(user.CityId);
                if (city != null)
                {
                    var country = await this.countryRepository.GetCountryAsync(city);
                    if (country != null)
                    {
                        model.CountryId = country.Id;
                        model.Cities = this.countryRepository.GetComboCities(country.Id);
                        model.Countries = this.countryRepository.GetComboCountries();
                        model.CityId = user.CityId;
                    }
                }
            }

            model.Cities = this.countryRepository.GetComboCities(model.CountryId);
            model.Countries = this.countryRepository.GetComboCountries();
            return this.View(model);
        }


        //POST CHANGE USER
        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var city = await this.countryRepository.GetCityAsync(model.CityId);

                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Document = model.Document;
                    user.Address = model.Address;
                    user.PhoneNumber = model.PhoneNumber;
                    user.CityId = model.CityId;
                    user.City = city;

                    var respose = await this.userHelper.UpdateUserAsync(user);
                    if (respose.Succeeded)
                    {
                        this.ViewBag.UserMessage = "User updated!";
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, respose.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return this.View(model);
        }


        //CHANGE PASSWORD GET
        public IActionResult ChangePassword()
        {
            return this.View();
        }

        //POST CHANGE PASSWORD 

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await this.userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return this.View(model);
        }

        //VALIDATION USER-PASSWORD TO ACCESS API 
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)//este metodo solo tien POST solo se consume por un llamado de API no se usa por pagina web
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.Username);//llama el usuario
                if (user != null) //pregunta si el usuario existe
                {
                    var result = await this.userHelper.ValidatePasswordAsync(//llama el metodo para validar el password del usuario consultado
                        user,
                        model.Password);

                    if (result.Succeeded)//si el resultado es satisfactorio
                    {
                        var claims = new[]
                        {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Tokens:Key"]));//se toma el key que esta en archivo appsetting.json
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//algoritmo de seguridad
                        var token = new JwtSecurityToken(
                            this.configuration["Tokens:Issuer"],//se toma el Issuer que esta en ar appsetting.json
                            this.configuration["Tokens:Audience"],//se toma el Audience que esta en ar appsetting.json
                            claims,
                            expires: DateTime.UtcNow.AddDays(15), //vigencia del token, en este caso solo dura 15 dias, teminado este tiempo debe volvel a loggerse 
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return this.Created(string.Empty, results);
                    }
                }
            }

            return this.BadRequest();
        }

        //REDIRECCIONAMIENTO vista para usuarios no autorizados
        public IActionResult NotAuthorized()
        {
            return this.View();
        }

        //GET CITIES 
        public async Task<JsonResult> GetCitiesAsync(int countryId)//devuelve datos de un Json por medio del Ajax de la pagina html register user
        {
            var country = await this.countryRepository.GetCountryWithCitiesAsync(countryId);
            return this.Json(country.Cities.OrderBy(c => c.Name));
        }

        //EMAIL CONFIRMED envia un correo para confirmar el usuario registrado
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return this.NotFound();
            }

            var user = await this.userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return this.NotFound();
            }

            var result = await this.userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return this.NotFound();
            }

            return View();
        }

        //PASSWORD RECOVER
        public IActionResult RecoverPassword()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
                    return this.View(model);
                }

                var myToken = await this.userHelper.GeneratePasswordResetTokenAsync(user);
                var link = this.Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);
                this.mailHelper.SendMail(model.Email, "SATENA TCP App Password Reset", $"<h1>TCP App Password Reset</h1>" +
                    $"To reset the password click in this link:</br></br>" +
                    $"<a href = \"{link}\">Reset Password</a>");
                this.ViewBag.Message = "The instructions to recover your password has been sent to email.";
                return this.View();

            }

            return this.View(model);
        }

        public IActionResult ResetPassword(string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await this.userHelper.GetUserByEmailAsync(model.UserName);
            if (user != null)
            {
                var result = await this.userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    this.ViewBag.Message = "Password reset successful.";
                    return this.View();
                }

                this.ViewBag.Message = "Error while resetting the password.";
                return View(model);
            }

            this.ViewBag.Message = "User not found.";
            return View(model);
        }

        //USER MANAGEMENT administracion y contro de los usuarios
        [Authorize(Roles = "Super, Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await this.userHelper.GetAllUsersAsync();
            foreach (var user in users)
            {
                var myUser = await this.userHelper.GetUserByIdAsync(user.Id);
                if (myUser != null)
                {
                    user.IsAdmin = await this.userHelper.IsUserInRoleAsync(myUser, "Admin");
                }
            }

            return this.View(users);
        }


        [Authorize(Roles = "Super")]
        public async Task<IActionResult> AdminOff(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await this.userHelper.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await this.userHelper.RemoveUserFromRoleAsync(user, "Admin");
            return this.RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Super")]
        public async Task<IActionResult> AdminOn(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await this.userHelper.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await this.userHelper.AddUserToRoleAsync(user, "Admin");
            return this.RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Super, Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await this.userHelper.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await this.userHelper.DeleteUserAsync(user);
            return this.RedirectToAction(nameof(Index));
        }


    }
}
