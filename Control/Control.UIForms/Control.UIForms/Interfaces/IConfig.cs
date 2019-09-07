namespace Control.UIForms.Interfaces
{


    public interface IConfig
    {
        SQLite.SQLiteConnection DbConnection();
    }

    //*************************ORIGINAL CON SQlite.Net-PCL***********************************
    //using SQLite.Net.Interop;
    //public interface IConfig   //interface usada para Sqlite, establece las propiedades para interfazar los dispositivos
    //{
    //    string DirectorioDB { get; }//como optener el directorio 
    //    ISQLitePlatform Plataforma { get; }//como optener la plataforma

    //}
}
