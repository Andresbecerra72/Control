namespace Control.Web.Data
{
    using Entities;

    //esta clase establece el repositorio solo para la tabla pasajeros
    public class PassangerRepository : GenericRepository<Passanger>, IPassangerRepository
    {
        public PassangerRepository (DataContext context) : base(context)//inyecta la coneccion con base de datos
        {
        }
    }

}
