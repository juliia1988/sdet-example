using System.Text;
using apiTestRadancy.Models;
using Newtonsoft.Json;

namespace apiTestRadancy.Tests
{
    public class AccountDelete : BaseTestClass
    {
        private string? _accountId;

        

       [Fact]
        public async Task DeleteAccountById_Returns204()
        {
            var requestContent = new StringContent("{\"initialFunds\": 666}", Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/accounts", requestContent);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var account = JsonConvert.DeserializeObject<Account>(responseBody);

            Assert.NotNull(account);
            Assert.NotNull(account.Id);

            _accountId = account.Id;

            response = await _client.DeleteAsync($"/accounts/{_accountId}");

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteNotExistedAccountById_Returns404()
        {
            var response = await _client.DeleteAsync($"/accounts/68952838-347d-4229-8c06-517fa8d66666");

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }

}


