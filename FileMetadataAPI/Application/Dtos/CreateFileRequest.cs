using FileMetadataAPI.Domain.Enums;

namespace FileMetadataAPI.Application.Dtos
{
    public class CreateFileRequest
    {
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SharingType SharingType { get; set; }
    }
}
