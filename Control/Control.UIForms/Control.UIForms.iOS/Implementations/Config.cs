namespace Control.UIForms.iOS.Implementations
{

    using System;
    using Control.UIForms.Interfaces;
    using SQLite.Net.Interop;

    

    class Config : IConfig //clase que permite llegar al directorio local con sqlite
    {
        private string directorioDB;
        private ISQLitePlatform plataforma;
        public string DirectorioDB
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

        public ISQLitePlatform Plataforma
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