namespace WebApplication1.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime LastWriteTime { get; set; }
        public int ImageIndex { get; set; }
        public int? ParentFolderId { get; set; }
        public FolderModel ParentFolder { get; set; }
    }
}
