using AutoMapper;
using FileMetadataAPI.Application.Dtos;
using FileMetadataAPI.Application.Queries;
using FileMetadataAPI.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileMetadataAPI.Application.Handlers
{
    public class GetFileByIdQueryHandler : IRequestHandler<GetFileByIdQuery, FileDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetFileByIdQueryHandler(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<FileDto> Handle(GetFileByIdQuery request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;

            if (userIdClaim is null)
                throw new UnauthorizedAccessException("Kullanıcı kimliği doğrulanamadı.");

            int ownerId = int.Parse(userIdClaim);

            var file = await _context.Files
                .FirstOrDefaultAsync(f => f.Id == request.Id && f.OwnerId == ownerId, cancellationToken);

            if (file == null)
                throw new KeyNotFoundException("Dosya bulunamadı veya erişim izniniz yok.");

            return _mapper.Map<FileDto>(file);
        }
    }
}
