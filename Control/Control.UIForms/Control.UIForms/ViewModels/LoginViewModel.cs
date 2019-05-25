namespace Control.UIForms.ViewModels
{
    using System;
    using System.Windows.Input;
    using Control.UIForms.Views;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class LoginViewModel
    {
        public string Usuario { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(Login);//codigo para la accion del boton
                                                                
        //codigo Quemado**                                                      
        public LoginViewModel()
        {
            this.Usuario = "1234";
            this.Password = "1234";
        }

        private async void Login()
        {
            if(string.IsNullOrEmpty(this.Usuario)) //condicion cuando el no ingresa usuario
            {
                await Application.Current.MainPage.DisplayAlert("Error","You must Enter an User","Accept");//imprime mensaje de error
                return;
            }
            if (string.IsNullOrEmpty(this.Password))//condicion cuando no ingresa el password
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You must Enter a Passwor", "Accept");//imprime mensaje de error
                return;
            }

            if(!this.Usuario.Equals("1234") || !this.Password.Equals("1234"))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "User and Password Worng", "Accept"); //Imprime mensaje de error
                return;
            }

            //await Application.Current.MainPage.DisplayAlert("Ok", "Wellcome", "Accept");//imprime mensaje de Bienvenida

            //este codigo permite que despues de validar el usuario y la clave se muestre la pagina de pasajeros.
            MainViewModel.GetInstance().Passangers = new PassangersViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new PassangersPage());

        }

    }
}
