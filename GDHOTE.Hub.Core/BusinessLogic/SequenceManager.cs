using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Core.BusinessLogic
{
    public class SequenceManager
    {
        public static string ReturnNextSequence(string sex)
        {
            string nextSequence = "";
            int nextSequnceTemp = 0;
            if (sex == "M") nextSequnceTemp = SequenceService.GetNextMaleSequence();
            if (sex == "F") nextSequnceTemp = SequenceService.GetNextFemaleSequence();
            if (nextSequnceTemp == 0) return "";
            nextSequence = nextSequnceTemp.ToString().PadLeft(8, '0');

            return nextSequence;
        }
    }
}
