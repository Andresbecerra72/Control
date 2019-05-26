namespace Control.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterNewUserViewModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Numero de Documento")]
        public string Document { get; set; }

        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        [Display(Name = "Celular")]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }
    }
}
