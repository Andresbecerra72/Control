namespace Control.UIForms
{
    using Control.Common.Helpers;
    using Control.Common.Models;
    using Control.UIForms.ViewModels;
    using Control.UIForms.Views;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using System;
    using Xamarin.Forms;
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; } //nueva navegacion 
        public static MasterPage Master { get; internal set; }



        public App()
        {
            InitializeComponent();

           

            if (Settings.IsRemember)//condicion para verificar si el usuario esta recordado "logueado"
            {
                var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);//se trae el token de acceso
                var user = JsonConvert.DeserializeObject<User>(Settings.User);//se trae el usuario logueado
                if (token.Expiration > DateTime.Now)//esta condicion verifica la fecha de vencimiento del token
                {
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.Token = token;
                    mainViewModel.User = user;
                    mainViewModel.UserEmail = Settings.UserEmail;
                    mainViewModel.UserPassword = Settings.UserPassword;
                    mainViewModel.Passangers = new PassangersViewModel();//si el usuario esta recordado pasa directo a la pagina PassangerPage
                   // mainViewModel.InsertPassanger = new InsertPassangerViewModel();//todo: cambio
                    this.MainPage = new MasterPage();
                    return;
                }
            }


            MainViewModel.GetInstance().Login = new LoginViewModel();//se instancia primero el viewmodel antes que la pagina
            MainPage = new NavigationPage(new LoginPage());//navigationPage muestr el titulo de la pagina
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
