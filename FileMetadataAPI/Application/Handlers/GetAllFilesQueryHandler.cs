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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllFilesQueryHandler(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<FileDto>> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new UnauthorizedAccessException("Kullanıcı kimliği doğrulanamadı.");
            }

            var files = await _context.Files
                .Where(f => f.OwnerId == userId)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<FileDto>>(files);
        }
    }
}
