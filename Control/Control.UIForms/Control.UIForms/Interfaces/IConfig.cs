namespace Control.UIForms.Interfaces
{
    using SQLite.Net.Interop;
    public interface IConfig   //interface usada para Sqlite, establece las propiedades para interfazar los dispositivos
    {
        string DirectorioDB { get; }//como optener el directorio 
        ISQLitePlatform Plataforma { get; }//como optener la plataforma

    }
}
