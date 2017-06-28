using System.Collections.Generic;
using BlindDriver.Models;
using BlindDriver.Resources;
using Xamarin.Forms;
using System.Linq;

namespace BlindDriver.ViewModel
{
    /// <summary>
    /// Klasa do obsługi widoku wybierania trasy wyścigu
    /// TODO: 
    /// 1. Przenieść dane wyścigów do pliku JSON i inicjalizować listę tras z uprzednim odczytem
    /// 2. Stopień trudności zakrętu zamienić na enum
    /// </summary>
    public class RaceChooseViewModel
    {
        /// <summary>
        /// Lista wyścigów widoczna w widoku
        /// </summary>
        public static List<Race> Races { get; set; }
        
        /// <summary>
        /// Konstruktor bezparametrowy.
        /// 
        /// </summary>
        static RaceChooseViewModel()
        {
            if(!DependencyService.Get<IFile>().FileExists("scores.txt"))
                DependencyService.Get<IFile>().SaveText("scores.txt", "100 150 300 200 350");
            
            var scores = DependencyService.Get<IFile>().ReadText("scores.txt");

            string[] records = scores.Split(' ');
            int i = 0;
            double[] bestTimes = new double[5];

            foreach (var record in records)
            {
                bestTimes[i] = double.Parse(record);
                i++;
            }

            Races = new List<Race>
            {
                new Race
                {
                    Id = 1,
                    Name = "Pinamar",
                    BestTime = bestTimes[0],
                    Length = 1000,
                    Difficulty = Resource.easy,
                    ImageName = "trasa.png",
                    Turns = new List<Turn>
                    {
                        new Turn
                        {
                            TurnType = Resource.r3,
                            OnMeter = 275,
                            ImageName = "right3.png",
                            Value = -3
                        },
                        new Turn
                        {
                            TurnType = Resource.r3,
                            OnMeter = 425,
                            ImageName = "right3.png",
                            Value = -3
                        },
                        new Turn
                        {
                            TurnType = Resource.r3,
                            OnMeter = 675,
                            ImageName = "right3.png",
                            Value = -3
                        },
                        new Turn
                        {
                            TurnType = Resource.r3,
                            OnMeter = 825,
                            ImageName = "right3.png",
                            Value = -3
                        }
                    }
                },
                new Race
                {
                    Id = 2,
                    Name = "Rio Gallegos",
                    BestTime = bestTimes[1],
                    Length = 1500,
                    Difficulty = Resource.medium,
                    ImageName =    "trasa2.png",
                    Turns = new List<Turn>
                    {
                        new Turn
                        {
                            TurnType = Resource.r2,
                            OnMeter = 75,
                            ImageName = "right2.png",
                            Value = -2
                        },
                        new Turn
                        {
                            TurnType = Resource.r1,
                            OnMeter = 160,
                            ImageName = "right1.png",
                            Value = -1
                        },
                        new Turn
                        {
                            TurnType = Resource.r2,
                            OnMeter = 220,
                            ImageName = "right2.png",
                            Value = -2
                        },
                        new Turn
                        {
                            TurnType = Resource.r1,
                            OnMeter = 270,
                            ImageName = "right1.png",
                            Value = -1
                        },
                        new Turn
                        {
                            TurnType = Resource.r3,
                            OnMeter = 340,
                            ImageName = "right3.png",
                            Value = -3
                        },
                        new Turn
                        {
                            TurnType = Resource.r1,
                            OnMeter = 450,
                            ImageName = "right1.png",
                            Value = -1
                        },
                        new Turn
                        {
                            TurnType = Resource.r1,
                            OnMeter = 570,
                            ImageName = "right1.png",
                            Value = -1
                        },
                        new Turn
                        {
                            TurnType = Resource.r2,
                            OnMeter = 660,
                            ImageName = "right2.png",
                            Value = -2
                        },
                        new Turn
                        {
                            TurnType = Resource.r1,
                            OnMeter = 705,
                            ImageName = "right1.png",
                            Value = -1
                        },
                        new Turn
                        {
                            TurnType = Resource.r2,
                            OnMeter = 800,
                            ImageName = "right2.png",
                            Value = -2
                        },
                        new Turn
                        {
                            TurnType = Resource.r2,
                            OnMeter = 875,
                            ImageName = "right2.png",
                            Value = -2
                        }
                    }
                },
                new Race
                {
                    Id = 3,
                    Name = "Gran Premio",
                    BestTime = bestTimes[2],
                    Length = 3000,
                    Difficulty = Resource.hard,
                    ImageName = "trasa3.png",
                    Turns = new List<Turn>
                    {
                        new Turn
                        {
                            TurnType = Resource.r3,
                            OnMeter = 120,
                            ImageName = "right3.png",
                            Value = -3
                        },
                        new Turn
                        {
                            TurnType = Resource.l4,
                            OnMeter = 300,
                            ImageName = "left4.png",
                            Value = 4
                        },
                        new Turn
                        {
                            TurnType = Resource.l1,
                            OnMeter = 550,
                            ImageName = "left1.png",
                            Value = 1
                        },
                        new Turn
                        {
                            TurnType = Resource.r4,
                            OnMeter = 650,
                            ImageName = "right4.png",
                            Value = -4
                        },
                        new Turn
                        {
                            TurnType = Resource.r2,
                            OnMeter = 720,
                            ImageName = "right2.png",
                            Value = -2
                        },
                        new Turn
                        {
                            TurnType = Resource.r3,
                            OnMeter = 820,
                            ImageName = "right3.png",
                            Value = -3
                        },
                        new Turn
                        {
                            TurnType = Resource.l3,
                            OnMeter = 900,
                            ImageName = "left3.png",
                            Value = 3
                        },
                        new Turn
                        {
                            TurnType = Resource.l3,
                            OnMeter = 1000,
                            ImageName = "left3.png",
                            Value = 3
                        },
                        new Turn
                        {
                            TurnType = Resource.r3,
                            OnMeter = 1150,
                            ImageName = "right3.png",
                            Value = -3
                        },
                        new Turn
                        {
                            TurnType = Resource.r5,
                            OnMeter = 1300,
                            ImageName = "right5.png",
                            Value = -5
                        },
                        new Turn
                        {
                            TurnType = Resource.l3,
                            OnMeter = 1400,
                            ImageName = "left3.png",
                            Value = 3
                        },
                        new Turn
                        {
                            TurnType = Resource.r1,
                            OnMeter = 1480,
                            ImageName = "right1.png",
                            Value = -1
                        },
                        new Turn
                        {
                            TurnType = Resource.l4,
                            OnMeter = 1600,
                            ImageName = "left4.png",
                            Value = -4
                        },
                        new Turn
                        {
                            TurnType = Resource.r4,
                            OnMeter = 1800,
                            ImageName = "right4.png",
                            Value = -4
                        },
                        new Turn
                        {
                            TurnType = Resource.l1,
                            OnMeter = 1840,
                            ImageName = "left1.png",
                            Value = 1
                        },
                        new Turn
                        {
                            TurnType = Resource.r4,
                            OnMeter = 2100,
                            ImageName = "right4.png",
                            Value = -4
                        },
                        new Turn
                        {
                            TurnType = Resource.l3,
                            OnMeter = 2200,
                            ImageName = "left3.png",
                            Value = 3
                        },
                        new Turn
                        {
                            TurnType = Resource.r5,
                            OnMeter = 2300,
                            ImageName = "right5.png",
                            Value = -5
                        },
                        new Turn
                        {
                            TurnType = Resource.l3,
                            OnMeter = 2400,
                            ImageName = "left3.png",
                            Value = 3
                        },
                        new Turn
                        {
                            TurnType = Resource.l3,
                            OnMeter = 2550,
                            ImageName = "left3.png",
                            Value = 3
                        },
                        new Turn
                        {
                            TurnType = Resource.r4,
                            OnMeter = 2700,
                            ImageName = "right4.png",
                            Value = -4
                        },
                        new Turn
                        {
                            TurnType = Resource.l1,
                            OnMeter = 2800,
                            ImageName = "left1.png",
                            Value = 1
                        },
                        new Turn
                        {
                            TurnType = Resource.r3,
                            OnMeter = 2870,
                            ImageName = "right3.png",
                            Value = -3
                        }
                    }
                },
                new Race
                {
                    Id = 4,
                    Name = "Las Flores",
                    BestTime = bestTimes[3],
                    Length = 2000,
                    Difficulty = Resource.hard,
                    ImageName = "trasa4.png",
                    Turns = new List<Turn>
                    {
                        new Turn
                        {
                            TurnType = Resource.l2,
                            OnMeter = 120,
                            ImageName = "left2.png",
                            Value = 2
                        },
                        new Turn
                        {
                            TurnType = Resource.r4,
                            OnMeter = 300,
                            ImageName = "right4.png",
                            Value = -4
                        },
                        new Turn
                        {
                            TurnType = Resource.r3,
                            OnMeter = 550,
                            ImageName = "right3.png",
                            Value = -3
                        },
                        new Turn
                        {
                            TurnType = Resource.l3,
                            OnMeter = 650,
                            ImageName = "left3.png",
                            Value = 3
                        },
                        new Turn
                        {
                            TurnType = Resource.l3,
                            OnMeter = 720,
                            ImageName = "left3.png",
                            Value = 3
                        },
                        new Turn
                        {
                            TurnType = Resource.l1,
                            OnMeter = 820,
                            ImageName = "left1.png",
                            Value = 1
                        },
                        new Turn
                        {
                            TurnType = Resource.r4,
                            OnMeter = 900,
                            ImageName = "right4.png",
                            Value = -4
                        },
                        new Turn
                        {
                            TurnType = Resource.l1,
                            OnMeter = 1000,
                            ImageName = "left1.png",
                            Value = 1
                        },
                        new Turn
                        {
                            TurnType = Resource.r1,
                            OnMeter = 1150,
                            ImageName = "right1.png",
                            Value = -1
                        },
                        new Turn
                        {
                            TurnType = Resource.l5,
                            OnMeter = 1300,
                            ImageName = "left5.png",
                            Value = 5
                        },
                        new Turn
                        {
                            TurnType = Resource.r4,
                            OnMeter = 1400,
                            ImageName = "right4.png",
                            Value = -4
                        },
                        new Turn
                        {
                            TurnType = Resource.r3,
                            OnMeter = 1480,
                            ImageName = "right3.png",
                            Value = -3
                        },
                        new Turn
                        {
                            TurnType = Resource.r3,
                            OnMeter = 1600,
                            ImageName = "right3.png",
                            Value = -3
                        },
                        new Turn
                        {
                            TurnType = Resource.l3,
                            OnMeter = 1800,
                            ImageName = "left3.png",
                            Value = 3
                        }
                    }
                },
                new Race
                {
                    Id = 5,
                    Name = "Puerto Madero",
                    BestTime = bestTimes[4],
                    Length = 3000,
                    Difficulty = Resource.extreme,
                    ImageName = "trasa5.png",
                    Turns = new List<Turn>
                    {
                        new Turn
                        {
                            TurnType = Resource.r4,
                            OnMeter = 120,
                            ImageName = "right4.png",
                            Value = -4
                        },
                        new Turn
                        {
                            TurnType = Resource.l4,
                            OnMeter = 300,
                            ImageName = "left4.png",
                            Value = 4
                        },
                        new Turn
                        {
                            TurnType = Resource.l3,
                            OnMeter = 550,
                            ImageName = "left3.png",
                            Value = 3
                        },
                        new Turn
                        {
                            TurnType = Resource.r5,
                            OnMeter = 650,
                            ImageName = "right5.png",
                            Value = -5
                        },
                        new Turn
                        {
                            TurnType = Resource.r5,
                            OnMeter = 1000,
                            ImageName = "right5.png",
                            Value = -5
                        },
                        new Turn
                        {
                            TurnType = Resource.l5,
                            OnMeter = 1060,
                            ImageName = "left5.png",
                            Value = 5
                        },
                        new Turn
                        {
                            TurnType = Resource.r5,
                            OnMeter = 1120,
                            ImageName = "right5.png",
                            Value = -5
                        },
                        new Turn
                        {
                            TurnType = Resource.r2,
                            OnMeter = 1200,
                            ImageName = "right2.png",
                            Value = -2
                        },
                        new Turn
                        {
                            TurnType = Resource.l1,
                            OnMeter = 1280,
                            ImageName = "left1.png",
                            Value = 1
                        },
                        new Turn
                        {
                            TurnType = Resource.l3,
                            OnMeter = 1360,
                            ImageName = "left3.png",
                            Value = 3
                        },
                        new Turn
                        {
                            TurnType = Resource.l3,
                            OnMeter = 1430,
                            ImageName = "left3.png",
                            Value = 3
                        },
                        new Turn
                        {
                            TurnType = Resource.l1,
                            OnMeter = 1490,
                            ImageName = "left1.png",
                            Value = 1
                        },
                        new Turn
                        {
                            TurnType = Resource.r4,
                            OnMeter = 1600,
                            ImageName = "right4.png",
                            Value = -4
                        },
                        new Turn
                        {
                            TurnType = Resource.r5,
                            OnMeter = 1800,
                            ImageName = "right5.png",
                            Value = -5
                        },
                        new Turn
                        {
                            TurnType = Resource.l3,
                            OnMeter = 1840,
                            ImageName = "left3.png",
                            Value = 3
                        },
                        new Turn
                        {
                            TurnType = Resource.l5,
                            OnMeter = 2100,
                            ImageName = "left5.png",
                            Value = 5
                        },
                        new Turn
                        {
                            TurnType = Resource.r2,
                            OnMeter = 2200,
                            ImageName = "right2.png",
                            Value = -2
                        },
                        new Turn
                        {
                            TurnType = Resource.r3,
                            OnMeter = 2300,
                            ImageName = "right3.png",
                            Value = -3
                        },
                        new Turn
                        {
                            TurnType = Resource.r4,
                            OnMeter = 2400,
                            ImageName = "right4.png",
                            Value = -4
                        },
                        new Turn
                        {
                            TurnType = Resource.r1,
                            OnMeter = 2550,
                            ImageName = "right1.png",
                            Value = -1
                        },
                        new Turn
                        {
                            TurnType = Resource.l3,
                            OnMeter = 2700,
                            ImageName = "left4.png",
                            Value = 3
                        },
                        new Turn
                        {
                            TurnType = Resource.l1,
                            OnMeter = 2800,
                            ImageName = "left1.png",
                            Value = 1
                        },
                        new Turn
                        {
                            TurnType = Resource.l3,
                            OnMeter = 2870,
                            ImageName = "left3.png",
                            Value = 3
                        },
                    }
                }
            };


        }
        public static void ReadRaceDetails(int carouselPageIndex)
        {
            Race race = Races.Where(x => x.Id == carouselPageIndex + 1).First();
            DependencyService.Get<ITextToSpeech>().Speak(string.Format(Resource.RaceChooseDetails, 
                race.Name, race.Difficulty, race.Length, race.BestTime.ToString() == "0" ? Resource.not_set : race.BestTime + " " + Resource.seconds),false);
            DependencyService.Get<ITextToSpeech>().Speak(Resource.choose_race_read);
        }
    }
}
