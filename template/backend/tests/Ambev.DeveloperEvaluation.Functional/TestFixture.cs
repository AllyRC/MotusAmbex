using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Functional.Tests
{
    public class TestFixture : IDisposable
    {
        public HttpClient Client { get; }

        public TestFixture()
        {
            // Ajuste para a URL onde sua API está rodando nos testes
            Client = new HttpClient { BaseAddress = new Uri("https://localhost:8081") };
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
