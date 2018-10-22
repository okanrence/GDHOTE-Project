using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class StringCaseService
    {
        public static string TitleCase(string input)
        {
            string result = "";
            //TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            if (!string.IsNullOrEmpty(input)) result = ti.ToTitleCase(input.ToLower());
            return result;
        }
        public static bool IsValidEmail(string email)
        {
            // Return true if strIn is in valid e-mail format.
            if (string.IsNullOrEmpty(email)) return false;
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
