using FileMetadataAPI.Application.Dtos;
using MediatR;

namespace FileMetadataAPI.Application.Queries
{
    public class GetAllFilesQuery : IRequest<List<FileDto>>
    {
    }

}
