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
            return this.context.Passangers.Include(p => p.User).OrderBy(c => c.PublishOn);//hace la relacion de los registros pasajeros con el usuario
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

        //TODO: NEW CHANGE
        //este metodo trae la lista con el usuario registrado
        public IQueryable GetAllWithUsersAuthenticated(string user)
        {

            var result = this.context.Passangers
               .Include(p => p.User)
               .Where(r => r.User.UserName == user)
               .OrderBy(c => c.PublishOn);

            return result;
        }
    }

}
