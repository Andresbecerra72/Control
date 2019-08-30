namespace Control.UIForms.Helpers
{
    using System;
    using Xamarin.Forms;
    using SQLite.Net;
    using Control.UIForms.Interfaces;
    using System.IO;
    using Control.UIForms.Helpers.LocalStore;
    using System.Linq;
    using System.Collections.Generic;
    using Control.Common.Models;

    class DataAcces : IDisposable
    {
        private SQLiteConnection connection;//crea la coneccion para la base de datos local Sqlite

        //constructor
        public DataAcces()
        {
            //codigo para crear la base de datos local
            var config = DependencyService.Get<IConfig>();//llama la configuracion de la plataform segun el dispositivo 
            connection = new SQLiteConnection(config.Plataforma, Path.Combine(config.DirectorioDB, "Passanger.db"));
            connection.CreateTable<Passanger>();

           // db = new SQLiteConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("database.db"));
           // db.CreateTable<Item>();
        }


        public void InsertPassangerSqlite(Passanger passanger)
        {
            connection.Insert(passanger);
        }

        public void UpdatePassangerSqlite(Passanger passanger)
        {
            connection.Update(passanger);
        }

        public void DeletePassangerSqlite(Passanger passanger)
        {
            connection.Delete(passanger);
        }

        public Passanger GetPassangerSqlite(int id)
        {
            return connection.Table<Passanger>().FirstOrDefault(p => p.Id == id);
        }

        public List<Passanger> GetManyPassangerSqlite()
        {
            return connection.Table<Passanger>().OrderBy(p => p.PublishOn).ToList();
        }


        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
