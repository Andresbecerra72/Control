namespace Control.Web.Data
{
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    //esta clase establece el repositorio solo para la tabla pasajeros
    public class PassangerRepository : GenericRepository<Passanger>, IPassangerRepository
    {
        private readonly DataContext context;

        public PassangerRepository (DataContext context) : base(context)//inyecta la coneccion con base de datos
        {
            this.context = context;

        }
        //este metodo organiza la lista que se consulta por el orden de usuarios
        public IQueryable GetAllWithUsers()
        {
            DateTime DateToday = DateTime.Now; // Fecha Actual           
                                                                                       
            TimeSpan timeSpan = new TimeSpan(31, 12, 1, 1);// TimeSpan object with 30 days, 12 hours, 1 minutes and 1 second. 

            DateTime substarctResult = DateToday - timeSpan; // fecha con 30 días menos (Operacion de Resta)  

            return this.context.Passangers
                .Include(p => p.User) //hace la relacion de los registros pasajeros con el usuario
                .Where(b => b.Flight != null) 
                .Where(s => s.PublishOn > substarctResult) // selecciona el rango de fechas 
                .OrderByDescending(c => c.PublishOn);
        }


        // metodo para eliminar un registro
        public async Task DeleteItemAsync(int id)
       {
            var orderDetailTemp = await this.context.Passangers.FindAsync(id);
            if (orderDetailTemp == null)
            {
                return;
            }

            this.context.Passangers.Remove(orderDetailTemp);
            await this.context.SaveChangesAsync();
        }

       
        //este metodo trae la lista con el usuario registrado
        public IQueryable GetAllWithUsersAuthenticated(string user)
        {

            var result = this.context.Passangers
               .Include(p => p.User)
               .Where(r => r.User.UserName == user)
               .OrderBy(c => c.PublishOn);

            return result;
        }

        //consulta para el API lista de pasajeros por fecha actual en la AppMovil y el usuario valido
        public IQueryable GetPassangerByDate(string day, string month, string year)
        {
            return this.context.Passangers
                .Include(p => p.User)//hace la relacion de los registros pasajeros con el usuario
                .Where(d => d.Day == day)
                .Where(m => m.Month == month)
                .Where(y => y.Year == year);

                
        }


        // metodo para llamar todos los datos de la tabla Passanger: Usado para descargar el Archivo de Excel
        public async Task<List<Passanger>> GetAllDataAsync()
        {
            return await this.context.Passangers
             .Include(p => p.User)
             .Select(reg => reg).ToListAsync();


        }

        // Elimina los registros del entity Passanger
        public async Task DeleteAllReportAsync()
        {

            // var all = from c in context.Passangers select c;

            var all = this.context.Passangers.Select(reg => reg);

            if (all.ToList().Count() > 1000)
            {

                foreach (var row in all)
                {
                    Passanger passangerTemp = await this.context.Passangers.FindAsync(row.Id);
                    this.context.Passangers.Remove(passangerTemp);
                   
                }
                try
                {
                      this.context.SaveChanges();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
              

            }
            else
            {
                this.context.Passangers.RemoveRange(all.ToList());
                this.context.SaveChanges();
            }            
           
                     
            
        }


    }

}
