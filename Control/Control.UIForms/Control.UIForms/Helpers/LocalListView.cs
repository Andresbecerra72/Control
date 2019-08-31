using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Control.UIForms.Helpers
{
    public class LocalListView : ViewCell
    {
       
        public LocalListView()
        {
           
            //texto de fecha
            var PublishOnLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Goldenrod
            };
            PublishOnLabel.SetBinding(Label.TextProperty, new Binding("PublishOnFormat")); //se liga el Label con el campo PublishOnFOrmat

            //texto de FLight
            var FlightLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 14,
                FontAttributes = FontAttributes.Italic

            };
            FlightLabel.SetBinding(Label.TextProperty, new Binding("Flight")); //se liga el label con el campo

            //texto de Total
            var TotalLabel = new Label
            {
               
                HorizontalOptions = LayoutOptions.End

            };
            TotalLabel.SetBinding(Label.TextProperty, new Binding("Total")); //se liga el label con el campo

           
            //texto de label Total
            var label = new Label
            {
                Text = "Total:",
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.End

            };
            

            //******Organizacion en el listview
            var Line1 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    PublishOnLabel
                },
            };

            var Line2 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    FlightLabel , label ,TotalLabel
                },
            };

            View = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    Line1,Line2
                },
            };

        }
    }
}
