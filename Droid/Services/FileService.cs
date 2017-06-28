using System;
using Xamarin.Forms;
using BlindDriver.Droid;
using Android.Media;
using BlindDriver;
using System.IO;

[assembly: Dependency(typeof(FileService))]
namespace BlindDriver.Droid
{
    /// <summary>
    /// Klasa serwisu pozwala na dostęp do plików urządzenia
    /// </summary>
    public class FileService : IFile
    {
        /// <summary>
        /// Odczytywanie tekstu
        /// </summary>
        /// <param name="fileName">Nazwa pliku</param>
        /// <returns>Tekst odczytany z pliku</returns>
        public string ReadText(string fileName)
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(folderPath, fileName);
            return System.IO.File.ReadAllText(filePath);
        }

        /// <summary>
        /// Zapisywanie tekstu do pliku
        /// </summary>
        /// <param name="fileName">Nazwa pliku</param>
        /// <param name="text">Tekst</param>
        public void SaveText(string fileName, string text)
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(folderPath, fileName);
            System.IO.File.WriteAllText(filePath, text);
        }

        /// <summary>
        /// Sprawdzanie czy plik istnieje
        /// </summary>
        /// <param name="fileName">Nazwa pliku</param>
        /// <returns>Flaga informująca o tym czy plik istnieje</returns>
        public bool FileExists(string fileName)
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(folderPath, fileName);
            return File.Exists(filePath);
        } 

    }
}