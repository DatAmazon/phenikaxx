namespace PhenikaaX.Models
{
    public class Element
    {
        public string? Text { get; set; }
        public float FontSize { get; set; }
        public string ImagePath { get; set; }
        public bool IsItalic { get; set; }
    }

    public class TextAndCheckboxOptions
    {
        public int RowIndex { get; set; }
        public int ColIndex { get; set; }
        public string? Title { get; set; }
        public List<Element> Elements { get; set; } = new List<Element>();
    }
}
