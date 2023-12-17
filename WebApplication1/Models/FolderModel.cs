namespace WebApplication1.Models
{
    public class FolderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }
        public DateTime LastWriteTime { get; set; }
        public List<FileModel> Files { get; set; }
        public List<FolderModel> Folders { get; set; }
        public string ParentFolderName { get; set; }
    }
}
