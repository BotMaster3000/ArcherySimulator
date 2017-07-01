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
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> EventLog = new ObservableCollection<string>();
        Random rand = new Random();
        string currentStamina;
        public string CurrentStamina
        {
            get
            {
                if(currentStamina == null)
                {
                    currentStamina = "N/A";
                }
                return currentStamina;
            }
            set
            {
                if(currentStamina != value)
                {
                    currentStamina = value;
                }
            }
        }
        string currentExperience;
        public string CurrentExperience
        {
            get
            {
                if(currentExperience == null)
                {
                    currentExperience = "0";
                }
                return currentExperience;
            }
            set
            {
                if(currentExperience != value)
                {
                    currentExperience = value;
                }
            }
        }
        string currentLevel;
        public string CurrentLevel
        {
            get
            {
                if(currentLevel == null)
                {
                    currentLevel = "1";
                }
                return currentLevel;
            }
            set
            {
                if(currentLevel != value)
                {
                    currentLevel = value;
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
        public void Train(object sender, RoutedEventArgs e)
        {

        }
        public void Sleep(object sender, RoutedEventArgs e)
        {
            CurrentStamina = "100";
            AddToLog("You went to sleep");
        }
        public void Break(object sender, RoutedEventArgs e)
        {
            int stamina = Convert.ToInt32(CurrentStamina);
            stamina += 50;
            if(stamina > 100)
            {
                stamina = 100;
            }
            CurrentStamina = stamina.ToString();
        }
        public void Shoot(object sender, RoutedEventArgs e)
        {
            int result = (int)(1 / (rand.Next(1, 11) + Math.Round(0.1 * Convert.ToInt32(CurrentLevel))));
            AddToLog("You hit a " + result.ToString());
            CurrentExperience += result;
            AddToLog("You received " + result.ToString() + " experience");
        }
    }
}
