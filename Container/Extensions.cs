using Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Things;

namespace Container
{
    public static class Extension
    {
        public static void UpdateId<T>(this T thing, IContainer<T> container) where T : IThing
        {
            thing.Barcode = thing.Id.ToString() + " " + container.Id + " " + container.SearchById(thing.Id);
        }
    }
}
