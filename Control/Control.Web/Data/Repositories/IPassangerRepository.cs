namespace Control.Web.Data
{
    using Entities;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IPassangerRepository : IGenericRepository<Passanger> //es una interface que hereda el CRUD del repositorio generico
    {
        IQueryable GetAllWithUsers();

        Task DeleteItemAsync(int id);

    }

}
