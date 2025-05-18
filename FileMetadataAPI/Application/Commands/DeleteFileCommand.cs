using MediatR;

namespace FileMetadataAPI.Application.Commands
{
    public class DeleteFileCommand : IRequest<Unit>
    {
        public int Id { get; set; } 
    }
}
