//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BlindDriver.ViewModel;
//using Xamarin.Forms;
//using DeviceMotion.Plugin;
//using DeviceMotion.Plugin.Abstractions;

//namespace BlindDriver.Views
//{
//    public class InstructionPageCS : ContentPage
//    {
//        public static readonly BindableProperty AngleProperty = BindableProperty.Create("Angle", typeof(double), typeof(InstructionPage), 0.0);

//        public double Angle
//        {
//            get
//            {
//                return (double)GetValue(AngleProperty);
//            }
//            set
//            {
//                try
//                {
//                    SetValue(AngleProperty, value);
//                }
//                catch
//                {
//                    DisplayAlert("Alert", "Angle must be between 0-360", "OK");
//                }
//            }

//        }

//        public InstructionPageCS()
//        {
//            BindingContext = this;


//            var angleEntry = new Entry { WidthRequest = 50 };
//            angleEntry.SetBinding(Entry.TextProperty, "Angle");

//            var label = new Label { WidthRequest = 50 };
//            label.SetBinding(Label.TextProperty, "Angle");
//            this.Angle = 15.0;


//            Content = new StackLayout
//            {
//                Padding = new Thickness(0, 20, 0, 0),
//                Children = {
//                    label,
//                    angleEntry
//                        }
//            };

//            label.Text = "OMG!";
//        }
//}
