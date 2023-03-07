namespace Recognition.Extensions
{
    public static class FormFileExtension
    {
        public static async Task<byte[]> GetBytes(this IFormFile file)
        { 
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
