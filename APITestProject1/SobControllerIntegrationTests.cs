using Contracts;
using Entities.Models;
using LoggerService;
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
using static APITestProject1.testObjects;

namespace APITestProject1
{
    public class SobControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Startup>>
    {
        private readonly HttpClient _client;
        private ILoggerManager logger = new LoggerManager();

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
            List<string> sobs = new List<string>();
            
            foreach (var togsob in testObjects.TestObjGsobs)
            {
                sobs.Add(togsob.SourceOfBusiness);
            }

            List<int> responseIds = new List<int>();
            List<string> responseSobs = new List<string>();


            // Act *********************************************
            var response = await _client.GetAsync("api/GuestSourceOfBusiness");
            response.EnsureSuccessStatusCode();
            var responseString = JArray.Parse(await response.Content.ReadAsStringAsync());
            int responseStrLength = responseString.Count;
                        
            for (int i = 0; i < responseStrLength; i++)
            {
                responseIds.Add((int)responseString[i]["id"]);
                responseSobs.Add((string)responseString[i]["sourceOfBusiness"]);
            }


            // Assert ***********************************************
            for (int i = 0; i < responseStrLength; i++)
            {
                Assert.Equal( i+1, responseIds[i]);
                Assert.Equal(sobs[i], responseSobs[i]);
            }

            Assert.NotEqual(99, responseIds[0]);
            Assert.NotEqual("abcde", responseSobs[0]);
        }

        [Fact]
        //*************************** testing POST api/GuestSourceOfBusiness and GET api/GuestSourceOfBusiness/{Id}**********************************
        public async Task create_and_read_1_gsob()
        {
            // Arrange ********************************
            GuestSourceOfBusiness gsob = new GuestSourceOfBusiness();
            gsob.SourceOfBusiness = "Test1234";
            string gsobJson = JsonConvert.SerializeObject(gsob);

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(gsobJson, Encoding.UTF8, "application/json");


            // Act ****************************************
            var postResponse = await _client.PostAsync("api/GuestSourceOfBusiness", httpContent); // Posting gsob
            postResponse.EnsureSuccessStatusCode();
            var postResponseString = JObject.Parse(await postResponse.Content.ReadAsStringAsync());
            int gsobId = (int)postResponseString.GetValue("id");

            HttpResponseMessage getResponse = await _client.GetAsync($"/api/GuestSourceOfBusiness/{gsobId}"); // Getting gsob
            string getResponseBody = await getResponse.Content.ReadAsStringAsync();
            GuestSourceOfBusiness getResponseGsob = JsonConvert.DeserializeObject<GuestSourceOfBusiness>(getResponseBody);


            // Assert ****************************************************************
            Assert.Equal(gsob.SourceOfBusiness, getResponseGsob.SourceOfBusiness);


            // Restoring DB
            await _client.DeleteAsync($"api/GuestSourceOfBusiness/{gsobId}");
        }

        [Fact]
        //*************************** testing DELETE /api/GuestSourceOfBusiness/{id} **********************************
        public async Task delete_gsob()
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
            var gsobHighestId = responseString.Where(rs => (int)rs["id"] == highestId).First();

            GuestSourceOfBusiness gsobThatWillBeDeleted = new GuestSourceOfBusiness
            {
                Id = (int)gsobHighestId["id"],
                SourceOfBusiness = (string)gsobHighestId["sourceOfBusiness"]
            }; 


            // Act ****************************************
            var postResponse = await _client.DeleteAsync ($"api/GuestSourceOfBusiness/{highestId}");
            var responseAfter = await _client.GetAsync("api/GuestSourceOfBusiness");
            var responseStringAfter = JArray.Parse(await responseAfter.Content.ReadAsStringAsync());
            int endNrOfGsobs = responseStringAfter.Count;

            // Assert ****************************************
            Assert.Equal(startNrOfGsobs - 1, endNrOfGsobs);


            // Restoring DB
            string gsobJson = JsonConvert.SerializeObject(gsobThatWillBeDeleted);
            var httpContent = new StringContent(gsobJson, Encoding.UTF8, "application/json");

            await _client.PostAsync("api/GuestSourceOfBusiness", httpContent);
        }

        [Fact]
        //*************************** testing PUT /api/GuestSourceOfBusiness/{id} **********************************
        public async Task update_gsob()
        {
            string newSob = "Updated";
            var response = await _client.GetAsync("api/GuestSourceOfBusiness");
            var responseString = JArray.Parse(await response.Content.ReadAsStringAsync());

            List<int> gsobIds = new List<int>();

            foreach (var rs in responseString)
            {
                gsobIds.Add((int)rs["id"]);
            }

            var random = new Random();
            int gsobId = random.Next(gsobIds.Count) + 1;

            string sobBeforeUpdate = responseString.Where(rs => (int)rs["id"] == gsobId).First()["sourceOfBusiness"].ToString();

            GuestSourceOfBusiness updatedGsob = new GuestSourceOfBusiness
            {
                Id = gsobId,
                SourceOfBusiness = newSob
            };

            string newGsobJson = JsonConvert.SerializeObject(updatedGsob);

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(newGsobJson, Encoding.UTF8, "application/json");

            var putResponse = await _client.PutAsync($"api/GuestSourceOfBusiness/{gsobId}", httpContent); // Putting gsob
            putResponse.EnsureSuccessStatusCode();

            HttpResponseMessage getResponse = await _client.GetAsync($"/api/GuestSourceOfBusiness/{gsobId}"); // Getting gsob
            string getResponseBody = await getResponse.Content.ReadAsStringAsync();
            GuestSourceOfBusiness getResponseGsob = JsonConvert.DeserializeObject<GuestSourceOfBusiness>(getResponseBody);

            string sobAfterUpdate = getResponseGsob.SourceOfBusiness;


            // Assert ***************************************************************************
            Assert.Equal(newSob, sobAfterUpdate);
            Assert.NotEqual(newSob, sobBeforeUpdate);

            // Restoring DB
            GuestSourceOfBusiness restoredGsob = new GuestSourceOfBusiness
            {
                Id = gsobId,
                SourceOfBusiness = sobBeforeUpdate
            };

            string restoredGsobJson = JsonConvert.SerializeObject(restoredGsob);
            var restoredHttpContent = new StringContent(restoredGsobJson, Encoding.UTF8, "application/json");
            var putRestoredResponse = await _client.PutAsync($"api/GuestSourceOfBusiness/{gsobId}", restoredHttpContent); // Putting original gsob
        }
    }
}
