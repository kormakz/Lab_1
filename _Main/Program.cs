using Containers;
using Extensions;
using Things;
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
    //Action<Container<Thing>> delegato = a => Console.WriteLine($"Контейнер {nameof(a)} изменил свой идентификатор на: {a.Id}");
    //a.OnUpdate = a => Console.WriteLine($"Контейнер {nameof(a)} изменил свой идентификатор на: {a.Id}");
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

    Console.WriteLine(a.SearchByName("bsf"));
    Console.WriteLine("end");

    a.SortById();
    a[0].Id = 2; ;
    //a[0].Id(1);
    Console.WriteLine(a);
}

static void Lab4()
{
    Console.WriteLine("Введите размер витрины: "); int capacity = Convert.ToInt32(Console.ReadLine());
    Container<IThing> container = capacity;
    Console.WriteLine("0 - добавить товар на витрину");
    Console.WriteLine("1 - Убрать товар из витрины");
    Console.WriteLine("2 - Поменять Id витрины");
    Console.WriteLine("3 - Поменять Id товара");
    Console.WriteLine("4 - Сортировать витрину по Id");
    Console.WriteLine("5 - Сортировать витрину по имени");
    Console.WriteLine("6 - Найти место товара в витрине по Id");
    Console.WriteLine("7 - Найти место товара в витрине по имени");
    Console.WriteLine("8 - Вывести витрину");
    while (true)
    {
        Console.Write("Введите, что вы хотите сделать: "); int action_selection = Convert.ToInt32(Console.ReadLine());
        switch (action_selection)
        {
            case 0:
                Console.WriteLine("0 - Mouse");
                Console.WriteLine("1 - HyperMouse");
                Console.Write("Какой товар хотите добавить?: "); int thing_selection = Convert.ToInt32(Console.ReadLine());
                switch (thing_selection)
                {
                    case 0:
                        Console.Write("Введите Id мыши: "); int id0 = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Введите Имя мыши: "); string name0 = Console.ReadLine();
                        Console.Write("Введите Цвет мыши: "); string color0 = Console.ReadLine();
                        Console.Write("Введите Бренд мыши: "); string brand0 = Console.ReadLine();
                        Console.Write("Введите Чувствительность мыши: "); int sensetivity0 = Convert.ToInt32(Console.ReadLine());
                        container.Push(new Mouse(id0, name0, color0, brand0, sensetivity0));
                        break;
                    case 1:
                        Console.Write("Введите Id гипермыши: "); int id1 = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Введите Имя гипермыши: "); string name1 = Console.ReadLine();
                        Console.Write("Введите Цвет гипермыши: "); string color1 = Console.ReadLine();
                        Console.Write("Введите Бренд гипермыши: "); string brand1 = Console.ReadLine();
                        Console.Write("Введите Чувствительность гипермыши: "); int sensetivity1 = Convert.ToInt32(Console.ReadLine());
                        container.Push(new HyperMouse(id1, name1, color1, brand1, sensetivity1));
                        break;

                }
                break;
            case 1:
                Console.Write("Введите место товара на витрине для удаления: "); int place = Convert.ToInt32(Console.ReadLine());
                container[place] = null;
                break;
            case 2:
                Console.Write("Введите новый Id витрины: "); int new_container_id = Convert.ToInt32(Console.ReadLine());
                container.Id = new_container_id;
                break;
            case 3:
                Console.Write("Введите место товара на витрине для изменения Id: "); int place_id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите новый Id товара: "); int new_thing_id = Convert.ToInt32(Console.ReadLine());
                IThing tmp = container[place_id];
                tmp.Id = new_thing_id;
                container[place_id] = tmp;
                break;
            case 4:
                container.SortById();
                break;
            case 5:
                container.SortByName();
                break;
            case 6:
                Console.Write("Введите Id товара для поиска: "); int search_id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(container.SearchById(search_id));
                break;
            case 7:
                Console.Write("Введите Имя товара для поиска: "); string search_name = Console.ReadLine();
                Console.WriteLine(container.SearchByName(search_name));
                break;
            case 8:
                Console.WriteLine(container);
                break;

        }
    }
}

Lab4();