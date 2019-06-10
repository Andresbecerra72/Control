namespace Control.Web.Controllers.API
{
    using Common.Models;
    using Control.Web.Data.Repositories;
    using Helpers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[Controller]")]
    public class AccountController : Controller //este controlador permite crear un nuevo usuario desde el movil
    {
        private readonly IUserHelper userHelper;
        private readonly ICountryRepository countryRepository;
        private readonly IMailHelper mailHelper;
        
        //se definen las inyeccines necesarias para crear un nuevo usuario desde la App Movil
        public AccountController(
            IUserHelper userHelper,
            ICountryRepository countryRepository,
            IMailHelper mailHelper)
        {
            this.userHelper = userHelper;
            this.countryRepository = countryRepository;
            this.mailHelper = mailHelper;
        }

        //POST USER crea el usuario
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] NewUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request"
                });
            }

            var user = await this.userHelper.GetUserByEmailAsync(request.Email);
            if (user != null)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "This email is already registered."
                });
            }

            var city = await this.countryRepository.GetCityAsync(request.CityId);
            if (city == null)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "City don't exists."
                });
            }

            //nuevo objeto usuario
            user = new Data.Entities.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                Address = request.Address,
                PhoneNumber = request.Phone,
                CityId = request.CityId,
                City = city
            };

            var result = await this.userHelper.AddUserAsync(user, request.Password);
            if (result != IdentityResult.Success)
            {
                return this.BadRequest(result.Errors.FirstOrDefault().Description);
            }

            //confirmacion del cuenta de correo
            var myToken = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
            var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
            {
                userid = user.Id,
                token = myToken
            }, protocol: HttpContext.Request.Scheme);

            this.mailHelper.SendMail(request.Email, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                $"To allow the user, " +
                $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");

            return Ok(new Response
            {
                IsSuccess = true,
                Message = "A Confirmation email was sent. Plese confirm your account and log into the App."
            });
        }
    }

}
