namespace Control.UIForms.ViewModels
{
    using System;
    using System.Windows.Input;
    using Common.Models;
    using Common.Services;
    using Control.UIForms.Helpers;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class InsertPassangerViewModel : BaseViewModel
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

        public string Image { get; set; }

        public string Flight { get; set; }

        public string Adult { get; set; }

        public string Child { get; set; }

        public string Infant { get; set; }

        public string Total { get; set; }

        public DateTime PublishOn { get; set; }

       
        
        


        public ICommand SaveCommand => new RelayCommand(this.Save);

        //METODOS:

        public InsertPassangerViewModel()//contructor
        {
            this.apiService = new ApiService();// se instancian los servicios del API, para crear un nuevo registro de pasajeros
            this.Image = "no_image";
            this.PublishOn = DateTime.Now;
            this.IsEnabled = true;
            
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Flight))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.FlightEnter, Languages.Accept);//"Error", "You must enter a Flight Number.", "Accept"
                return;
            }

            if (string.IsNullOrEmpty(this.Adult))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.AdultEnter, Languages.Accept);//"Error", "You must enter an Adults Total.", "Accept"
                return;
            }

            var adult = int.Parse(this.Adult);

            if (string.IsNullOrEmpty(this.Child))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.ChlidEnter, Languages.Accept);//"Error", "You must enter a Children Total.", "Accept"
                return;
            }

            var child = int.Parse(this.Child);

            if (string.IsNullOrEmpty(this.Infant))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.InfantEnter, Languages.Accept);//"Error", "You must enter an Infants Total.", "Accept"
                return;
            }

            var infant = int.Parse(this.Infant);

            if (string.IsNullOrEmpty(this.Total))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.TotalEnter, Languages.Accept);//"Error", "You must enter a Total Passangers.", "Accept"
                return;
            }
                                  
            var total = int.Parse(this.Total);


            this.IsRunning = true;
            this.IsEnabled = false;

            //TODO: Add image
            var passanger = new Passanger
            {
                Flight = this.Flight,
                Adult = adult,
                Child = child,
                Infant = infant,
                Total = total,
                PublishOn = this.PublishOn,
                User = new User { UserName = MainViewModel.GetInstance().UserEmail }
                
            };

            //se ejecuta el POST para crear el registro en BD
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PostAsync(
                url,
                "/api",
                "/Passanger",
                passanger,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("ErrorXX", response.Message, "Accept");
                return;
            }

            var newPassanger = (Passanger)response.Result;
            MainViewModel.GetInstance().Passangers.AddProductToList(newPassanger);

            this.IsRunning = false;
            this.IsEnabled = true;
            await App.Navigator.PopAsync();
        }
    }

}
