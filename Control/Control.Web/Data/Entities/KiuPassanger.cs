namespace Control.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    

    public class KiuPassanger : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Dates")]
        public string PublishOn { get; set; }

        [Display(Name = "Flights")]
        public string Flight { get; set; }

        [Display(Name = "Adults")]
        public int Adult { get; set; }

        [Display(Name = "Children")]
        public int Child { get; set; }

        [Display(Name = "Infants")]
        public int Infant { get; set; }

        [Display(Name = "Total Pax")]
        public int Total { get; set; }

        [MaxLength(80, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        public string Remark { get; set; }

        public string Day { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public User User { get; set; }//relacion de usuarios con los datos reportados



    }
}
