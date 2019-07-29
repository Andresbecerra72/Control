namespace Control.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    

    public class KiuPassanger : IEntity
    {
        public int Id { get; set; }
        
        [Display(Name = "Dates")]
        public string PublishOnKIU { get; set; }

        [Display(Name = "Adults")]
        public int TotalAdult { get; set; }

        [Display(Name = "Children")]
        public int TotalChild { get; set; }

        [Display(Name = "Infants")]
        public int TotalInfant { get; set; }

        [Display(Name = "Total Pax")]
        public int TotalPax { get; set; }

        [Display(Name = "Flights")]
        public string Vuelo { get; set; }

   

    }
}
