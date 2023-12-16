using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class FileService(FileContext context) : IFileService
    {
        public IEnumerable<FileModel> GetAllFiles()
        {
            return context.Files.ToList();
        }

        public FileModel GetFileById(int fileId)
        {
            return context.Files.FirstOrDefault(f => f.Id == fileId);
        }

        public void CreateFile(FileModel file)
        {
            context.Files.Add(file);
            context.SaveChanges();
        }

        public void UpdateFile(int fileId, FileModel updatedFile)
        {
            var existingFile = context.Files.FirstOrDefault(f => f.Id == fileId);

            if (existingFile != null)
            {
                existingFile.Name = updatedFile.Name;
                existingFile.Type = updatedFile.Type;
                existingFile.LastWriteTime = updatedFile.LastWriteTime;
                existingFile.ImageIndex = updatedFile.ImageIndex;

                context.SaveChanges();
            }
        }

        public void DeleteFile(int fileId)
        {
            var fileToDelete = context.Files.FirstOrDefault(f => f.Id == fileId);

            if (fileToDelete != null)
            {
                context.Files.Remove(fileToDelete);
                context.SaveChanges();
            }
        }

        public IEnumerable<FileModel> SearchFiles(string term)
        {
            return context.Files.Where(file => file.Name.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }

}
