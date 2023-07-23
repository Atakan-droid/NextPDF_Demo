using Google.Cloud.Speech.V1;
using Google.Protobuf;
using NextPdf.Utils;

namespace NextPDF.Services;

public class SpeechToTextService
{
    private readonly SpeechClientFactory _factory;

    public SpeechToTextService(SpeechClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<string> SpeechToTextAsync(byte[] byteArray)
    {
        var client = _factory.Create();

        var result = await client.RecognizeAsync(new RecognizeRequest()
        {
            Audio = new RecognitionAudio()
            {
                Content = ByteString.CopyFrom(byteArray),
            },
            Config = new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 24000,
                LanguageCode = NextPdfConsts.DefaultLanguageCode,
            }
        });

        var operation = result.Results;

        return operation.ToString();
    }
}