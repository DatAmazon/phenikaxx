using Spire.Doc;
using Spire.Doc.Documents;

namespace PhenikaaX.Models
{
    public class MultiStyleTextRow
    {
        public int rowIndex {  get; set; }
        public int cellIndex { get; set; }
        public int? LeftIndent { get; set; }
        public HorizontalAlignment? alignment { get; set; }
        public string[]? texts { get; set; }
        public float[]? fontSizes { get; set; }
        public bool[]? bolds { get; set; }
        public bool[]? italics { get; set; }
    }
}
