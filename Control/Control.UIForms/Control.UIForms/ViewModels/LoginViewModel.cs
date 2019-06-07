namespace Control.UIForms.ViewModels
{
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

        public ICommand LoginCommand => new RelayCommand(this.Login);//codigo para la accion del boton

        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.IsRemember = true;
            
        }

        private async void Login()
        {
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

            this.IsRunning = true;
            this.IsEnabled = false;

            var request = new TokenRequest
            {
                Password = this.Password,
                Username = this.Email
            };

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
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.Passangers = new PassangersViewModel();
            mainViewModel.UserEmail = this.Email;
            mainViewModel.UserPassword = this.Password;
            //recuerda el usuario logueado
            Settings.IsRemember = this.IsRemember;
            Settings.UserEmail = this.Email;
            Settings.UserPassword = this.Password;
            Settings.Token = JsonConvert.SerializeObject(token);//se convierte el objeto token en string

            //await Application.Current.MainPage.Navigation.PushAsync(new PassangersPage()); //esto cambia las paginas
            Application.Current.MainPage = new MasterPage();//inicia con la master pagedespues de login valido
        }
    }

}
