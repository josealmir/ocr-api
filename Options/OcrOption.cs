using System.ComponentModel.DataAnnotations;

namespace Recognition.Options
{
    public sealed class OcrOption
    {
        public const string SectionName = "Ocr";

        [Required]
        public required string TessData { get; set; }
        
        [Required]
        public required string Language { get; set; }
    }
}
