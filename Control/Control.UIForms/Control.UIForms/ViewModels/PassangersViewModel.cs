namespace Control.UIForms.ViewModels
{
    using Control.Common.Models;
    using Control.Common.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class PassangersViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        private ObservableCollection<Passanger> passangers;
        private bool isRefreshing;

        //esta es la lista de productos que se van mostrar en la listview
        public ObservableCollection<Passanger> Passangers
        {
            get => this.passangers; 
            set => this.SetValue(ref this.passangers, value); 
        }

        public bool IsRefreshing //codigo para que refresque la lista de passajeros
        {
            get => this.isRefreshing; 
            set => this.SetValue(ref this.isRefreshing, value); 
        }

        public PassangersViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            
            var url = Application.Current.Resources["UrlAPI"].ToString();//este es el Urlbase que es la pagina donde esta el API, el dato de la url esta en el diccionario de recursos
            var response = await this.apiService.GetListAsync<Passanger>(
                url,
                "/api",//servicePrefix
                "/Passanger",//controller
                "bearer", //token
                MainViewModel.GetInstance().Token.Token);


            this.IsRefreshing = false;

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
