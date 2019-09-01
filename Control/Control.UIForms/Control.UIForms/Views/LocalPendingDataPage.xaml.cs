namespace Control.UIForms.Views
{
    using Control.UIForms.Helpers;
    using Control.UIForms.Helpers.LocalStore;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocalPendingDataPage : ContentPage
    {


        public LocalPendingDataPage()
        {
            InitializeComponent();

            DatosListView.ItemTemplate = new DataTemplate(typeof(LocalListView)); //selecciona el formato personalizado del listview
            DatosListView.RowHeight = 50;

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

    }
}
