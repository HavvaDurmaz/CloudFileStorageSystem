using FileMetadataAPI.Application.Dtos;
using MediatR;

namespace FileMetadataAPI.Application.Queries
{
    public class GetSharedFilesQuery : IRequest<List<SharedFileDto>>
    {
        public int CurrentUserId { get; set; }
    }

}
