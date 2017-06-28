namespace BlindDriver
{
    /// <summary>
    /// Interfejs do odtwarzania audio
    /// </summary>
    public interface IAudio
    {
        /// <summary>
        /// Odtwarzanie plików MP3
        /// </summary>
        /// <param name="fileName">Nazwa pliku</param>
        /// <param name="looping">Flaga określająca czy zapętlać plik</param>
        /// <param name="volumeLevel">Głośność odtwarzanego pliku</param>
        void PlayMp3File(string fileName, bool? looping = false, double? volumeLevel = 0.2);

        /// <summary>
        /// Resetowanie odtwarzania plików
        /// </summary>
        void ResetPlayer();
    }
}