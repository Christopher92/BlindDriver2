using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BlindDriver.Views
{
    public partial class LanguagePage : ContentPage
    {
        public LanguagePage()
        {
            InitializeComponent();
        }
        async void PolishButtonClicked(object sender, System.EventArgs e)
        {
            DependencyService.Get<IAudio>().PlayMp3File(
                "PolishLanguageChose.mp3"
            );
            await Navigation.PushAsync(new HomePage());
        }

        async void EnglishButtonClicked(object sender, System.EventArgs e)
        {
            DependencyService.Get<IAudio>().PlayMp3File(
                "EnglishLanguageChose.mp3"
            );
            await Navigation.PushAsync(new HomePage());
        }
    }
}
