namespace Control.Web.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGenericRepository<T> where T : class //es una repositorio completamente generico para usar diferentes tablas u objetos
    {
        IQueryable<T> GetAll();//devuelve una lista

        Task<T> GetByIdAsync(int id);//devuelve un item de la tabla

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(int id);
    }

}
