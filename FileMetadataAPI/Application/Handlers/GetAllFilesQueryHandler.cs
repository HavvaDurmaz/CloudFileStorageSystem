using AutoMapper;
using FileMetadataAPI.Application.Dtos;
using FileMetadataAPI.Application.Queries;
using MediatR;
using System.Security.Claims;
using System;
using FileMetadataAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FileMetadataAPI.Application.Handlers
{

    public class GetAllFilesQueryHandler : IRequestHandler<GetAllFilesQuery, List<FileDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllFilesQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FileDto>> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
        {
            var files = await _context.Files.ToListAsync(cancellationToken);
            return _mapper.Map<List<FileDto>>(files);
        }
    }

}
