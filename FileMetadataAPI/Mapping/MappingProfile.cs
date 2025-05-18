using AutoMapper;
using FileMetadataAPI.Application.Dtos;
using FileMetadataAPI.Domain.Entities;

namespace FileMetadataAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.File, FileDto>();
        }
    }
}
