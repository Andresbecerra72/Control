namespace Control.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CityViewModel //modelo para gestionar las ciudades y paises
    {
        public int CountryId { get; set; }

        public int CityId { get; set; }

        [Required]
        [Display(Name = "City")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        public string Name { get; set; }
    }

}
