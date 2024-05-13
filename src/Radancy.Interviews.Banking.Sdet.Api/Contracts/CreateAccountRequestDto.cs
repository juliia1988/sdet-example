namespace Radancy.Interviews.Examples.Banking.Sdet.Api.Contracts;

public sealed class CreateAccountRequestDto
{
    public required decimal InitialFunds { get; init; }
}
