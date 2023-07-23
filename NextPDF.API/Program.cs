using NextPDF.Options;
using NextPDF.Services;
using NextPdf.Utils;
using OpenAI.GPT3.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<OpenAIOptions>(
    builder.Configuration.GetSection(OpenAIOptions.OpenAI)
);

builder.Services.AddScoped<OpenAIFactory>(src =>
    new OpenAIFactory(builder.Configuration.GetSection(OpenAIOptions.OpenAI).Get<OpenAIOptions>().ApiKey));


builder.Services.AddOpenAIService();
builder.Services.AddScoped<PdfService>();
builder.Services.AddScoped<TextToSpeechClientFactory>(scope =>
{
    return new TextToSpeechClientFactory(builder.Configuration.GetSection("Google_Credentials_Json_Path")
        .Get<string>());
});
builder.Services.AddScoped<SpeechClientFactory>(scope =>
{
    return new SpeechClientFactory(builder.Configuration.GetSection("Google_Credentials_Json_Path")
        .Get<string>());
});
builder.Services.AddScoped<TextToSpeechService>();
builder.Services.AddScoped<WordService>();
builder.Services.AddScoped<SpeechToTextService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();