namespace Radancy.Interviews.Examples.Banking.Sdet.Api.Contracts;

public sealed class CreateTransactionRequest
{
    public required decimal Amount { get; init; }
}