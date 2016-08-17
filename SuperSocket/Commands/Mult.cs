using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using SuperSocketServer.AppSessions;

namespace SuperSocketServer.Commands
{
    public class Mult : CommandBase<TelnetSession, StringRequestInfo>
    {
        public override void ExecuteCommand(TelnetSession session, StringRequestInfo requestInfo)
        {
            var result = requestInfo.Parameters.Select(p => Convert.ToInt32(p)).Aggregate(1, (current, factor) => current * factor);

            session.Send(result.ToString());
        }
    }
}
