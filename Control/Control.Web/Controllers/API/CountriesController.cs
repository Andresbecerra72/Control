namespace Control.Web.Controllers.API
{
    using Control.Web.Data.Repositories;
    using Microsoft.AspNetCore.Mvc;

    //este meetodo no requiere token de seguridad
    [Route("api/[Controller]")]
    public class CountriesController : Controller //Este control permite la seleccion de paises desde la App movil
    {
        private readonly ICountryRepository countryRepository;

        public CountriesController(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }
        //GET COUNTRIES AND CITIES metodo que consulta la lista de paises y ciudades
        [HttpGet]
        public IActionResult GetCountries()
        {
            return Ok(this.countryRepository.GetCountriesWithCities());
        }

    }

}
