using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ArcherySimulator.Commands;
using ArcherySimulator.Models;

namespace ArcherySimulator.ViewModels
{
    public class ArcheryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Command trainCommand;
        public Command TrainCommand
        {
            get { return trainCommand; }
            set
            {
                if (trainCommand != value)
                {
                    trainCommand = value;
                    OnPropertyChanged(nameof(TrainCommand));
                }
            }
        }

        private Command shootCommand;
        public Command ShootCommand
        {
            get { return shootCommand; }
            set
            {
                if (shootCommand != value)
                {
                    shootCommand = value;
                    OnPropertyChanged(nameof(ShootCommand));
                }
            }
        }

        private Command breakCommand;
        public Command BreakCommand
        {
            get { return breakCommand; }
            set
            {
                if (breakCommand != value)
                {
                    breakCommand = value;
                    OnPropertyChanged(nameof(BreakCommand));
                }
            }
        }

        private Command sleepCommand;
        public Command SleepCommand
        {
            get { return sleepCommand; }
            set
            {
                if (sleepCommand != value)
                {
                    sleepCommand = value;
                    OnPropertyChanged(nameof(SleepCommand));
                }
            }
        }

        private Random rand = new Random();

        private ObservableCollection<string> eventLog = new ObservableCollection<string>();
        public ObservableCollection<string> EventLog
        {
            get { return eventLog; }
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
            get { return stamina; }
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
            get { return experience; }
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
            get { return level; }
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
            get { return breakIsEnabled; }
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
            return (Stamina - requiredStamina) >= 0;
        }

        private void CheckForLevelChange()
        {
            int oldExperience = experience;
            int oldLevel = level;

            if (level == 1 && experience < 0)
            {
                experience = 0;
            }

            if (experience >= 100)
            {
                experience -= 100;
                level += 1;
            }
            else if (experience < 0)
            {
                level -= 1;
                experience = 100 + experience;
            }

            if (experience != oldExperience)
            {
                OnPropertyChanged(nameof(Experience));
            }

            if (level != oldLevel)
            {
                AddToLog("You are now level " + level);
                OnPropertyChanged(nameof(Level));
            }
        }

        public void Train()
        {
            while (HasEnoughStamina(10))
            {
                Shoot();
            }
            AddToLog("You don't have enough stamina");
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

            int stamina = Stamina;
            stamina += 50;
            if (stamina > 100)
            {
                stamina = 100;
            }

            AddToLog("You took a break");

            Stamina = stamina;
        }

        public void Shoot()
        {
            int requiredStamina = 10;
            if (!HasEnoughStamina(requiredStamina))
            {
                AddToLog("You don't have enough stamina");
                return;
            }
            Stamina -= requiredStamina;

            // Math.Round uses banker's rounding which would round values like 0.5 to
            // the nearest even number (0). That behaviour causes level modifiers
            // to not increase as expected at level 5, 15, ... . Use AwayFromZero
            // rounding so that a value of 0.5 becomes 1.
            int result = rand.Next(1, 11) +
                         (int)Math.Round(0.1 * Level, MidpointRounding.AwayFromZero);
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
