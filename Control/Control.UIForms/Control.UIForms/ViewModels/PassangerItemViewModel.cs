namespace Control.UIForms.ViewModels
{
    using Control.Common.Models;
    using Control.UIForms.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    public class PassangerItemViewModel: Passanger //esta clase sirve como interfase cuando tocan un producto en la lista de vuelos para abrir otra ventana 
    {
        public ICommand SelectPassangerCommand => new RelayCommand(this.SelectPassanger);

        private async void SelectPassanger()
        {
            MainViewModel.GetInstance().EditPassanger = new EditPassangerViewModel((Passanger)this);
            await App.Navigator.PushAsync(new EditPassangerPage());
        }

    }
}
