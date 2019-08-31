using Control.UIForms.Helpers.LocalStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Control.UIForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditLocalPassangerPage : ContentPage
    {
        public EditLocalPassangerPage(PassangerLocal passangerLocal)
        {
            InitializeComponent();

            Flight.Text = passangerLocal.Flight;
            Adult.Text = passangerLocal.Adult.ToString();
            Child.Text = passangerLocal.Child.ToString();
            Infant.Text = passangerLocal.Infant.ToString();
            Total.Text = passangerLocal.Total.ToString();
            PublishOn.Date = passangerLocal.PublishOn;
        }
    }
}