using MediatR;

namespace FileMetadataAPI.Application.Commands
{
    public class ShareFileCommand : IRequest<bool>
    {
        public int FileId { get; set; }
        public int UserId { get; set; }
        public string Permission { get; set; }  // "Read" veya "Edit"
    }

}
