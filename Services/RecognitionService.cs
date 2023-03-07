using Tesseract;
using Recognition.Extensions;
using Microsoft.Extensions.Options;
using Recognition.Options;
using System.Net.Mime;

namespace Recognition.Services
{
    public sealed class RecognitionService : IRecognitionService
    {
        private readonly OcrOption _ocrOptions;
        public RecognitionService(IOptions<OcrOption> _options)
            => _ocrOptions = _options.Value;

        public async Task<string> GetTextAsync(IFormFile file)
        {
            return file.ContentType switch
            {
                MediaTypeNames.Application.Pdf => await OcrFromPdf(file),
                MediaTypeNames.Image.Tiff => await OcrFromImage(file),
                MediaTypeNames.Image.Gif => await OcrFromImage(file),
                MediaTypeNames.Image.Jpeg => await OcrFromImage(file),
                "image/png" => await OcrFromImage(file),
                _ => await Task.FromResult("Erro ao ler tentar processar o arquivo.")
            };
        }

        internal async Task<string> OcrFromImage(IFormFile file)
        {
            using var engine = new TesseractEngine($@"{_ocrOptions.TessData}", _ocrOptions.Language, EngineMode.Default);
            using var image = Pix.LoadFromMemory(await file.GetBytes());
            using var page = engine.Process(image);
            return page.GetText();
        }

        internal async Task<string> OcrFromPdf(IFormFile file)
        { 
            throw new NotImplementedException();
        }
    }
}
