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
    private readonly WordService _wordService;

    public DocumentToSpeechController(OpenAIFactory openAi,
        PdfService pdfService, TextToSpeechService textToSpeechService, WordService wordService)
    {
        _openAi = openAi;
        _pdfService = pdfService;
        _textToSpeechService = textToSpeechService;
        _wordService = wordService;
    }

    [HttpPost]
    public async Task<ActionResult> PostFileToSpeech(IFormFile file)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var fileBytes = ms.ToArray();

        var extractedString = "";
        if (file.FileName.Contains(".pdf"))
        {
            extractedString = await _pdfService.ExtractTextFromPdfAsync(fileBytes);
        }
        else if (file.FileName.Contains(".doc") && file.FileName.Contains(".docx"))
        {
            extractedString = await _wordService.ParseWordFileAsync(ms);
        }
        else
        {
            throw new ApplicationException("File type not supported");
        }

        var audioContent = await _textToSpeechService.GetAudioContentAsync(extractedString);


        using var output = System.IO.File.Create("audio_result.mp3");
        audioContent.WriteTo(output);


        return Ok(audioContent.ToByteArray());
    }
}