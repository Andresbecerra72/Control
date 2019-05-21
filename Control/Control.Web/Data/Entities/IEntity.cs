namespace Control.Web.Data.Entities
{
    public interface IEntity //obliga que todas las tablas (entities) tengan un atributo Id
    {
        int Id { get; set; }
    }

}
