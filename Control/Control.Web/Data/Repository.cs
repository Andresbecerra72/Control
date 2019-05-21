namespace Control.Web.Data
   
{
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;

    public class Repository : IRepository
    {
        private readonly DataContext context;//inyecta la conexion con base de datos
        public Repository(DataContext context)
        {
            this.context = context;
        }
        //el siguiente codigo permite realizar el CRUD
        public IEnumerable<Passanger> GetProducts()
        {
            return this.context.Passangers.OrderBy(f => f.PublishOn);//devuelve una lista de ordenada por fechas
        }

        public Passanger GetProduct(int id)
        {
            return this.context.Passangers.Find(id);
        }

        public void AddProduct(Passanger product)
        {
            this.context.Passangers.Add(product);
        }

        public void UpdateProduct(Passanger product)
        {
            this.context.Passangers.Update(product);
        }

        public void RemoveProduct(Passanger product)
        {
            this.context.Passangers.Remove(product);
        }

        public async Task<bool> SaveAllAsync()//graba los cambios en la base de datos
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        public bool ProductExists(int id)
        {
            return this.context.Passangers.Any(p => p.PassangerId == id);//para que devuelva verdadero si exite el pasajero referenciado con el id
        }


    }
}
