using Recognition.Options;
using Recognition.Services;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions<OcrOption>()
       .Bind(builder.Configuration.GetSection(OcrOption.SectionName))
       .ValidateDataAnnotations()
       .ValidateOnStart();
builder.Services.AddTransient<IRecognitionService, RecognitionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/ocr", async (IFormFile file, IRecognitionService recognitionService) => {
    var value = await recognitionService.GetTextAsync(file);
    return Results.Ok(new { Texto = value });
})
.Accepts<IFormFile>("multipart/form-data")
.Produces(200);

app.Run();