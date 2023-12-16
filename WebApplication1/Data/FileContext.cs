using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Data
{
    public class FileContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }
        public DbSet<FolderModel> Folders { get; set; }

        public FileContext(DbContextOptions<FileContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the File entity
            modelBuilder.Entity<FileModel>(ConfigureFileModel);

            // Configure the Folder entity
            modelBuilder.Entity<FolderModel>(ConfigureFolderModel);

            ////// Seed initial data (if needed)
            ////SeedData.Initialize(this);
        }

        private static void ConfigureFileModel(EntityTypeBuilder<FileModel> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Name).IsRequired();
            builder.Property(f => f.Type).IsRequired(false);
            builder.Property(f => f.LastWriteTime).IsRequired();
            builder.Property(f => f.ImageIndex).IsRequired();
            builder.Property(f => f.ParentFolderId).IsRequired(false);

            builder.HasOne(f => f.ParentFolder)
                .WithMany(f => f.Files)
                .HasForeignKey(f => f.ParentFolderId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ConfigureFolderModel(EntityTypeBuilder<FolderModel> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Name).IsRequired();
            builder.Property(f => f.Type).IsRequired(false);
            builder.Property(f => f.LastWriteTime).IsRequired();
        }
    }
}