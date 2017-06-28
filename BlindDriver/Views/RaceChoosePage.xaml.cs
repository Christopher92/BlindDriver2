using Xamarin.Forms;
using BlindDriver.ViewModel;
using BlindDriver.Models;
using BlindDriver.Resources;

namespace BlindDriver
{
    public partial class RaceChoosePage : CarouselPage
    {
        public static int index;


        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            index = Children.IndexOf(CurrentPage);
            RaceChooseViewModel.ReadRaceDetails(index);
        }

        public RaceChoosePage()
        {
            InitializeComponent();
            ItemsSource = RaceChooseViewModel.Races;
        }
        void OnTapGestureRecognizerTapped(object sender, System.EventArgs args)
        {
            var senderBindingContext = ((StackLayout)sender).BindingContext;
            Race race = (Race)senderBindingContext;
            Navigation.PushAsync(new Views.RacePage(race));
        }

        void ReturnOnDoubleTap(object sender, System.EventArgs args)
        {
            DependencyService.Get<ITextToSpeech>().Speak(Resource.introduction_read, false);
            Navigation.PopToRootAsync();
        }
    }
}

