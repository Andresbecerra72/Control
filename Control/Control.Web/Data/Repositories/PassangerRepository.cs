namespace Control.Web.Data
{
    using Entities;
    using Microsoft.EntityFrameworkCore;
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
            return this.context.Passangers.Include(p => p.User);
        }

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


    }

}
