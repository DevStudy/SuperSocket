using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace SuperSocketServer.AppSessions
{
    public class TelnetSession : AppSession<TelnetSession>
    {
        protected override void OnSessionStarted()
        {
            //this.Send("Welcome to SuperSocket Telnet Server");
        }
        protected override void HandleUnknownRequest(StringRequestInfo requestInfo)
        {
            this.Send("TCC Unknow request" + requestInfo.Body);
        }

        protected override void HandleException(Exception e)
        {
            this.Send("Application error: {0}", e.Message);
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            string closeReason = reason.ToString();
            //add you logics which will be executed after the session is closed
            base.OnSessionClosed(reason);
        }
    }
}
