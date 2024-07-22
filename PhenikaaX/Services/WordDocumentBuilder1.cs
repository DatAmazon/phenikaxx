using PhenikaaX.Models;
using Spire.Barcode;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Drawing;

namespace PhenikaaX.Services.BuiderService
{
    public class WordDocumentBuilder1
    {
        private readonly Document _document;
        private readonly Section _section;
        private readonly Table _table;
        private readonly float _pageWidth;

        public WordDocumentBuilder1(Document document, Section section, Table table, int rowsNum, int columnsNum)
        {
            _document = document;
            _section = section;
            _table = table;

            _table.ResetCells(rowsNum, columnsNum);
            _table.TableFormat.Borders.BorderType = BorderStyle.None;
            _pageWidth = _section.PageSetup.PageSize.Width - _section.PageSetup.Margins.Left - _section.PageSetup.Margins.Right;

            for (int rowIndex = 0; rowIndex < _table.Rows.Count; rowIndex++)
            {
                _table.Rows[rowIndex].Cells[0].SetCellWidth(_pageWidth * 0.4f, CellWidthType.Point);
                _table.Rows[rowIndex].Cells[1].SetCellWidth(_pageWidth * 0.3f, CellWidthType.Point);
                _table.Rows[rowIndex].Cells[2].SetCellWidth(_pageWidth * 0.3f, CellWidthType.Point);
            }
        }

        public WordDocumentBuilder1 InsertRowToTable(TextStyleOptions rowInsert)
        {
            TableRow row = _table.Rows[rowInsert.rowIndex];
            Paragraph paragraph = row.Cells[rowInsert.cellIndex].AddParagraph();
            if (rowInsert.leftIndent > 0)
            {
                paragraph.Format.LeftIndent = rowInsert.leftIndent;
            }
            TextRange textRange = paragraph.AppendText(rowInsert.text);
            textRange.CharacterFormat.FontSize = rowInsert.fontSize;
            textRange.CharacterFormat.Bold = rowInsert.bold ?? false;
            textRange.CharacterFormat.Italic = rowInsert.italic ?? false;
            if (rowInsert.textColor.HasValue)
                textRange.CharacterFormat.TextColor = rowInsert.textColor.Value;
            if (!string.IsNullOrEmpty(rowInsert.binding))
            {
                TextRange bindingTextRange = paragraph.AppendText(rowInsert.binding);
                bindingTextRange.CharacterFormat.FontSize = rowInsert.underlineStyle.HasValue ? 9 : 11;
                bindingTextRange.CharacterFormat.UnderlineStyle = rowInsert.underlineStyle ?? UnderlineStyle.None;
                bindingTextRange.CharacterFormat.Bold = rowInsert.boldBinding ?? false;
            }
            paragraph.Format.HorizontalAlignment = rowInsert.alignment ?? HorizontalAlignment.Left;
            return this;
        }
        public WordDocumentBuilder1 InsertTextToCellWithManyDifferentStyle(MultiStyleTextRow text)
        {
            if (text.texts?.Length != text.fontSizes?.Length
                || (text.bolds != null && text.texts?.Length != text.bolds.Length)
                || (text.italics != null && text.texts?.Length != text.italics.Length))
            {
                throw new ArgumentException("Array lengths must match.");
            }

            TableRow row = _table.Rows[text.rowIndex];
            Paragraph paragraph = row.Cells[text.cellIndex].AddParagraph();
            paragraph.Format.HorizontalAlignment = text.alignment ?? HorizontalAlignment.Left;
            if (text.LeftIndent > 0)
                paragraph.Format.LeftIndent = (int)text.LeftIndent;

            for (int i = 0; i < text.texts?.Length; i++)
            {
                TextRange textRange = paragraph.AppendText(text.texts[i]);
                textRange.CharacterFormat.FontSize = text.fontSizes[i];

                if (text.bolds != null)
                    textRange.CharacterFormat.Bold = text.bolds[i];

                if (text.italics != null)
                    textRange.CharacterFormat.Italic = text.italics[i];
            }
            return this;
        }

        public WordDocumentBuilder1 GenerateBarcode(BarcodeOptions barcode)
        {
            BarcodeSettings bs = new BarcodeSettings();// create barcode
            bs.Type = barcode.barcodeType;
            bs.Data = barcode.barcodeData;
            BarCodeGenerator bg = new BarCodeGenerator(bs);

            Image barcodeImage = bg.GenerateImage();  // save barcode in an image
            barcodeImage.Save(barcode.imagePath);
            DocPicture picture = _table.Rows[barcode.rowIndex].Cells[barcode.colIndex].AddParagraph().AppendPicture(barcodeImage) as DocPicture;
            picture.Width = barcode.imageWidth;
            return this;
        }
        public WordDocumentBuilder1 DrawBorderAround(int startRow, int endRow, int cellIndex)
        {
            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                TableCell cell = _table.Rows[rowIndex].Cells[cellIndex];
                if (rowIndex == startRow)
                    cell.CellFormat.Borders.Top.BorderType = BorderStyle.Single;

                if (rowIndex == endRow)
                    cell.CellFormat.Borders.Bottom.BorderType = BorderStyle.Single;

                cell.CellFormat.Borders.Left.BorderType = BorderStyle.Single;
                cell.CellFormat.Borders.Right.BorderType = BorderStyle.Single;
            }
            return this;
        }

        public WordDocumentBuilder1 InsertImageToWord(Paragraph paragraph, string imagePath)
        {
            DocPicture pic = paragraph.AppendPicture(Image.FromFile(imagePath));
            pic.Width = 10;
            pic.Height = 10;
            return this;
        }
        public WordDocumentBuilder1 MergeCellHorizontal(int rowIndex, params (int colIndex, CellMerge mergeValue)[] mergeParams)
        {
            foreach (var (colIndex, mergeValue) in mergeParams)
            {
                _table.Rows[rowIndex].Cells[colIndex].CellFormat.HorizontalMerge = mergeValue;
            }
            return this;
        }

        public WordDocumentBuilder1 InsertTextAndCheckbox(TextAndCheckboxOptions options)
        {
            Paragraph paragraph = _table.Rows[options.RowIndex].Cells[options.ColIndex].AddParagraph();
            if (!string.IsNullOrEmpty(options.Title))
            {
                paragraph.Format.LeftIndent = 10;
                TextRange objectPatientTR = paragraph.AppendText(options.Title);
                objectPatientTR.CharacterFormat.FontSize = 11;
            }

            foreach (var element in options.Elements)
            {
                TextRange textRange = paragraph.AppendText(element.Text);
                textRange.CharacterFormat.FontSize = element.FontSize;
                textRange.CharacterFormat.Italic = element.IsItalic;
                this.InsertImageToWord(paragraph, element.ImagePath);
            }
            return this;
        }

        public Document Build()
        {
            return _document;
        }

        public void Save(string filePath)
        {
            _document.SaveToFile(filePath, FileFormat.Docx2013);
            _document.Close();
        }
    }
}
