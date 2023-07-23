using Google.Cloud.TextToSpeech.V1;
using Google.Protobuf;
using NextPdf.Utils;

namespace NextPDF.Services;

public class TextToSpeechService
{
    private readonly TextToSpeechClientFactory _factory;

    public TextToSpeechService(TextToSpeechClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<ByteString> GetAudioContentAsync(string inputText)
    {
        var client = _factory.Create();

        var input = new SynthesisInput
        {
            Text = inputText,
        };

        var voice = new VoiceSelectionParams
        {
            LanguageCode = NextPdfConsts.DefaultLanguageCode,
            SsmlGender = NextPdfConsts.DefaultSsmlGender
        };

        var audioConfig = new AudioConfig
        {
            AudioEncoding = NextPdfConsts.DefaultAudioEncoding
        };

        var response = await client.SynthesizeSpeechAsync(input, voice, audioConfig);

        if (response?.AudioContent is null)
        {
            throw new ApplicationException("Audio content is null");
        }

        return response.AudioContent;
    }
}