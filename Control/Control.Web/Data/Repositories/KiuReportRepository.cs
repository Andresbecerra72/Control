namespace Control.Web.Data.Repositories
{

    using Control.Web.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class KiuReportRepository : GenericRepository<KiuPassanger>, IKiuReportRepository
    {
     
        private readonly DataContext context;

        public KiuReportRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        //codigo para cargar datos pormedio de una lista
        public async Task<List<KiuReport>> CreateKiuReportAsync(List<KiuReport> list)
        {
            
            await this.context.KiuReports.AddRangeAsync(list);
            await SaveAllAsync();
            return list;

        }

      //elimina los registros del entity KiuPassanger
        public async Task DeleteKiuReportAsync()
        {
            this.context.KiuPassangers.RemoveRange(context.KiuPassangers);
            await SaveAllAsync();
        }

       

        //cargar combo FECHAS en la pagina index del Calculate
        public IEnumerable<SelectListItem> GetComboFechas()
        {
            var list = this.context.KiuPassangers
                .Select(c => new SelectListItem
            {
                Text = c.PublishOnKIU
                
            }).OrderBy(l => l.Text).Distinct();

            //list.Insert(0, new SelectListItem
            //{
            //    Text = "(Select a Flight...)",

            //});

            return list;
        }


        //******************************************
        //public IEnumerable TotalPaxKiuReportAsync(string Date, string Flight)//TODO: CAMBIO DE ENTITY
        //{
        //    var Total= this.context.KiuReports
        //                     .Where(d => d.Fecha_vuelo_real == Date)
        //                     .Where(f => f.Vuelo == Flight)
        //                     .Count();

        //    return Total.ToString();
        //}

        //public IEnumerable TotalAdultKiuReportAsync(string Date, string Flight)//TODO: CAMBIO DE ENTITY
        //{
        //    var Total = this.context.KiuReports
        //                     .Where(d => d.Fecha_vuelo_real == Date)
        //                     .Where(f => f.Vuelo == Flight)
        //                     .Where(f => f.Tpax == "A")
        //                     .Count();

        //    return Total.ToString();
        //}

        //public IEnumerable TotalChildKiuReportAsync(string Date, string Flight)//TODO: CAMBIO DE ENTITY
        //{
        //    var Total = this.context.KiuReports
        //                     .Where(d => d.Fecha_vuelo_real == Date)
        //                     .Where(f => f.Vuelo == Flight)
        //                     .Where(f => f.Tpax == "C")
        //                     .Count();

        //    return Total.ToString();
        //}

        //public IEnumerable TotalInfantKiuReportAsync(string Date, string Flight)//TODO: CAMBIO DE ENTITY
        //{
        //    var Total = this.context.KiuReports
        //                     .Where(d => d.Fecha_vuelo_real == Date)
        //                     .Where(f => f.Vuelo == Flight)
        //                     .Where(f => f.Tpax == "I")
        //                     .Count();

        //    return Total.ToString();
        //}





    }
}
