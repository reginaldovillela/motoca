using Motoca.Domain.Riders.AggregatesModel;

namespace Motoca.Infrastructure.Riders.Services;

public class RidersStorageService : IRidersStorageService
{
    private readonly string RootPath = AppDomain.CurrentDomain.BaseDirectory;

    public async Task<byte[]> GetFileAsync(string fileName)
    {
        var path = Path.Combine(RootPath, "Images", fileName);

        var file = await File.ReadAllBytesAsync(path);

        return file;
    }

    public async Task<bool> SaveFileAsync(string fileName, byte[] file)
    {
        var basePathImages = Path.Combine(RootPath, "Images");
        CreatePathIfNotExists(basePathImages);

        var path = Path.Combine(basePathImages, fileName);

        await File.WriteAllBytesAsync(path, file);

        return true;
    }

    private void CreatePathIfNotExists(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
}
