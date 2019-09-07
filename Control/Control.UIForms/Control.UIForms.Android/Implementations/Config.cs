using Control.UIForms.Interfaces;
using SQLite;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(Control.UIForms.Droid.Implementations.Config))]//esta linea sirve para compatibilidad entre las plataformas


namespace Control.UIForms.Droid.Implementations
{

    public class Config : IConfig
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "Passangers.db";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            return new SQLiteConnection(path);
        }
    }



    //*************************ORIGINAL CON SQlite.Net-PCL***********************************
    //using SQLite.Net.Interop;
    //class Config : IConfig //clase que permite llegar al directorio local con sqlite en Android
    //{

    //    private string directorioDB;
    //    private ISQLitePlatform plataforma;

    //    public string DirectorioDB //codigo para obtener el directorio
    //    {
    //        get
    //        {
    //            if (string.IsNullOrEmpty(directorioDB))
    //            {
    //                directorioDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
    //            }

    //            return directorioDB;
    //        }
    //    }

    //    public ISQLitePlatform Plataforma //codigo para obtener la plataforma
    //    {
    //        get
    //        {
    //            if (plataforma == null)
    //            {
    //                plataforma = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
    //            }
    //            return plataforma;
    //        }
    //    }
    //}
}