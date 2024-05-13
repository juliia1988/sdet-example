namespace Radancy.Interviews.Examples.Banking.Sdet.Api.Contracts;

public sealed class AccountDto
{
    public required Guid Id { get; init; }

    public required decimal Funds { get; init; }
}