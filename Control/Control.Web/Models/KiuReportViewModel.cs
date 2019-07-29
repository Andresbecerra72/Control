namespace Control.Web.Models
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using Control.Web.Data.Entities;
    using System.ComponentModel.DataAnnotations;

    public class KiuReportViewModel : KiuPassanger //TODO: CAMBIO DE ENTITY
    {
        public IFormFile ExcelFile { get; set; }

        public IEnumerable<SelectListItem> Fechas { get; set; }

        //TODO: CAMBIO DE ENTITY*************************

        //[Display(Name = "Total")]
        //public int TotalPax { get; set; }

        //[Display(Name = "Adult")]
        //public int TotalAdult { get; set; }

        //[Display(Name = "Infant")]
        //public int TotalInfant { get; set; }

        //[Display(Name = "Child")]
        //public int TotalChild { get; set; }


    }
}
