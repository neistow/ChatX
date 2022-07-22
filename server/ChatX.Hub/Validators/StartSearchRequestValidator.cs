using ChatX.Hub.Requests;
using FluentValidation;

namespace ChatX.Hub.Validators;

public class StartSearchRequestValidator : AbstractValidator<StartSearchRequest>
{
    public StartSearchRequestValidator()
    {
        RuleFor(r => r.UserInfo.Gender).IsInEnum();
        RuleFor(r => r.UserInfo.Age).IsInEnum();
        
        RuleFor(r => r.SearchPreferences.Gender).IsInEnum();
        RuleFor(r => r.SearchPreferences.Age).IsInEnum();
    }
}