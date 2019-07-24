using Control.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Control.Web.Models
{
    public class KiuReportViewModel : KiuReport
    {
        public IFormFile ExcelFile { get; set; }

    }
}
