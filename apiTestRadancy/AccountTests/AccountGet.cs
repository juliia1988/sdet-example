using System.Text;
using apiTestRadancy.Models;
using Newtonsoft.Json;

namespace apiTestRadancy.Tests
{
    public class AccountGet : BaseTestClass
    {
        private string? _accountId;
        [Fact]
        public async Task GetAccountById_Returns200AndCorrectFunds()
        {
            var requestContent = new StringContent("{\"initialFunds\": 999}", Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/accounts", requestContent);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var account = JsonConvert.DeserializeObject<Account>(responseBody);

            Assert.NotNull(account);
            Assert.NotNull(account.Id);

            _accountId = account.Id;

            response = await _client.GetAsync($"/accounts/{_accountId}");

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            responseBody = await response.Content.ReadAsStringAsync();

            account = JsonConvert.DeserializeObject<Account>(responseBody);

            Assert.NotNull(account);
            Assert.Equal(999, account.Funds);
        }

        [Fact]
        public async Task GetNotExistedAccountById_Returns404()
        {
            var response = await _client.GetAsync($"/accounts/aa27b33f-aa84-4f1e-a8e4-c8d42e666666");

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]

        public async Task GetAllAccounts_Returns200()
        {
            var response = await _client.GetAsync($"/accounts");

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
