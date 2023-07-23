using System.Threading.Channels;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Speech.V1;
using Grpc.Auth;
using Grpc.Core;

namespace NextPdf.Utils;

public class SpeechClientFactory
{
    private readonly SpeechClient _speechClient;

    private GoogleCredential _googleCredential;

    public SpeechClientFactory(string credential_path)
    {
        _googleCredential = GoogleCredential.FromFile(credential_path);
        ;
    }

    public SpeechClient Create()
    {
        if (_speechClient is not null)
        {
            return _speechClient;
        }

        var client = new SpeechClientBuilder
        {
            ChannelCredentials = _googleCredential.ToChannelCredentials()
        };
        return client.Build();
    }
}