using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class FolderService : IFolderService
    {
        private readonly FileContext context;
        private readonly Stack<string> navigationHistory;

        public FolderService(FileContext context)
        {
            this.context = context;
            navigationHistory = new Stack<string>();
            navigationHistory.Push("root"); // Assuming "root" is the default root directory
        }

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
            // Check if the navigation history is empty (root folder)
            string parentFolderName = navigationHistory.Count > 0 ? navigationHistory.Peek() : "root";

            // Set the parent folder name for the new folder
            folder.ParentFolderName = parentFolderName;
            folder.Type = "Folder";

            // Add the folder to the context
            context.Folders.Add(folder);
            context.SaveChanges();

            // Update the navigation history with the new folder name
            navigationHistory.Push(folder.Name);
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

        public IEnumerable<FolderModel> GetFolderContents(string folderPath)
        {
            try
            {
                var folderContents = context.Folders
                    .Where(f => f.ParentFolderName == folderPath)
                    .ToList();

                return folderContents;
            }
            catch (Exception ex)
            {
                // Handle exceptions if necessary
                Console.WriteLine($"Error getting folder contents: {ex.Message}");
                return Enumerable.Empty<FolderModel>();
            }
        }

        public IEnumerable<FolderModel> GoBack()
        {
            if (navigationHistory.Count > 1)
            {
                navigationHistory.Pop();
                string previousFolder = navigationHistory.Peek();
                return GetFolderContents(previousFolder);
            }

            return GetFolderContents("root");
        }
    }
}
