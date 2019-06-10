namespace Control.UIForms.Helpers
{
    using Interfaces;
    using Resources;
    using Xamarin.Forms;

    public static class Languages
    {
        static Languages()//este metodo busca el ILocalize que verifica el sistema operativo si es Android o IOS para llamar el lenguaje
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }
        //mensajes emergentes
        public static string Accept => Resource.Accept;
        public static string Error => Resource.Error;
        public static string PasswordError => Resource.PasswordError;
        public static string EmailPasswordError => Resource.EmailPasswordError;
        public static string AdultEnter => Resource.AdultEnter;
        public static string ChildEnter => Resource.ChildEnter;
        public static string EmailError => Resource.EmailError;
        public static string FlightEnter => Resource.FlightEnter;
        public static string InfantEnter => Resource.InfantEnter;
        public static string TotalEnter => Resource.TotalEnter;

        //XAML lables, titulos y placeholders 
        public static string LoginTitle => Resource.LoginTitle;
        public static string AddTitle => Resource.AddTitle;
        public static string EditTitle => Resource.EditTitle;
        public static string RegisterUserTitle => Resource.RegisterUserTitle;
        public static string PaxTitle => Resource.PaxTitle;
        public static string FlightEntry => Resource.FlightEntry;
        public static string AdultsEntry => Resource.AdultsEntry;
        public static string ChildrenEntry => Resource.ChildrenEntry;
        public static string InfantsEntry => Resource.InfantsEntry;
        public static string TotalEntry => Resource.TotalEntry;
        public static string Adults => Resource.Adults;
        public static string Children => Resource.Children;
        public static string Infants => Resource.Infants;
        public static string TotalPassangers => Resource.TotalPassangers;
        public static string Flight => Resource.Flight;
        public static string User => Resource.User;
        public static string Password => Resource.Password;
        public static string UserPlaceHolder => Resource.UserPlaceHolder;
        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;
        public static string Remember => Resource.Remember;
        public static string ImageTap => Resource.ImageTap;


        //botones y CRUD
        public static string Save => Resource.Save;
        public static string Edit => Resource.Edit;
        public static string Delete => Resource.Delete;
        public static string Create => Resource.Create;
        public static string RegisterNewUser => Resource.RegisterNewUser;
        public static string Login => Resource.Login;




    }


}
