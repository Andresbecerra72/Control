namespace Control.Web.Data
{
    using Entities;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    //esta clase es el alimentador de la base de datos cundo esta vacia ***pendiente modificar startp y program
    public class SeedDB
    {
        private readonly DataContext context;
        private Random random;

        public SeedDB(DataContext context) //aqui se establece la conexion con la base de datos
        {
            this.context = context;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            if (!this.context.Passangers.Any())
            {
                this.AddProduct("0001");
                this.AddProduct("0002");
                this.AddProduct("0003");
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name)
        {
            this.context.Passangers.Add(new Passanger
            {
                Flight = name,
                Adult = this.random.Next(50),
                Child = this.random.Next(10),
                Infant = this.random.Next(10),
                Total = this.random.Next(10),
                PublishOn = DateTime.Now
            });

        }



    }
}
