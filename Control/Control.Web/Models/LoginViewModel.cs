namespace Control.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    public class LoginViewModel //modelo para el registro y acceso de usuarios
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
