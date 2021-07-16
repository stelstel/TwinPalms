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
            List<int> outletIds = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            DateTime fromDate = new DateTime(2020, 01, 01);
            DateTime toDate = new DateTime(2022, 01, 01);
            List<GuestSourceOfBusiness> expGsobs = new List<GuestSourceOfBusiness>();
            List<int> expGsobNrOfGuests = new List<int>();
            List<Weather> expWeathers = new List<Weather>();

            string URL = $"outlets/fbReports?" +
                $"outletIds={outletIds.ElementAt(0)}&" +
                $"outletIds={outletIds.ElementAt(1)}&" +
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
            //var report0 = responseString[0]; // Getting 1 report from the report array

            //GuestSourceOfBusiness gsob1 = new GuestSourceOfBusiness { Id = 3, SourceOfBusiness = gsobs[2].SourceOfBusiness };
            //GuestSourceOfBusiness gsob2 = new GuestSourceOfBusiness { Id = 4, SourceOfBusiness = gsobs[3].SourceOfBusiness };

            //expGsobs.Clear();
            //expGsobs.Add(gsob1);
            //expGsobs.Add(gsob2);

            //expGsobNrOfGuests.Clear();
            //expGsobNrOfGuests.Add(22);
            //expGsobNrOfGuests.Add(13);

            //Weather weather1 = new Weather { Id = 6, TypeOfWeather = weathers[5].TypeOfWeather };

            //expWeathers.Clear();
            //expWeathers.Add(weather1);

            //CheckFbReport
            //(
            //    expectedTables = 169,
            //    expFood = 19487,
            //    expBeverage = 100248,
            //    expOtherIncome = 48962,
            //    expGuestsFromHotelTP = 43,
            //    expGuestsFromHotelTM = 1,
            //    expguestsFromOutsideHotel = 60,
            //    expIsPublicHoliday = false,
            //    expEventNotes = eventNotes[0],
            //    expGSourceOfBusinessNotes = gsobNotes[0],
            //    expOutletId = 6,
            //    expUserId = userIds.ElementAt(0),
            //    expLocalEventId = 2,
            //    expDate = new DateTime(2021, 01, 02, 3, 40, 45),
            //    expGsobs,
            //    expGsobNrOfGuests,
            //    expWeathers,
            //    report0
            //);
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