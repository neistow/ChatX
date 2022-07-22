using FluentValidation;
using FluentValidation.Internal;
using Microsoft.AspNetCore.SignalR;

namespace ChatX.Hub.HubFilters;

public class FluentValidationFilter : IHubFilter
{
    public async ValueTask<object?> InvokeMethodAsync(
        HubInvocationContext invocationContext,
        Func<HubInvocationContext, ValueTask<object?>> next)
    {
        foreach (var argument in invocationContext.HubMethodArguments.Where(arg => arg != null))
        {
            var validatorType =  typeof(IValidator<>).MakeGenericType(argument!.GetType());
            var validator = invocationContext.ServiceProvider.GetService(validatorType) as IValidator;
            if (validator == null)
            {
                continue;
            }

            var validatorConfig = invocationContext.ServiceProvider.GetRequiredService<ValidatorConfiguration>();
            var validationContext = new ValidationContext<object>(
                argument,
                new PropertyChain(),
                validatorConfig.ValidatorSelectors.DefaultValidatorSelectorFactory());

            var result = await validator.ValidateAsync(validationContext);
            if (!result.IsValid)
            {
                throw new HubException(result.ToString());
            }
        }

        return await next(invocationContext);
    }
}