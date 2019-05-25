using Control.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Control.Web.Models
{
    public class PassangerViewModel : Passanger //clase para gestion de la imagen
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; } //es el archivo en memoria IFromFile es una interface para capturar la imagen

    }
}
