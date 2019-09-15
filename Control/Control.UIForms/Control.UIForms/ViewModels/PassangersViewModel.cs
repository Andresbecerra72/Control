namespace Control.UIForms.ViewModels
{
    using Control.Common.Models;
    using Control.Common.Services;
    using Control.UIForms.Views;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;
    using Control.UIForms.Helpers;

    public class PassangersViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        private List<Passanger> myPassangers; //es una lista del objeto Passanger
        private ObservableCollection<PassangerItemViewModel> passangers;
        private bool isRefreshing;
        private User user;


        //esta es la lista de productos que se van mostrar en la listview
        public ObservableCollection<PassangerItemViewModel> Passangers
        {
            get => this.passangers;
            set => this.SetValue(ref this.passangers, value);
        }

        public bool IsRefreshing //codigo para que refresque la lista de passajeros
        {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);
        }




        //Constructor
        public PassangersViewModel()
        {
            
            this.apiService = new ApiService();
            this.LoadProducts();
            //this.GoAddPageAsync();
        }

       
        //carga el listado de pasajeros que estan en la BD
        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            //**********CHECK CONNECTION************
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
                return;
            }
            //*****************************


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
                   Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            this.myPassangers = (List<Passanger>)response.Result;
            this.RefresProductsList();//**
           
        }

        public void AddProductToList(Passanger passanger)
        {
            this.myPassangers.Add(passanger);
            this.RefresProductsList();//**
        }

        public void UpdateProductInList(Passanger passanger)
        {
            var previousProduct = this.myPassangers.Where(p => p.Id == passanger.Id).FirstOrDefault();
            if (previousProduct != null)
            {
                this.myPassangers.Remove(previousProduct);
            }

            this.myPassangers.Add(passanger);
            this.RefresProductsList();//**
        }

        public void DeleteProductInList(int productId)
        {
            var previousProduct = this.myPassangers.Where(p => p.Id == productId).FirstOrDefault();
            if (previousProduct != null)
            {
                this.myPassangers.Remove(previousProduct);
            }

            this.RefresProductsList();//**
        }

        private void RefresProductsList()//este metodo arma nuevamente la Observable collection
        {
            this.Passangers = new ObservableCollection<PassangerItemViewModel>(myPassangers.Select(p => new PassangerItemViewModel
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                ImageFullPath = p.ImageFullPath,
                Flight = p.Flight,
                Adult = p.Adult,
                Child = p.Child,
                Infant = p.Infant,
                Total = p.Total,
                PublishOn = p.PublishOn,
                Remark = p.Remark,
                Day = p.PublishOn.ToString("dd"),
                Month = p.PublishOn.ToString("MMMM"),
                Year = p.PublishOn.ToString("yyyy"),
                User = p.User
                

            })
            .OrderByDescending(p => p.PublishOn)
            .ToList());
        }


    }
}
