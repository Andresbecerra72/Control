namespace Control.Web.Models
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using Control.Web.Data.Entities;
    using System.ComponentModel.DataAnnotations;

    public class KiuReportViewModel : KiuPassanger 
    {
        public IFormFile ExcelFile { get; set; }

        public IEnumerable<SelectListItem> Fechas { get; set; }

       


    }
}
