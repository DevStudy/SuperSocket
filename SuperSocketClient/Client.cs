using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperSocketClient
{
    public partial class Client : Form
    {
        private string _serverAddress = String.Empty;
        private int _port = 0;
        private static byte[] _bufferBytes = new byte[1024];  

        public Client()
        {
            InitializeComponent();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            int threadCount = (int)num_ThreadCount.Value;
            
            //for (int i = 0; i < threadCount; i++)
            //{
            //    ThreadRunProgress threadRunProgress = new ThreadRunProgress { CurrentIndex = i,TotalCount = threadCount};

            //    Thread thread = new Thread(new ParameterizedThreadStart(DoSend)) {IsBackground = true};
            //    thread.Start(threadRunProgress);
            //}
            var factory = new TaskFactory();
            var tasks = new List<Task>();
            for (int i = 0; i < threadCount; i++)
            {
                int index = i;
                Action<object> action = process =>
                {
                    ThreadRunProgress threadRunProgress = new ThreadRunProgress { CurrentIndex = index, TotalCount = threadCount };
                    DoSend(threadRunProgress);
                };
                tasks.Add(factory.StartNew(action, i));
            }
            Stopwatch totalStopwatch = new Stopwatch();
            totalStopwatch.Start();
            Task.WaitAll(tasks.ToArray());
            totalStopwatch.Stop();

            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Format("All task finished! Spend:{0}",totalStopwatch.ElapsedMilliseconds));
        }

        

        private void DoSend(ThreadRunProgress threadRunProgress)
        {
            //Create Connection:
            _serverAddress = txt_Server.Text;
            _port = (int)num_Port.Value;

            IPAddress ip = IPAddress.Parse(_serverAddress);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                clientSocket.Connect(new IPEndPoint(ip, _port));  
                //Console.WriteLine("Server Connected");
            }
            catch
            {
                Console.WriteLine("Server Connect Failed");
                return;
            }
            Stopwatch sp = new Stopwatch();
            string receivedMsg;
            //Sned message and receiced
            try
            {
                sp.Start();
                string sendMessage = "echo:" + txt_Conent.Text.TrimEnd() + "\n" + DateTime.Now + "\r\n";
                clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));

                //Receive Message
                int receiveLength = clientSocket.Receive(_bufferBytes);
                receivedMsg = Encoding.ASCII.GetString(_bufferBytes, 0, receiveLength);
                receivedMsg.Replace("\r\n", "\r");
                sp.Stop();
            }
            catch (Exception ex)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            finally
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            
            //Console.WriteLine(string.Format("Received:{0}",receivedMsg));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(string.Format("Thread:{0},Send and receice spend time:{1}", threadRunProgress.CurrentIndex, sp.ElapsedMilliseconds));

            
        }
    }

    public class ThreadRunProgress
    {
        public int CurrentIndex { get; set; }
        public int TotalCount { get; set; }
    }
}
