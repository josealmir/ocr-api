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

        public static async Task<string> SaveDiskAsync(this IFormFile file)
        {
            var path = "./file";
            if (!Directory.Exists(@path))
                Directory.CreateDirectory(@path);
        
            var pathFile = $"{path}/{file.FileName}";
            if (File.Exists(pathFile))
                File.Delete(@pathFile);
            
            using var streamFile = new FileStream(@$"{path}/{file.FileName}", FileMode.CreateNew, FileAccess.ReadWrite);
            await file.CopyToAsync(streamFile);
            return pathFile;
        }
    }
}
