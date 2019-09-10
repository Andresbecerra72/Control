namespace Control.Web.Data.Entities
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Passanger : IEntity //hereda la clase IEntity.. para obligar a las tablas a tener id
    {

        public int Id { get; set; }

        [Display(Name = "Date ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishOn { get; set; }

        [Required]
        [MaxLength(4, ErrorMessage = "The field {0} only can contain {1} characters")]
        public string Flight { get; set; }

        public int Adult { get; set; }

        public int Child { get; set; }

        public int Infant { get; set; }

        public int Total { get; set; }
                
        [MaxLength(80, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        public string Remark { get; set; }

        public string Day { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public User User { get; set; }//relacion de usuarios con los datos reportados





        //Atributo para el control de la imagen en el API por medio del path en el json
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImageUrl))
                {
                    return null;
                }

                return $"http://186.154.237.242:8080{this.ImageUrl.Substring(1)}";
            }
        }



    }
}
