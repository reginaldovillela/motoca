using Motoca.Domain.Riders.AggregatesModel;

namespace Motoca.Infrastructure.Riders.Services;

public class RidersStorageService : IRidersStorageService
{
    private readonly string RootPath = AppDomain.CurrentDomain.BaseDirectory;

    private readonly string RidersImages = "RidersImages";

    public async Task<byte[]> GetFileAsync(string fileName)
    {
        var filePath = Path.Combine(RootPath, RidersImages, fileName);

        var fileInfo = new FileInfo(filePath);

        if (!fileInfo.Exists)
            return [];

        var file = await File.ReadAllBytesAsync(filePath);

        return file;
    }

    public async Task<bool> SaveFileAsync(string fileName, byte[] file)
    {
        var imagesPath = Path.Combine(RootPath, RidersImages);
        CreatePathIfNotExists(imagesPath);

        var filePath = Path.Combine(imagesPath, fileName);

        var fileInfo = new FileInfo(filePath);

        if (fileInfo.Exists)
            fileInfo.Delete();

        await File.WriteAllBytesAsync(filePath, file);

        return true;
    }

    private static void CreatePathIfNotExists(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
}
