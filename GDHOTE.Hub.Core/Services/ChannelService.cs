using GDHOTE.Hub.CoreObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.Core.Services
{
    public class ChannelService
    {
        public static List<Channel> GetChannels()
        {
            var channels = new List<Channel>
            {
                new Channel {Id = 1, ChannelName="Web" },
                new Channel {Id = 2, ChannelName ="Mobile"},
                new Channel {Id = 3, ChannelName ="Kiosk"}
            };
            return channels;
        }
    }
}
