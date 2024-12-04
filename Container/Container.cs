using Container;
using Events;
using System;
using System.ComponentModel;
using System.Reflection;
using Things;
namespace Containers
{
    public class Container<T> : IContainer<T> where T : class, IThing
    {
        private int id;
        private int amount;
        private T[] cont;
      
        private Container(int amount)
        {
            this.amount = amount;
            cont = new T[amount];
        }
        private Action<IContainer<T>> OnUpdate { get; set; }


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
            }
        }


        public T this[int index]
        {
            get
            {
                if (index >= amount || index < 0) return null;
                if (cont[index] == null) return null;
                T tmp = cont[index];
                //cont[index].IdChanged -= HandleIdChanged;
                if (tmp != null)
                {
                    OnUpdate -= tmp.UpdateId;
                    tmp.IdChanged -= HandleIdChanged;
                }
                cont[index] = null;
                return tmp;
            }
            set
            {
                if (index >= amount || index < 0) return;
                cont[index] = value;
                if (value != null)
                {
                    value.UpdateId(this);
                    OnUpdate += value.UpdateId;
                    value.IdChanged += HandleIdChanged;
                }
            }
        }
        public void Push(T thing)
        {
            for (int i = 0; i < amount; i++)
            {
                if (cont[i] == null)
                {

                    this[i] = thing;
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
                    cont[i] = null;
                    return tmp;
                }
            }
            return null;
        }
        public void Swap(int first, int second)
        {
            (this[first], this[second]) = (this[second], this[first]);
        }
        public int Search(Predicate<T> func)
        {
            return cont.Where(x => x is { }).ToList().FindIndex(x => func(x));
        }
        public int SearchById(int id)
        {
            return Search(pr => pr.Id == id);
        }
        public int SearchByName(string name)
        {
            return Search(pr => pr.Name == name);
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
            OnUpdate?.Invoke(this);
        }
        public void SortById()
        {
            Sort((i, j) => cont[i].Id.CompareTo(cont[j].Id));
        }
        public void SortByName()
        {
            Sort((i, j) => cont[i].Name.CompareTo(cont[j].Name));
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
            ((T)sender).UpdateId(this);
        }
    }
}
