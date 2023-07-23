using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.TextToSpeech.V1;
using Grpc.Auth;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Mvc;
using NextPDF.Services;
using NextPdf.Utils;
using OpenAI.API.Completions;
using OpenAI.API.Models;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace NextPDF.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DocumentToSpeechController : ControllerBase
{
    private readonly OpenAIFactory _openAi;
    private readonly PdfService _pdfService;
    private readonly TextToSpeechService _textToSpeechService;

    public DocumentToSpeechController(OpenAIFactory openAi,
        PdfService pdfService, TextToSpeechService textToSpeechService)
    {
        _openAi = openAi;
        _pdfService = pdfService;
        _textToSpeechService = textToSpeechService;
    }

    [HttpPost]
    public async Task<ActionResult> PostFileToSpeech(IFormFile file)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var fileBytes = ms.ToArray();

        var extractedString = await _pdfService.ExtractTextFromPdfAsync(fileBytes);

        var audioContent = await _textToSpeechService.GetAudioContentAsync(extractedString);

#if DEBUG
        using var output = System.IO.File.Create("hello.mp3");
        audioContent.WriteTo(output);
#endif

        return Ok(audioContent.ToByteArray());
    }
}