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

            //adiciona ciudades y pais colombia
            if (!this.context.Countries.Any())
            {
                await this.AddCountriesAndCitiesAsync();
            }


            await this.CheckUser("sandra.roncancio@satena.com", "Sandra", "Roncancio", "Customer");
            await this.CheckUser("charlene.esguerra@satena.com", "Charlene", "Esguerra", "Customer");
            await this.CheckUser("enye.salazar@satena.com", "Enye", "Salazar", "Customer");
            await this.CheckUser("maria.riascos@satena.com", "Coni", "Riascos", "Customer");
            await this.CheckUser("lina.ramirez@satena.com", "Linita :)", "Ramirez", "Customer");
            await this.CheckUser("liliana.nieto@satena.com", "Liliana", "Nieto", "Customer");
            await this.CheckUser("diana.garcia@satena.com", "Diana", "Garcia", "Customer");
            await this.CheckUser("martha.amaya@satena.com", "Martha", "Amaya", "Customer");
            await this.CheckUser("eloisa.cardenas@satena.com", "Eloisa", "Cardenas", "Customer");
            await this.CheckUser("luz.ramirez@satena.com", "Luz", "Ramirez", "Customer");
            await this.CheckUser("maria.galeano@satena.com", "Maria", "Galeano", "Customer");
            await this.CheckUser("ruth.forero@satena.com", "Ruth", "Forero", "Customer");
            await this.CheckUser("laura.castaneda@satena.com", "Laurita", "Castañeda", "Customer");
            await this.CheckUser("elida.paz@satena.com", "Elida", "Paz", "Customer");
            await this.CheckUser("marcela.castrillon@satena.com", "Marcela", "Castrillon", "Customer");
            await this.CheckUser("quena.bedoya@satena.com", "Quena", "Bedoya", "Customer");
            await this.CheckUser("maria.garcia@satena.com", "Maria", "Garcia", "Customer");
            await this.CheckUser("margareth.tejada@satena.com", "Margareth", "Tejada", "Customer");
            await this.CheckUser("ana.gallego@satena.com", "Ana", "Gallego", "Customer");
            await this.CheckUser("norma.grijalba@satena.com", "Norma", "Grijalba", "Customer");
            await this.CheckUser("maria.luisa@satena.com", "Maria", "Luisa", "Customer");
            await this.CheckUser("yuli.camero@satena.com", "Yuli", "Camero", "Customer");
            await this.CheckUser("kelly.padilla@satena.com", "Kelly", "Padilla", "Customer");
            await this.CheckUser("july.barbosa@satena.com", "July", "Barbosa", "Customer");
            await this.CheckUser("francisco.aguas@satena.com", "Francisco", "Aguas", "Customer");
            await this.CheckUser("lina.garcia@satena.com", "Lina", "Garcia", "Customer");
            await this.CheckUser("andrea.leguizamon@satena.com", "Andrea", "Leguizamon", "Customer");
            await this.CheckUser("amanda.angel@satena.com", "Amanda", "Angel", "Customer");
            await this.CheckUser("bryid.castro@satena.com", "Lore", "Castro", "Customer");
            await this.CheckUser("diana.carvajal@satena.com", "Diana", "Carvajal", "Customer");
            await this.CheckUser("janne.mosquera@satena.com", "Janne", "Mosquera", "Customer");
            await this.CheckUser("ana.perafan@satena.com", "Ana", "Perafan", "Customer");
            await this.CheckUser("antonio.calderon@satena.com", "Antonio", "Calderon", "Customer");
            await this.CheckUser("grace.pretelt@satena.com", "Grace", "Pretelt", "Customer");
            await this.CheckUser("gloria.saavedra@satena.com", "Gloria", "Saavedra", "Customer");
            await this.CheckUser("karen.elizabeth@satena.com", "Karen", "Elizabeth", "Customer");
            await this.CheckUser("maryori.gallego@satena.com", "Maryori", "Gallego", "Customer");
            await this.CheckUser("juan.yepes@satena.com", "Juan", "Yepes", "Customer");
            await this.CheckUser("claudia.tavera@satena.com", "Claudia", "Tavera", "Customer");
            await this.CheckUser("sandra.ortiz@satena.com", "Sandra", "Ortiz", "Customer");
            await this.CheckUser("maria.rodriguez@satena.com", "Maria", "Rodriguez", "Customer");
            await this.CheckUser("natalia.osorio@satena.com", "Natalia", "Osorio", "Customer");
            await this.CheckUser("laura.angelica@satena.com", "Laura", "Garcia", "Customer");            
            await this.CheckUser("test.test@satena.com", "Customer", "Test", "Customer");
            await this.CheckUser("administrador.kiu@satena.com", "Administrador", "KIU", "Admin");
            var user = await this.CheckUser("andres.becerra@satena.com", "Andres", "Becerra", "Super");

            // Add Passangers
            if (!this.context.Passangers.Any())
            {
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
                Address = "Actualizar",
                PhoneNumber = "00000000",
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
            this.AddCountry("Colombia", new string[] { "Bogota", "Medellín", "Calí", "Barranquilla", "Bucaramanga", "Cartagena", "Pereira", "Tunja", "Villavicencio", "Otro" });
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
            this.AddCountry(" Otro...", new string[] { "Otro..." });
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
            await this.userHelper.CheckRoleAsync("Super");
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
                Remark = "Discrepancias",
                User = user,
                Day = DateTime.Now.ToString("dd"),
                Month = DateTime.Now.ToString("MMMM"),
                Year = DateTime.Now.ToString("yyyy"),
               
        });

        }



    }
}
