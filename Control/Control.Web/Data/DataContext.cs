namespace Control.Web.Data
{
    using Control.Web.Data.Entities;
    using Microsoft.EntityFrameworkCore;


    public class DataContext : DbContext
    {
        public DbSet<Passanger> Passangers { get; set; } //es una propiedad es para acceder al objeto pasajeros 
        public DataContext(DbContextOptions<DataContext> options) : base(options)//es la conexion a la base de datos
        {

        }



    }



}









