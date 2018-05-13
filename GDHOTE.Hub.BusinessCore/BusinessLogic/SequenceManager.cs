using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.BusinessCore.Services;

namespace GDHOTE.Hub.BusinessCore.BusinessLogic
{
    public class SequenceManager
    {
        private static readonly int PadLength = 6;
        public static string ReturnNextSequence(string gender)
        {
            string nextSequence = "";
            int nextSequnceTemp = 0;
            if (gender == "M") nextSequnceTemp = SequenceService.GetNextMaleSequence();
            if (gender == "F") nextSequnceTemp = SequenceService.GetNextFemaleSequence();
            if (nextSequnceTemp == 0) return "";
            nextSequence = nextSequnceTemp.ToString().PadLeft(PadLength, '0');

            return nextSequence;
        }
        public static string PadGenderSequence(string gender, string sequence)
        {
            string genderSequence = "";
            genderSequence = gender + sequence.PadLeft(PadLength, '0');

            return genderSequence;
        }
    }
}
