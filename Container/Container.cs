using Events;
using System;
using System.Reflection;
using Things;
namespace Containers
{
    public class Container<T> : IContainer<T> where T : IThing
    {
        private int id;
        private int amount;
        private T[] cont;
        private void ChangeBarcode(T thing, int number)
        {
            if (thing == null) return;
            thing.Barcode = thing.Id.ToString() + " " + id + " " + number;
        }
        private void ChangeBarcode()
        {
            for (int i = 0; i < amount; i++)
            {
                //if (cont[i] != null)
                ChangeBarcode(cont[i], i);
            }
        }
        private Container(int amount)
        {
            this.amount = amount;
            cont = new T[amount];
        }
        public Action<Container<T>> OnUpdate { get; set; }


        public static implicit operator Container<T>(int amount)
        {
            return new Container<T>(amount);
        }
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnUpdate?.Invoke(this);
                ChangeBarcode();
            }
        }


        public T this[int index]
        {
            get
            {
                if (index >= amount || index < 0) return default(T);
                if (cont[index] == null) return default(T);
                T tmp = cont[index]; 
                //cont[index].IdChanged -= HandleIdChanged;
                //cont[index] = default(T);
                return tmp;
            }
            set
            {
                if (index >= amount || index < 0) return;
                cont[index] = value;
                ChangeBarcode(cont[index], index);
            }
        }
        public void Push(T thing)
        {
            for (int i = 0; i < amount; i++)
            {
                if (cont[i] == null)
                {

                    this[i] = thing;
                    cont[i].IdChanged += HandleIdChanged;
                    return;
                }
            }
        }
        public T Pop()
        {
            T tmp;
            for (int i = amount - 1; i >= 0; i--)
            {
                if (cont[i] != null)
                {
                    tmp = cont[i];
                    cont[i].IdChanged -= HandleIdChanged;
                    cont[i] = default(T);
                    return tmp;
                }
            }
            return default(T);
        }
        public void Swap(int first, int second)
        {
            (this[first], this[second]) = (this[second], this[first]);
        }
        //todo Часть 3,4 сделано
        public int Search(Predicate<T> func)
        {
            //return Array.IndexOf(cont,func);

            for (int i = 0; i < amount; i++)
            {
                if (func(cont[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        public int SearchById(int id)
        {
            return Search(pr => pr.Id == id);

            //for (int i = 0; i < amount; i++)
            //{
            //    if (cont[i].Id == id)
            //    {
            //        return i;
            //    }
            //}
            //return -1;
        }
        public int SearchByName(string name)
        {
            return Search(pr => pr.Name == name);

            //for (int i = 0; i < amount; i++)
            //{
            //    if (cont[i].Name == name)
            //    {
            //        return i;
            //    }
            //}
            //return -1;
        }
        public void Sort(Func<int, int, int> func)
        {
            for (int i = 0; i < amount; i++)
            {
                for (int j = i + 1; j < amount; j++)
                {
                    if (cont[j] != null && cont[i] != null)
                    {
                        if (func(i, j) == 1)
                        {
                            Swap(i, j);
                        }
                    }
                }
            }
            //Array.Sort(cont, (x, y) =>
            //{
            //    if (x == null) return 1;
            //    if (y == null) return -1;
            //    return func(cont[0])
            //    //x.Id.CompareTo(y.Id);
            //});
            ChangeBarcode();
        }
        public void SortById()
        {
            Sort((i, j) => cont[i].Id.CompareTo(cont[j].Id));
            //for(int i = 0;i < amount; i++)
            //{
            //    for (int j = i + 1; j < amount; j++)
            //    {
            //        if (cont[j] != null && cont[i]!= null)
            //        {
            //            int temp = cont[i].Id.CompareTo(cont[j].Id);
            //            if (temp == 1)
            //            {
            //                Swap(i, j);
            //            }
            //        }
            //    }
            //}
            //Array.Sort(cont, (x, y) =>
            //{
            //    if (x == null) return 1;
            //    if (y == null) return -1;
            //    return x.Id.CompareTo(y.Id);
            //});
            //ChangeBarcode();
        }
        public void SortByName()
        {
            Sort((i, j) => cont[i].Name.CompareTo(cont[j].Name));
            //for (int i = 0; i < amount; i++)
            //{
            //    for (int j = i + 1; j < amount; j++)
            //    {
            //        if (cont[j] != null && cont[i] != null)
            //        {
            //            int temp = cont[i].Name.CompareTo(cont[j].Name);
            //            if (temp == 1)
            //            {
            //                Swap(i, j);
            //            }
            //        }
            //    }
            //}
            //Array.Sort(cont, (x, y) =>
            //{
            //    if (x == null) return 1;
            //    if (y == null) return -1;
            //    return x.Name.CompareTo(y.Name);
            //});
            //ChangeBarcode();
        }

        public override string ToString()
        {
            string tmp = "";
            for (int i = 0; i < amount; i++)
            {
                if (cont[i] == null)
                {
                    tmp += '\n' + "Пусто место номер: " + i + '\n' + '\n';
                }
                else
                {
                    tmp += "Место номер: " + i + '\n';
                    tmp += cont[i].ToString();
                }
            }

            return tmp;
        }
        private void HandleIdChanged(object sender, IdChangeEventArgs args)
        {
            Console.WriteLine($"Товар изменил свой идентификатор с {args.OldId} на {args.NewId}");
            ChangeBarcode(); 
        }
    }
}
