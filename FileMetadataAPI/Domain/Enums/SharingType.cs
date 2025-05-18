using System.ComponentModel.DataAnnotations;

namespace FileMetadataAPI.Domain.Enums
{
    public enum SharingType
    {
        [Display(Name = "Gizli")]
        Private = 1,
        // Özel
        [Display(Name = "Herkese Açık")]
        Public = 2,

        [Display(Name = "Sınırlı")]
        Restricted = 3
    }
}
