namespace Control.UIForms.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class MainViewModel
    {
        private static MainViewModel instance; //esto es un apuntador

        //en esta parte se referencias las viewmodels de las paginas creadas
        public LoginViewModel Login { get; set; }

        public PassangersViewModel Passangers { get; set; }

        public InsertPassangerViewModel InsertPassanger { get; set; }


        //el siguiente codigo sirve para referirce a la mainviewmodel desde cualquier pagina
        public MainViewModel()
        {
            instance = this;
        }

        public static MainViewModel GetInstance()
        {
            if(instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
    }
}
