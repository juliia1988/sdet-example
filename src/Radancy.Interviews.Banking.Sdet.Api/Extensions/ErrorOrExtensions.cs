using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Radancy.Interviews.Examples.Banking.Sdet.Api.Extensions;

public static class ErrorOrExtensions
{
    public static ProblemDetails ToProblemDetails(this Error error)
    {
        return new ProblemDetails
        {
            Detail = error.Description
        };
    }
}