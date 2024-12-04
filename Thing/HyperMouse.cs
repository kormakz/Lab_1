using Barcodes;
using Events;
using System.Security.Cryptography;

namespace Things
{
    public sealed class HyperMouse : Mouse
    {
        public override int Id { get => id; set { id = value; } }
        public override string Barcode { get => barcode.Text; set {  barcode.Text = id.ToString(); barcode.Text = barcode.Text;  } }
        public HyperMouse(int id, string name, string color, string brand, int sensitivity) : base(id, name, color, brand, sensitivity)
        {
            barcode = new Barcode_Record();
            Id = id;
        }

    }
}
