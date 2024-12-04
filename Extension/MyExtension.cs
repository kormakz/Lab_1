namespace Extensions
{
    using Barcodes;
    using Things;
    using Containers;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public static class Extension 
    {
        public static void UpdateId<T>(this T thing,IContainer<T> container) where T : IThing
        {
            thing.Barcode = thing.Id.ToString() + " " + container.Id + " " + container.SearchById(thing.Id);
        }
    }
}
