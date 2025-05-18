using FileMetadataAPI.Application.Dtos;
using FileMetadataAPI.Domain.Enums;
using MediatR;

namespace FileMetadataAPI.Application.Commands
{
    public class UpdateFileCommand : IRequest<FileDto>
    {
        public int Id { get; set; } // Güncellenecek dosyanın ID'si
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public SharingType SharingType { get; set; }
    }
}
