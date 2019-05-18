namespace Control.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Passanger
    {
        public int PassangerId { get; set; }
        public string Flight { get; set; }

        public int Adult { get; set; }

        public int Child { get; set; }

        public int Infant { get; set; }

        public int Total { get; set; }

        [Display(Name = "Fecha")]
        public DateTime PublishOn { get; set; }
    }
}
