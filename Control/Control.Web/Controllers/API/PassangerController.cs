namespace Control.Web.Controllers.API
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Control.Web.Data;
    using Control.Web.Data.Entities;
    using Control.Web.Helpers;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;



    [Route("api/[Controller]")] //este es el enrutamiento
    //TODO:*********************************************************OJO TOKEN*********************************
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //autenticacion para validar el acceso al api
    public class PassangerController : Controller
    {
        //el siguiete codigo resuelvve la inyeccion del repositorio en para el API
        private readonly IPassangerRepository passangerRepository;

        private readonly IUserHelper userHelper;


        public PassangerController(
            IPassangerRepository passangerRepository,
            IUserHelper userHelper)
        {
            this.passangerRepository = passangerRepository;
            this.userHelper = userHelper;
        }
        //GET/Passangers
        [HttpGet]
        public IActionResult GetPassagers()
        {
            return this.Ok(this.passangerRepository.GetAllWithUsers());
        }

        //POST PASSANGER metodo para crear desde el movil
        [HttpPost]
        public async Task<IActionResult> PostPassanger([FromBody] Common.Models.Passanger passanger)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var user = await this.userHelper.GetUserByEmailAsync(passanger.User.UserName);
            if (user == null)
            {
                return this.BadRequest("Invalid user");
            }

            //crea la imagen
            var imageUrl = string.Empty;
            if (passanger.ImageArray != null && passanger.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(passanger.ImageArray);//coleccion de bytes para enviar la imagen
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Passangers";
                var fullPath = $"~/images/Passangers/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl = fullPath;
                }
            }

            //construye el modelo del entity contodos los datos ingresados por el usuario desde la app movil
            var entityPassanger = new Passanger
            {
                Flight = passanger.Flight,
                Adult = passanger.Adult,
                Child = passanger.Child,
                Infant = passanger.Infant,
                Total = passanger.Total,
                PublishOn = passanger.PublishOn,
                User = user,
                Remark = passanger.Remark,
                Day = passanger.PublishOn.ToString("dd"),
                Month = passanger.PublishOn.ToString("MMMM"),
                Year = passanger.PublishOn.ToString("yyyy"),
                ImageUrl = imageUrl
            };

            var newPassanger = await this.passangerRepository.CreateAsync(entityPassanger);
            return Ok(newPassanger);
        }

        //PUT PASSANGER metodo para modificar
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassanger([FromRoute] int id, [FromBody] Common.Models.Passanger passanger)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (id != passanger.Id)
            {
                return BadRequest();
            }

            var oldPassanger = await this.passangerRepository.GetByIdAsync(id);
            if (oldPassanger == null)
            {
                return this.BadRequest("Product Id don't exists.");
            }

            
            oldPassanger.Adult = passanger.Adult;
            oldPassanger.Child = passanger.Child;
            oldPassanger.Infant = passanger.Infant;
            oldPassanger.Flight = passanger.Flight;
            oldPassanger.Total = passanger.Total;
            oldPassanger.PublishOn = passanger.PublishOn;
            oldPassanger.Remark = passanger.Remark;

            var updatedProduct = await this.passangerRepository.UpdateAsync(oldPassanger);
            return Ok(updatedProduct);
        }

        //DELETE PASSANGER
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassanger([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var passanger = await this.passangerRepository.GetByIdAsync(id);
            if (passanger == null)
            {
                return this.NotFound();
            }

            await this.passangerRepository.DeleteAsync(passanger);
            return Ok(passanger);
        }



    }
}
