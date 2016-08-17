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
    public class Add : CommandBase<TelnetSession, StringRequestInfo>
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public override void ExecuteCommand(TelnetSession session, StringRequestInfo requestInfo)
        {
            _logger.Info("Add Command was invoked:{0},{1}",requestInfo.Parameters[0],requestInfo.Parameters[1]);

            session.Send(requestInfo.Parameters.Select(p => Convert.ToInt32(p)).Sum().ToString());
        }
    }
}
