using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IFolderService
{
    IEnumerable<FolderModel> GetAllFolders();
    FolderModel GetFolderById(int folderId);
    void CreateFolder(FolderModel folder);
    void UpdateFolder(int folderId, FolderModel updatedFolder);
    void DeleteFolder(int folderId);

    IEnumerable<FolderModel> GetFolderContents(string folderName);

    IEnumerable<FolderModel> GoBack();
}