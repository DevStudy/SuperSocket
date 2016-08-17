using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using SuperSocketServer.AppSessions;

namespace SuperSocketServer.AppServers
{
    public class TelnetServer : AppServer<TelnetSession>
    {
        //It will receive the command like this:"LOGIN:kerry,12345" + NewLine
        public TelnetServer()
            : base(new CommandLineReceiveFilterFactory(Encoding.Default, new BasicRequestInfoParser(":", ",")))
        {
        }

        protected override void OnNewSessionConnected(TelnetSession session)
        {
            base.OnNewSessionConnected(session);
        }

        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            return base.Setup(rootConfig, config);
        }

        protected override void OnStarted()
        {
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            base.OnStopped();
        }
    }
}
