using FluentValidation;
using System.Text.Json.Serialization;

namespace RFECase.API.ViewModel
{
    public class DiffRequestViewModel
    {
        [JsonPropertyName("input")]
        public string Input { get; set; }
    }

    public class DiffRequestViewModelValidator : AbstractValidator<DiffRequestViewModel>
    {
        public DiffRequestViewModelValidator()
        {
            RuleFor(x => x.Input)
                .NotNull().WithMessage("Input must not be null !")
                .NotEmpty().WithMessage("Input must not be leaved empty !");
        }
    }
}
