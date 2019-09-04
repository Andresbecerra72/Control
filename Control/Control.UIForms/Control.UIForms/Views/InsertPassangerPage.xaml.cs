namespace Control.UIForms.Views
{
    using System;
    using Control.Common.Models;
    using Control.UIForms.Helpers;
    using Control.UIForms.Helpers.LocalStore;
    using Control.UIForms.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InsertPassangerPage : ContentPage
    {
        //Variables
        private byte[] GetImage = null;



        //constructor
        public InsertPassangerPage()
        {
            InitializeComponent();



            DatosListView.ItemTemplate = new DataTemplate(typeof(LocalListView)); //selecciona el formato personalizado del listview
            DatosListView.RowHeight = 50;

            BtnSalvar.Clicked += BtnSalvar_Clicked;//codigo para el boton
            DatosListView.ItemSelected += DatosListView_ItemSelected; //codigo para la seleccion de datos del listview

        }

        //codigo para actualizar el listview 
        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (var datos = new DataAcces()) //abre la conexion con la base de datos
            {
                DatosListView.ItemsSource = datos.GetManyPassangerSqlite();//codigo para cargar los datos en el listview "DatosListView"
            }
        }

        //codigo cuando se selecciona un objeto desde el listview
        private async void DatosListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new EditLocalPassangerPage((PassangerLocal)e.SelectedItem));
        }


        //codigo cuando se pulsa el boton
        private async void BtnSalvar_Clicked(object sender, System.EventArgs e)
        {
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


            //codigo para cargar el array de la imagen desde InsertPasangerViewmodel
            GetImage = MainViewModel.GetInstance().InsertPassanger.imageArray;


            if (GetImage == null || GetImage.Length <= 0)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.ImageEnter, Languages.Accept);//"Error", "You must enter an Image.", "Accept"
                return;
            }



           


            PassangerLocal passangerLocal = new PassangerLocal
            {
                Flight = Flight.Text,
                Adult = int.Parse(Adult.Text),
                Child = int.Parse(Child.Text),
                Infant = int.Parse(Infant.Text),
                Total = int.Parse(Total.Text),
                PublishOn = PublishOn.Date,
                ImageArray = GetImage,
                Remark = Remark.Text,
                Day = PublishOn.Date.ToString("dd"),
                Month = PublishOn.Date.ToString("MMMM"),
                Year = PublishOn.Date.ToString("yyyy"),
               




            };

            using (var datos = new DataAcces()) //abre la conexion con la base de datos
            {
                datos.InsertPassangerSqlite(passangerLocal);
                DatosListView.ItemsSource = datos.GetManyPassangerSqlite();
            }

            Flight.Text = string.Empty;
            Adult.Text = string.Empty;
            Child.Text = string.Empty;
            Infant.Text = string.Empty;
            Total.Text = string.Empty;
            Remark.Text = string.Empty;
            PublishOn.Date = DateTime.Now;
            Image.Source = "no_image";
            //await DisplayAlert("Message", "Data Insert", "Done");


        }
    }
}