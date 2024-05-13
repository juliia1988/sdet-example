using System.Text;
using apiTestRadancy.Models;
using Newtonsoft.Json;

namespace apiTestRadancy.Tests
{
    public class TransactionCreate : BaseTestClass
    {

         private string? _accountId; // Declare the _accountId variable at the class level as nullable

        [Fact]
        public async Task CreateTransactionByAccountId_Returns204()
        {
            // Send POST request to create an account with initial funds
            var requestContent = new StringContent("{\"initialFunds\": 100}", Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/accounts", requestContent);

            // Check if the request was successful (status code 201)
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            // Get the response body as a JSON string
            var responseBody = await response.Content.ReadAsStringAsync();

            // Parse the JSON into an Account object
            var account = JsonConvert.DeserializeObject<Account>(responseBody);

            // Check if the object is not null
            Assert.NotNull(account);

            // Check if the Id field is not null
            Assert.NotNull(account.Id);

            // Save the Id value to the _accountId variable
            _accountId = account.Id;

            // Send POST request to create a transaction
            requestContent = new StringContent("{\"amount\": 100}", Encoding.UTF8, "application/json");
            response = await _client.PostAsync($"/accounts/{_accountId}/transactions", requestContent);

            // Check if the request was successful (status code 200)
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateTransactionByFakeAccountId_Returns404()
        {
            var requestContent = new StringContent("{\"amount\": 100}", Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/accounts/aa27b33f-aa84-4f1e-a8e4-c8d42e666666/transactions", requestContent);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateTransactionWithMoreThenAllowedWithdrowal_Returns422()
        {
            var requestContent = new StringContent("{\"initialFunds\": 100}", Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/accounts", requestContent);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var account = JsonConvert.DeserializeObject<Account>(responseBody);

            Assert.NotNull(account);
            Assert.NotNull(account.Id);

            _accountId = account.Id;

            requestContent = new StringContent("{\"amount\": -10000}", Encoding.UTF8, "application/json");
            response = await _client.PostAsync($"/accounts/{_accountId}/transactions", requestContent);

            responseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var error = JsonConvert.DeserializeObject<Error>(responseBody);

            Assert.NotNull(error);
            Assert.Equal("More than 90% of the current funds cannot be withdrawn.", error.Detail);
        }

        [Fact]
        public async Task TransactionIncreasesFundsByAmount()
        {
            var requestContent = new StringContent("{\"initialFunds\": 100}", Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/accounts", requestContent);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var account = JsonConvert.DeserializeObject<Account>(responseBody);

            Assert.NotNull(account);
            Assert.NotNull(account.Id);

            var initialFunds = account.Funds;
            var transactionIncrease = 66;

            requestContent = new StringContent($"{{\"amount\": {transactionIncrease}}}", Encoding.UTF8, "application/json");
            response = await _client.PostAsync($"/accounts/{account.Id}/transactions", requestContent);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            response = await _client.GetAsync($"/accounts/{account.Id}");
            responseBody = await response.Content.ReadAsStringAsync();
            account = JsonConvert.DeserializeObject<Account>(responseBody);

            Assert.Equal(initialFunds + transactionIncrease, account.Funds);
        }

        [Fact]
        public async Task TransactionDecreasesFundsByAmount()
        {
            var response = await _client.PostAsync("/accounts", new StringContent("{\"initialFunds\": 10000}", Encoding.UTF8, "application/json"));
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            var account = JsonConvert.DeserializeObject<Account>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(account);

            var initialFunds = account.Funds;
            var transactionDecrease = -10;

            response = await _client.PostAsync($"/accounts/{account.Id}/transactions", new StringContent($"{{\"amount\": {transactionDecrease}}}", Encoding.UTF8, "application/json"));
            response = await _client.GetAsync($"/accounts/{account.Id}");

            var accountIncreased = JsonConvert.DeserializeObject<Account>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(accountIncreased);
            Assert.Equal(initialFunds + transactionDecrease, accountIncreased.Funds);
        }

        [Fact]
        public async Task TransactionWithAmountMoreThen10000_Returns422()
        {
            var response = await _client.PostAsync("/accounts", new StringContent("{\"initialFunds\": 100}", Encoding.UTF8, "application/json"));
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            var account = JsonConvert.DeserializeObject<Account>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(account);

            response = await _client.PostAsync($"/accounts/{account.Id}/transactions", new StringContent("{\"amount\": 10001}", Encoding.UTF8, "application/json"));

            Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<Error>(responseBody);

            Assert.NotNull(error);
            Assert.Equal("Deposit limit is 10000$.", error.Detail);
        }

        [Fact]
        public async Task TransactionWithAmoun10000_Returns200()
        {
            var response = await _client.PostAsync("/accounts", new StringContent("{\"initialFunds\": 100}", Encoding.UTF8, "application/json"));
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            var account = JsonConvert.DeserializeObject<Account>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(account);

            response = await _client.PostAsync($"/accounts/{account.Id}/transactions", new StringContent("{\"amount\": 10000}", Encoding.UTF8, "application/json"));

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        }


        [Fact]
        public async Task TransactionWhenAccountStaytLessThen100_Returns422()
        {
            var response = await _client.PostAsync("/accounts", new StringContent("{\"initialFunds\": 200}", Encoding.UTF8, "application/json"));
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            var account = JsonConvert.DeserializeObject<Account>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(account);

            response = await _client.PostAsync($"/accounts/{account.Id}/transactions", new StringContent("{\"amount\": -150}", Encoding.UTF8, "application/json"));

            Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<Error>(responseBody);

            Assert.NotNull(error);
            Assert.Equal("Account must have at least 100$ at any moment in time.", error.Detail);
        }

    }
}


