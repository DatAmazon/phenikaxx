using PhenikaaX.Models;
using Spire.Doc;
using Spire.Doc.Documents;

namespace PhenikaaX.Interfaces
{
    public interface IMedicalBillWordDocumentService
    {
        void InsertRowToTable(TextStyleOptions rowInsert);
        void InsertTextToCellWithManyDifferentStyle(MultiStyleTextRow text);
        void GenerateBarcode(BarcodeOptions barcode);
        void DrawBorderAround(int startRow, int endRow, int cellIndex);
        void InsertImageToWord(Paragraph paragraph, string imagePath);
        void MergeCellHorizontal(int rowIndex, params (int colIndex, CellMerge mergeValue)[] mergeParams);
        void InsertTextAndCheckbox(TextAndCheckboxOptions options);
        void GenderAndJob(string gender, string job);
        void SavePdf();
    }
}
