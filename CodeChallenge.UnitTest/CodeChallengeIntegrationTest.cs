using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using CodeChallenge.UnitTest.Helpers;

namespace CodeChallenge.UnitTest
{
    public class CodeChallengeIntegrationTest
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly ITestOutputHelper _output;
        public CodeChallengeIntegrationTest(ITestOutputHelper output)
        {
            _output = output;
            _factory = new WebApplicationFactory<Program>();
        }

        [Theory]
        [InlineData(1)]
        public async Task Test(int productId)
        {
            try
            {
                var client = _factory.CreateClient();
                var response = await client.GetAsync($"api/Products/{productId}");
                var responseContent = await response.Content.ReadAsStringAsync();
                Assert.NotNull(responseContent);
                _output.WriteLine(responseContent.AsJson());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
