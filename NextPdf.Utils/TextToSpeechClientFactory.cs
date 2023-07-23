using Google.Apis.Auth.OAuth2;
using Google.Cloud.TextToSpeech.V1;
using Grpc.Auth;

namespace NextPdf.Utils;

public class TextToSpeechClientFactory
{
    private TextToSpeechClient _textToSpeechClient;
    private GoogleCredential _googleCredential;

    public TextToSpeechClientFactory(string credential_path)
    {
        _googleCredential = GoogleCredential.FromFile(credential_path);
    }

    public TextToSpeechClient Create()
    {
        if (_textToSpeechClient is not null)
        {
            return _textToSpeechClient;
        }

        return new TextToSpeechClientBuilder
        {
            ChannelCredentials = _googleCredential.ToChannelCredentials()
        }.Build();
    }
    
}