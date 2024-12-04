using Barcodes;
using Events;

namespace Things
{
    public abstract class Thing : IThing
    {
        protected int id;
        protected string name;

        protected IBarcode barcode = new Barcode();

        public event EventHandler<IdChangeEventArgs> IdChanged;

        public virtual int Id
        {
            get => id;
            set
            {
                int old_id = id;
                id = value;
                barcode.Text = id.ToString();
                IdChanged?.Invoke(this, new IdChangeEventArgs(old_id, id));
            }
        }
        public string Name { get => name; set { name = value; } }
        public virtual string Barcode { get => barcode.Text; set { barcode.Text = value; } }
        public override string ToString()
        {
            return barcode.ToString();
        }
        protected Thing(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
