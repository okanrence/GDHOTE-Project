using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.BusinessCore.BusinessLogic
{
    public class StringCaseManager
    {
        public static string TitleCase(string input)
        {
            string result = "";
            //TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            if (!string.IsNullOrEmpty(input)) result = ti.ToTitleCase(input.ToLower());
            return result;
        }
    }
}
