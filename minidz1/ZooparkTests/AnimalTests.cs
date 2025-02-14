using System;
using System.Linq;
using Xunit;
using Zoopark;
using Microsoft.Extensions.DependencyInjection;

namespace ZooparkTests;

public class AnimalTests
{
    [Fact]
    public void InvalidParams_ShouldThrowException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Monkey(0, 5, 1, 5));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Rabbit(11, 5, 2, 6));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Tiger(5, 0, 3));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Wolf(5, 11, 4));
    }

    [Fact]
    public void HealthyAdd()
    {
        var service = new ZooService(new AlwaysHealthyVetClinic());
        var m = new Monkey(5, 7, 10, 6, "A1");
        bool added = service.AddAnimal(m);
        Assert.True(added);
        Assert.Equal(1, service.GetAnimalCount());
    }

    [Fact]
    public void UnhealthyAdd()
    {
        var service = new ZooService(new AlwaysUnhealthyVetClinic());
        var t = new Tiger(5, 7, 20, "B1");
        bool added = service.AddAnimal(t);
        Assert.False(added);
        Assert.Equal(0, service.GetAnimalCount());
    }

    [Fact]
    public void InteractiveFilter()
    {
        var service = new ZooService(new AlwaysHealthyVetClinic());
        var r = new Rabbit(5, 7, 31, 5, "C1");
        var m = new Monkey(5, 7, 30, 7, "D1");
        var t = new Tiger(5, 7, 32, "E1");
        service.AddAnimal(r);
        service.AddAnimal(m);
        service.AddAnimal(t);
        var inter = service.GetInteractiveAnimals().ToList();
        Assert.DoesNotContain(r, inter);
        Assert.Contains(m, inter);
        Assert.DoesNotContain(t, inter);
    }

    [Fact]
    public void TotalFood()
    {
        var service = new ZooService(new AlwaysHealthyVetClinic());
        var m = new Monkey(3, 7, 40, 6);
        var r = new Rabbit(4, 7, 41, 7);
        var t = new Tiger(5, 7, 42);
        service.AddAnimal(m);
        service.AddAnimal(r);
        service.AddAnimal(t);
        int total = service.GetTotalFoodConsumption();
        Assert.Equal(12, total);
    }

    [Fact]
    public void InventoryList()
    {
        var service = new ZooService(new AlwaysHealthyVetClinic());
        var m = new Monkey(5, 7, 50, 6, "F1");
        var tbl = new Table("X", 100);
        service.AddAnimal(m);
        service.AddThing(tbl);
        var inv = service.GetInventoryItems().ToList();
        Assert.Contains(m, inv);
        Assert.Contains(tbl, inv);
        Assert.Equal(2, inv.Count());
    }

    [Fact]
    public void DefaultName()
    {
        var m = new Monkey(5, 7, 60, 6);
        var t = new Tiger(5, 7, 61);
        Assert.Equal("Monkey", m.Name);
        Assert.Equal("Tiger", t.Name);
    }

    [Fact]
    public void ThingAdd()
    {
        var service = new ZooService(new AlwaysHealthyVetClinic());
        var comp = new Computer("Y", 200);
        service.AddThing(comp);
        var inv = service.GetInventoryItems().ToList();
        Assert.Contains(comp, inv);
    }

    [Fact]
    public void MultipleAdd()
    {
        var service = new ZooService(new AlwaysHealthyVetClinic());
        var m = new Monkey(5, 7, 300, 6, "G1");
        var r = new Rabbit(4, 7, 301, 7, "H1");
        service.AddAnimal(m);
        service.AddAnimal(r);
        Assert.Equal(2, service.GetAnimalCount());
    }

    [Fact]
    public void EmptyZoo()
    {
        var service = new ZooService(new AlwaysHealthyVetClinic());
        Assert.Equal(0, service.GetAnimalCount());
        Assert.Equal(0, service.GetTotalFoodConsumption());
        Assert.Empty(service.GetInteractiveAnimals());
        Assert.Empty(service.GetInventoryItems());
    }

    [Fact]
    public void FoodProperty()
    {
        var m = new Monkey(8, 7, 101, 6, "I1");
        Assert.Equal(8, m.Food);
    }

    [Fact]
    public void HealthProperty()
    {
        var t = new Tiger(5, 9, 102, "J1");
        Assert.Equal(9, t.Health);
    }

    [Fact]
    public void InteractiveThreshold()
    {
        var service = new ZooService(new AlwaysHealthyVetClinic());
        var r = new Rabbit(5, 7, 103, 5, "K1");
        var r2 = new Rabbit(5, 7, 104, 6, "L1");
        service.AddAnimal(r);
        service.AddAnimal(r2);
        var inter = service.GetInteractiveAnimals().ToList();
        Assert.DoesNotContain(r, inter);
        Assert.Contains(r2, inter);
    }

    [Fact]
    public void InventoryOrder()
    {
        var service = new ZooService(new AlwaysHealthyVetClinic());
        var m = new Monkey(5, 7, 105, 6, "M1");
        var tbl = new Table("Z", 200);
        service.AddAnimal(m);
        service.AddThing(tbl);
        var inv = service.GetInventoryItems().ToList();
        Assert.Equal(m, inv.First());
        Assert.Equal(tbl, inv.Last());
    }

    [Fact]
    public void DuplicateThing()
    {
        var service = new ZooService(new AlwaysHealthyVetClinic());
        var tbl = new Table("Z", 300);
        service.AddThing(tbl);
        service.AddThing(tbl);
        var inv = service.GetInventoryItems().ToList();
        Assert.Equal(2, inv.Count());
        Assert.All(inv, item => Assert.IsType<Table>(item));
    }

    [Fact]
    public void PredatorNotInteractive()
    {
        var service = new ZooService(new AlwaysHealthyVetClinic());
        var w = new Wolf(6, 7, 400, "N1");
        service.AddAnimal(w);
        var inter = service.GetInteractiveAnimals().ToList();
        Assert.DoesNotContain(w, inter);
    }

    [Fact]
    public void DIContainerTest()
    {
        var provider = DependencyInjection.Configure();
        var service = provider.GetRequiredService<IZooService>();
        var clinic = provider.GetRequiredService<IVetClinic>();
        Assert.NotNull(service);
        Assert.NotNull(clinic);
        Assert.IsType<ZooService>(service);
        Assert.IsType<VetClinic>(clinic);
    }
}

class AlwaysHealthyVetClinic : IVetClinic
{
    public bool CheckAnimalHealth(Animal animal) => true;
}

class AlwaysUnhealthyVetClinic : IVetClinic
{
    public bool CheckAnimalHealth(Animal animal) => false;
}
