namespace Control.Web.Data
{
    using Control.Web.Helpers;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

   
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
            await this.CheckRoles();

            //adiciona ciudades de y pais colombia
            if (!this.context.Countries.Any())
            {
                await this.AddCountriesAndCitiesAsync();
            }

           
            //await this.CheckUser("carolina@satena.com", "carolina", "Pit", "Customer");
            await this.CheckUser("administrador.kiu@satena.com", "Aministrador", "KIU", "Customer");
            var user = await this.CheckUser("andres.becerra@satena.com", "Andres", "Becerra", "Admin");

            // Add Passangers
            if (!this.context.Passangers.Any())
            {
                this.AddProduct("8710", user);
                this.AddProduct("8711", user);
                this.AddProduct("8740", user);
                this.AddProduct("8741", user);
                this.AddProduct("8702", user);
                this.AddProduct("8703", user);
                this.AddProduct("8864", user);
                this.AddProduct("8616", user);
                this.AddProduct("8619", user);
                this.AddProduct("8422", user);
                this.AddProduct("8423", user);
                this.AddProduct("8788", user);
                this.AddProduct("8789", user);
                this.AddProduct("8888", user);
                await this.context.SaveChangesAsync();
            }

                        
            }//***end


            //user y roles
            private async Task<User> CheckUser(string userName, string firstName, string lastName, string role)
            {
                // Add user
                var user = await this.userHelper.GetUserByEmailAsync(userName);
                if (user == null)
                {
                    user = await this.AddUser(userName, firstName, lastName, role);
                }

                var isInRole = await this.userHelper.IsUserInRoleAsync(user, role);
                if (!isInRole)
                {
                    await this.userHelper.AddUserToRoleAsync(user, role);
                }

                return user;
            }

            //User
            private async Task<User> AddUser(string userName, string firstName, string lastName, string role)
            {
                var user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = userName,
                    UserName = userName,
                    Document = "888888",
                    Address = "Carrera 87 17-35",
                    PhoneNumber = "3202456321",
                    CityId = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await this.userHelper.AddUserToRoleAsync(user, role);
                var token = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);//valida el token verificandole la validacion
                await this.userHelper.ConfirmEmailAsync(user, token);
                return user;
            }

                             


        private async Task AddCountriesAndCitiesAsync()
        {
            this.AddCountry("Colombia", new string[] { "Villavicencio", "Medellín", "Bogota", "Calí", "Barranquilla", "Bucaramanga", "Cartagena", "Pereira", "Tunja" });
            this.AddCountry("Argentina", new string[] { "Córdoba", "Buenos Aires", "Rosario", "Tandil", "Salta", "Mendoza" });
            this.AddCountry("Estados Unidos", new string[] { "New York", "Los Ángeles", "Chicago", "Washington", "San Francisco", "Miami", "Boston" });
            this.AddCountry("Ecuador", new string[] { "Quito", "Guayaquil", "Ambato", "Manta", "Loja", "Santo" });
            this.AddCountry("Peru", new string[] { "Lima", "Arequipa", "Cusco", "Trujillo", "Chiclayo", "Iquitos" });
            this.AddCountry("Chile", new string[] { "Santiago", "Valdivia", "Concepcion", "Puerto Montt", "Temucos", "La Sirena" });
            this.AddCountry("Uruguay", new string[] { "Montevideo", "Punta del Este", "Colonia del Sacramento", "Las Piedras" });
            this.AddCountry("Bolivia", new string[] { "La Paz", "Sucre", "Potosi", "Cochabamba" });
            this.AddCountry("Venezuela", new string[] { "Caracas", "Valencia", "Maracaibo", "Ciudad Bolivar", "Maracay", "Barquisimeto" });
            this.AddCountry("Paraguay", new string[] { "Asunción", "Ciudad del Este", "Encarnación", "San  Lorenzo", "Luque", "Areguá" });
            this.AddCountry("Brasil", new string[] { "Rio de Janeiro", "São Paulo", "Salvador", "Porto Alegre", "Curitiba", "Recife", "Belo Horizonte", "Fortaleza" });
            await this.context.SaveChangesAsync();
        }

        private void AddCountry(string country, string[] cities)
        {
            var theCities = cities.Select(c => new City { Name = c }).ToList();
            this.context.Countries.Add(new Country
            {
                Cities = theCities,
                Name = country
            });
        }

        private async Task CheckRoles()
        {
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");
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
