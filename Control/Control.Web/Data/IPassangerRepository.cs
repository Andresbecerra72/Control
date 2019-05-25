namespace Control.Web.Data
{
    using Entities;
    using System.Linq;

    public interface IPassangerRepository : IGenericRepository<Passanger> //es una interface que hereda el CRUD del repositorio generico
    {
        IQueryable GetAllWithUsers();
    }

}
