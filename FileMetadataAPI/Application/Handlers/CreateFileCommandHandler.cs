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

            Console.WriteLine(">>> HANDLER çalıştı >>>");



            Console.WriteLine($"Name: {request.Name}");
            Console.WriteLine($"Description: {request.Description}");
            Console.WriteLine($"Extension: {request.FileExtension}");
            Console.WriteLine($"Owner: {request.OwnerId}");
            var file = new Domain.Entities.File
            {
                Name = request.Name,
                Description = request.Description,
                SharingType = request.SharingType,
                FileExtension = request.FileExtension,
                OwnerId = request.OwnerId,
                UploadDate = DateTime.UtcNow
            };

            _context.Files.Add(file);
            await _context.SaveChangesAsync();

            Console.WriteLine("EKLENDİ: " + file.Name + " / ID: " + file.Id);
            Console.WriteLine("EKLENDİ: " + file.Name);

            return file.Id;
        }
    }
}



