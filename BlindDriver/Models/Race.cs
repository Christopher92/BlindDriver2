using System.Collections.Generic;

namespace BlindDriver.Models
{
    public class Race
    {
        /// <summary>
        /// Identyfikator wyścigu
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nazwa wyścigu
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Najlepszy czas 
        /// </summary>
        public double BestTime { get; set; }

        /// <summary>
        /// Długość trasy wyścigu
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Trudność wyścigu
        /// </summary>
        public string Difficulty { get; set; }

        /// <summary>
        /// Poglądowy obrazek dla trasy
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// Lista zakrętów w trasie
        /// </summary>
        public IList<Turn> Turns { get; set; }

    }
}
