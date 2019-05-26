namespace Controllers.API
{
    using Control.Web.Data;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //autenticacion para validar el acceso al api
    [Route("api/[Controller]")] //este es el enrutamiento
    public class PassangerController : Controller
    {
        //el siguiete codigo resuelvve la inyeccion del repositorio en para el API
        private readonly IPassangerRepository passangerRepository;

        public PassangerController(IPassangerRepository passangerRepository)
        {
            this.passangerRepository = passangerRepository;
        }
        //GET/Passangers
        [HttpGet]
        public IActionResult GetPassagers()
        {
            return this.Ok(this.passangerRepository.GetAllWithUsers());
        }


    }
}
