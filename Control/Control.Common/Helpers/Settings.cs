﻿namespace Control.Common.Helpers
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    public static class Settings
    {
        //se definen las variables constantes que requiere la aplicacion
        private const string token = "token";// el token es un objeto
        private const string userEmail = "userEmail";
        private const string userPassword = "userPassword";
        private const string isRemember = "isRemember";
        private const string user = "user";//almacena el objeto user en persistencia para que sea recordado por la aplicacion
        private static readonly string stringDefault = string.Empty;
        private static readonly bool boolDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string User
        {
            get => AppSettings.GetValueOrDefault(user, stringDefault);
            set => AppSettings.AddOrUpdateValue(user, value);
        }


        public static string Token
        {
            get => AppSettings.GetValueOrDefault(token, stringDefault);
            set => AppSettings.AddOrUpdateValue(token, value);
        }

        public static string UserEmail
        {
            get => AppSettings.GetValueOrDefault(userEmail, stringDefault);
            set => AppSettings.AddOrUpdateValue(userEmail, value);
        }

        public static string UserPassword
        {
            get => AppSettings.GetValueOrDefault(userPassword, stringDefault);
            set => AppSettings.AddOrUpdateValue(userPassword, value);
        }

        public static bool IsRemember
        {
            get => AppSettings.GetValueOrDefault(isRemember, boolDefault);
            set => AppSettings.AddOrUpdateValue(isRemember, value);
        }
    }

}
