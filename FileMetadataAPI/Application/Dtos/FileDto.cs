namespace FileMetadataAPI.Application.Dtos
{
    public class FileDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SharingType { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }
    }
}
