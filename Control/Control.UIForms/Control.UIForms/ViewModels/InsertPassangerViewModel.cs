﻿namespace Control.UIForms.ViewModels
{
    using System;
    using System.Windows.Input;
    using Common.Models;
    using Common.Services;
    using Control.Common.Helpers;
    using Control.UIForms.Helpers;
    using Control.UIForms.Views;
    using GalaSoft.MvvmLight.Command;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Xamarin.Forms;

    public class InsertPassangerViewModel : BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;
        private ImageSource imageSource;
        private MediaFile file; //Para la captura de fotos

        public ImageSource ImageSource
        {
            get => this.imageSource;
            set => this.SetValue(ref this.imageSource, value);
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

        public string Flight { get; set; }

        public string Adult { get; set; }

        public string Child { get; set; }

        public string Infant { get; set; }

        public string Total { get; set; }

        public DateTime PublishOn { get; set; }

        public string Remark { get; set; }

        public string Day { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }

        public int ImageFlag = 0;

        //acciones por comandos 
        public ICommand ChangeImageCommand => new RelayCommand(this.ChangeImage);
                
        public ICommand SaveCommand => new RelayCommand(this.Save);

       

       
        //contructor
        public InsertPassangerViewModel()
        {
            this.apiService = new ApiService();// se instancian los servicios del API, para crear un nuevo registro de pasajeros
            this.ImageSource = "no_image";
            this.PublishOn = DateTime.Now;
            this.IsEnabled = true;
            this.Flight = string.Empty;
            this.Adult = string.Empty;
            this.Remark = string.Empty;
            this.Day = this.PublishOn.ToString("dd");
            this.Month = this.PublishOn.ToString("MMMM");
            this.Year = this.PublishOn.ToString("yyyy");


        }

        //METODOS:


        //metodo para salvar el registro de pasajeros
        private async void Save()
        {
           
            if (string.IsNullOrEmpty(this.Flight))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.FlightEnter, Languages.Accept);//"Error", "You must enter a Flight Number.", "Accept"
                return;
            }

            if (string.IsNullOrEmpty(this.Adult))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.AdultEnter, Languages.Accept);//"Error", "You must enter an Adults Total.", "Accept"
                return;
            }

            var adult = int.Parse(this.Adult);

            if (string.IsNullOrEmpty(this.Child))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.ChildEnter, Languages.Accept);//"Error", "You must enter a Children Total.", "Accept"
                return;
            }

            var child = int.Parse(this.Child);

            if (string.IsNullOrEmpty(this.Infant))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.InfantEnter, Languages.Accept);//"Error", "You must enter an Infants Total.", "Accept"
                return;
            }

            var infant = int.Parse(this.Infant);

            if (string.IsNullOrEmpty(this.Total))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.TotalEnter, Languages.Accept);//"Error", "You must enter a Total Passangers.", "Accept"
                return;
            }
                                  
            var total = int.Parse(this.Total);

            //if (ImageFlag == 0)
            //{
            //    await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.ImageEnter, Languages.Accept);//"Error", "You must enter an Image.", "Accept"
            //    return;
            //}


            this.IsRunning = true;
            this.IsEnabled = false;

            //codigo para armar el image array de la foto tomada por el movil
            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
                
            }

              //codigo que construye el modelo para el api
            var passanger = new Passanger
            {
                Flight = this.Flight,
                Adult = adult,
                Child = child,
                Infant = infant,
                Total = total,
                PublishOn = this.PublishOn,
                Remark = this.Remark,
                Day = this.PublishOn.ToString("dd"),
                Month = this.PublishOn.ToString("MMMM"),
                Year = this.PublishOn.ToString("yyyy"),
                User = new User { UserName = MainViewModel.GetInstance().UserEmail },
                ImageArray = imageArray

            };



            //se ejecuta el POST para crear el registro en BD
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PostAsync(
                url,
                "/api",
                "/Passanger",
                passanger,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var newPassanger = (Passanger)response.Result;
            MainViewModel.GetInstance().Passangers.AddProductToList(newPassanger);

            using (var datos = new DataAcces())
            {
               // datos.InsertPassangerSqlite(passanger);
                //DatosListView.ItemsSource = datos.GetManyPassangerSqlite();
            }


            this.IsRunning = false;
            this.IsEnabled = true;
                      
            await App.Navigator.PopAsync();
            //MainViewModel.GetInstance().Passangers = new PassangersViewModel();//todo: cambio
            //await App.Navigator.PushAsync(new PassangersPage());//todo: cambio
           
        }


        
        //metodo para cambiar la imagen 
        private async void ChangeImage()
        {
            ImageFlag = 1;
            await CrossMedia.Current.Initialize();

            //se muesta un mensaje con las opciones para la captura de la imagen
            var source = await Application.Current.MainPage.DisplayActionSheet(
                "Where do you take the picture?",
                "Cancel",
                null,
                "From Gallery",
                "From Camera");

            if (source == "Cancel")
            {
                this.file = null;
                return;
            }

            if (source == "From Camera")
            {
                //toma la foto desde la camara del dispositivo
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Pictures",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                //toma la imagen desde la galeria
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.file != null)
            {
                //si el usuario captura la imagen se almacena
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }

    }

}
