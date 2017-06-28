using Android.Speech.Tts;
using Xamarin.Forms;
using System.Collections.Generic;
using BlindDriver.Droid;
using System;
using System.Linq;

[assembly: Xamarin.Forms.Dependency(typeof(BlindDriver.Droid.TextToSpeechService))]
namespace BlindDriver.Droid
{
    /// <summary>
    /// Klasa serwisu pozwala na dostęp do syntezatora mowy urządzenia
    /// </summary>
    public class TextToSpeechService : Java.Lang.Object, ITextToSpeech, TextToSpeech.IOnInitListener
    {

        TextToSpeech speaker;
        string toSpeak;

        /// <summary>
        /// Konstruktor bezparametrowy
        /// </summary>
        public TextToSpeechService() { }

        /// <summary>
        /// Metoda uruchamiająca syntezę mowy urządzenia
        /// </summary>
        /// <param name="text">Tekst do syntezowania na mowę</param>
        /// <param name="queueing">Czy kolejkować dźwięki syntezatora</param>
        public void Speak(string text, bool? queueing = true)
        {
            Android.Speech.Tts.QueueMode queueMode = queueing == true ? QueueMode.Add : QueueMode.Flush;
            var localesAvailable = Java.Util.Locale.GetAvailableLocales().ToList();

            var ctx = Forms.Context;
            toSpeak = text;
            if (speaker == null)
            {
                speaker = new TextToSpeech(ctx, this);
                //speaker.SetSpeechRate(2);
            }
            else
            {
                var p = new Dictionary<string, string>();
                speaker.Speak(toSpeak, queueMode, p);
            }
        }

        #region IOnInitListener implementation

        /// <summary>
        /// Metoda wywoływana przy inizjalizacji syntezatora
        /// </summary>
        /// <param name="status"></param>
        public void OnInit(OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
                var p = new Dictionary<string, string>();
                speaker.Speak(toSpeak, QueueMode.Flush, p);
            }
        }

        #endregion
    }
}