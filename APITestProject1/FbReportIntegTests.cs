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
using static APITestProject1.testObjects;

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
        private int expGuestsFromOutsideHotel;
        private bool expIsPublicHoliday;
        private string expEventNotes;
        private string expGSourceOfBusinessNotes;
        private string expNotes;
        private int expOutletId;
        private string expUserId;
        private int? expLocalEventId;
        private DateTime expDate;

        private List<string> userIds;
        private List<string> notes = new List<string>();
        private List<string> eventNotes = new List<string>();
        private List<string> gsobNotes = new List<string>();


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

            notes = testObjectsNotes;
            eventNotes = testObjectsEventNotes;
            gsobNotes = testObjectsGSOBNotes;
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
            FbReport responseReport = await GetOneResponse(URL);

            CheckFbReport
            (
                expId = id,
                expTables = 1,
                expFood = 10000,
                expBeverage = 20000,
                expOtherIncome = 5000,
                expGuestsFromHotelTP = 15,
                expGuestsFromHotelTM = 13,
                expGuestsFromOutsideHotel = 10,
                expIsPublicHoliday = false,
                expEventNotes = eventNotes[0],
                expGSourceOfBusinessNotes = gsobNotes[0],
                expNotes = notes[0],
                expOutletId = 1,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = 2,
                expDate = new DateTime(2021, 12, 2, 4, 0, 0),
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
            FbReport responseReport = await GetOneResponse(URL);

            CheckFbReport
            (
                expId = id,
                expTables = 14,
                expFood = 19000,
                expBeverage = 31000,
                expOtherIncome = 9100,
                expGuestsFromHotelTP = 25,
                expGuestsFromHotelTM = 9,
                expGuestsFromOutsideHotel = 4,
                expIsPublicHoliday = false,
                expEventNotes = eventNotes[1],
                expGSourceOfBusinessNotes = gsobNotes[1],
                expNotes = notes[1],
                expOutletId = 1,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = 3,
                expDate = new DateTime(2021, 8, 6, 1, 19, 42),
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
            FbReport responseReport = await GetOneResponse(URL);

            CheckFbReport
            (
                expId = id,
                expTables = 19,
                expFood = 15000,
                expBeverage = 21000,
                expOtherIncome = 6500,
                expGuestsFromHotelTP = 35,
                expGuestsFromHotelTM = 22,
                expGuestsFromOutsideHotel = 18,
                expIsPublicHoliday = false,
                expEventNotes = eventNotes[2],
                expGSourceOfBusinessNotes = gsobNotes[2],
                expNotes = notes[2],
                expOutletId = 1,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = 4,
                expDate = new DateTime(2021, 7, 12, 1, 4, 9),
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
            FbReport responseReport = await GetOneResponse(URL);

            CheckFbReport
            (
                expId = id,
                expTables = 16,
                expFood = 27000,
                expBeverage = 28000,
                expOtherIncome = 51000,
                expGuestsFromHotelTP = 11,
                expGuestsFromHotelTM = 14,
                expGuestsFromOutsideHotel = 44,
                expIsPublicHoliday = false,
                expEventNotes = eventNotes[3],
                expGSourceOfBusinessNotes = gsobNotes[3],
                expNotes = notes[3],
                expOutletId = 1,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = null,
                expDate = new DateTime(2021, 6, 22, 1, 19, 44),
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
            FbReport responseReport = await GetOneResponse(URL);

            CheckFbReport
            (
                expId = id,
                expTables = 10,
                expFood = 88000,
                expBeverage = 91000,
                expOtherIncome = 17400,
                expGuestsFromHotelTP = 29,
                expGuestsFromHotelTM = 21,
                expGuestsFromOutsideHotel = 21,
                expIsPublicHoliday = true,
                expEventNotes = eventNotes[4],
                expGSourceOfBusinessNotes = gsobNotes[4],
                expNotes = notes[4],
                expOutletId = 2,
                expUserId = userIds.ElementAt(1),
                expLocalEventId = 2,
                expDate = new DateTime(2021, 11, 2, 4, 0, 0),
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
            FbReport responseReport = await GetOneResponse(URL);

            CheckFbReport
            (
                expId = id,
                expTables = 20,
                expFood = 21000,
                expBeverage = 32000,
                expOtherIncome = 8500,
                expGuestsFromHotelTP = 24,
                expGuestsFromHotelTM = 20,
                expGuestsFromOutsideHotel = 30,
                expIsPublicHoliday = false,
                expEventNotes = eventNotes[5],
                expGSourceOfBusinessNotes = gsobNotes[5],
                expNotes = notes[5],
                expOutletId = 4,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = 1,
                expDate = new DateTime(2021, 10, 3, 3, 29, 0),
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
            FbReport responseReport = await GetOneResponse(URL);

            CheckFbReport
            (
                expId = id,
                expTables = 20,
                expFood = 21000,
                expBeverage = 32000,
                expOtherIncome = 8500,
                expGuestsFromHotelTP = 24,
                expGuestsFromHotelTM = 21,
                expGuestsFromOutsideHotel = 30,
                expIsPublicHoliday = false,
                expEventNotes = eventNotes[6],
                expGSourceOfBusinessNotes = gsobNotes[6],
                expNotes = notes[6],
                expOutletId = 4,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = 1,
                expDate = new DateTime(2021, 9, 5, 2, 39, 10),
                responseReport
            );
        }

        [Fact]
        // Testing GET /api/FbReports/999. Since no FbReport exists with Id 999 the return from GetOneResponse should be null
        public async Task get_FbReport_999()
        {
            // Arrange
            int id = 999;
            string URL = $"api/FbReports/{id}";

            // Act
            FbReport responseReport = await GetOneResponse(URL);

            // Assert
            Assert.Null(responseReport);
        }

        
        [Fact]
        // *************** Testing GET /api/FbReports. How many reports gets returned? *******************************
        public async Task get_FbReports_length()
        {
            // Arrange
            string URL = $"api/FbReports";
            int expNrOfReports = TestObjNrOfReports;

            List<FbReport> responseReports = new List<FbReport>();

            // Act
            responseReports = await GetResponseList(URL);
            int actNrOfReports = responseReports.Count();

            // Assert
            Assert.Equal(expNrOfReports, actNrOfReports);
        }

        // ************************************** GetOneResponse ****************************************************************
        private async Task<FbReport> GetOneResponse(string URL)
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

        // ************************************** GetResponseList ****************************************************************
        private async Task<List<FbReport>> GetResponseList(string URL)
        {
            var response = await client.GetAsync(URL);
            response.EnsureSuccessStatusCode();
            List<FbReport> responseReports = JsonConvert.DeserializeObject<List<FbReport>>(await response.Content.ReadAsStringAsync());
            return responseReports;
        }

        // *************************************** CheckFbReport *************************************************************
        private static void CheckFbReport(
            int expFbReportId, int expTables, int expFood, int expBeverage, int expOtherIncome, 
            int expGuestsFromHotelTP, int expGuestsFromHotelTM, int expGuestsFromOutsideHotel, bool expIsPublicHoliday, string expEventNotes,
            string expGSourceOfBusinessNotes, string expNotes, int expOutletId,  string expUserId, int? expLocalEventId, DateTime expDate,
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
            string actGSourceOfBusinessNotes = fbReport.GSourceOfBusinessNotes;
            string actNotes = fbReport.Notes;
            int actOutletId = fbReport.OutletId;
            string actUserId = fbReport.UserId;
            int? actLocalEventId = fbReport.LocalEventId;
            DateTime actDate = fbReport.Date;


            // Assert **************************************
            Assert.Equal(expFbReportId, actFbReportId);
            Assert.Equal(expTables, actTables);
            Assert.Equal(expFood, actFood);
            Assert.Equal(expBeverage, actBeverage);
            Assert.Equal(expOtherIncome, actOtherIncome);
            Assert.Equal(expGuestsFromHotelTP, actGuestsFromHotelTP);
            Assert.Equal(expGuestsFromHotelTM, actGuestsFromHotelTM);
            Assert.Equal(expGuestsFromOutsideHotel, actGuestsFromOutsideHotel);
            Assert.Equal(expIsPublicHoliday.ToString(), actIsPublicHoliday.ToString());
            Assert.Equal(expEventNotes, actEventNotes);
            Assert.Equal(expGSourceOfBusinessNotes, actGSourceOfBusinessNotes);
            Assert.Equal(expNotes, actNotes);
            Assert.Equal(expOutletId, actOutletId);
            Assert.Equal(expUserId, actUserId);
            Assert.Equal(expLocalEventId, actLocalEventId);
            Assert.Equal(expDate, actDate);

            Assert.NotEqual(expFood, actGuestsFromHotelTM);
        }
    }
}