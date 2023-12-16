using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class FolderService(FileContext context) : IFolderService
    {
        public IEnumerable<FolderModel> GetAllFolders()
        {
            return context.Folders.ToList();
        }

        public FolderModel GetFolderById(int folderId)
        {
            return context.Folders.FirstOrDefault(f => f.Id == folderId);
        }

        public void CreateFolder(FolderModel folder)
        {
            context.Folders.Add(folder);
            context.SaveChanges();
        }

        public void UpdateFolder(int folderId, FolderModel updatedFolder)
        {
            var existingFolder = context.Folders.FirstOrDefault(f => f.Id == folderId);

            if (existingFolder != null)
            {
                existingFolder.Name = updatedFolder.Name;
                existingFolder.LastWriteTime = updatedFolder.LastWriteTime;

                context.SaveChanges();
            }
        }

        public void DeleteFolder(int folderId)
        {
            var folderToDelete = context.Folders.FirstOrDefault(f => f.Id == folderId);

            if (folderToDelete != null)
            {
                context.Folders.Remove(folderToDelete);
                context.SaveChanges();
            }
        }

        public IEnumerable<FolderModel> GetFolderContents(string folderName)
        {
            var folder = context.Folders.FirstOrDefault(f => f.Name == folderName);

            if (folder != null)
            {
                // Assuming a one-to-many relationship between folders and files
                return context.Folders.Where(file => file.Id == folder.Id).ToList();
            }

            return new List<FolderModel>();
        }

    }

}
