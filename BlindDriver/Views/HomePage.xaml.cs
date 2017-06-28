using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlindDriver.Resources;
using Xamarin.Forms;

namespace BlindDriver.Views
{
    public partial class HomePage : ContentPage
    {

        public HomePage()
        {
            InitializeComponent();
            DependencyService.Get<ITextToSpeech>().Speak(Resource.introduction_read, false);
        }

        async void InstructionButtonClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new InstructionPage());

        }

        async void RaceChooseButtonClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new RaceChoosePage());
        }
    }

}
