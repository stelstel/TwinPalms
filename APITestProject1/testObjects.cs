using Entities.Models;
using System.Collections.Generic;

namespace APITestProject1
{
    public static class testObjects
    // Hard coded objects for integration tests
    {
        public static int TestObjNrOfReports = 7;

        public static List<GuestSourceOfBusiness> TestObjGsobs = new List<GuestSourceOfBusiness>
        {
            new GuestSourceOfBusiness() { Id = 1, SourceOfBusiness = "Hotel Website" },
            new GuestSourceOfBusiness() { Id = 2, SourceOfBusiness = "Hungry Hub" },
            new GuestSourceOfBusiness() { Id = 3, SourceOfBusiness = "Facebook referral" },
            new GuestSourceOfBusiness() { Id = 4, SourceOfBusiness = "Google search" },
            new GuestSourceOfBusiness() { Id = 5, SourceOfBusiness = "Instagram referral" },
            new GuestSourceOfBusiness() { Id = 6, SourceOfBusiness = "Hotel referral" },
            new GuestSourceOfBusiness() { Id = 7, SourceOfBusiness = "Other Hotel referral" },
            new GuestSourceOfBusiness() { Id = 8, SourceOfBusiness = "Agent referral" },
            new GuestSourceOfBusiness() { Id = 9, SourceOfBusiness = "Walk in" },
            new GuestSourceOfBusiness() { Id = 10, SourceOfBusiness = "Other" }
        };

        public static List<Weather> TestObjWeathers = new List<Weather>
        {
            new Weather() { Id = 1, TypeOfWeather = "Sunny/Clear" },
            new Weather() { Id = 2, TypeOfWeather = "Partially Cloudy" },
            new Weather() { Id = 3, TypeOfWeather = "Overcast" },
            new Weather() { Id = 4, TypeOfWeather = "Rain" },
            new Weather() { Id = 5, TypeOfWeather = "Showers" },
            new Weather() { Id = 6, TypeOfWeather = "Stormy" }
        };

        public static List<string> TestObjNotes = new List<string>
        {
            "Lorem ipsum dolor sit amet",
            "Consectetur adipiscing elit",
            "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
            "Ut enim ad minim veniam",
            "Quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
            "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur",
            "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum"
        };

        public static List<string> TestObjEventNotes = new List<string>
        {
            "The DJ got everybody dancing",                                         // 0
            "The DJ was really good",                                               // 1
            "The Flamenco dance lesson was quite nice, had many people dancing",    // 2
            "The DJ was a star",                                                    // 3
            "Umpa Umpa DJ",                                                         // 4
            "The samba night was a success especially with the Italians",           // 5
            "Busy night"                                                            // 6
        };

        public static List<string> TestObjGSOBNotes = new List<string>
        {
            "A lot of people just dropped in at around 1:00 AM",    //0
            "A lot of people came from Google Search",              //1
            "Instagram",                                            //2
            null,                                                   //3
            "Hectic day. A lot of Germans. Since they didn't speak english " +
                "we were unable to find out how they got to know about the Umpa Umpa Madness Night", // 4
            "Most of the guest had been handed leaflets down town", // 5
            "A lot of the guests came from Agent referral"          // 6
        };

        public static List<string> TestObjUserIds = new List<string>
            {
                "35947f01-393b-442c-b815-d6d9f7d4b81e",
                "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
            };
}
}
