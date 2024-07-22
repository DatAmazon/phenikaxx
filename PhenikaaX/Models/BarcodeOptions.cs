using Spire.Barcode;

namespace PhenikaaX.Models
{
    public class BarcodeOptions
    {
        public int rowIndex {  get; set; }
        public int colIndex { get; set; }
        public BarCodeType barcodeType { get; set; }
        public string barcodeData { get; set; }
        public string imagePath { get; set; }
        public float imageWidth { get; set; }
    }
}
