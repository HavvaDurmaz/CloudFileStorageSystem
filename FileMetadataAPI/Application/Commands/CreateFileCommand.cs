using FileMetadataAPI.Domain.Enums;
using MediatR;

namespace FileMetadataAPI.Application.Commands
{
    public class CreateFileCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public SharingType SharingType { get; set; }  
        public string FileExtension { get; set; } = null!;
        public int OwnerId { get; set; }
    }
}
