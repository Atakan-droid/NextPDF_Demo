using Microsoft.AspNetCore.Mvc;
using NextPDF.Services;

namespace NextPDF.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SpeechToTextController : ControllerBase
{
    private readonly SpeechToTextService _speechToTextService;

    public SpeechToTextController(SpeechToTextService speechToTextService)
    {
        _speechToTextService = speechToTextService;
    }

    [HttpPost]
    public async Task<ActionResult> PostFileToText(IFormFile file)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var fileBytes = ms.ToArray();

        var extractedString = await _speechToTextService.SpeechToTextAsync(fileBytes);

        return Ok(extractedString);
    }
}