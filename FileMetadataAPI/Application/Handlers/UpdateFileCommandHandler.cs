using AutoMapper;
using FileMetadataAPI.Application.Commands;
using FileMetadataAPI.Application.Dtos;
using FileMetadataAPI.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FileMetadataAPI.Application.Handlers
{
    public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, FileDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateFileCommandHandler(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<FileDto> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("Kullanıcı kimliği bulunamadı.");

            var userId = int.Parse(userIdClaim.Value);

            var file = await _context.Files
                .FirstOrDefaultAsync(f => f.Id == request.Id && f.OwnerId == userId, cancellationToken);

            if (file == null)
                throw new KeyNotFoundException("Dosya bulunamadı veya yetkiniz yok.");

            file.Name = request.Name;
            file.Description = request.Description;
            file.SharingType = request.SharingType;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FileDto>(file);
        }
    }

}
