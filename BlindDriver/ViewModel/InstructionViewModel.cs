using BlindDriver.Resources;
using Xamarin.Forms;

namespace BlindDriver.ViewModel
{
    /// <summary>
    /// Klasa do obsługi widoku instrukcji
    /// </summary>
    public class InstructionViewModel
    {
        /// <summary>
        /// Tekst instrukcji użytkownika
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Konstruktor bezparametrowy
        /// </summary>
        public InstructionViewModel()
        {
            Text = Resource.instructionContent;
            DependencyService.Get<ITextToSpeech>().Speak(Text, false);
        }
    }
}