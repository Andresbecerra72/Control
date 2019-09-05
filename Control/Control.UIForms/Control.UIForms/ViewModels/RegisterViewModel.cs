namespace Control.UIForms.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Common.Models;
    using Control.Common.Helpers;
    using Control.Common.Services;
    using Control.UIForms.Helpers;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class RegisterViewModel : BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private ObservableCollection<Country> countries;
        private Country country;
        private ObservableCollection<City> cities;
        private City city;
        private readonly ApiService apiService;//atributo para consumir los servicios del API en la consulta de paises y ciudades

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string Confirm { get; set; }

        public Country Country
        {
            get => this.country;
            set
            {
                this.SetValue(ref this.country, value);
                this.Cities = new ObservableCollection<City>(this.Country.Cities.OrderBy(c => c.Name));
            }

        }

        public City City
        {
            get => this.city;
            set => this.SetValue(ref this.city, value);
        }

        public ObservableCollection<Country> Countries
        {
            get => this.countries;
            set => this.SetValue(ref this.countries, value);
        }

        public ObservableCollection<City> Cities
        {
            get => this.cities;
            set => this.SetValue(ref this.cities, value);
        }

        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public ICommand RegisterCommand => new RelayCommand(this.Register);

        //Constructor de la clase
        public RegisterViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.LoadCountries();

        }

        //metodo para cargar la lista de paises y ciudades
        private async void LoadCountries()
        {
           

            //**********CHECK CONNECTION************
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                   Languages.Error,
                   connection.Message,
                   Languages.Accept);
               // await App.Navigator.PopAsync();
                return;
            }
            //*****************************

            this.IsRunning = true;
            this.IsEnabled = false;

            //se consume el api para obtener los paises
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Country>(
                url,
                "/api",
                "/Countries");

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            var myCountries = (List<Country>)response.Result;
            this.Countries = new ObservableCollection<Country>(myCountries);
        }

        //este metodo valida toda la informacion del formulario Nuevo registro de usuario

        private async void Register()
        {
            //**********CHECK CONNECTION************
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                   Languages.Error,
                   connection.Message,
                   Languages.Accept);
               // await App.Navigator.PopAsync();
                return;
            }
            //*****************************

            if (string.IsNullOrEmpty(this.FirstName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.FirstNameEnter,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.LastName))
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                    Languages.LastNameEnter,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailEnter,
                    Languages.Accept);
                return;
            }

            if (!RegexHelper.IsValidEmail(this.Email)) //condicion que valida el formato del email
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidEnter,
                    Languages.Accept);
                return;
            }

            if (this.Country == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CountryEnter,
                    Languages.Accept);
                return;
            }

            if (this.City == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                   Languages.Error,
                    Languages.CityEnter,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Address))
            {
                await Application.Current.MainPage.DisplayAlert(
                   Languages.Error,
                    Languages.AddressEnter,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Phone))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PhoneEnter,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordError,
                    Languages.Accept);
                return;
            }

            if (this.Password.Length < 6) //se valida que el passsword sea de 6 caracteres
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordChart,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Confirm))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordConfirm,
                    Languages.Accept);
                return;
            }

            if (!this.Password.Equals(this.Confirm))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordMatch,
                    Languages.Accept);
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var request = new NewUserRequest //se arma el request 
            {
                Address = this.Address,
                CityId = this.City.Id,
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Password = this.Password,
                Phone = this.Phone
            };

            var url = Application.Current.Resources["UrlAPI"].ToString(); //se envia al api
            var response = await this.apiService.RegisterUserAsync(
                url,
                "/api",
                "/Account",
                request);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            await Application.Current.MainPage.DisplayAlert(
                "Ok",
                response.Message,
                Languages.Accept);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }

}
