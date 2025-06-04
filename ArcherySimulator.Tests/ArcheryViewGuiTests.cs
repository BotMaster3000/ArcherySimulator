using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Headless;
using ArcherySimulator.Views;
using ArcherySimulator.ViewModels;

namespace ArcherySimulator.Tests;

[TestClass]
public class ArcheryViewGuiTests
{
    [TestMethod]
    public async Task BreakButton_Disables_After_Command()
    {
        using var session = HeadlessUnitTestSession.StartNew(typeof(Program));
        await session.Dispatch(() =>
        {
            var view = new ArcheryView();
            var grid = (Grid)view.Content!;
            var breakButton = grid.Children.OfType<Button>()
                .First(c => (string?)c.Content == "Break");

            Assert.IsTrue(breakButton.IsEnabled);
            breakButton.Command!.Execute(null);
            Assert.IsFalse(breakButton.IsEnabled);
        });
    }

    [TestMethod]
    public async Task View_Has_All_Action_Buttons()
    {
        using var session = HeadlessUnitTestSession.StartNew(typeof(Program));
        await session.Dispatch(() =>
        {
            var view = new ArcheryView();
            var grid = (Grid)view.Content!;
            var buttons = grid.Children.OfType<Button>().Select(b => b.Content?.ToString()).ToList();
            CollectionAssert.AreEquivalent(new[] { "Train", "Sleep", "Shoot", "Break" }, buttons);
        });
    }
}
