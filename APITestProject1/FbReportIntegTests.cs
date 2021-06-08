using Contracts;
using Entities.Models;
using LoggerService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TwinPalmsKPI;
using Xunit;

namespace APITestProject1
{
    public class FbReportIntegTests : IClassFixture<TestingWebAppFactory<Startup>>
    {
        // exp is short for expected
        // act is short for actual

        private readonly HttpClient client;
        private ILoggerManager logger = new LoggerManager();

        private int expId;
        private int expTables;
        private int expFood;
        private int expBeverage;
        private int expOtherIncome;
        private int expGuestsFromHotel;
        private int expguestsFromOutsideHotel;
        private bool expIsPublicHoliday;
        private string expEventNotes;
        private string expGSourceOfBusinessNotes;
        private int expOutletId;
        private string expUserId;
        private int? expLocalEventId;

        // Constructor
        public FbReportIntegTests(TestingWebAppFactory<Startup> factory)
        {
            client = factory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:44306/");
        }

        [Fact]
        //*************************************** Testing GET /api/FbReports/{id} ***********************************************

        // Testing GET /api/FbReports/1
        public async Task get_FbReport_1()
        {
            // Arrange
            string URL = $"api/FbReports/1";

            // Act
            FbReport responseReport = await GetResponse(URL);

            CheckFbReport
            (
                expId = 1,
                expTables = 1,
                expFood = 10000,
                expBeverage = 20000,
                expOtherIncome = 5000,
                expGuestsFromHotel = 15,
                expguestsFromOutsideHotel = 10,
                expIsPublicHoliday = false,
                expEventNotes = "The DJ got everybody dancing",
                expGSourceOfBusinessNotes = "A lot of people just dropped in at around 1:00 AM",
                expOutletId = 1,
                expUserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                expLocalEventId = 2,
                responseReport
            );
        }
        
        [Fact]
        // Testing GET /api/FbReports/2
        public async Task get_FbReport_2()
        {
            // Arrange
            string URL = $"api/FbReports/2";

            // Act
            FbReport responseReport = await GetResponse(URL);

            CheckFbReport
            (
                expId = 2,
                expTables = 14,
                expFood = 19000,
                expBeverage = 31000,
                expOtherIncome = 9100,
                expGuestsFromHotel = 25,
                expguestsFromOutsideHotel = 4,
                expIsPublicHoliday = false,
                expEventNotes = "The DJ was really good",
                expGSourceOfBusinessNotes = "A lot of peolpe came from Google Search",
                expOutletId = 1,
                expUserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                expLocalEventId = 3,
                responseReport
            );
        }

        // ************************************** GetResponse ****************************************************************
        private async Task<FbReport> GetResponse(string URL)
        {
            var response = await client.GetAsync(URL);
            response.EnsureSuccessStatusCode();
            FbReport responseReport = JsonConvert.DeserializeObject<FbReport>(await response.Content.ReadAsStringAsync());
            return responseReport;
        }

        // *************************************** CheckFbReport *************************************************************
        private static void CheckFbReport(
            int expFbReportId, int expTables, int expFood, int expBeverage, int expOtherIncome, 
            int expGuestsFromHotel, int expGuestsFromOutsideHotel, bool expIsPublicHoliday, string expEventNotes,
            string expGSourceOfBusinessNotes, int expOutletId,  string expUserId, int? expLocalEventId, 
            FbReport fbReport
        )
        {
            // Act *****************************************
            int actFbReportId = (int)fbReport.Id;
            int actTables = (int)fbReport.Tables;
            int actFood = (int)fbReport.Food;
            int actBeverage = (int)fbReport.Beverage;
            int actOtherIncome = (int)fbReport.OtherIncome;
            int actGuestsFromHotel = (int)fbReport.GuestsFromHotel;
            int actGuestsFromOutsideHotel = (int)fbReport.GuestsFromOutsideHotel;
            bool actIsPublicHoliday = (bool)fbReport.IsPublicHoliday;
            string actEventNotes = (string)fbReport.EventNotes;
            int actOutletId = (int)fbReport.OutletId;
            string actUserId = (string)fbReport.UserId;
            int? actLocalEventId = (int?)fbReport.LocalEventId;


            // Assert **************************************
            Assert.Equal(expFbReportId, actFbReportId);
            Assert.Equal(expTables, actTables);
            Assert.Equal(expFood, actFood);
            Assert.Equal(expBeverage, actBeverage);
            Assert.Equal(expOtherIncome, actOtherIncome);
            Assert.Equal(expGuestsFromHotel, actGuestsFromHotel);
            Assert.Equal(expGuestsFromOutsideHotel, actGuestsFromOutsideHotel);
            Assert.Equal(expIsPublicHoliday.ToString(), actIsPublicHoliday.ToString());
            Assert.Equal(expEventNotes, actEventNotes);
            Assert.Equal(expOutletId, actOutletId);
            Assert.Equal(expUserId, actUserId);
            Assert.Equal(expLocalEventId, actLocalEventId);
        }
    }
}