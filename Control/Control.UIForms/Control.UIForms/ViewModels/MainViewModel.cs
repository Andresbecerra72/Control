namespace Control.UIForms.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Control.Common.Models;
    using Control.UIForms.Views;
    using GalaSoft.MvvmLight.Command;
    

    public class MainViewModel
    {
        private static MainViewModel instance; //esto es un apuntador

        public TokenResponse Token { get; set; }//almacena el token en memoria

        public string UserEmail { get; set; }//datos con los que el usuario se loguea

        public string UserPassword { get; set; }


        //en esta parte se referencias las viewmodels de las paginas creadas
        public LoginViewModel Login { get; set; }

        public PassangersViewModel Passangers { get; set; }

      
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public InsertPassangerViewModel InsertPassanger { get; set; }//se liga la pagina InsertPassangerViewModel

        public ICommand AddPassangerCommand => new RelayCommand(this.GoAddPassanger);

        public EditPassangerViewModel EditPassanger { get; set; }



        //el siguiente codigo sirve para referirce a la mainviewmodel desde cualquier pagina
        public MainViewModel()
        {
            instance = this;
            this.LoadMenus();
        }

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }

        //codigo para cargar el menu de la pagina menus con iconos
        private void LoadMenus()
        {
            var menus = new List<Menu>
                 {
                    new Menu
                    {
                        Icon = "ic_info",
                        PageName = "AboutPage",
                        Title = "About"
                    },

                    new Menu
                    {
                        Icon = "ic_settings",
                        PageName = "SetupPage",
                        Title = "Setup"
                    },

                    new Menu
                    {
                        Icon = "ic_exit",
                        PageName = "LoginPage",
                        Title = "Close session"
                    }
                 };

            this.Menus = new ObservableCollection<MenuItemViewModel>(menus.Select(m => new MenuItemViewModel
            {
                Icon = m.Icon,
                PageName = m.PageName,
                Title = m.Title
            }).ToList());
        }

        //direcciona la pagin para insertar los pasajeros
        private async void GoAddPassanger()
        {
            this.InsertPassanger = new InsertPassangerViewModel();
            await App.Navigator.PushAsync(new InsertPassangerPage());
        }


    }
}
