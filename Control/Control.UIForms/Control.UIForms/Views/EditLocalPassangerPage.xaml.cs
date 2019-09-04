namespace Control.UIForms.Views
{
    using System;
    using System.IO;
    using Control.UIForms.Helpers;
    using Control.UIForms.Helpers.LocalStore;
    using Control.UIForms.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditLocalPassangerPage : ContentPage
    {
        private PassangerLocal passangerLocal;
        

              

        public EditLocalPassangerPage(PassangerLocal passangerLocal)
        {
            InitializeComponent();

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


       



        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            //var rta = await DisplayAlert("Confirm", "Delete Item?", "Delete", "Cancel");

            //if (!rta)
            //{
            //    return;
            //}
            using (var datos = new DataAcces()) //abre la conexion con la base de datos
            {
                datos.DeletePassangerSqlite(passangerLocal);
            };

            //await DisplayAlert("Message", "Item Deleted", "Close");
            //await Navigation.PopToRootAsync();

        }
    }
}