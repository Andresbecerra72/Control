namespace Control.UIForms.Views
{
    using Control.Common.Models;
    using Control.Common.Services;
    using Control.UIForms.Helpers;
    using Control.UIForms.Helpers.LocalStore;
    using Control.UIForms.ViewModels;
    using Plugin.Connectivity;
    using System;
    using System.IO;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditLocalPassangerPage : ContentPage
    {
        private PassangerLocal passangerLocal;

        private readonly ApiService apiService;



        public int imageFlag = 0;



        public EditLocalPassangerPage(PassangerLocal passangerLocal)
        {
            InitializeComponent();

            this.apiService = new ApiService();
            this.passangerLocal = passangerLocal;



            Flight.Text = passangerLocal.Flight;
            Adult.Text = passangerLocal.Adult.ToString();
            Child.Text = passangerLocal.Child.ToString();
            Infant.Text = passangerLocal.Infant.ToString();
            Total.Text = passangerLocal.Total.ToString();
            PublishOn.Date = passangerLocal.PublishOn;
            Remark.Text = passangerLocal.Remark;
            ImageStore.Source = ImageSource.FromStream(() => new MemoryStream(passangerLocal.ImageArray));// convierte el Array en imagen

            // ImageStore.Source = MainViewModel.GetInstance().InsertPassanger.ImageSource;


            BtnDelete.Clicked += BtnDelete_Clicked;//codigo para el boton


        }


        //codigo del boton delete
        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            //********************CHECK CONNECTION************************
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert(Languages.Alert, Languages.ReportNoSent, Languages.Close);//"The report was NOT sent"
                await DisplayAlert(Languages.Error, Languages.InternetConn, Languages.Accept);
                return;
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                await DisplayAlert(Languages.Alert, Languages.ReportNoSent, Languages.Close);//"The report was NOT sent"
                await DisplayAlert(Languages.Error, Languages.InternetConn, Languages.Accept);
                return;
            }

            //*************************
            if (string.IsNullOrEmpty(Flight.Text))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.FlightEnter, Languages.Accept);//"Error", "You must enter a Flight Number.", "Accept"
                Flight.Focus();
                return;
            }

            if (string.IsNullOrEmpty(Adult.Text))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.AdultEnter, Languages.Accept);//"Error", "You must enter an Adults Total.", "Accept"
                Adult.Focus();
                return;
            }


            if (string.IsNullOrEmpty(Child.Text))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.ChildEnter, Languages.Accept);//"Error", "You must enter a Children Total.", "Accept"
                Child.Focus();
                return;
            }



            if (string.IsNullOrEmpty(Infant.Text))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.InfantEnter, Languages.Accept);//"Error", "You must enter an Infants Total.", "Accept"
                Infant.Focus();
                return;
            }



            if (string.IsNullOrEmpty(Total.Text))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.TotalEnter, Languages.Accept);//"Error", "You must enter a Total Passangers.", "Accept"
                Total.Focus();
                return;
            }





           

            Indicator.IsRunning = true;




            //codigo que construye el modelo para el api
            var passanger = new Passanger
            {
                Flight = Flight.Text,
                Adult = int.Parse(Adult.Text),
                Child = int.Parse(Child.Text),
                Infant = int.Parse(Infant.Text),
                Total = int.Parse(Total.Text),
                PublishOn = PublishOn.Date,
                Remark = Remark.Text,
                Day = PublishOn.Date.ToString("dd"),
                Month = PublishOn.Date.ToString("MMMM"),
                Year = PublishOn.Date.ToString("yyyy"),
                User = new User { UserName = MainViewModel.GetInstance().UserEmail },
                ImageArray = passangerLocal.ImageArray

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
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            //codigo para eliminar registro de Sqlite
            using (var datos = new DataAcces()) //abre la conexion con la base de datos
            {
                datos.DeletePassangerSqlite(passangerLocal);
            };

            var newPassanger = (Passanger)response.Result;
            MainViewModel.GetInstance().Passangers.AddProductToList(newPassanger);



            await App.Navigator.PopAsync();





            

        }
    }
}