using Control.Web.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Control.Web.Data.Repositories
{
    public class KiuReportRepository : GenericRepository<KiuReport>, IKiuReportRepository
    {
        private readonly DataContext context;

        public KiuReportRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<KiuReport>> CreateKiuReportAsync(List<KiuReport> list)
        {
            
            await this.context.KiuReports.AddRangeAsync(list);
            await SaveAllAsync();
            return list;

        }


        public async Task DeleteKiuReportAsync()
        {
            this.context.KiuReports.RemoveRange(context.KiuReports);
            await SaveAllAsync();
        }

        public IEnumerable TotalPaxKiuReportAsync(string Date, string Flight)
        {
            var Total= this.context.KiuReports
                             .Where(d => d.Fecha_vuelo_real == Date)
                             .Where(f => f.Vuelo == Flight)
                             .Count();

            return Total.ToString();
        }


    }
}
