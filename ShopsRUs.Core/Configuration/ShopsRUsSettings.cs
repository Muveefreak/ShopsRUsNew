using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Core.Configuration
{
    public class ShopsRUsSettings
    {
        public static string DbConnectionString { get; set; }
    }

    public class ApiResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public object Data { get; set; }
    }

    public static class AppSettings
    {
        public static int LoyaltyYears { get; set; }
        public static string[] ExemptedItems { get; set; }
    }
}
