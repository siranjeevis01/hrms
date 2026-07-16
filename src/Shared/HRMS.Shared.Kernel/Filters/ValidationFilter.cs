using FluentValidation;
using HRMS.Shared.Kernel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HRMS.Shared.Kernel.Filters;

public class ValidationFilter<T> : IAsyncActionFilter where T : class
{
    private readonly IValidator<T> _validator;

    public ValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionArguments.ContainsKey("request"))
        {
            await next();
            return;
        }

        var request = context.ActionArguments["request"] as T;
        if (request is null)
        {
            await next();
            return;
        }

        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new ApiValidationError
            {
                Field = e.PropertyName,
                Message = e.ErrorMessage,
                AttemptedValue = e.AttemptedValue
            }).ToList();

            var response = ApiErrorResponse.CreateWithValidation(
                StatusCodes.Status400BadRequest,
                "Validation failed",
                errors);

            context.Result = new BadRequestObjectResult(response);
            return;
        }

        await next();
    }
}

public class ValidationFilter : IAsyncActionFilter
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var argument = context.ActionArguments.Values.FirstOrDefault();
        if (argument is null)
        {
            await next();
            return;
        }

        var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
        var validator = _serviceProvider.GetService(validatorType) as IValidator;

        if (validator is null)
        {
            await next();
            return;
        }

        var validationContext = new ValidationContext<object>(argument);
        var validationResult = await validator.ValidateAsync(validationContext);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new ApiValidationError
            {
                Field = e.PropertyName,
                Message = e.ErrorMessage,
                AttemptedValue = e.AttemptedValue
            }).ToList();

            var response = ApiErrorResponse.CreateWithValidation(
                StatusCodes.Status400BadRequest,
                "Validation failed",
                errors);

            context.Result = new BadRequestObjectResult(response);
            return;
        }

        await next();
    }
}
