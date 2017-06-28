using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlindDriver.Resources;
using BlindDriver.ViewModel;
using Xamarin.Forms;
using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;

namespace BlindDriver.Views
{
    public partial class InstructionPage : ContentPage
    {
        public InstructionPage()
        {
            BindingContext = new InstructionViewModel();
            InitializeComponent();
            //DependencyService.Get<ITextToSpeech>().Speak("Instrukcja");

        }

        void ReturnOnDoubleTap(object sender, System.EventArgs args)
        {
            DependencyService.Get<ITextToSpeech>().Speak(Resource.introduction_read, false);
            Navigation.PopToRootAsync();
        }
    }
}
