using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ArcherySimulator.ViewModels;

namespace ArcherySimulator.Views;

public partial class ArcheryView : UserControl
{
    public ArcheryView()
    {
        InitializeComponent();
        DataContext = new ArcheryViewModel();
    }
}
