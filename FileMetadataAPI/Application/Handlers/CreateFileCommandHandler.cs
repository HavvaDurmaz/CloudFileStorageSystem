using System.Security.Claims;
using System;
using AutoMapper;
using FileMetadataAPI.Application.Commands;
using FileMetadataAPI.Domain.Entities;
using FileMetadataAPI.Domain.Enums;
using FileMetadataAPI.Infrastructure;
using MediatR;

namespace FileMetadataAPI.Application.Handlers
{
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, int>
    {
        private readonly ApplicationDbContext _context;
        public CreateFileCommandHandler(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<int> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {


            // Yeni File entity oluşturuyoruz
            var file = new Domain.Entities.File
            {
                Name = request.Name, // Bu geçici, hemen sonra güncellenecek
                Description = request.Description,
                SharingType = request.SharingType,
                FileExtension = request.FileExtension
            };

            // Veritabanına ekle ve kaydet
            await _context.Files.AddAsync(file, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // Dosya adını ID + uzantı olarak güncelle
            file.Name = $"{file.Id}{file.FileExtension}";

            // Güncelle ve kaydet
            _context.Files.Update(file);
            await _context.SaveChangesAsync(cancellationToken);

            // Kaydedilen dosyanın ID'sini döndür
            return file.Id;


        }


    }
    
}
