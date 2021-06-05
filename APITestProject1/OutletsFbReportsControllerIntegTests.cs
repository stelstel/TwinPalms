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

namespace APITestProject1
{
    public class OutletsFbReportsControllerIntegTests : IClassFixture<TestingWebAppFactory<Startup>>
    {
        private readonly HttpClient _client;
        private ILoggerManager logger = new LoggerManager();

        public OutletsFbReportsControllerIntegTests(TestingWebAppFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44306/");

        }

        [Fact]
        //*************************************** testing GET api/outlets/fbReports ***********************************************
        public async Task get_OutletFbReports()
        {
            // Arrange *************************************************
            int expectedNrOfReports;
            int expectedTables;
            int expFood;
            int expBeverage;
            int expOtherIncome;
            int expGuestsFromHotel;
            int expguestsFromOutsideHotel;
            bool expIsPublicHoliday;
            string expEventNotes;
            string expGSourceOfBusinessNotes;
            int expOutletId;
            string expUserId;
            int expLocalEventId;
            List<int> outletIds = new List<int> { 1, 2, 4 };
            DateTime fromDate = new DateTime(2021, 01, 01);
            DateTime toDate = new DateTime(2022, 01, 01);
            string URL = $"outlets/fbReports?outletIds={outletIds.ElementAt(0)}&outletIds={outletIds.ElementAt(1)}&outletIds={outletIds.ElementAt(2)}&" +
                $"fromDate={fromDate}&toDate={toDate}";

            // Act ******************************************************
            var response = await _client.GetAsync(URL);
            response.EnsureSuccessStatusCode();
            var responseString = JArray.Parse(await response.Content.ReadAsStringAsync());
            int actualNrOfReports = responseString.Count();
            var report1 = responseString[0];

            CheckFbReport
            (
                expectedNrOfReports = 7,
                expectedTables = 1,
                expFood = 10000,
                expBeverage = 20000,
                expOtherIncome = 5000,
                expGuestsFromHotel = 15,
                expguestsFromOutsideHotel = 10,
                expIsPublicHoliday = false,
                expEventNotes = "The DJ got everybody dancing",
                expGSourceOfBusinessNotes = "A lot of drunk people just dropped in at around 1:00 AM",
                expOutletId = 1,
                expUserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                expLocalEventId = 2,
                actualNrOfReports,
                report1
            );
        }

        // *************************************** CheckFbReport *************************************************************
        private static void CheckFbReport(int expectedNrOfReports, int expectedTables, int expFood, int expBeverage, int expOtherIncome, 
            int expGuestsFromHotel, int expGuestsFromOutsideHotel, bool expIsPublicHoliday, string expEventNotes,
            string expGSourceOfBusinessNotes, int expOutletId,  string expUserId, int expLocalEventId, 
            int actualNrOfReports, JToken report)
        {
            int actualTables = (int)report["tables"];
            int actFood = (int)report["food"];
            int actBeverage = (int)report["beverage"];
            int actOtherIncome = (int)report["otherIncome"];
            int actGuestsFromHotel = (int)report["guestsFromHotel"];
            int actGuestsFromOutsideHotel = (int)report["guestsFromOutsideHotel"];
            bool actIsPublicHoliday = (bool)report["isPublicHoliday"];
            string actEventNotes = (string)report["eventNotes"];
            int actOutletId = (int)report["outletId"];
            string actUserId = (string)report["userId"];
            int actLocalEventId = (int)report["localEventId"];
            string actGSourceOfBusinessNotes = (string)report["gSourceOfBusinessNotes"];


            // Assert
            Assert.Equal(expectedNrOfReports, actualNrOfReports);
            Assert.Equal(expectedTables, actualTables);
            Assert.Equal(expFood, actFood);
            Assert.Equal(expBeverage, actBeverage);
            Assert.Equal(expOtherIncome, actOtherIncome);
            Assert.Equal(expGuestsFromHotel, actGuestsFromHotel);
            Assert.Equal(expGuestsFromOutsideHotel, actGuestsFromOutsideHotel);
            Assert.Equal(expIsPublicHoliday.ToString(), actIsPublicHoliday.ToString());
            Assert.Equal(expEventNotes, actEventNotes);
            Assert.Equal(expGSourceOfBusinessNotes, actGSourceOfBusinessNotes);
            Assert.Equal(expOutletId, actOutletId);
            Assert.Equal(expUserId, actUserId);
            Assert.Equal(expLocalEventId, actLocalEventId);
        }
    }
}