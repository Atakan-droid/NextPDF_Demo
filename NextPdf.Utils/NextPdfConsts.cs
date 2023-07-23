using Google.Cloud.TextToSpeech.V1;

namespace NextPdf.Utils;

public class NextPdfConsts
{
    public const string DefaultLanguageCode = "tr-TR";
    public const SsmlVoiceGender DefaultSsmlGender = SsmlVoiceGender.Male;
    public const AudioEncoding DefaultAudioEncoding = AudioEncoding.Linear16;
}