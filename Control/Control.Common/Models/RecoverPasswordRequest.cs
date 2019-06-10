namespace Control.Common.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RecoverPasswordRequest //modelo que permite la recoperacion del password validadndo el usuario
    {
        [Required]
        public string Email { get; set; }
    }

}
