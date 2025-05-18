using FileMetadataAPI.Infrastructure.Interface;

namespace FileMetadataAPI.Infrastructure.Repositories
{
    public class FileMetadataRepository : IFileMetadataRepository
    {
        private readonly ApplicationDbContext _context;

        public FileMetadataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Domain.Entities.File file)
        {
            await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Domain.Entities.File file)
        {
            _context.Files.Update(file);
            await _context.SaveChangesAsync();
        }

        async Task<Domain.Entities.File?> IFileMetadataRepository.GetByIdAsync(int id)
        {
             return await _context.Files.FindAsync(id);
        }
    }
}
