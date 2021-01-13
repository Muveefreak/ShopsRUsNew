using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Infrastructure.Configuration
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime createdDate)
        {
            var today = DateTime.Today;
            var age = today.Year - createdDate.Year;
            if (createdDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
