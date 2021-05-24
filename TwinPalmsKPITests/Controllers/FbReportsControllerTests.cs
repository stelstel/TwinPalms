using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwinPalmsKPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace TwinPalmsKPI.Controllers.Tests
{
    [TestClass()]
    public class FbReportsControllerTests
    {
        /*
        [TestMethod()]
        public void FbReportsControllerTest()
        {
            Assert.Fail();
        }
        */

        /*
        [TestMethod()]
        public void GetFbReportsTest()
        {
            Assert.Fail();
        }
        */

        /*
        [TestMethod()]
        public void GetFbReportTest()
        {
            Assert.Fail();
        }
        */

        [TestMethod()]
        public void CreateFbReportTest()
        {
            // Arrange *****************
            int beverage = 10;
            DateTime date = new DateTime(2021, 05, 05);
            int food = 100;
            bool isPublicHoliday = true;
            int otherIncome = 201;
            int outletId = 3;
            int tables = 22;
            string userId = "35947f01-393b-442c-b815-d6d9f7d4b81e";
            int[] weatherIds = new int[] { 5, 4 };
            int fbReportid = 1;


            // Act *********************
            FbReport fbRep = new FbReport();
            fbRep.Beverage = beverage;
            fbRep.Date = date;
            fbRep.Food = food;
            // fbRep.Id = 1;
            fbRep.IsPublicHoliday = isPublicHoliday;
            fbRep.OtherIncome = otherIncome;
            fbRep.OutletId = outletId;
            fbRep.Tables = tables;
            fbRep.UserId = userId;

            ICollection<WeatherFbReport> wfbReports = new List<WeatherFbReport>();

            wfbReports.Add(
                new WeatherFbReport
                {
                    WeatherId = weatherIds[0],
                    FbReportId = fbReportid
                }
            );

            wfbReports.Add(
                new WeatherFbReport
                {
                    WeatherId = weatherIds[1],
                    FbReportId = fbReportid
                }
            );


            // Assert **********************************
            Assert.AreEqual(beverage, fbRep.Beverage);



            //Assert.Fail();
        }

        /*
        [TestMethod()]
        public void DeleteFbReportTest()
        {
            Assert.Fail();
        }
        */

        /*
        [TestMethod()]
        public void UpdateFbReportTest()
        {
            Assert.Fail();
        }
        */
    }
}