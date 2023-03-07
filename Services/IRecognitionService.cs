
namespace Recognition.Services
{
    public interface IRecognitionService
    {
        public Task<string> GetTextAsync(IFormFile file);
    }
}
