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
    public class SobControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Startup>>
    {
        private readonly HttpClient _client;

        private List<string> sobs = new List<string>{
            "Hotel Website",
            "Hungry Hub",
            "Facebook referral",
            "Google search",
            "Instagram referral",
            "Hotel referral",
            "Other Hotel referral",
            "Agent referral",
            "Walk in",
            "Other"
        };

        

        public SobControllerIntegrationTests(TestingWebAppFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44306/");
        }

        [Fact]
        // testing GET api/GuestSourceOfBusiness
        public async Task get_all_seeded_sobs()
        {
            var response = await _client.GetAsync("api/GuestSourceOfBusiness");

            response.EnsureSuccessStatusCode();

            var responseString = JArray.Parse(await response.Content.ReadAsStringAsync());
            int responseStrLength = responseString.Count;
            List<int> responseIds = new List<int>();
            List<string> responseSobs = new List<string>();
            
            for (int i = 0; i < responseStrLength; i++)
            //for (int i = 0; i < 3; i++)
            {
                responseIds.Add((int)responseString[i]["id"]);
                responseSobs.Add((string)responseString[i]["sourceOfBusiness"]);
            }

            for (int i = 0; i < responseStrLength; i++)
            //for (int i = 0; i < 3; i++)
            {
                Assert.Equal( i+1, responseIds[i]);
                Assert.Equal(sobs[i], responseSobs[i]);
            }

            Assert.NotEqual(99, responseIds[0]);
            Assert.NotEqual("abcde", responseSobs[0]);
        }
    }
}
