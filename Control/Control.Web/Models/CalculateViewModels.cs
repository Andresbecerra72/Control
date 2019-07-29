namespace Control.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CalculateViewModels   //TODO: ELIMINAR
    {
        [Display(Name = "Total")]
        public string Vuelos { get; set; }

        [Display(Name = "Total")]
        public int TotalPasajeros { get; set; }

        [Display(Name = "Adult")]
        public int TotalAdultos { get; set; }

        [Display(Name = "Infant")]
        public int TotalInfantes { get; set; }

        [Display(Name = "Child")]
        public int TotalNinos { get; set; }

    }
}
