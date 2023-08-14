// See https://aka.ms/new-console-template for more information


using CognitiveHelper;
using SpeechRecognition.ConsoleApp;


var speechToText = new SpeechRecognitionService(Constants.speechKey, Constants.speechRegion);
await speechToText.SpeechToTextAsync("en-US");

await speechToText.SpeechTranslationAsync("en-US", "es");

await speechToText.TextToSpeechAsync("es-MX-HildaRUS");