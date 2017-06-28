using System;

namespace BlindDriver.Models
{
    public class Turn
    {
        /// <summary>
        /// Typ zakrętu
        /// </summary>
        public string TurnType { get; set; }

        /// <summary>
        /// Metr, w którym następuje zakręt
        /// </summary>
        public int OnMeter { get; set; }

        /// <summary>
        /// Flaga informująca o tym czy zakręt został obsłużony
        /// </summary>
        public bool Handled { get; set; }

        /// <summary>
        /// Flaga określająca czy zakręt został już odczytany przez urządzenie
        /// </summary>
        public bool HandledSound { get; set; }

        /// <summary>
        /// Nazwa grafiki opisującej zakręt
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// Stopień trudności zakrętu
        /// </summary>
        public int Value { get; set; }
    }
}
