namespace FileMetadataAPI.Application.Dtos
{
    public class SharedFileDto
    {
            public int FileId { get; set; }
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public int OwnerId { get; set; }
            public string Permission { get; set; } = "";

        
    }
}
