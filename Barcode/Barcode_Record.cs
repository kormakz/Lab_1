namespace Barcodes;

public record class Barcode_Record : IBarcode
{
    private string text;
    private string barcode;
    public static BarcodeType BarcodeType = BarcodeType.Full;
    public string Text
    {
        get => text;
        set
        {
            if (value == text) return;
            text = value;
            barcode = BarcodeHelper.GetCode(value);
        }
    }
    public override string ToString()
    {
        return BarcodeType switch
        {
            BarcodeType.Text => text,
            BarcodeType.Barcode => barcode,
            _ => barcode + '\n' + string.Concat(Enumerable.Repeat(" ", ((barcode.Length - 6) / 6) / 2 - text.Length / 2)) + "* " + text + " *" + '\n',

        };
    }
}
