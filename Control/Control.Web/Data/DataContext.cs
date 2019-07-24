namespace Control.Web.Data
{
    using Control.Web.Data.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class DataContext : IdentityDbContext<User>//DbContext
    {
        public DbSet<Passanger> Passangers { get; set; } //es una propiedad es para acceder al objeto pasajeros 

        public DbSet<KiuReport> KiuReports { get; set; } //es una propiedad es para acceder al objeto pasajeros 

        public DbSet<Country> Countries { get; set; } //es una propiedad es para acceder al objeto Paises 

        public DbSet<City> Cities { get; set; } //es una propiedad es para acceder al objeto Ciudades

        public DataContext(DbContextOptions<DataContext> options) : base(options)//es la conexion a la base de datos
        {

        }

        //este codigo evita el borrado en cascada, el borrado de registros enlazados en BD con key foraneos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model
                .G­etEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Casca­de);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restr­ict; //restringe el borrado
            }

            base.OnModelCreating(modelBuilder);
        }

    }



}









