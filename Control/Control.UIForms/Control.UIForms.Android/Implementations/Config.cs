using Control.UIForms.Interfaces;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(Control.UIForms.Droid.Implementations.Config))]//esta linea sirve para compatibilidad entre las plataformas




namespace Control.UIForms.Droid.Implementations
{
    
    class Config : IConfig //clase que permite llegar al directorio local con sqlite en Android
    {

        private string directorioDB;
        private ISQLitePlatform plataforma;

        public string DirectorioDB //codigo para obtener el directorio
        {
            get
            {
                if (string.IsNullOrEmpty(directorioDB))
                {
                    directorioDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }

                return directorioDB;
            }
        }

        public ISQLitePlatform Plataforma //codigo para obtener la plataforma
        {
            get
            {
                if (plataforma == null)
                {
                    plataforma = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }
                return plataforma;
            }
        }
    }
}