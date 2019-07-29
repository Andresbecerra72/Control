namespace Control.Web.Data.Repositories
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Control.Web.Data.Entities;
    using System.Linq;

    public interface IKiuReportRepository : IGenericRepository<KiuPassanger>
    {
        Task<List<KiuReport>> CreateKiuReportAsync(List<KiuReport> list);

        Task DeleteKiuReportAsync();
               

        //para cargar vuelos
        IEnumerable<SelectListItem> GetComboFechas();

        //******************************************************************************
        // IEnumerable TotalPaxKiuReportAsync(string Date, string Flight);

        // IEnumerable TotalAdultKiuReportAsync(string Date, string Flight);

        //  IEnumerable TotalChildKiuReportAsync(string Date, string Flight);

        //  IEnumerable TotalInfantKiuReportAsync(string Date, string Flight);

    }
}
