
using FileMetadataAPI.Application.Dtos;
using FileMetadataAPI.Application.Queries;
using FileMetadataAPI.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileMetadataAPI.Application.Handlers
{
    public class GetSharedFilesQueryHandler : IRequestHandler<GetSharedFilesQuery, List<SharedFileDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetSharedFilesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SharedFileDto>> Handle(GetSharedFilesQuery request, CancellationToken cancellationToken)
        {
            var sharedFiles = await _context.FileShares
                .Include(fs => fs.File)
                .Where(fs => fs.UserId == request.CurrentUserId)
                .ToListAsync(cancellationToken);

            return sharedFiles.Select(fs => new SharedFileDto
            {
                FileId = fs.File.Id,
                Name = fs.File.Name,
                Description = fs.File.Description,
                OwnerId = fs.File.OwnerId,
                Permission = fs.Permission.ToString()
            }).ToList();
        }
    }
}