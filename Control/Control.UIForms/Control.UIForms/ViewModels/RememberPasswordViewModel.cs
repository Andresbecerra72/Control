namespace Control.UIForms.ViewModels
{
    using System.Windows.Input;
    using Common.Helpers;
    using Common.Services;
    using Control.Common.Models;
    using Control.UIForms.Helpers;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class RememberPasswordViewModel : BaseViewModel //Esta clase sirve para recordar el password cuando el usuario lo olvida
    {
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;

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

        public ICommand RecoverCommand => new RelayCommand(this.Recover);

        //Constructor
        public RememberPasswordViewModel()
        {
            this.apiService = new ApiService();//consume servicios del api
            this.IsEnabled = true;
        }

        private async void Recover() //este metodo valida los datos ingresados
        {
            //**********CHECK CONNECTION************
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                   Languages.Error,
                   connection.Message,
                   Languages.Accept);
                await App.Navigator.PopAsync();
                return;
            }
            //*****************************

            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailEnter,
                    Languages.Accept);
                return;
            }

            if (!RegexHelper.IsValidEmail(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                   Languages.Error,
                    Languages.EmailValidEnter,
                    Languages.Accept);
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var request = new RecoverPasswordRequest
            {
                Email = this.Email
            };

            //CONSULTA EL API
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.RecoverPasswordAsync(
                url,
                "/api",
                "/Account/RecoverPasswordApp",
                request);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            await Application.Current.MainPage.DisplayAlert(
                "Ok",
                response.Message,
                Languages.Accept);
            await Application.Current.MainPage.Navigation.PopAsync();

        }
    }

}
