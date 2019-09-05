namespace Control.UIForms.ViewModels
{
    using Common.Models;
    using Common.Services;
    using Control.UIForms.Helpers;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EditPassangerViewModel : BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;


        public Passanger Passanger { get; set; }


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



        public ICommand SaveCommand => new RelayCommand(this.Save);

        public ICommand DeleteCommand => new RelayCommand(this.Delete);

        public EditPassangerViewModel(Passanger passanger)
        {
            this.Passanger = passanger;
            this.apiService = new ApiService();
            this.IsEnabled = true;
        }

        //PUT PASSANGER FROM API

        private async void Save()
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

            if (string.IsNullOrEmpty(this.Passanger.Flight))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.FlightEnter, Languages.Accept);//"Error", "You must enter a Flight number.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Passanger.Adult.ToString()))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.AdultEnter, Languages.Accept);//"Error", "You must enter a Adult number.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Passanger.Child.ToString()))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.ChildEnter, Languages.Accept);//"Error", "You must enter a Child number.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Passanger.Infant.ToString()))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.InfantEnter, Languages.Accept);//"Error", "You must enter a Infant number.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Passanger.Total.ToString()))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.TotalEnter, Languages.Accept);//"Error", "You must enter a Total number.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Passanger.PublishOn.ToString()))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.PublishOnEnter, Languages.Accept);//"Error", "You must enter a Date.", "Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PutAsync(
                url,
                "/api",
                "/Passanger",
                this.Passanger.Id,
                this.Passanger,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var modifiedPassanger = (Passanger)response.Result;
            MainViewModel.GetInstance().Passangers.UpdateProductInList(modifiedPassanger);
            await App.Navigator.PopAsync();
        }

        //DELETE PASSANGER FROM API

        private async void Delete()
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

            var confirm = await Application.Current.MainPage.DisplayAlert(Languages.Confirm, Languages.DeleteReport, Languages.Yes, Languages.No);
            if (!confirm)
            {
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.DeleteAsync(
                url,
                "/api",
                "/Passanger",
                this.Passanger.Id,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            MainViewModel.GetInstance().Passangers.DeleteProductInList(this.Passanger.Id);
            await App.Navigator.PopAsync();
        }




    }





}
