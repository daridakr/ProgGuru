using System;
using System.Collections.Generic;
using System.Text;

namespace Daridakr.ProgGuru.Helpers
{
    public class ProgGuruService
    {
        public static int CalculateAge(DateTime bornDate, DateTime nowDate)
        {
            int age = nowDate.Year - bornDate.Year;
            if (nowDate.Month < bornDate.Month ||
            (nowDate.Month == bornDate.Month
            && nowDate.Day < bornDate.Day)) age--;
            return age;
        }
    }
}
