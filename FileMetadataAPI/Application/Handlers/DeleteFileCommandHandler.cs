using FileMetadataAPI.Application.Commands;
using FileMetadataAPI.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace FileMetadataAPI.Application.Handlers
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, Unit>
    {
        private readonly ApplicationDbContext _context;

        public DeleteFileCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _context.Files.FindAsync(request.Id);
            if (file == null)
            {
                return Unit.Value;
            }

            _context.Files.Remove(file);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

