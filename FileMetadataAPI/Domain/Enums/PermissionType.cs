namespace FileMetadataAPI.Domain.Enums
{
    public enum PermissionType
    {
        Read = 1,   // Sadece okuma izni
        Edit = 2,   // Düzenleme izni
        Owner = 3   // Sahiplik, tam yetki
    }
}
