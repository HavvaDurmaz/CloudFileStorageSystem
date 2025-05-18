namespace FileMetadataAPI.Infrastructure.Interface

{
    public interface IFileMetadataRepository
    {
        Task AddAsync(Domain.Entities.File file);
        Task UpdateAsync(Domain.Entities.File file);
        Task<Domain.Entities.File?> GetByIdAsync(int id);
    }
}
