namespace Control.Common.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ChangePasswordRequest //Modelo para la peticion de cambio de passwor desde el App movil
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string Email { get; set; }
    }

}
