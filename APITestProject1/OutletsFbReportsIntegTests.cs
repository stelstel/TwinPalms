using Contracts;
using Entities.Models;
using LoggerService;
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
    public class OutletsFbReportsIntegTests : IClassFixture<TestingWebAppFactory<Startup>>
    {
        // exp is short for expected
        // act is short for actual

        private readonly HttpClient _client;
        private ILoggerManager logger = new LoggerManager();
        private List<GuestSourceOfBusiness> gsobs = new List<GuestSourceOfBusiness>();
        private List<Weather> weathers = new List<Weather>();
        private List<string> notes = new List<string>();
        private List<string> eventNotes = new List<string>();
        private List<string> gsobNotes = new List<string>();
        private List<string> userIds;

        // Constructor
        public OutletsFbReportsIntegTests(TestingWebAppFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44306/");

            gsobs = TestObjGsobs;
            weathers = TestObjWeathers;
            notes = TestObjNotes;
            eventNotes = TestObjEventNotes;
            gsobNotes = TestObjGSOBNotes;
            userIds = TestObjUserIds;
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
            int expGuestsFromHotelTP;
            int expGuestsFromHotelTM;
            int expguestsFromOutsideHotel;
            bool expIsPublicHoliday;
            string expEventNotes;
            string expGSourceOfBusinessNotes;
            int expOutletId;
            string expUserId;
            int? expLocalEventId;
            DateTime expDate;
            List<int> outletIds = new List<int> { 1, 2, 4 };
            DateTime fromDate = new DateTime(2021, 01, 01);
            DateTime toDate = new DateTime(2022, 01, 01);
            List<GuestSourceOfBusiness> expGsobs = new List<GuestSourceOfBusiness>();
            List<int> expGsobNrOfGuests = new List<int>();
            List<Weather> expWeathers = new List<Weather>();

            string URL = $"outlets/fbReports?outletIds={outletIds.ElementAt(0)}&outletIds={outletIds.ElementAt(1)}&outletIds={outletIds.ElementAt(2)}&" +
                $"fromDate={fromDate}&toDate={toDate}";

            expectedNrOfReports = TestObjNrOfReports;


            // Act
            var response = await _client.GetAsync(URL);
            response.EnsureSuccessStatusCode();
            var responseString = JArray.Parse(await response.Content.ReadAsStringAsync());
            int actualNrOfReports = responseString.Count();


            // Assert
            Assert.Equal(expectedNrOfReports, actualNrOfReports);


            // Check 1 ------
            var report0 = responseString[0]; // Getting 1 report from the report array

            GuestSourceOfBusiness gsob1 = new GuestSourceOfBusiness { Id = 3, SourceOfBusiness = gsobs[2].SourceOfBusiness };
            GuestSourceOfBusiness gsob2 = new GuestSourceOfBusiness { Id = 4, SourceOfBusiness = gsobs[3].SourceOfBusiness };

            expGsobs.Clear();
            expGsobs.Add(gsob1);
            expGsobs.Add(gsob2);

            expGsobNrOfGuests.Clear();
            expGsobNrOfGuests.Add(22);
            expGsobNrOfGuests.Add(13);

            Weather weather1 = new Weather { Id = 6, TypeOfWeather = weathers[5].TypeOfWeather };

            expWeathers.Clear();
            expWeathers.Add(weather1);

            CheckFbReport
            (
                expectedTables = 1,
                expFood = 10000,
                expBeverage = 20000,
                expOtherIncome = 5000,
                expGuestsFromHotelTP = 15,
                expGuestsFromHotelTM = 13,
                expguestsFromOutsideHotel = 10,
                expIsPublicHoliday = false,
                expEventNotes = eventNotes[0],
                expGSourceOfBusinessNotes = gsobNotes[0],
                expOutletId = 1,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = 2,
                expDate = new DateTime(2021, 12, 2, 4, 0, 0),
                expGsobs,
                expGsobNrOfGuests,
                expWeathers,
                report0
            );

            // Check 2 ------
            var report1 = responseString[1];

            expGsobs.Clear();
            expGsobNrOfGuests.Clear();
            expWeathers.Clear();

            CheckFbReport
            (
                expectedTables = 10,
                expFood = 88000,
                expBeverage = 91000,
                expOtherIncome = 17400,
                expGuestsFromHotelTP = 29,
                expGuestsFromHotelTM = 21,
                expguestsFromOutsideHotel = 21,
                expIsPublicHoliday = true,
                expEventNotes = eventNotes[4],
                expGSourceOfBusinessNotes = gsobNotes[4],
                expOutletId = 2,
                expUserId = userIds.ElementAt(1),
                expLocalEventId = 2,
                expDate = new DateTime(2021, 11, 2, 4, 0, 0),
                expGsobs,
                expGsobNrOfGuests,
                expWeathers,
                report1
            );

            // Check 3 ------
            var report2 = responseString[2];

            expGsobs.Clear();
            expGsobNrOfGuests.Clear();
            expWeathers.Clear();

            CheckFbReport
            (
                expectedTables = 20,
                expFood = 21000,
                expBeverage = 32000,
                expOtherIncome = 8500,
                expGuestsFromHotelTP = 24,
                expGuestsFromHotelTM = 20,
                expguestsFromOutsideHotel = 30,
                expIsPublicHoliday = false,
                expEventNotes = eventNotes[5],
                expGSourceOfBusinessNotes = gsobNotes[5],
                expOutletId = 4,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = 1,
                expDate = new DateTime(2021, 10, 3, 3, 29, 0),
                expGsobs,
                expGsobNrOfGuests,
                expWeathers,
                report2
            );

            // Check 4 ------
            var report3 = responseString[3];
            
            expGsobs.Clear();
            expGsobNrOfGuests.Clear();
            expWeathers.Clear();

            CheckFbReport
            (
                expectedTables = 20,
                expFood = 21000,
                expBeverage = 32000,
                expOtherIncome = 8500,
                expGuestsFromHotelTP = 24,
                expGuestsFromHotelTM = 21,
                expguestsFromOutsideHotel = 30,
                expIsPublicHoliday = false,
                expEventNotes = eventNotes[6],
                expGSourceOfBusinessNotes = gsobNotes[6],
                expOutletId = 4,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = 1,
                expDate = new DateTime(2021, 9, 5, 2, 39, 10),
                expGsobs,
                expGsobNrOfGuests,
                expWeathers,
                report3
            );

            // Check 5 ------
            var report4 = responseString[4];

            GuestSourceOfBusiness gsob41 = new GuestSourceOfBusiness { Id = 1, SourceOfBusiness = gsobs[0].SourceOfBusiness };
            GuestSourceOfBusiness gsob42 = new GuestSourceOfBusiness { Id = 2, SourceOfBusiness = gsobs[1].SourceOfBusiness };

            expGsobs.Clear();
            expGsobs.Add(gsob41);
            expGsobs.Add(gsob42);

            expGsobNrOfGuests.Clear();
            expGsobNrOfGuests.Add(32);
            expGsobNrOfGuests.Add(3);

            Weather weather41 = new Weather { Id = 5, TypeOfWeather = weathers[4].TypeOfWeather };

            expWeathers.Clear();
            expWeathers.Add(weather41);

            CheckFbReport
            (
                expectedTables = 14,
                expFood = 19000,
                expBeverage = 31000,
                expOtherIncome = 9100,
                expGuestsFromHotelTP = 25,
                expGuestsFromHotelTM = 9,
                expguestsFromOutsideHotel = 4,
                expIsPublicHoliday = false,
                expEventNotes = eventNotes[1],
                expGSourceOfBusinessNotes = gsobNotes[1],
                expOutletId = 1,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = 3,
                expDate = new DateTime(2021, 8, 6, 1, 19, 42),
                expGsobs,
                expGsobNrOfGuests,
                expWeathers,
                report4
            );

            // Check 6 ------
            var report5 = responseString[5];

            GuestSourceOfBusiness gsob51 = new GuestSourceOfBusiness { Id = 1, SourceOfBusiness = gsobs[0].SourceOfBusiness };
            GuestSourceOfBusiness gsob52 = new GuestSourceOfBusiness { Id = 5, SourceOfBusiness = gsobs[4].SourceOfBusiness };

            expGsobs.Clear();
            expGsobs.Add(gsob51);
            expGsobs.Add(gsob52);

            expGsobNrOfGuests.Clear();
            expGsobNrOfGuests.Add(45);
            expGsobNrOfGuests.Add(22);

            Weather weather51 = new Weather { Id = 1, TypeOfWeather = weathers[0].TypeOfWeather };
            Weather weather52 = new Weather { Id = 2, TypeOfWeather = weathers[1].TypeOfWeather };
            Weather weather53 = new Weather { Id = 5, TypeOfWeather = weathers[4].TypeOfWeather };
            Weather weather54 = new Weather { Id = 6, TypeOfWeather = weathers[5].TypeOfWeather };

            expWeathers.Clear();
            expWeathers.Add(weather51);
            expWeathers.Add(weather52);
            expWeathers.Add(weather53);
            expWeathers.Add(weather54);

            CheckFbReport
            (
                expectedTables = 19,
                expFood = 15000,
                expBeverage = 21000,
                expOtherIncome = 6500,
                expGuestsFromHotelTP = 35,
                expGuestsFromHotelTM = 22,
                expguestsFromOutsideHotel = 18,
                expIsPublicHoliday = false,
                expEventNotes = eventNotes[2],
                expGSourceOfBusinessNotes = gsobNotes[2],
                expOutletId = 1,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = 4,
                expDate = new DateTime(2021, 7, 12, 1, 4, 9),
                expGsobs,
                expGsobNrOfGuests,
                expWeathers,
                report5
            );

            // Check 7 ------
            var report6 = responseString[6];

            expGsobs.Clear();
            expGsobNrOfGuests.Clear();
            expWeathers.Clear();

            CheckFbReport
            (
                expectedTables = 16,
                expFood = 27000,
                expBeverage = 28000,
                expOtherIncome = 51000,
                expGuestsFromHotelTP = 11,
                expGuestsFromHotelTM = 14,
                expguestsFromOutsideHotel = 44,
                expIsPublicHoliday = false,
                expEventNotes = eventNotes[3],
                expGSourceOfBusinessNotes = gsobNotes[3],
                expOutletId = 1,
                expUserId = userIds.ElementAt(0),
                expLocalEventId = null,
                expDate = new DateTime(2021, 6, 22, 1, 19, 44),
                expGsobs,
                expGsobNrOfGuests,
                expWeathers,
                report6
            );
        }

        // *************************************** CheckFbReport *************************************************************
        private static void CheckFbReport(
            int expectedTables, int expFood, int expBeverage, int expOtherIncome, 
            int expGuestsFromHotelTP, int expGuestsFromHotelTM, int expGuestsFromOutsideHotel, bool expIsPublicHoliday, string expEventNotes,
            string expGSourceOfBusinessNotes, int expOutletId,  string expUserId, int? expLocalEventId, DateTime expDate, 
            List<GuestSourceOfBusiness> expGsobs, List<int> expGsobNrOfGuests, List<Weather> expWeathers,
            JToken report
        )
        {
            // Act *****************************************
            int actualTables = (int)report["tables"];
            int actFood = (int)report["food"];
            int actBeverage = (int)report["beverage"];
            int actOtherIncome = (int)report["otherIncome"];
            int actGuestsFromHotelTP = (int)report["guestsFromHotelTP"];
            int actGuestsFromHotelTM = (int)report["guestsFromHotelTM"];
            int actGuestsFromOutsideHotel = (int)report["guestsFromOutsideHotel"];
            bool actIsPublicHoliday = (bool)report["isPublicHoliday"];
            string actEventNotes = (string)report["eventNotes"];
            int actOutletId = (int)report["outletId"];
            string actUserId = (string)report["userId"];
            int? actLocalEventId = (int?)report["localEventId"];
            DateTime actDate = (DateTime)report["date"];
            string actGSourceOfBusinessNotes = (string)report["gSourceOfBusinessNotes"];

            // Getting actual GuestSourceOfBusinesses
            List<GuestSourceOfBusiness> actGsobs = new List<GuestSourceOfBusiness>();

            for (int i = 0; i < report["guestSourceOfBusinesses"].Count(); i++)
            {
                GuestSourceOfBusiness actGsob = new GuestSourceOfBusiness { 
                    Id = (int)report["guestSourceOfBusinesses"][i]["id"],
                    SourceOfBusiness = (string)report["guestSourceOfBusinesses"][i]["sourceOfBusiness"]
                };

                actGsobs.Add(actGsob);
            }

            // Getting actual gsobNrOfGuest
            List<int> actGsobNrOfGuests = new List<int>();
            var tempConvert = report["gsobNrOfGuest"];

            foreach (var item in tempConvert)
            {
                actGsobNrOfGuests.Add((int)item);
            }

            // Getting actual weathers
            List<Weather> actWeathers = new List<Weather>();

            for (int i = 0; i < report["weathers"].Count(); i++)
            {
                Weather weather = new Weather
                {
                    Id = (int)report["weathers"][i]["id"],
                    TypeOfWeather = (string)report["weathers"][i]["typeOfWeather"]
                };

                actWeathers.Add(weather);
            }

            // Assert
            Assert.Equal(expectedTables, actualTables);
            Assert.Equal(expFood, actFood);
            Assert.Equal(expBeverage, actBeverage);
            Assert.Equal(expOtherIncome, actOtherIncome);
            Assert.Equal(expGuestsFromHotelTP, actGuestsFromHotelTP);
            Assert.Equal(expGuestsFromHotelTM, actGuestsFromHotelTM);
            Assert.Equal(expGuestsFromOutsideHotel, actGuestsFromOutsideHotel);
            Assert.Equal(expIsPublicHoliday.ToString(), actIsPublicHoliday.ToString());
            Assert.Equal(expEventNotes, actEventNotes);
            Assert.Equal(expGSourceOfBusinessNotes, actGSourceOfBusinessNotes);
            Assert.Equal(expOutletId, actOutletId);
            Assert.Equal(expUserId, actUserId);
            Assert.Equal(expLocalEventId, actLocalEventId);
            Assert.Equal(expDate, actDate);

            Assert.NotEqual(expFood, actGuestsFromHotelTP);

            for (int i = 0; i < expGsobs.Count(); i++)
            {
                Assert.Equal(expGsobs.ElementAt(i).Id, actGsobs.ElementAt(i).Id);
                Assert.Equal(expGsobs.ElementAt(i).SourceOfBusiness, actGsobs.ElementAt(i).SourceOfBusiness);
            }

            Assert.Equal(expGsobNrOfGuests, actGsobNrOfGuests);

            for (int i = 0; i < expWeathers.Count(); i++)
            {
                Assert.Equal(expWeathers.ElementAt(i).Id, actWeathers.ElementAt(i).Id);
                Assert.Equal(expWeathers.ElementAt(i).TypeOfWeather, actWeathers.ElementAt(i).TypeOfWeather);
            }
        }
    }
}