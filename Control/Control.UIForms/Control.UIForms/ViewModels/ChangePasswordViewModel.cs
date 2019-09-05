namespace Control.UIForms.ViewModels
{
    using Common.Models;
    using Common.Services;
    using Control.Common.Helpers;
    using Control.UIForms.Helpers;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ChangePasswordViewModel : BaseViewModel //permite el cambio del passwor por app movil
    {
        private readonly ApiService apiService;
        private bool isRunning;
        private bool isEnabled;

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string PasswordConfirm { get; set; }

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

        public ICommand ChangePasswordCommand => new RelayCommand(this.ChangePassword);

        //Contructor
        public ChangePasswordViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
        }

        private async void ChangePassword()
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
            if (string.IsNullOrEmpty(this.CurrentPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordCurrent,
                    Languages.Accept);
                return;
            }

            if (!MainViewModel.GetInstance().UserPassword.Equals(this.CurrentPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordIncorrect,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.NewPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                   Languages.PasswordNew,
                    Languages.Accept);
                return;
            }

            if (this.NewPassword.Length < 6)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordChart,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.PasswordConfirm))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordConfirm,
                    Languages.Accept);
                return;
            }

            if (!this.NewPassword.Equals(this.PasswordConfirm))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordMatch,
                    Languages.Accept);
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var request = new ChangePasswordRequest
            {
                Email = MainViewModel.GetInstance().UserEmail,
                NewPassword = this.NewPassword,
                OldPassword = this.CurrentPassword
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.ChangePasswordAsync(
                url,
                "/api",
                "/Account/ChangePassword",
                request,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

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

            MainViewModel.GetInstance().UserPassword = this.NewPassword;
            Settings.UserPassword = this.NewPassword;

            await Application.Current.MainPage.DisplayAlert(
                "Ok",
                response.Message,
                Languages.Accept);

            await App.Navigator.PopAsync();
        }
    }

}
