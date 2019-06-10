namespace Control.Common.Models
{
    using System.ComponentModel.DataAnnotations;

    public class NewUserRequest //este modelo es para hacer el Request que crea un nuevo USER, eto es lo que requiere el api
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int CityId { get; set; }
    }

}
