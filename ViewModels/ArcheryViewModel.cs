﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArcherySimulator.ViewModels
{
    public class ArcheryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Random rand = new Random();
        private ObservableCollection<string> eventLog;
        public ObservableCollection<string> EventLog
        {
            get
            {
                if (eventLog == null)
                {
                    eventLog = new ObservableCollection<string>();
                }
                return eventLog;
            }
            set
            {
                if (eventLog != value)
                {
                    eventLog = value;
                }
            }
        }
        private int stamina = -1;
        public int Stamina
        {
            get
            {
                if (stamina == -1)
                {
                    stamina = 100;
                }
                return stamina;
            }
            set
            {
                if (stamina != value)
                {
                    stamina = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentStamina"));
                }
            }
        }
        private int experience;
        public int Experience
        {
            get
            {
                return experience;
            }
            set
            {
                if (experience != value)
                {
                    if (Level == 1 && (value < 0))
                    {
                        experience = 0;
                    }
                    else
                    {
                        experience = value;
                    }
                    if (experience >= 100)
                    {
                        experience = experience - 100;
                        Level += 1;
                    }
                    else if (experience < 0)
                    {
                        Level -= 1;
                        experience = 100 + value;
                    }
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentExperience"));
                }
            }
        }
        private int level;
        public int Level
        {
            get
            {
                if (level == 0)
                {
                    level = 1;
                }
                return level;
            }
            set
            {
                if (level != value)
                {
                    level = value;
                    AddToLog("You are now level " + Level);
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentLevel"));
                }
            }
        }

        private bool breakIsEnabled;
        public bool BreakIsEnabled
        {
            get { return breakIsEnabled; }
            set
            {
                if(breakIsEnabled != value)
                {
                    breakIsEnabled = value;
                    OnPropertyChanged(nameof(BreakIsEnabled));
                }
            }
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
            if ((Stamina - requiredStamina) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Train(object sender, RoutedEventArgs e)
        {
            while (HasEnoughStamina(10))
            {
                Shoot(new object(), new RoutedEventArgs());
            }
            AddToLog("You dont habe enough Stamina");
        }
        public void Sleep(object sender, RoutedEventArgs e)
        {
            Stamina = 100;

            Experience -= 50;

            AddToLog("You went to sleep");

            BreakIsEnabled = true;
            //if (btnBreak.IsEnabled == false)
            //{
            //    btnBreak.IsEnabled = true;
            //}
        }
        public void Break(object sender, RoutedEventArgs e)
        {
            BreakIsEnabled = false;
            //btnBreak.IsEnabled = false;

            int stamina = Stamina;
            stamina += 50;
            if (stamina > 100)
            {
                stamina = 100;
            }

            AddToLog("You took a break");

            Stamina = stamina;
        }
        public void Shoot(object sender, RoutedEventArgs e)
        {
            int requiredStamina = 10;
            if (!HasEnoughStamina(requiredStamina))
            {
                AddToLog("You dont have enough Stamina");
                return;
            }
            Stamina -= requiredStamina;

            int result = (int)(rand.Next(1, 11) + Math.Round(0.1 * Level));
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