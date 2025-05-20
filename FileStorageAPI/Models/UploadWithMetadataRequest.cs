namespace FileStorageAPI.Models
{
    public class UploadWithMetadataRequest
    {
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SharingType { get; set; }


    }
}
