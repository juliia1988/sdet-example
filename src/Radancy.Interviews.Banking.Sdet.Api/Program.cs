using Radancy.Interviews.Examples.Banking.Sdet.Api;
using Radancy.Interviews.Examples.Banking.Sdet.Api.Contracts;
using Radancy.Interviews.Examples.Banking.Sdet.Api.Extensions;
using Radancy.Interviews.Examples.Banking.Sdet.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<IAccountRepository, AccountRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/accounts", async (CreateAccountRequestDto request, IAccountRepository repository) =>
    {
        var result = Radancy.Interviews.Examples.Banking.Sdet.Api.Domain.Account.Create(request.InitialFunds);

        return await result.MatchFirstAsync<IResult>(
            onValue: async account =>
            {
                await repository.AddAccount(account);
                await repository.SaveChanges();

                return Results.Created($"/accounts/{account.Id}", account);
            },
            onFirstError: error =>
            {
                var httpResult = Results.UnprocessableEntity(error.ToProblemDetails());

                return Task.FromResult(httpResult);
            });
    })
    .WithName("CreateAccount");

app.MapDelete("/accounts/{accountId}", async (Guid accountId, IAccountRepository repository) =>
    {
        var account = await repository.GetAccount(accountId);

        if (account is null) return Results.NotFound();
        
        await repository.DeleteAccount(accountId);
        await repository.SaveChanges();
            
        return Results.Ok();
    })
    .WithName("DeleteAccount");

app.MapGet("/accounts/{accountId}", async (Guid accountId, IAccountRepository repository) =>
    {
        var account = await repository.GetAccount(accountId);
        
        return account is not null 
            ? Results.Ok(account) 
            : Results.NotFound();
    })
    .WithName("GetAccount");

app.MapGet("/accounts", async (IAccountRepository repository) =>
    {
        var accounts = await repository.GetAccounts();

        return Results.Ok(accounts);
    })
    .WithName("GetAccounts");

app.MapPost(
        "/accounts/{accountId}/transactions", 
        async (Guid accountId, CreateTransactionRequest request, IAccountRepository repository) =>
    {
        var account = await repository.GetAccount(accountId);

        if (account is null)
        {
            return Results.NotFound();
        }

        var result = account.ExecuteTransaction(request.Amount);

        return await result.MatchFirstAsync(
            onValue: async _ =>
            {
                await repository.SaveChanges();
                
                return Results.Ok(new AccountDto
                {
                    Id = account.Id,
                    Funds = account.Funds
                });
            },
            onFirstError: error =>
            {
                var httpResult = Results.UnprocessableEntity(error.ToProblemDetails());

                return Task.FromResult(httpResult);
            });
    })
    .WithName("CreateTransaction");

app.Run();

// Reference used to bootstrap the service in tests.
namespace Radancy.Interviews.Examples.Banking.Sdet.Api
{
    public partial class Program;
} 