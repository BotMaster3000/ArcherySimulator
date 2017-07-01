using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArcherySimulator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Random rand = new Random();
        private ObservableCollection<string> eventLog;
        public ObservableCollection<string> EventLog
        {
            get
            {
                if(eventLog == null)
                {
                    eventLog = new ObservableCollection<string>();
                }
                return eventLog;
            }
            set
            {
                if(eventLog != value)
                {
                    eventLog = value;                    
                }
            }
        }
        private int currentStamina = -1;
        public int CurrentStamina
        {
            get
            {
                if(currentStamina == -1)
                {
                    currentStamina = 100;
                }
                return currentStamina;
            }
            set
            {
                if(currentStamina != value)
                {
                    currentStamina = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentStamina"));
                }
            }
        }
        private int currentExperience;
        public int CurrentExperience
        {
            get
            {
                return currentExperience;
            }
            set
            {
                if(currentExperience != value)
                {
                    currentExperience = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentExperience"));
                }
            }
        }
        private int currentLevel;
        public int CurrentLevel
        {
            get
            {
                if(currentLevel == 0)
                {
                    currentLevel = 1;
                }
                return currentLevel;
            }
            set
            {
                if(currentLevel != value)
                {
                    currentLevel = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentLevel"));
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        private void AddToLog(string entry)
        {
            if(EventLog.Count >= 10)
            {
                EventLog.RemoveAt(0);
            }
            EventLog.Add(entry);
        }
        private bool HasEnoughStamina(int requiredStamina)
        {
            if((CurrentStamina - requiredStamina) >= 0)
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
            for(int i = 0; i < 10; ++i)
            {
                Shoot(new object(), new RoutedEventArgs());
            }
        }
        public void Sleep(object sender, RoutedEventArgs e)
        {
            CurrentStamina = 100;
            AddToLog("You went to sleep");
        }
        public void Break(object sender, RoutedEventArgs e)
        {
            int stamina = CurrentStamina;
            stamina += 50;
            if(stamina > 100)
            {
                stamina = 100;
            }
            CurrentStamina = stamina;
        }
        public void Shoot(object sender, RoutedEventArgs e)
        {
            int requiredStamina = 10;
            if (!HasEnoughStamina(requiredStamina))
            {
                AddToLog("You dont have enough Stamina");
                return;
            }
            CurrentStamina -= requiredStamina;

            int result = (int) (rand.Next(1, 11) + Math.Round(0.1 * CurrentLevel));
            AddToLog("You hit a " + result);
            CurrentExperience += result;            
            AddToLog("You received " + result + " experience");
        }
    }
}
