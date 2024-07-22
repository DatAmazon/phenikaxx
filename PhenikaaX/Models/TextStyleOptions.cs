using Spire.Doc.Documents;
using System.Drawing;

namespace PhenikaaX.Models
{
    public class TextStyleOptions
    {
        public int rowIndex;
        public int cellIndex;
        public float leftIndent;
        public string? text;
        public float fontSize;
        public bool? bold;
        public bool? italic;
        public Color? textColor;
        public HorizontalAlignment? alignment;
        public string? binding;
        public UnderlineStyle? underlineStyle;
        public bool? boldBinding;
    }
}
