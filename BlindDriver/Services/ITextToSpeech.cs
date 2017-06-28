namespace BlindDriver
{
    public interface ITextToSpeech
    {
        /// <summary>
        /// Metoda uruchamiająca syntezę mowy urządzenia
        /// </summary>
        /// <param name="text">Tekst do syntezowania na mowę</param>
        /// <param name="queueing">Czy kolejkować dźwięki syntezatora</param>
        void Speak(string text, bool? queueing = true);
    }
}
