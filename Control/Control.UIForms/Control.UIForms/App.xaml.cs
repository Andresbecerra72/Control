namespace Control.UIForms
{
    using Control.UIForms.ViewModels;
    using Control.UIForms.Views;
    using Xamarin.Forms;
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; } //nueva navegacion 
        public static MasterPage Master { get; internal set; }

        public App()
        {
            InitializeComponent();
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
