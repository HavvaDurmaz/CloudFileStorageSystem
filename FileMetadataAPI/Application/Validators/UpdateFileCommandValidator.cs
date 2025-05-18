using FileMetadataAPI.Application.Commands;
using FluentValidation;

namespace FileMetadataAPI.Application.Validators
{
    public class UpdateFileCommandValidator : AbstractValidator<UpdateFileCommand>
    {
        public UpdateFileCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.SharingType).IsInEnum();
        }
    }
}
