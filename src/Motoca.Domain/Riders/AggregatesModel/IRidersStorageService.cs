namespace Motoca.Domain.Riders.AggregatesModel;

public interface IRidersStorageService
{
    Task<byte[]> GetFileAsync(string fileName);

    Task<bool> SaveFileAsync(string fileName, byte[] file);
}
