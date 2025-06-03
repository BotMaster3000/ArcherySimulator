using ArcherySimulator.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArcherySimulator.Tests;

[TestClass]
public class ArcheryViewModelTests
{
    [TestMethod]
    public void Level_Increases_When_Experience_Reaches_100()
    {
        var vm = new ArcheryViewModel();
        vm.Experience = 99;
        int initialLevel = vm.Level;

        vm.Experience += 1; // triggers level up

        Assert.AreEqual(initialLevel + 1, vm.Level);
        Assert.AreEqual(0, vm.Experience);
    }

    [TestMethod]
    public void Level_Decreases_But_Not_Below_One_When_Sleeping()
    {
        var vm = new ArcheryViewModel();
        vm.Level = 2;
        vm.Experience = 0;

        vm.Sleep();

        Assert.AreEqual(1, vm.Level);
        Assert.AreEqual(50, vm.Experience);

        vm.Sleep();

        Assert.AreEqual(1, vm.Level);
        Assert.AreEqual(0, vm.Experience);
    }

    [TestMethod]
    public void Event_Log_Trimmed_To_Ten_Entries()
    {
        var vm = new ArcheryViewModel();

        for (int i = 0; i < 11; i++)
        {
            vm.Sleep();
        }

        Assert.AreEqual(10, vm.EventLog.Count);
    }
}
