using Contracts;
using Entities.Models;
using LoggerService;
using Newtonsoft.Json;
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
        private int expGuestsFromHotelTP;
        private int expGuestsFromHotelTM;
        private int expguestsFromOutsideHotel;
        private bool expIsPublicHoliday;
        private string expEventNotes;
        private string expGSourceOfBusinessNotes;
        private int expOutletId;
        private string expUserId;
        private int? expLocalEventId;
        private List<string> userIds;

        // Constructor
        public FbReportIntegTests(TestingWebAppFactory<Startup> factory)
        {
            client = factory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:44306/");

            userIds = new List<string>
            {
                "35947f01-393b-442c-b815-d6d9f7d4b81e",
                "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
            };
        }

        //*************************************** Testing GET /api/FbReports/{id} ***********************************************

        [Fact]
        // Testing GET /api/FbReports/1
        public async Task get_FbReport_1()
        {
            // Arrange
            int id = 1;
            string URL = $"api/FbReports/{id}";

            // Act
            FbReport responseReport = await GetResponse(URL);

            CheckFbReport
            (
                expId = id,
                expTables = 1,
                expFood = 10000,
                expBeverage = 20000,
                expOtherIncome = 5000,
                expGuestsFromHotelTP = 15,
                expGuestsFromHotelTM = 0,
                expguestsFromOutsideHotel = 10,
                expIsPublicHoliday = false,
                expEventNotes = "The DJ got everybody dancing",
                expGSourceOfBusinessNotes = "A lot of people just dropped in at around 1:00 AM",
                expOutletId = 1,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = 2,
                responseReport
            );
        }
        
        [Fact]
        // Testing GET /api/FbReports/2
        public async Task get_FbReport_2()
        {
            // Arrange
            int id = 2;
            string URL = $"api/FbReports/{id}";

            // Act
            FbReport responseReport = await GetResponse(URL);

            CheckFbReport
            (
                expId = id,
                expTables = 14,
                expFood = 19000,
                expBeverage = 31000,
                expOtherIncome = 9100,
                expGuestsFromHotelTP = 25,
                expGuestsFromHotelTM = 0,
                expguestsFromOutsideHotel = 4,
                expIsPublicHoliday = false,
                expEventNotes = "The DJ was really good",
                expGSourceOfBusinessNotes = "A lot of peolpe came from Google Search",
                expOutletId = 1,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = 3,
                responseReport
            );
        }

        [Fact]
        // Testing GET /api/FbReports/3
        public async Task get_FbReport_3()
        {
            // Arrange
            int id = 3;
            string URL = $"api/FbReports/{id}";

            // Act
            FbReport responseReport = await GetResponse(URL);

            CheckFbReport
            (
                expId = id,
                expTables = 19,
                expFood = 15000,
                expBeverage = 21000,
                expOtherIncome = 6500,
                expGuestsFromHotelTP = 35,
                expGuestsFromHotelTM = 0,
                expguestsFromOutsideHotel = 18,
                expIsPublicHoliday = false,
                expEventNotes = "The Flamenco dance lesson was quite nice, had many people dancing",
                expGSourceOfBusinessNotes = "Instagram",
                expOutletId = 1,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = 4,
                responseReport
            );
        }

        [Fact]
        // Testing GET /api/FbReports/4
        public async Task get_FbReport_4()
        {
            // Arrange
            int id = 4;
            string URL = $"api/FbReports/{id}";

            // Act
            FbReport responseReport = await GetResponse(URL);

            CheckFbReport
            (
                expId = id,
                expTables = 16,
                expFood = 27000,
                expBeverage = 28000,
                expOtherIncome = 51000,
                expGuestsFromHotelTP = 11,
                expGuestsFromHotelTM = 0,
                expguestsFromOutsideHotel = 44,
                expIsPublicHoliday = false,
                expEventNotes = "The DJ was a star",
                expGSourceOfBusinessNotes = null,
                expOutletId = 1,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = null,
                responseReport
            );
        }

        [Fact]
        // Testing GET /api/FbReports/5
        public async Task get_FbReport_5()
        {
            // Arrange
            int id = 5;
            string URL = $"api/FbReports/{id}";

            // Act
            FbReport responseReport = await GetResponse(URL);

            CheckFbReport
            (
                expId = id,
                expTables = 10,
                expFood = 88000,
                expBeverage = 91000,
                expOtherIncome = 17400,
                expGuestsFromHotelTP = 29,
                expGuestsFromHotelTM = 0,
                expguestsFromOutsideHotel = 21,
                expIsPublicHoliday = true,
                expEventNotes = "Umpa Umpa DJ",
                expGSourceOfBusinessNotes = "Hectic day.A lot of Germans.Since they didn't speak english " +
                    "we were unable to find out how they got to know about the Umpa Umpa Madness Night",
                expOutletId = 2,
                expUserId = userIds.ElementAt(1),
                expLocalEventId = 2,
                responseReport
            );
        }

        [Fact]
        // Testing GET /api/FbReports/6
        public async Task get_FbReport_6()
        {
            // Arrange
            int id = 6;
            string URL = $"api/FbReports/{id}";

            // Act
            FbReport responseReport = await GetResponse(URL);

            CheckFbReport
            (
                expId = id,
                expTables = 20,
                expFood = 21000,
                expBeverage = 32000,
                expOtherIncome = 8500,
                expGuestsFromHotelTP = 24,
                expGuestsFromHotelTM = 0,
                expguestsFromOutsideHotel = 30,
                expIsPublicHoliday = false,
                expEventNotes = "The samba night was a success especially with the Italians",
                expGSourceOfBusinessNotes = "Most of the guest had been handed leaflets down town",
                expOutletId = 4,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = 1,
                responseReport
            );
        }

        [Fact]
        // Testing GET /api/FbReports/7
        public async Task get_FbReport_7()
        {
            // Arrange
            int id = 7;
            string URL = $"api/FbReports/{id}";

            // Act
            FbReport responseReport = await GetResponse(URL);

            CheckFbReport
            (
                expId = id,
                expTables = 20,
                expFood = 21000,
                expBeverage = 32000,
                expOtherIncome = 8500,
                expGuestsFromHotelTP = 24,
                expGuestsFromHotelTM = 0,
                expguestsFromOutsideHotel = 30,
                expIsPublicHoliday = false,
                expEventNotes = "Busy night",
                expGSourceOfBusinessNotes = "A lot of the guests came from Agent referral",
                expOutletId = 4,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = 1,
                responseReport
            );
        }

        [Fact]
        // Testing GET /api/FbReports/999. Since no FbReport exists with Id 999 the return from GetResponse should be null
        public async Task get_FbReport_999()
        {
            // Arrange
            int id = 999;
            string URL = $"api/FbReports/{id}";

            // Act
            FbReport responseReport = await GetResponse(URL);

            // Assert
            Assert.Null(responseReport);
        }

        // ************************************** GetResponse ****************************************************************
        private async Task<FbReport> GetResponse(string URL)
        {
            var response = await client.GetAsync(URL);

            try
            {
                response.EnsureSuccessStatusCode();
                FbReport responseReport = JsonConvert.DeserializeObject<FbReport>(await response.Content.ReadAsStringAsync());
                return responseReport;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // *************************************** CheckFbReport *************************************************************
        private static void CheckFbReport(
            int expFbReportId, int expTables, int expFood, int expBeverage, int expOtherIncome, 
            int expGuestsFromHotelTP, int expGuestsFromHotelTM, int expGuestsFromOutsideHotel, bool expIsPublicHoliday, string expEventNotes,
            string expGSourceOfBusinessNotes, int expOutletId,  string expUserId, int? expLocalEventId, 
            FbReport fbReport
        )
        {
            // Act *****************************************
            int actFbReportId = fbReport.Id;
            int? actTables = fbReport.Tables;
            int? actFood = fbReport.Food;
            int? actBeverage = fbReport.Beverage;
            int? actOtherIncome = fbReport.OtherIncome;
            int? actGuestsFromHotelTP = fbReport.GuestsFromHotelTP;
            int? actGuestsFromHotelTM = fbReport.GuestsFromHotelTM;
            int? actGuestsFromOutsideHotel = fbReport.GuestsFromOutsideHotel;
            bool actIsPublicHoliday = fbReport.IsPublicHoliday;
            string actEventNotes = fbReport.EventNotes;
            int actOutletId = fbReport.OutletId;
            string actUserId = fbReport.UserId;
            int? actLocalEventId = fbReport.LocalEventId;


            // Assert **************************************
            Assert.Equal(expFbReportId, actFbReportId);
            Assert.Equal(expTables, actTables);
            Assert.Equal(expFood, actFood);
            Assert.Equal(expBeverage, actBeverage);
            Assert.Equal(expOtherIncome, actOtherIncome);
            Assert.Equal(expGuestsFromHotelTP, actGuestsFromHotelTP);
            Assert.Equal(expGuestsFromHotelTP, actGuestsFromHotelTP);
            Assert.Equal(expGuestsFromOutsideHotel, actGuestsFromOutsideHotel);
            Assert.Equal(expIsPublicHoliday.ToString(), actIsPublicHoliday.ToString());
            Assert.Equal(expEventNotes, actEventNotes);
            Assert.Equal(expOutletId, actOutletId);
            Assert.Equal(expUserId, actUserId);
            Assert.Equal(expLocalEventId, actLocalEventId);
        }
    }
}