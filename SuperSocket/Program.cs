using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketEngine;
using SuperSocketServer.AppSessions;

namespace SuperSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //StartServerByCode();

            StartServerByConfig();
        }

        private static void StartServerByConfig()
        {
            Console.WriteLine("Press any key to start the server!");

            Console.ReadKey();
            Console.WriteLine();

            var bootstrap = BootstrapFactory.CreateBootstrap();

            if (!bootstrap.Initialize())
            {
                Console.WriteLine("Failed to initialize!");
                Console.ReadKey();
                return;
            }

            var result = bootstrap.Start();

            foreach (IWorkItem appServer in bootstrap.AppServers)
            {
                if (appServer is AppServer<TelnetSession>)
                {
                    var appS = appServer as AppServer<TelnetSession>;
                    appS.SessionClosed += AppSOnSessionClosed;
                }
            }

            Console.WriteLine("Start result: {0}!", result);

            if (result == StartResult.Failed)
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            Console.WriteLine();

            //Stop the appServer
            bootstrap.Stop();

            Console.WriteLine("The server was stopped!");
            Console.ReadKey();
        }

        static void appS_NewRequestReceived(TelnetSession session, StringRequestInfo requestInfo)
        {
             Console.WriteLine(requestInfo.Body);
        }

        /// <summary>
        /// This event will be occured only at client exit the Application
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        private static void AppSOnSessionClosed(TelnetSession session, CloseReason value)
        {
            string sessionId = session.SessionID;
        }

        private static void StartServerByCode()
        {
            Console.WriteLine("Press any key to start the server!");

            Console.ReadKey();
            Console.WriteLine();

            //Setup the appServer
            var appServer = new AppServer<PlayerSession>(); //Setup with listening port
            //var appServer = new AppServer<>(); Default Session is AppSession
            if (!appServer.Setup(2012))
            {
                Console.WriteLine("Failed to setup!");
                Console.ReadKey();
                return;
            }
            //Must be here to register the related event!-WL
            appServer.NewSessionConnected += new SessionHandler<PlayerSession>(appServer_NewSessionConnected);
            //If use Command then we don't need NewRequestReceived-WL
            //appServer.NewRequestReceived += new RequestHandler<AppSession, StringRequestInfo>(appServer_NewRequestReceived);

            Console.WriteLine();

            //Try to start the appServer
            if (!appServer.Start())
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("The server started successfully, press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            //Stop the appServer
            appServer.Stop();

            Console.WriteLine("The server was stopped!");
            Console.ReadKey();
        }

        private static void appServer_NewSessionConnected(PlayerSession session)
        {
            session.Send("Welcome to SuperSocket Telnet Server");
        }

        static void appServer_NewRequestReceived(AppSession session, StringRequestInfo requestInfo)
        {
            switch (requestInfo.Key.ToUpper())
            {
                case ("ECHO"):
                    session.Send(requestInfo.Body);
                    break;

                case ("ADD"):
                    session.Send(requestInfo.Parameters.Select(p => Convert.ToInt32(p)).Sum().ToString());
                    break;

                case ("MULT"):

                    var result = 1;

                    foreach (var factor in requestInfo.Parameters.Select(p => Convert.ToInt32(p)))
                    {
                        result *= factor;
                    }

                    session.Send(result.ToString());
                    break;
            }
        }
    }
}
