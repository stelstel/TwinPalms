using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwinPalmsKPI;
using Xunit;

namespace APITestProject1
{
    public class EmployeesControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Startup>>
    {
        private readonly HttpClient _client;

        public EmployeesControllerIntegrationTests(TestingWebAppFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Index_WhenCalled_ReturnsApplicationForm()
        {
            _client.BaseAddress = new Uri("https://localhost:44306/");
            var response = await _client.GetAsync("api/GuestSourceOfBusiness");

            response.EnsureSuccessStatusCode();

            var responseString = JArray.Parse(await response.Content.ReadAsStringAsync());

            var responseID = responseString[0]["id"];
            var responseSob = responseString[0]["sourceOfBusiness"];

            //var temp = responseString.ElementAt(0).ElementAt(0);

            Assert.Equal(1, responseID);
            Assert.Equal("Hotel Website", responseSob);
        }
    }
}
