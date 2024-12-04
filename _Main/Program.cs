using Containers;
using Things;
using Extensions;
static void TestLab2()
{
    Container<Thing> showcase = 5;

    var sample = new Mouse(4000, "Dragon", "red", "MSI", 40000);
    var lab2Data = new List<Thing>
    {
        new Mouse(3000, "asd", "blu", "Intel", 3200),
        new Mouse(2000, "dsa", "ulb", "Xiomi", 6400),
        new Mouse(4000, "sad", "green", "CHSU", 12800)
    };
    foreach (var thing in lab2Data)
    {
        showcase.Push(thing);
    }
    showcase[4] = sample;
    sample.Id++;
    Console.WriteLine(sample);
    showcase.SortByName();
    showcase.Id++;
    Console.WriteLine(showcase);
}

static void TestLab3()
{
    var lab3Data = new List<Thing>
    {
        new Mouse(3000, "asd", "blu", "Intel", 3200),
        new Mouse(2000, "dsa", "ulb", "Xiomi", 6400),
        new Mouse(4000, "sad", "green", "CHSU", 12800)
    };
    var lab3Data2 = new List<HyperMouse>
    {
        new (5555, "bsf", "red", "CHSU", 2071),
        new (6666, "sabtebed", "purple", "CHSU", 860),
    };

    IContainer<IThing> a1 = (Container<IThing>)7; a1.Id = 1;
    IContainer<HyperMouse> a2 = (Container<HyperMouse>)3; a2.Id = 10;

    foreach (var thing in lab3Data)
    {
        a1.Push(thing);
    }
    foreach (var thing in lab3Data2)
    {
        a1.Push(thing);
    }
    a1.SortByName();

    var sample1 = new HyperMouse(7777, "HyperDragon", "red", "MSI", 2257);
    var sample2 = new Mouse(4000, "Dragon", "red", "MSI", 40000);

    a2[0] = sample1;
    a1[5] = a2[0];
    a1[6] = sample2;

    a1.Id = 2;
    sample1.Id++;
    sample2.Id++;

    Console.WriteLine(a1);
    a2[0] = (HyperMouse)a1[5];
    Console.WriteLine("####################################################################33");
    Console.WriteLine(a2);
}

static void TestLab4()
{
    Container<Thing> a = 10;
    //todo форматированный вывод
    Action<Container<Thing>> delegato = a => Console.WriteLine($"Контейнер {nameof(a)} изменил свой идентификатор на: {a.Id}");
    a.OnUpdate = delegato;
    a.Id = 14;
    var lab3Data = new List<Thing>
    {
        new Mouse(3000, "asd", "blu", "Intel", 3200),
        new Mouse(2000, "dsa", "ulb", "Xiomi", 6400),
        new Mouse(4000, "sad", "green", "CHSU", 12800)
    };
    var lab3Data2 = new List<HyperMouse>
    {
        new (5000, "bsf", "red", "CHSU", 2071),
        new (6666, "sabtebed", "purple", "CHSU", 860),
    };
    foreach (var thing in lab3Data)
    {
        a.Push(thing);
    }
    foreach (var thing in lab3Data2)
    {
        a.Push(thing);
    }
    a[2].Id = 5;

    //Console.WriteLine(a.SearchByName("bsf"));
    //Console.WriteLine("end");

    a.SortById();
    //Console.WriteLine(a);
    Console.WriteLine("end");
    a[0].Id = 2; ;
    a[0].Id(1);
    Console.WriteLine(a[0]);
    //Console.WriteLine(a);
}
TestLab4();