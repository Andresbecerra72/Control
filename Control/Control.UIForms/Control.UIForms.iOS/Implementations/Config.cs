using System;
using Control.UIForms.Interfaces;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(Control.UIForms.iOS.Implementations.Config))]//esta linea sirve para compatibilidad entre las plataformas


namespace Control.UIForms.iOS.Implementations
{

    class Config : IConfig //clase que permite llegar al directorio local con sqlite
    {
        private string directorioDB;
        private ISQLitePlatform plataforma;

        public string DirectorioDB //este codigo muestra como se optiene el directorio
        {

            get
            {
                if (string.IsNullOrEmpty(directorioDB))
                {
                    var directorio = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    directorioDB = System.IO.Path.Combine(directorio, "..", "Library");

                }
                return directorioDB;
            }

        }

        public ISQLitePlatform Plataforma //este codigo optiene la plataforma en iOS
        {
            get
            {
                if (plataforma == null)
                {
                    plataforma = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();

                }
                return plataforma;
            }
        }
    }
}