using Avalonia.Controls;

namespace ArcherySimulator;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
    }
}
