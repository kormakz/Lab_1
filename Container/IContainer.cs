using Things;

namespace Containers
{
    public interface IContainer<T> where T : IThing
    {
        T this[int index] { get; set; }

        int Id { get; set; }

        T Pop();
        void Push(T thing);
        int SearchById(int id);
        int SearchByName(string name);
        void SortById();
        void SortByName();
        void Swap(int first, int second);
    }
}