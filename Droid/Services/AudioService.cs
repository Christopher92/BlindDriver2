using System;
using Xamarin.Forms;
using BlindDriver.Droid;
using Android.Media;
using BlindDriver;

[assembly: Dependency(typeof(AudioService))]
namespace BlindDriver.Droid
{
    /// <summary>
    /// Klasa serwisu pozwala na dostęp do odtwarzacza dźwięków urządzenia.
    /// </summary>
    public class AudioService : IAudio
    {
        /// <summary>
        /// Obiekt odtwarzacza dźwięków
        /// </summary>
        static MediaPlayer player = new MediaPlayer();

        /// <summary>
        /// KOnstruktor bezparametrowy
        /// </summary>
        public AudioService() { }

        /// <summary>
        /// Odtwarzanie plików mp3
        /// </summary>
        /// <param name="fileName">Nazwa pliku MP3</param>
        /// <param name="looping">Flaga informująca o tym czy zapętlać odtwarzacz na danym pliku MP3</param>
        /// <param name="volumeLevel">Głośność urządzenia(0-1)</param>
        public void PlayMp3File(string fileName, bool? looping, double? volumeLevel)
        {
            player.Reset();
            player.Looping = false;
            player.SetVolume((float)volumeLevel, (float)volumeLevel);
            var fd = global::Android.App.Application.Context.Assets.OpenFd(fileName);

            player.Prepared += (s, e) =>
            {
                player.Start();
            };

            player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
            player.Prepare();
            fd.Close();
        }

        /// <summary>
        /// Resetowanie odtwarzacza dźwięków
        /// </summary>
        public void ResetPlayer()
        {
            player.Reset();
        }
    }
}