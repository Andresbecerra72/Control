namespace Control.Web.Models
{
    using Control.Web.Data.Entities;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    public class PassangerViewModel : Passanger //clase para gestion de la imagen
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; } //es el archivo en memoria IFromFile es una interface para capturar la imagen


    }
}
