using FileMetadataAPI.Application.Commands;
using FileMetadataAPI.Domain.Enums;
using FluentValidation;

namespace FileMetadataAPI.Application.Validators
{
    public class CreateFileCommandValidator : AbstractValidator<CreateFileCommand>
    {
        public CreateFileCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Dosya adı boş olamaz.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
            RuleFor(x => x.SharingType).IsInEnum().WithMessage("Geçerli bir paylaşım türü seçiniz.");
        }
    }
}
