using Control.Web.Data.Entities;
namespace Control.Web.Data.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public interface IKiuReportRepository : IGenericRepository<KiuReport>
    {
        Task<List<KiuReport>> CreateKiuReportAsync(List<KiuReport> list);

        Task DeleteKiuReportAsync();

        IEnumerable TotalPaxKiuReportAsync(string Date, string Flight);
    }
}
