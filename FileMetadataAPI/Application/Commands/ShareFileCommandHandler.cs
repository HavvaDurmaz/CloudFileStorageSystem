using FileMetadataAPI.Domain.Enums;
using FileMetadataAPI.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileMetadataAPI.Application.Commands
{
    public class ShareFileCommandHandler : IRequestHandler<ShareFileCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public ShareFileCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ShareFileCommand request, CancellationToken cancellationToken)
        {
            var alreadyShared = await _context.FileShares
                .AnyAsync(fs => fs.FileId == request.FileId && fs.UserId == request.UserId, cancellationToken);

            if (alreadyShared)
                throw new InvalidOperationException("Bu dosya bu kullanıcıyla zaten paylaşılmış.");

            var fileShare = new Domain.Entities.FileShare
            {
                FileId = request.FileId,
                UserId = request.UserId,
                Permission = Enum.Parse<PermissionType>(request.Permission, true)
            };

            _context.FileShares.Add(fileShare);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

















    //public class ShareFileCommandHandler : IRequestHandler<ShareFileCommand, bool>
    //{
    //    private readonly ApplicationDbContext _context;

    //    public ShareFileCommandHandler(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    public async Task<bool> Handle(ShareFileCommand request, CancellationToken cancellationToken)
    //    {
    //        var fileShare = new Domain.Entities.FileShare
    //        {
    //            FileId = request.FileId,
    //            UserId = request.UserId,
    //            Permission = Enum.Parse<PermissionType>(request.Permission, true)
    //        };

    //        _context.FileShares.Add(fileShare);
    //        await _context.SaveChangesAsync(cancellationToken);
    //        return true;
    //    }
    //}
}

