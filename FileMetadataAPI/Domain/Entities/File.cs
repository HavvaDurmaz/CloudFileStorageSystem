using FileMetadataAPI.Domain.Enums;

namespace FileMetadataAPI.Domain.Entities
{
    public class File
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SharingType SharingType { get; set; }
        public DateTime UploadDate { get; set; }
        public string FileExtension { get; set; }
        public ICollection<FileShare> FileShares { get; set; } = new List<FileShare>();

    }

}

