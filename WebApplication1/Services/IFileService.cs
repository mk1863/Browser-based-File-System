using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IFileService
{
    IEnumerable<FileModel> GetAllFiles();
    FileModel GetFileById(int fileId);
    void CreateFile(FileModel file);
    void UpdateFile(int fileId, FileModel updatedFile);
    void DeleteFile(int fileId);

    IEnumerable<FileModel> SearchFiles(string term);
}