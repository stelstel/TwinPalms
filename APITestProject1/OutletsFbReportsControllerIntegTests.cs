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


            // Check 1
            var report1 = responseString[0]; // Getting 1 report from report array

            GuestSourceOfBusiness gsob1 = new GuestSourceOfBusiness { Id = 3, SourceOfBusiness = "Facebook referral" };
            GuestSourceOfBusiness gsob2 = new GuestSourceOfBusiness { Id = 4, SourceOfBusiness = "Google search" };
            expGsobs.Add(gsob1);
            expGsobs.Add(gsob2);

            expGsobNrOfGuests.Add(22);
            expGsobNrOfGuests.Add(13);

            Weather weather1 = new Weather { Id = 6, TypeOfWeather = "Stormy" };
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
                expGSourceOfBusinessNotes = "A lot of drunk people just dropped in at around 1:00 AM",
                expOutletId = 1,
                expUserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                expLocalEventId = 2,
                expGsobs,
                expGsobNrOfGuests,
                expWeathers,
                report1
            );
        }

        // *************************************** CheckFbReport *************************************************************
        private static void CheckFbReport(
            int expectedTables, int expFood, int expBeverage, int expOtherIncome, 
            int expGuestsFromHotel, int expGuestsFromOutsideHotel, bool expIsPublicHoliday, string expEventNotes,
            string expGSourceOfBusinessNotes, int expOutletId,  string expUserId, int expLocalEventId, List<GuestSourceOfBusiness> expGsobs,
            List<int> expGsobNrOfGuests, List<Weather> expWeathers,
            JToken report
        )
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