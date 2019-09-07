using Control.UIForms.Interfaces;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(Control.UIForms.iOS.Implementations.Config))]//esta linea sirve para compatibilidad entre las plataformas


namespace Control.UIForms.iOS.Implementations
{

    public class Config : IConfig
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "Passangers.db";
            string personalFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(personalFolder, "..", "Library");
            var path = Path.Combine(libraryFolder, dbName);
            return new SQLiteConnection(path);
        }

       
    }


    //*************************ORIGINAL CON SQlite.Net-PCL***********************************
    //using SQLite.Net.Interop;
    //class Config : IConfig //clase que permite llegar al directorio local con sqlite
    //{
    //    private string directorioDB;
    //    private ISQLitePlatform plataforma;

    //    public string DirectorioDB //este codigo muestra como se optiene el directorio
    //    {

    //        get
    //        {
    //            if (string.IsNullOrEmpty(directorioDB))
    //            {
    //                var directorio = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
    //                directorioDB = System.IO.Path.Combine(directorio, "..", "Library");

    //            }
    //            return directorioDB;
    //        }

    //    }

    //    public ISQLitePlatform Plataforma //este codigo optiene la plataforma en iOS
    //    {
    //        get
    //        {
    //            if (plataforma == null)
    //            {
    //                plataforma = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();

    //            }
    //            return plataforma;
    //        }
    //    }
    //}
}