using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketServer.AppSessions
{
    public class PlayerSession : SuperSocket.SocketBase.AppSession<PlayerSession>
    {
        public int GameHallId { get; internal set; }

        public int RoomId { get; internal set; }
    }
}
