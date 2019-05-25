namespace Control.UIForms.ViewModels
{
    using Control.Common.Models;
    using Control.Common.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class PassangersViewModel: BaseViewModel
    {
        private readonly ApiService apiService;
        private ObservableCollection<Passanger> passangers;
         //esta es la lista de productos que se van mostrar en la listview
    public ObservableCollection<Passanger> Passangers
    {
            get { return this.passangers; }
            set { this.SetValue(ref this.passangers, value); }
        }

        public PassangersViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiService.GetListAsync<Passanger>(
                "https://controlweb.azurewebsites.net",
                "/api",
                "/Passanger");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert( //mensaje de error cuando no envia la comunicacion
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            var myPassangers = (List<Passanger>)response.Result;
            this.Passangers = new ObservableCollection<Passanger>(myPassangers);
        }

    }
}
