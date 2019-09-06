namespace Control.Web.Controllers
{

    using Control.Web.Data.Repositories;
    using Data.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System.Threading.Tasks;

    [Authorize(Roles = "Super")]
    public class CountriesController : Controller // permite el crud de paises y ciudades
    {
        private readonly ICountryRepository countryRepository;

        public CountriesController(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public async Task<IActionResult> DeleteCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await this.countryRepository.GetCityAsync(id.Value);
            if (city == null)
            {
                return NotFound();
            }

            var countryId = await this.countryRepository.DeleteCityAsync(city);
            return this.RedirectToAction($"Details/{countryId}");
        }

        //EDIT CITY

        public async Task<IActionResult> EditCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await this.countryRepository.GetCityAsync(id.Value);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        [HttpPost]
        public async Task<IActionResult> EditCity(City city)
        {
            if (this.ModelState.IsValid)
            {
                var countryId = await this.countryRepository.UpdateCityAsync(city);
                if (countryId != 0)
                {
                    return this.RedirectToAction($"Details/{countryId}");
                }
            }

            return this.View(city);
        }

        //ADD CITY

        public async Task<IActionResult> AddCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await this.countryRepository.GetByIdAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            var model = new CityViewModel { CountryId = country.Id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(CityViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.countryRepository.AddCityAsync(model);
                return this.RedirectToAction($"Details/{model.CountryId}");
            }

            return this.View(model);
        }

        //GET COUNTRIES

        public IActionResult Index()
        {
            return View(this.countryRepository.GetCountriesWithCities());
        }

        //DETAILS 

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await this.countryRepository.GetCountryWithCitiesAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        //CREATE COUNTRIES

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                await this.countryRepository.CreateAsync(country);
                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }

        //EDIT COUNTRIES

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await this.countryRepository.GetByIdAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                await this.countryRepository.UpdateAsync(country);
                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }

        //DELETE COUNTRIES

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await this.countryRepository.GetByIdAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            try
            {
                await this.countryRepository.DeleteAsync(country);

            }
            catch { }
            return RedirectToAction(nameof(Index));
        }
    }
}
