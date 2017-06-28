namespace BlindDriver
{
    /// <summary>
    /// Interfejs dostepu do plików
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// Zapisywanie tekstu do pliku
        /// </summary>
        /// <param name="fileName">Nazwa pliku</param>
        /// <param name="text">Tekst</param>
        void SaveText(string fileName, string text);

        /// <summary>
        /// Odczytywanie tekstu
        /// </summary>
        /// <param name="fileName">Nazwa pliku</param>
        /// <returns>Tekst odczytany z pliku</returns>
        string ReadText(string fileName);

        /// <summary>
        /// Sprawdzanie czy plik istnieje
        /// </summary>
        /// <param name="fileName">Nazwa pliku</param>
        /// <returns>Flaga informująca o tym czy plik istnieje</returns>
        bool FileExists(string fileName);
    }
}