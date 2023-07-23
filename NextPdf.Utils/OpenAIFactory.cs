using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;

namespace NextPdf.Utils;

public class OpenAIFactory
{
    private IOpenAIService _openAiApi;

    private string _key = string.Empty;

    public OpenAIFactory(string key)
    {
        _key = key;
    }

    public IOpenAIService Create()
    {
        if (_openAiApi is not null)
        {
            return _openAiApi;
        }

        return _openAiApi = new OpenAIService(settings: new OpenAiOptions()
        {
            ApiKey = _key,
        });
    }
}