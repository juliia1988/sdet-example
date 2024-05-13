using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace apiTestRadancy.Tests
{
    public class BaseTestClass : IDisposable
    {
        protected readonly HttpClient _client;

        public BaseTestClass()
        {
            _client = new HttpClient { BaseAddress = new Uri("http://localhost:5128") };
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}