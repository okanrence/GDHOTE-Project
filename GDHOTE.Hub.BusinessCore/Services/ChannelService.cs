using GDHOTE.Hub.CoreObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class ChannelService
    {
        public static List<Channel> GetChannels()
        {
            var channels = new List<Channel>
            {
                new Channel {Id = 1, Name= "Web" },
                new Channel {Id = 2, Name ="Mobile"},
                new Channel {Id = 3, Name ="Kiosk"}
            };
            return channels;
        }
    }
}
