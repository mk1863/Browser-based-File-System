using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System;
using System.Collections.Generic;

namespace WebApplication1.Data
{
    public static class SeedData
    {
        public static void Initialize(FileContext context)
        {
            ////context.Database.EnsureCreated();

            if (!context.Folders.Any())
            {
                // Seed a root folder
                var rootFolder = new FolderModel
                {
                    Id = 1,
                    Name = "root",
                    LastWriteTime = DateTime.Now,
                };

                context.Folders.Add(rootFolder);

                // Seed some initial files associated with the root folder
                var sampleFile = new FileModel
                {
                    Id = 1,
                    Name = "SampleFile.txt",
                    Type = "File",
                    LastWriteTime = DateTime.Now,
                    ImageIndex = 1,
                    ParentFolderId = rootFolder.Id  // Specify the foreign key value
                };

                context.Files.Add(sampleFile);

                context.SaveChanges();
            }
        }
    }
}