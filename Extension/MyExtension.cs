namespace Extensions
{
    using Barcodes;
    using Things;
    public static class Extension 
    {
        public static void Id<T>(this T thing,int id) where T : IThing
        {
            thing.Id = id;
            Console.WriteLine($"Товар {thing.Name} изменил свой идентификатор на: {thing.Id}");
        }
    }
}
