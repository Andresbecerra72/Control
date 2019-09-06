﻿namespace Control.UIForms.Helpers
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
        public static string Message => Resource.Message;
        public static string Confirm => Resource.Confirm;
        public static string Yes => Resource.Yes;
        public static string No => Resource.No;
        public static string Error => Resource.Error;
        public static string Cancel => Resource.Cancel;
        public static string Close => Resource.Close;
        public static string Alert => Resource.Alert;
        public static string ImageTake => Resource.ImageTake;
        public static string PasswordError => Resource.PasswordError;
        public static string EmailPasswordError => Resource.EmailPasswordError;
        public static string AdultEnter => Resource.AdultEnter;
        public static string ChildEnter => Resource.ChildEnter;
        public static string EmailError => Resource.EmailError;
        public static string FlightEnter => Resource.FlightEnter;
        public static string InfantEnter => Resource.InfantEnter;
        public static string TotalEnter => Resource.TotalEnter;
        public static string ImageEnter => Resource.ImageEnter;
        public static string PublishOnEnter => Resource.PublisOnEnter;
        public static string DeleteReport => Resource.DeleteReport;
        public static string PasswordCurrent => Resource.PasswordCurrent;
        public static string PasswordIncorrect => Resource.PasswordIncorrect;
        public static string PasswordNew => Resource.PasswordNew;
        public static string PasswordChart => Resource.PasswordChart;
        public static string PasswordConfirm => Resource.PasswordConfirm;
        public static string PasswordMatch => Resource.PasswordMatch;
        public static string UserUpdated => Resource.UserUpdated;
        public static string FirstNameEnter => Resource.FirstNameEnter;
        public static string LastNameEnter => Resource.LastNameEnter;
        public static string AddressEnter => Resource.AddressEnter;
        public static string CityEnter => Resource.CityEnter;
        public static string CountryEnter => Resource.CountryEnter;
        public static string PhoneEnter => Resource.PhoneEnter;
        public static string EmailEnter => Resource.EmailEnter;
        public static string EmailValidEnter => Resource.EmailValidEnter;
        public static string ReportNoSent => Resource.ReportNoSent;
        public static string ReportStored => Resource.ReportStored;
        public static string InternetConn => Resource.InternetConn;





        //XAML lables, titulos y placeholders 
        public static string LoginTitle => Resource.LoginTitle;
        public static string AddTitle => Resource.AddTitle;
        public static string EditTitle => Resource.EditTitle;
        public static string RegisterUserTitle => Resource.RegisterUserTitle;
        public static string PaxTitle => Resource.PaxTitle;
        public static string RememberPasswordTitle => Resource.RememberPasswordTitle;
        public static string AboutTitle => Resource.AboutTitle;
        public static string SetupTitle => Resource.SetupTitle;
        public static string ModifyUserTitle => Resource.ModifyUserTitle;
        public static string MenuTitle => Resource.MenuTitle;
        public static string ChangePasswordTitle => Resource.ChangePasswordTitle;
        public static string LocalPageTitle => Resource.LocalPageTitle;
        public static string CloseSession => Resource.CloseSession;
        public static string LocalStorePageTitle => Resource.LocalStorePageTitle;



        //Entradas
        public static string FlightEntry => Resource.FlightEntry;
        public static string AdultsEntry => Resource.AdultsEntry;
        public static string ChildrenEntry => Resource.ChildrenEntry;
        public static string InfantsEntry => Resource.InfantsEntry;
        public static string TotalEntry => Resource.TotalEntry;
        public static string RemarkEntry => Resource.RemarkEntry;
        public static string Adults => Resource.Adults;
        public static string Children => Resource.Children;
        public static string Infants => Resource.Infants;
        public static string TotalPassangers => Resource.TotalPassangers;
        public static string Remark => Resource.Remark;
        public static string Flight => Resource.Flight;
        public static string User => Resource.User;
        public static string Password => Resource.Password;
        public static string UserPlaceHolder => Resource.UserPlaceHolder;
        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;
        public static string Remember => Resource.Remember;
        public static string ImageTap => Resource.ImageTap;
        public static string Welcome => Resource.Welcome;


        //botones y CRUD
        public static string Save => Resource.Save;
        public static string Edit => Resource.Edit;
        public static string Delete => Resource.Delete;
        public static string Create => Resource.Create;
        public static string RegisterNewUser => Resource.RegisterNewUser;
        public static string Login => Resource.Login;
        public static string ForgotPassword => Resource.ForgotPassword;
        public static string BtnModifyPassword => Resource.BtnModifyPassword;
        public static string BtnRecoverPassword => Resource.BtnRecoverPassword;
        public static string BtnRegisterNewUser => Resource.BtnRegisterNewUser;
        public static string BtnUpdatePassword => Resource.BtnUpdatePassword;
        public static string BtnSaveServer => Resource.BtnSaveServer;
        public static string BtnDevice => Resource.BtnDevice;

    }


}
