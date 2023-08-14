using System.Text;
using Novacode;

namespace NextPDF.Services;

public class WordService
{
    public async Task<string> ParseWordFileAsync(MemoryStream stream)
    {
        using DocX doc = DocX.Load(stream);
        return doc.Text;
    }
}