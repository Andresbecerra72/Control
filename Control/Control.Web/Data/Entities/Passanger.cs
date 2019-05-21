namespace Control.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Threading.Tasks;
    using Data;
    using MySql.Data.MySqlClient;
    using Newtonsoft.Json;

    public class Passanger: IEntity //hereda la clase IEntity.. para obligar a las tablas a tener id
    {
        //private DataContext context;
        public int Id { get; set; }

        [Required]
        [MaxLength(4, ErrorMessage = "The field {0} only can contain {1} characters")]
        public string Flight { get; set; }

        public int Adult { get; set; }

        public int Child { get; set; }

        public int Infant { get; set; }

        public int Total { get; set; }

        [Display(Name = "Fecha")]
        public DateTime PublishOn { get; set; }

        public User User { get; set; }//relacion de usuarios con los datos reportados

                     
    }
}
