using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using SuperSocketServer.AppSessions;

namespace SuperSocketServer.Commands
{
    public class Echo : CommandBase<TelnetSession, StringRequestInfo>
    {
        public override void ExecuteCommand(TelnetSession session, StringRequestInfo requestInfo)
        {
            Console.WriteLine(string.Format("Receive:{0}",requestInfo.Body));
            session.Send(requestInfo.Body);
        }
    }
}
