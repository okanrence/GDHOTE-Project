﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.BusinessCore.Services;

namespace GDHOTE.Hub.BusinessCore.BusinessLogic
{
    public class SequenceManager
    {
        public static string ReturnNextSequence(string gender)
        {
            string nextSequence = "";
            int nextSequnceTemp = 0;
            if (gender == "M") nextSequnceTemp = SequenceService.GetNextMaleSequence();
            if (gender == "F") nextSequnceTemp = SequenceService.GetNextFemaleSequence();
            if (nextSequnceTemp == 0) return "";
            nextSequence = nextSequnceTemp.ToString().PadLeft(8, '0');

            return nextSequence;
        }
    }
}