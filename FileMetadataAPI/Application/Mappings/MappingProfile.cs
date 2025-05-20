using AutoMapper;
using FileMetadataAPI.Application.Dtos;

namespace FileMetadataAPI.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.File, FileDto>().ReverseMap();
            CreateMap<CreateFileRequest, Domain.Entities.File>();
        }
    }

}
