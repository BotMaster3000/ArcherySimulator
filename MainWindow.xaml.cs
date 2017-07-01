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
    }
}
