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
        
        public OutletsFbReportsIntegTests(TestingWebAppFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44306/");

            gsobs.Add(new GuestSourceOfBusiness() { Id = 1, SourceOfBusiness = "Hotel Website" });
            gsobs.Add(new GuestSourceOfBusiness() { Id = 2, SourceOfBusiness = "Hungry Hub" });
            gsobs.Add(new GuestSourceOfBusiness() { Id = 3, SourceOfBusiness = "Facebook referral" });
            gsobs.Add(new GuestSourceOfBusiness() { Id = 4, SourceOfBusiness = "Google search" });
            gsobs.Add(new GuestSourceOfBusiness() { Id = 5, SourceOfBusiness = "Instagram referral" });
            gsobs.Add(new GuestSourceOfBusiness() { Id = 6, SourceOfBusiness = "Hotel referral" });
            gsobs.Add(new GuestSourceOfBusiness() { Id = 7, SourceOfBusiness = "Other Hotel referral" });
            gsobs.Add(new GuestSourceOfBusiness() { Id = 8, SourceOfBusiness = "Agent referral" });
            gsobs.Add(new GuestSourceOfBusiness() { Id = 9, SourceOfBusiness = "Walk in" });
            gsobs.Add(new GuestSourceOfBusiness() { Id = 10, SourceOfBusiness = "Other" });

            weathers.Add(new Weather(){ Id = 1, TypeOfWeather = "Sunny/Clear" });
            weathers.Add(new Weather(){ Id = 2, TypeOfWeather = "Partially Cloudy" });
            weathers.Add(new Weather(){ Id = 3, TypeOfWeather = "Overcast" });
            weathers.Add(new Weather(){ Id = 4, TypeOfWeather = "Rain" });
            weathers.Add(new Weather(){ Id = 5, TypeOfWeather = "Showers" });
            weathers.Add(new Weather(){ Id = 6, TypeOfWeather = "Stormy" });
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
            int? expLocalEventId;
            List<int> outletIds = new List<int> { 1, 2, 4 };
            DateTime fromDate = new DateTime(2021, 01, 01);
            DateTime toDate = new DateTime(2022, 01, 01);
            List<GuestSourceOfBusiness> expGsobs = new List<GuestSourceOfBusiness>();
            List<int> expGsobNrOfGuests = new List<int>();
            List<Weather> expWeathers = new List<Weather>();
            string URL = $"outlets/fbReports?outletIds={outletIds.ElementAt(0)}&outletIds={outletIds.ElementAt(1)}&outletIds={outletIds.ElementAt(2)}&" +
                $"fromDate={fromDate}&toDate={toDate}";
            expectedNrOfReports = 7;


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
                expGuestsFromHotel = 15,
                expguestsFromOutsideHotel = 10,
                expIsPublicHoliday = false,
                expEventNotes = "The DJ got everybody dancing",
                expGSourceOfBusinessNotes = "A lot of people just dropped in at around 1:00 AM",
                expOutletId = 1,
                expUserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                expLocalEventId = 2,
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
                expGuestsFromHotel = 29,
                expguestsFromOutsideHotel = 21,
                expIsPublicHoliday = true,
                expEventNotes = "Umpa Umpa DJ",
                expGSourceOfBusinessNotes = "Hectic day. A lot of Germans. Since they didn't speak english " +
                    "we were unable to find out how they got to know about the Umpa Umpa Madness Night",
                expOutletId = 2,
                expUserId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157",
                expLocalEventId = 2,
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
                expGuestsFromHotel = 24,
                expguestsFromOutsideHotel = 30,
                expIsPublicHoliday = false,
                expEventNotes = "The samba night was a success especially with the Italians",
                expGSourceOfBusinessNotes = "Most of the guest had been handed leaflets down town",
                expOutletId = 4,
                expUserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                expLocalEventId = 1,
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
                expGuestsFromHotel = 24,
                expguestsFromOutsideHotel = 30,
                expIsPublicHoliday = false,
                expEventNotes = "Busy night",
                expGSourceOfBusinessNotes = "A lot of the guests came from Agent referral",
                expOutletId = 4,
                expUserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                expLocalEventId = 1,
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
                expGuestsFromHotel = 25,
                expguestsFromOutsideHotel = 4,
                expIsPublicHoliday = false,
                expEventNotes = "The DJ was really good",
                expGSourceOfBusinessNotes = "A lot of peolpe came from Google Search",
                expOutletId = 1,
                expUserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                expLocalEventId = 3,
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
                expGuestsFromHotel = 35,
                expguestsFromOutsideHotel = 18,
                expIsPublicHoliday = false,
                expEventNotes = "The Flamenco dance lesson was quite nice, had many people dancing",
                expGSourceOfBusinessNotes = "Instagram",
                expOutletId = 1,
                expUserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                expLocalEventId = 4,
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
                expGuestsFromHotel = 11,
                expguestsFromOutsideHotel = 44,
                expIsPublicHoliday = false,
                expEventNotes = "The DJ was a star",
                expGSourceOfBusinessNotes = null,
                expOutletId = 1,
                expUserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                expLocalEventId = null,
                expGsobs,
                expGsobNrOfGuests,
                expWeathers,
                report6
            );
        }

        // *************************************** CheckFbReport *************************************************************
        private static void CheckFbReport(
            int expectedTables, int expFood, int expBeverage, int expOtherIncome, 
            int expGuestsFromHotel, int expGuestsFromOutsideHotel, bool expIsPublicHoliday, string expEventNotes,
            string expGSourceOfBusinessNotes, int expOutletId,  string expUserId, int? expLocalEventId, 
            List<GuestSourceOfBusiness> expGsobs, List<int> expGsobNrOfGuests, List<Weather> expWeathers,
            JToken report
        )
        {
            // Act *****************************************
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
            int? actLocalEventId = (int?)report["localEventId"];
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
            Assert.Equal(expGuestsFromHotel, actGuestsFromHotel);
            Assert.Equal(expGuestsFromOutsideHotel, actGuestsFromOutsideHotel);
            Assert.Equal(expIsPublicHoliday.ToString(), actIsPublicHoliday.ToString());
            Assert.Equal(expEventNotes, actEventNotes);
            Assert.Equal(expGSourceOfBusinessNotes, actGSourceOfBusinessNotes);
            Assert.Equal(expOutletId, actOutletId);
            Assert.Equal(expUserId, actUserId);
            Assert.Equal(expLocalEventId, actLocalEventId);

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