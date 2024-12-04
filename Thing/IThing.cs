using Events;
namespace Things
{
    public interface IThing
    {
        string Barcode { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        event EventHandler<IdChangeEventArgs> IdChanged;
    }
}