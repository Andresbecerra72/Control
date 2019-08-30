namespace Control.UIForms.Interfaces
{
    using SQLite.Net.Interop;
    public interface IConfig   //interface usada para Sqlite
    {
        string DirectorioDB { get; }
        ISQLitePlatform Plataforma { get; }

    }
}
