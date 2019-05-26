namespace Control.Web.Data
{
    using Control.Web.Helpers;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    //TODO: ***esta clase es el alimentador de la base de datos cundo esta vacia ***pendiente modificar startp y program
    public class SeedDB
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        //inyeccion del datacontext y el userhelper
        public SeedDB(DataContext context, IUserHelper userHelper)//conexion con tablas de usuarios y conteo de passajeros
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            //verificacion de roles
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");

            //adiciona el nuevo usuario
            var user = await this.userHelper.GetUserByEmailAsync("andres.becerra@satena.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Andres",
                    LastName = "Becerra",
                    Document = "1234567",
                    Email = "andres.becerra@satena.com",
                    UserName = "andres.becerra@satena.com",
                    PhoneNumber = "3202456321"

                };

                var result = await this.userHelper.AddUserAsync(user, "123456");//passsword
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await this.userHelper.AddUserToRoleAsync(user, "Admin");

            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }



            if (!this.context.Passangers.Any())
            {
                this.AddProduct("0001", user);
                this.AddProduct("0002", user);
                this.AddProduct("0003", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            this.context.Passangers.Add(new Passanger
            {
                Flight = name,
                Adult = this.random.Next(50),
                Child = this.random.Next(10),
                Infant = this.random.Next(10),
                Total = this.random.Next(10),
                PublishOn = DateTime.Now,
                ImageUrl = $"~/images/Passangers/{name}.png",
                User = user
            });

        }



    }
}
