namespace Control.UIForms.Helpers
{
    using Control.Common.Models;
    using Control.UIForms.Helpers.LocalStore;
    using Control.UIForms.Interfaces;
    using SQLite.Net;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Xamarin.Forms;

    class DataAcces : IDisposable //permite liberar el recurso Dispose
    {
        private SQLiteConnection connection;//crea la coneccion para la base de datos local Sqlite

        //constructor
        public DataAcces()
        {
            //codigo para crear la base de datos local
            var config = DependencyService.Get<IConfig>();//llama la configuracion de la plataform segun el dispositivo 
            connection = new SQLiteConnection(config.Plataforma, Path.Combine(config.DirectorioDB, "Passangers.db")); //llama los metodos Directorio y plataforma desde los sistemas de los equipos por Config
            connection.CreateTable<PassangerLocal>(); //crea la tabla en la base de datos "Passangers.db"

            // db = new SQLiteConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("database.db"));
            // db.CreateTable<Item>();
        }


        //metodos del CRUD
        public void InsertPassangerSqlite(PassangerLocal passanger)
        {
            connection.Insert(passanger);
        }

        public void UpdatePassangerSqlite(PassangerLocal passanger)
        {
            connection.Update(passanger);
        }

        public void DeletePassangerSqlite(PassangerLocal passanger)
        {
            connection.Delete(passanger);
        }

        public PassangerLocal GetPassangerSqlite(int id) //devuelve un registro
        {
            return connection.Table<PassangerLocal>().FirstOrDefault(c => c.Id == id);
        }

        public List<PassangerLocal> GetManyPassangerSqlite() //devuelve varios registros
        {
            return connection.Table<PassangerLocal>().OrderBy(p => p.PublishOn).ToList();
        }


        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
