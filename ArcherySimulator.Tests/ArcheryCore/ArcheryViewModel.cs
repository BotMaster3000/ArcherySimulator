using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ArcherySimulator.Commands;
using ArcherySimulator.Models;

namespace ArcherySimulator.ViewModels
{
    public class ArcheryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Command trainCommand = null!;
        public Command TrainCommand
        {
            get => trainCommand;
            set
            {
                if (trainCommand != value)
                {
                    trainCommand = value;
                    OnPropertyChanged(nameof(TrainCommand));
                }
            }
        }

        private Command shootCommand = null!;
        public Command ShootCommand
        {
            get => shootCommand;
            set
            {
                if (shootCommand != value)
                {
                    shootCommand = value;
                    OnPropertyChanged(nameof(ShootCommand));
                }
            }
        }

        private Command breakCommand = null!;
        public Command BreakCommand
        {
            get => breakCommand;
            set
            {
                if (breakCommand != value)
                {
                    breakCommand = value;
                    OnPropertyChanged(nameof(BreakCommand));
                }
            }
        }

        private Command sleepCommand = null!;
        public Command SleepCommand
        {
            get => sleepCommand;
            set
            {
                if (sleepCommand != value)
                {
                    sleepCommand = value;
                    OnPropertyChanged(nameof(SleepCommand));
                }
            }
        }

        private readonly Random rand = new();

        private ObservableCollection<string> eventLog = new();
        public ObservableCollection<string> EventLog
        {
            get => eventLog;
            set
            {
                if (eventLog != value)
                {
                    eventLog = value;
                    OnPropertyChanged(nameof(EventLog));
                }
            }
        }

        private int stamina;
        public int Stamina
        {
            get => stamina;
            set
            {
                if (stamina != value)
                {
                    stamina = value;
                    OnPropertyChanged(nameof(Stamina));
                }
            }
        }

        private int experience;
        public int Experience
        {
            get => experience;
            set
            {
                if (experience != value)
                {
                    experience = value;
                    CheckForLevelChange();
                    OnPropertyChanged(nameof(Experience));
                }
            }
        }

        private int level;
        public int Level
        {
            get => level;
            set
            {
                if (level != value)
                {
                    level = value;
                    AddToLog("You are now level " + Level);
                    OnPropertyChanged(nameof(Level));
                }
            }
        }

        private bool breakIsEnabled;
        public bool BreakIsEnabled
        {
            get => breakIsEnabled;
            set
            {
                if (breakIsEnabled != value)
                {
                    breakIsEnabled = value;
                    OnPropertyChanged(nameof(BreakIsEnabled));
                }
            }
        }

        public ArcheryViewModel()
        {
            TrainCommand = new Command(Train);
            SleepCommand = new Command(Sleep);
            BreakCommand = new Command(Break);
            ShootCommand = new Command(Shoot);

            Stamina = 100;
            Level = 1;
            Experience = 0;
            BreakIsEnabled = true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddToLog(string entry)
        {
            if (EventLog.Count >= 10)
            {
                EventLog.RemoveAt(0);
            }
            EventLog.Add(entry);
        }

        private bool HasEnoughStamina(int requiredStamina)
        {
            return Stamina - requiredStamina >= 0;
        }

        private void CheckForLevelChange()
        {
            if (Level == 1 && Experience < 0)
            {
                Experience = 0;
            }
            if (Experience >= 100)
            {
                Experience -= 100;
                Level += 1;
            }
            else if (Experience < 0)
            {
                Level -= 1;
                Experience = 100 + Experience;
            }
        }

        public void Train()
        {
            while (HasEnoughStamina(10))
            {
                Stamina -= 10;
                Experience += 10;
            }
        }

        public void Sleep()
        {
            Stamina = 100;
            Experience -= 50;
            AddToLog("You went to sleep");
            BreakIsEnabled = true;
        }

        public void Break()
        {
            BreakIsEnabled = false;
            int stamina = Stamina + 50;
            if (stamina > 100)
            {
                stamina = 100;
            }
            AddToLog("You took a break");
            Stamina = stamina;
        }

        public void Shoot()
        {
            const int requiredStamina = 10;
            if (!HasEnoughStamina(requiredStamina))
            {
                AddToLog("You dont have enough Stamina");
                return;
            }
            Stamina -= requiredStamina;
            int result = rand.Next(1, 11) + (int)Math.Round(0.1 * Level, MidpointRounding.AwayFromZero);
            if (result > 10)
            {
                result = 10;
            }
            AddToLog("You hit a " + result);
            Experience += result;
            AddToLog("You received " + result + " experience");
        }
    }
}
