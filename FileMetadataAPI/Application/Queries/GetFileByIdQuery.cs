using FileMetadataAPI.Application.Dtos;
using MediatR;

namespace FileMetadataAPI.Application.Queries
{
    public class GetFileByIdQuery : IRequest<FileDto>
    {
        public int Id { get; set; }

        public GetFileByIdQuery(int id)
        {
            Id = id;
        }
    }
}
