using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace NextPDF.Services;

public class PdfService
{
    public async Task<string> ExtractTextFromPdfAsync(byte[] fileBytes)
    {
        using var reader = new PdfReader(fileBytes);

        var text = new StringBuilder();
        for (int page = 1; page <= reader.NumberOfPages; page++)
        {
            string currentText = PdfTextExtractor.GetTextFromPage(reader, page);

            currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8,
                Encoding.Default.GetBytes(currentText)));
            text.Append(currentText);
        }

        reader.Close();

        return text.ToString();
    }
}