﻿namespace Control.Web.Data.Entities
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
        
        public int Id { get; set; }

        [Required]
        [MaxLength(4, ErrorMessage = "The field {0} only can contain {1} characters")]
        public string Flight { get; set; }

        public int Adult { get; set; }

        public int Child { get; set; }

        public int Infant { get; set; }

        public int Total { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishOn { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public User User { get; set; }//relacion de usuarios con los datos reportados

        //[MaxLength(80, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        public string Remark { get; set; }

        public string Day { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }

        //Atributo para el control de la imagen en el API por medio del path en el json
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImageUrl))
                {
                    return null;
                }

                return $"https://controlweb.azurewebsites.net{this.ImageUrl.Substring(1)}";
            }
        }

        

    }
}
