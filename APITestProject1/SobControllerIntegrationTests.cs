using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        // private string dbName = "InMemoryGsobTest";

        public SobControllerIntegrationTests(TestingWebAppFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44306/");
        }
  
        [Fact]
        //*************************************** testing GET api/GuestSourceOfBusiness ***********************************************
        public async Task get_all_seeded_sobs()
        {
            // Arrange ********************************
            List<string> sobs = new List<string>{
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

            List<int> responseIds = new List<int>();
            List<string> responseSobs = new List<string>();


            // Arrange *********************************************
            var response = await _client.GetAsync("api/GuestSourceOfBusiness");
            response.EnsureSuccessStatusCode();
            var responseString = JArray.Parse(await response.Content.ReadAsStringAsync());
            int responseStrLength = responseString.Count;
                        
            for (int i = 0; i < responseStrLength; i++)
            {
                responseIds.Add((int)responseString[i]["id"]);
                responseSobs.Add((string)responseString[i]["sourceOfBusiness"]);
            }

            // Assert
            for (int i = 0; i < responseStrLength; i++)
            {
                Assert.Equal( i+1, responseIds[i]);
                Assert.Equal(sobs[i], responseSobs[i]);
            }

            Assert.NotEqual(99, responseIds[0]);
            Assert.NotEqual("abcde", responseSobs[0]);
        }

        [Fact]
        //*************************** testing POST api/GuestSourceOfBusiness **********************************
        public async Task create_and_read_1_gsob()
        {
            // Arrange ********************************
            int gsobId = 11;
            GuestSourceOfBusiness gsob = new GuestSourceOfBusiness();
            gsob.SourceOfBusiness = "Test1234";
            string gsobJson = JsonConvert.SerializeObject(gsob);

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(gsobJson, Encoding.UTF8, "application/json");

            // Act ****************************************
            var postResponse = await _client.PostAsync("api/GuestSourceOfBusiness", httpContent);
            postResponse.EnsureSuccessStatusCode();
            HttpResponseMessage getResponse = await _client.GetAsync($"/api/GuestSourceOfBusiness/{gsobId}");
            string getResponseBody = await getResponse.Content.ReadAsStringAsync();
            GuestSourceOfBusiness responseGsob = JsonConvert.DeserializeObject<GuestSourceOfBusiness>(getResponseBody);

            // Assert
            Assert.Equal(gsob.SourceOfBusiness, responseGsob.SourceOfBusiness);
        }

        [Fact]
        //*************************** testing DELETE /api/GuestSourceOfBusiness/{id} **********************************
        public async Task delete_1_gsob()
        {
            // Arrange ********************************
            List<int> responseIds = new List<int>();
            var response = await _client.GetAsync("api/GuestSourceOfBusiness");
            var responseString = JArray.Parse(await response.Content.ReadAsStringAsync());
            int startNrOfGsobs = responseString.Count;

            for (int i = 0; i < startNrOfGsobs; i++)
            {
                responseIds.Add((int)responseString[i]["id"]);
            }

            int highestId = responseIds.Max();

            // Act ****************************************
            var postResponse = await _client.DeleteAsync ($"api/GuestSourceOfBusiness/{highestId}");
            var responseAfter = await _client.GetAsync("api/GuestSourceOfBusiness");
            var responseStringAfter = JArray.Parse(await responseAfter.Content.ReadAsStringAsync());
            int endNrOfGsobs = responseStringAfter.Count;

            // Assert
            Assert.Equal(startNrOfGsobs - 1, endNrOfGsobs);
        }
    }
}
