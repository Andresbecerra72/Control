namespace Control.UIForms.ViewModels
{
    using System;
    using System.Windows.Input;
    using Common.Services;
    using Control.Common.Helpers;
    using Control.Common.Models;
    using Control.UIForms.Helpers;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using Views;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;
        public bool IsRemember { get; set; }

        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public string Email { get; set; }

        public string Password { get; set; }

        //Comandos por botones o gesture reconizer
        public ICommand RememberPasswordCommand => new RelayCommand(this.RememberPassword);

        private async void RememberPassword()
        {
            MainViewModel.GetInstance().RememberPassword = new RememberPasswordViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RememberPasswordPage());//muestra la pagina de Recobro del password

        }

        public ICommand RegisterCommand => new RelayCommand(this.Register);//codigo para la accion del boton registrar nuevo usuario

        private async void Register()
        {
            MainViewModel.GetInstance().Register = new RegisterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());//muestra la pagina de registro de usuario

        }

        public ICommand LoginCommand => new RelayCommand(this.Login);//codigo para la accion del boton login

        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.IsRemember = true;
            
        }

        //metodo del boton Login
        private async void Login()
        {
            //realiza las validaciones
            if (string.IsNullOrEmpty(this.Email))  //condicion cuando el no ingresa usuario
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailError, Languages.Accept); //"Error", "You must enter an email.", "Accept"
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordError, Languages.Accept);//"Error", "You must enter a password.", "Accept"
                return;
            }
            //corre la espera
            this.IsRunning = true;
            this.IsEnabled = false;
            //arma el token
            var request = new TokenRequest
            {
                Password = this.Password,
                Username = this.Email
            };
            //verifica el token
            var url = Application.Current.Resources["UrlAPI"].ToString();//aqui se consume el servicio del API
            var response = await this.apiService.GetTokenAsync(
                url,
                "/Account",
                "/CreateToken",
                request);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)//si no fue satisfactoria la consulta de usuario y password
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailPasswordError, Languages.Accept);//"Error", "Email or password incorrect.", "Accept"
                return;
            }

            //este codigo permite que despues de validar el usuario y la clave se muestre la pagina de pasajeros.
            var token = (TokenResponse)response.Result;
            //Trae los datos del usuario que se acaba de loguear desde el api
            var response2 = await this.apiService.GetUserByEmailAsync(
                        url,
                        "/api",
                        "/Account/GetUserByEmail",
                        this.Email,
                        "bearer",
                        token.Token);

            var user = (User)response2.Result;

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.User = user;//este es el usuario que se loguea y es consultado desde el api
            mainViewModel.Token = token;
            //mainViewModel.InsertPassanger = new InsertPassangerViewModel();//todo: cambio
            mainViewModel.Passangers = new PassangersViewModel();
            mainViewModel.UserEmail = this.Email;
            mainViewModel.UserPassword = this.Password;

            //recuerda el usuario logueado
            Settings.IsRemember = this.IsRemember;
            Settings.UserEmail = this.Email;
            Settings.UserPassword = this.Password;
            Settings.Token = JsonConvert.SerializeObject(token);//se convierte el objeto token en string
            Settings.User = JsonConvert.SerializeObject(user);//serializa el objeto User como un string en persistencia

            await Application.Current.MainPage.Navigation.PushAsync(new PassangersPage()); //esto cambia las paginas
            //await Application.Current.MainPage.Navigation.PushAsync(new InsertPassangerPage());//todo: cambio
            Application.Current.MainPage = new MasterPage();//inicia con la master pagedespues de login valido
        }
    }

}
