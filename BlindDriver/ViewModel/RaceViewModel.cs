using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using BlindDriver.Models;
using BlindDriver.Resources;

namespace BlindDriver.ViewModel
{
    public class RaceViewModel : INotifyPropertyChanged
    {
        #region constances
        private const int tresholdSpeed = 40;
        private const string startRaceSoundFile = "start.ogg";
        private const string scoresFile = "scores.txt";
        private const int countdownSecondsTime = 3;
        #endregion

        #region properties
        public static bool isBound { get; set; }
        public static Race race { get; set; }
        public static int DriverTurn { get; set; }
        public string Text { get; set; }

        string speed, angle, timer, drivenMeters, command, turnImageName;

        public string SpeedText
        {
            get { return speed; }
            set
            {
                if (speed == value)
                    return;
                speed = value;
                OnPropertyChanged("SpeedText");
            }
        }
        public string AngleText
        {
            get { return angle; }
            set
            {
                if (angle == value)
                    return;
                angle = value;
                OnPropertyChanged("AngleText");
            }
        }

        public string TimerText
        {
            get { return timer; }
            set
            {
                if (timer == value)
                    return;
                timer = value;
                OnPropertyChanged("TimerText");
            }
        }

        public string DrivenMetersText
        {
            get { return drivenMeters; }
            set
            {
                if (drivenMeters == value)
                    return;
                drivenMeters = value;
                OnPropertyChanged("DrivenMetersText");
            }
        }

        public string CommandText
        {
            get { return command; }
            set
            {
                if (command == value)
                    return;
                command = value;
                OnPropertyChanged("CommandText");
            }
        }

        public string TurnImageName
        {
            get { return turnImageName; }
            set
            {
                if (turnImageName == value)
                    return;
                turnImageName = value;
                OnPropertyChanged("TurnImageName");
            }
        }
        #endregion

        #region fields
        double axisX, axisY, axisZ;

        private int actualMeter;
        private double kmph;
        private double dtimer;
        public static bool isPaused = false;
        public static bool isStopped = false;

        int turnScore = 0;
        int executedTurn = 0;
        int slowdownFactor = 30;
        int gear = 0;
        double acceleration;

        private static int curentSpeedSound = 0;

        //Potrzebne do niewelowania coraz szybszego przyspieszania
        private int raceTimesRun = 0;
        #endregion 


        /// <summary>
        /// Konstruktor bezparametrowy
        /// Inicjalizacja początkowych wartości w wyścigu.
        /// Uruchomienie odliczania czasu do rozpoczęcia wyścigu.
        /// </summary>
        public RaceViewModel()
        {
            actualMeter = 0;
            kmph = 0;
            dtimer = 0;

            curentSpeedSound = 0;

            DependencyService.Get<ITextToSpeech>().Speak(Resource.race_chosen + " " + race.Name + ". " + Resource.countdown, false);
            Device.StartTimer(TimeSpan.FromSeconds(countdownSecondsTime), () =>
            {
                int timer = countdownSecondsTime;
                DependencyService.Get<IAudio>().PlayMp3File(startRaceSoundFile);
                Device.StartTimer(TimeSpan.FromMilliseconds(1000), () =>
                {
                    if (timer > 0)
                    {
                        DependencyService.Get<ITextToSpeech>().Speak(timer.ToString());
                        timer -= 1;
                        return true;
                    }
                    else
                    {
                        DependencyService.Get<ITextToSpeech>().Speak(Resource.start);
                        startRace();
                        return false;
                    }
                });

                return false;
            });
        }

        /// <summary>
        /// Obiekt eventu zmiany wartości property
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Handler eventu zmiany wartości property
        /// </summary>
        /// <param name="propertyName"></param>
        void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Start czasomierza
        /// </summary>
        public void StartTimer()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                if (!isStopped)
                {
                    if (!isPaused)
                    {
                        dtimer += 0.1;
                        TimerText = dtimer.ToString("0.0");

                        if (actualMeter < race.Length)
                        {
                            ProcessRace();
                            return true;
                        }
                        else
                        {
                            ProcessFinishRace();
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            });

        }

        /// <summary>
        /// Przetwarzanie pętli wyścigu
        /// </summary>
        public void ProcessRace()
        {
            int closestTurnDistance = race.Length;
            foreach (var turn in race.Turns)
            {
                closestTurnDistance = Math.Abs(actualMeter - turn.OnMeter) < closestTurnDistance
                    ? Math.Abs(actualMeter - turn.OnMeter)
                    : closestTurnDistance;
                if (actualMeter >= turn.OnMeter - 50 && actualMeter <= turn.OnMeter - 35)
                {
                    if (!turn.HandledSound)
                    {
                        turn.HandledSound = ShowTurnToUser(turn);
                    }
                }
                else if (actualMeter >= turn.OnMeter - 5 && actualMeter <= turn.OnMeter + 5)
                {
                    if (!turn.Handled)
                    {
                        turn.Handled = EvaluatePerformedTurn(turn);
                    }
                }
            }

            if (closestTurnDistance > 50)
            {
                TurnImageName = "";
            }

            actualMeter += Convert.ToInt32((kmph * 1000 / 3600) / 10);
            DrivenMetersText = actualMeter + " " + Resource.meters;
        }

        /// <summary>
        /// Przetwarzanie zakończenia wyścigu
        /// </summary>
        public void ProcessFinishRace()
        {
            DependencyService.Get<IAudio>().ResetPlayer();
            DependencyService.Get<ITextToSpeech>().Speak(Resource.Finish);
            TimerText = Resource.your_time + " " + dtimer.ToString("0.0") + Resource.seconds;
            isPaused = true;
            isStopped = true;
            DependencyService.Get<ITextToSpeech>().Speak(TimerText);
            if (dtimer < race.BestTime)
            {
                DependencyService.Get<ITextToSpeech>().Speak(Resource.best_time);

                var scores = DependencyService.Get<IFile>().ReadText(scoresFile);
                scores = scores.Replace(race.BestTime.ToString(), dtimer.ToString("0.0"));
                DependencyService.Get<IFile>().SaveText(scoresFile, scores);

                race.BestTime = Math.Round(dtimer, 1);
            }
            actualMeter = 0;
        }

        /// <summary>
        /// Pokazywanie i odczytywanie kolejnego zakrętu
        /// </summary>
        /// <param name="turn"></param>
        /// <returns>Status wykonania</returns>
        private bool ShowTurnToUser(Turn turn)
        {
            CommandText = turn.TurnType;
            TurnImageName = turn.ImageName;
            DependencyService.Get<ITextToSpeech>().Speak(turn.TurnType);

            return true;
        }

        /// <summary>
        /// Przetworzenie i wyliczenie punktów dla zakrętu.
        /// </summary>
        /// <param name="turn">Zakręt</param>
        /// <returns>Flaga określająca status przetworzenia zakrętu</returns>
        private bool EvaluatePerformedTurn(Turn turn)
        {
            executedTurn = (DriverTurn / 14);
            turnScore = Math.Abs(turn.Value - executedTurn);

            int previousSpeedSound = Convert.ToInt32((kmph + 20) / tresholdSpeed);

            kmph -= (turnScore * slowdownFactor) > kmph ? kmph : turnScore * slowdownFactor;

            curentSpeedSound = Convert.ToInt32((kmph + 19) / tresholdSpeed) > 0 ?
            Convert.ToInt32((kmph + 19) / tresholdSpeed)
            : 1;

            GenerateTurnSound(previousSpeedSound);

            return true;
        }

        /// <summary>
        /// Generowanie dźwięku zakrętu
        /// </summary>
        /// <param name="previousSpeedSound">Szybkość dźwięku silnika z poprzedniego przetwarzania</param>
        private void GenerateTurnSound(int previousSpeedSound)
        {
            if (previousSpeedSound != curentSpeedSound)
            {
                var engineSoundFile = curentSpeedSound + ".ogg";
                DependencyService.Get<IAudio>().PlayMp3File(engineSoundFile, true);
            }

            if (turnScore == 0)
            {
                DependencyService.Get<ITextToSpeech>().Speak(Resource.excelent);
            }
            else if (turnScore <= 1)
            {
                DependencyService.Get<ITextToSpeech>().Speak(Resource.good);
            }
            else if (turnScore <= 2)
            {
                DependencyService.Get<ITextToSpeech>().Speak(Resource.decent);
            }
            else if (turnScore <= 4)
            {
                DependencyService.Get<ITextToSpeech>().Speak(Resource.bad);
            }
            else
            {
                DependencyService.Get<ITextToSpeech>().Speak(Resource.awful);
            }
        }

        /// <summary>
        /// Metoda nasłuchująca zmian ruchu akcelerometru urządzenia
        /// </summary>
        private void SensorChangeListener()
        {
            CrossDeviceMotion.Current.Start(MotionSensorType.Accelerometer, MotionSensorDelay.Ui);
            CrossDeviceMotion.Current.SensorValueChanged += (s, a) =>
            {
                if (!isStopped)
                {
                    if (!isPaused)
                    {
                        switch (a.SensorType)
                        {
                            case MotionSensorType.Accelerometer:

                                axisX = ((MotionVector)a.Value).X;
                                axisY = ((MotionVector)a.Value).Y;
                                axisZ = ((MotionVector)a.Value).Z < 0 ? 0 : ((MotionVector)a.Value).Z;
                                int angle = Convert.ToInt32(axisY * -7.2);
                                DriverTurn = angle;
                                gear = Convert.ToInt32(axisZ / 2);

                                int previousSpeedSound = Convert.ToInt32((kmph + 20) / tresholdSpeed);

                                acceleration = (axisZ / 15) / raceTimesRun;

                                switch (gear)
                                {
                                    case 0:
                                        if (kmph < 0)
                                        {
                                            kmph = 0;
                                        }
                                        else
                                        {
                                            kmph -= kmph <= 0.8 ? kmph : 0.8;
                                        }
                                        break;
                                    case 1:
                                        kmph += (kmph + acceleration) < tresholdSpeed ? acceleration : -0.1 / acceleration;
                                        break;
                                    case 2:
                                        kmph += (kmph + acceleration) < tresholdSpeed * 2 ? acceleration : -0.1 / acceleration;
                                        break;
                                    case 3:
                                        kmph += (kmph + acceleration) < tresholdSpeed * 3 ? acceleration : -0.1 / acceleration;
                                        break;
                                    case 4:
                                        kmph += (kmph + acceleration) < tresholdSpeed * 4 ? acceleration : -0.1 / acceleration;
                                        break;
                                    case 5:
                                        kmph += (kmph + acceleration) < tresholdSpeed * 5 ? acceleration : -0.1 / acceleration;
                                        break;
                                }

                                curentSpeedSound = Convert.ToInt32((kmph + 19) / tresholdSpeed) > 0
                                    ? Convert.ToInt32((kmph + 19) / tresholdSpeed)
                                    : 1;

                                if (previousSpeedSound != curentSpeedSound)
                                {
                                    var engineSoundFile = curentSpeedSound + ".ogg";
                                    DependencyService.Get<IAudio>().PlayMp3File(engineSoundFile, true);
                                }

                                SpeedText = string.Format(Resource.GearAndSpeed, gear, Convert.ToInt32(kmph));
                                break;
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Startowanie wyścigu
        /// </summary>
        private void startRace()
        {
            raceTimesRun += 1;
            isPaused = false;
            isStopped = false;


            foreach (var turn in race.Turns)
            {
                turn.Handled = false;
                turn.HandledSound = false;
            }

            StartTimer();
            SensorChangeListener();
        }

        /// <summary>
        /// Zatrzymanie/wznowienie wyścigu
        /// </summary>
        static public void TogglePause()
        {
            if (!isPaused)
            {
                isPaused = !isPaused;
                DependencyService.Get<ITextToSpeech>().Speak(Resource.pause);
                DependencyService.Get<IAudio>().ResetPlayer();
            }
            else
            {
                DependencyService.Get<ITextToSpeech>().Speak(Resource.resume);
                Device.StartTimer(TimeSpan.FromMilliseconds(1000), () =>
                {
                    isPaused = !isPaused;
                    //W wolnej chwili napisać metodą w serwisie wznawiającą wcześniej grany dzwięk

                    var soundFile = curentSpeedSound + ".ogg";

                    DependencyService.Get<IAudio>().PlayMp3File(soundFile, true);
                    return false;
                });
            }
        }
    }
}