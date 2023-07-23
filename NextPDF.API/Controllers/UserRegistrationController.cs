using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NextPDF.Options;

namespace NextPDF.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserRegistrationController: ControllerBase
{
    private readonly OpenAIOptions _openAi;

    public UserRegistrationController(IOptions<OpenAIOptions> openAiOptions)
    {
        _openAi = openAiOptions.Value;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_openAi.ApiKey);
    }
}