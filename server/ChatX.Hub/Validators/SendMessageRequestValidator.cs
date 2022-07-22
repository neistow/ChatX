using ChatX.Hub.Requests;
using FluentValidation;

namespace ChatX.Hub.Validators;

public class SendMessageRequestValidator : AbstractValidator<SendMessageRequest>
{
    public SendMessageRequestValidator()
    {
        RuleFor(r => r.Text).NotEmpty().MaximumLength(256);
    }
}