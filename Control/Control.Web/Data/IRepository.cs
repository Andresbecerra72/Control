namespace Control.Web.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Control.Web.Data.Entities;
    public interface IRepository //este codigo es la interfase del repositorio Repository.cs
    {
        void AddProduct(Passanger product);

        Passanger GetProduct(int id);

        IEnumerable<Passanger> GetProducts();

        bool ProductExists(int id);

        void RemoveProduct(Passanger product);

        Task<bool> SaveAllAsync();

        void UpdateProduct(Passanger product);
    }
}