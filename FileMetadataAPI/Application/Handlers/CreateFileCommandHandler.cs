using System.Security.Claims;
using System;
using AutoMapper;
using FileMetadataAPI.Application.Commands;
using FileMetadataAPI.Domain.Entities;
using FileMetadataAPI.Domain.Enums;
using FileMetadataAPI.Infrastructure;
using MediatR;
using FileMetadataAPI.Application.Dtos;

namespace FileMetadataAPI.Application.Handlers
{
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, int>
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateFileCommandHandler(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("Kullanıcı kimliği bulunamadı.");

            var userId = int.Parse(userIdClaim.Value);

            var file = new Domain.Entities.File
            {
                Name = request.Name,
                Description = request.Description,
                SharingType = request.SharingType,
                FileExtension = request.FileExtension,
                OwnerId = userId
            };

            _context.Files.Add(file);
            await _context.SaveChangesAsync(cancellationToken);

            return file.Id; 
        }
    }
}



