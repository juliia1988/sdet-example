using System.Text;
using apiTestRadancy.Models;
using Newtonsoft.Json;

namespace apiTestRadancy.Tests
{
    public class AccountCreate : BaseTestClass
    {
        private string? _accountId;

        [Fact]
        public async Task CreateAccountReturnsCorrectIdAndFunds()
        {
        
            var requestContent = new StringContent("{\"initialFunds\": 101}", Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/accounts", requestContent);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var account = JsonConvert.DeserializeObject<Account>(responseBody);
            
            if (account != null)
            {
                _accountId = account.Id;
            }
            else
            {
    
                Assert.NotNull(account);
            }

            Assert.NotNull(account.Id);
            Assert.Equal(101, account.Funds);
                
            }
        

        [Fact]
        public async Task CreateAccount_WithInitialFundsLessThan100_Returns422AndErrorMessage()
        {
        
            var requestContent = new StringContent("{\"initialFunds\": 50}", Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/accounts", requestContent);

            Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<Error>(responseBody);

            Assert.NotNull(error);
            Assert.Equal("Account must have at least 100$ at any moment in time.", error?.Detail);
        }


        [Fact]
        public async Task CreateAccount_WithInitialFundsMoreThan10000_Returns201AndCorrectIdAndFunds()
        {
    
            var requestContent = new StringContent("{\"initialFunds\": 10001}", Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/accounts", requestContent);

            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<Error>(responseBody);

            Assert.NotNull(error);
            Assert.Equal("Deposit limit is 10000$.", error?.Detail);

        }

        [Fact]
        public async Task CreateAccount_WithInitialFundsEqualTo100_Returns201AndCorrectIdAndFunds()
        {
            var requestContent = new StringContent("{\"initialFunds\": 100}", Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/accounts", requestContent);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var account = JsonConvert.DeserializeObject<Account>(responseBody);

            Assert.NotNull(account);
            Assert.NotNull(account.Id);
            Assert.Equal(100, account.Funds);
        }

        [Fact]
        public async Task CreateAccount_WithInitialFundsEqualTo0_Returns422AndErrorMessage()
        {
         
            var requestContent = new StringContent("{\"initialFunds\": 0}", Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/accounts", requestContent);

            Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();           
            var error = JsonConvert.DeserializeObject<Error>(responseBody);
           
            Assert.NotNull(error);
            Assert.Equal("Account must have at least 100$ at any moment in time.", error?.Detail);
        }   

        [Fact]  
        public async Task CreateAccount_WithInitialFundsLessThan0_Returns422AndErrorMessage()
        {
           
            var requestContent = new StringContent("{\"initialFunds\": -1}", Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/accounts", requestContent);

            Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<Error>(responseBody);

            Assert.NotNull(error);
            Assert.Equal("Account must have at least 100$ at any moment in time.", error?.Detail);
        }

        [Fact]

        public async Task CreateAccount_WithInitialFundsEqualTo1000_Returns201AndCorrectIdAndFunds()
        {
            var requestContent = new StringContent("{\"initialFunds\": 1000}", Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/accounts", requestContent);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var account = JsonConvert.DeserializeObject<Account>(responseBody);

            Assert.NotNull(account);

            Assert.NotNull(account.Id);
            Assert.Equal(1000, account.Funds);
        }

    }
}