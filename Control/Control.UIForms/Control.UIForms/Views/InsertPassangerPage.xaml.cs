namespace Control.UIForms.Views
{
    using Control.Common.Models;
    using Control.UIForms.Helpers;
    using Control.UIForms.Helpers.LocalStore;
    using Control.UIForms.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InsertPassangerPage : ContentPage
    {

        public InsertPassangerPage()
        {
            InitializeComponent();

            BtnSalvar.Clicked += BtnSalvar_Clicked;//codigo para el boton

            //using (var datos = new DataAcces())
            //{
            //    DatosListView.ItemsSource = datos.GetManyPassangerSqlite();
            //}


        }

        private void BtnSalvar_Clicked(object sender, System.EventArgs e)
        {
            //if (string.IsNullOrEmpty(Flight.Text))
            //{
            //    await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.FlightEnter, Languages.Accept);//"Error", "You must enter a Flight Number.", "Accept"
            //    return;
            //}


            PassangerLocal passangerLocal = new PassangerLocal
            {
                Flight = Flight.Text,
                Adult = int.Parse(Adult.Text),
                Child = int.Parse(Child.Text),
                Infant = int.Parse(Infant.Text),
                Total = int.Parse(Total.Text),
                PublishOn = PublishOn.Date,
                //ImageUrl = Image.ToString(),
                Remark = Remark.Text,
                Day = PublishOn.Date.ToString("dd"),
                Month = PublishOn.Date.ToString("MMMM"),
                Year = PublishOn.Date.ToString("yyyy"),
                User = new User { UserName = MainViewModel.GetInstance().UserEmail }



            };

            using (var datos = new DataAcces())
            {
               // datos.InsertPassangerSqlite(passangerLocal);
                DatosListView.ItemsSource = datos.GetManyPassangerSqlite();
            }

            //throw new System.NotImplementedException();

        }
    }
}