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
using System.Timers;
using System.Windows.Forms;
using ThreadingTimer = System.Threading.Timer;
using WindowsTimer = System.Windows.Forms.Timer;

namespace SuperSocketClient
{
    public partial class Client : Form
    {

        public delegate void ResetTxetBox(string text);

        private string _serverAddress = String.Empty;
        private int _port = 0;
        private static byte[] _bufferBytes = new byte[1024];
        private List<AccessRunesult> _resultList = new List<AccessRunesult>();
        private List<AccessRunesult> _errResultList = new List<AccessRunesult>();
        Random _rd = new Random(0);
        private ThreadingTimer _timer;
        
        public Client()
        {
            InitializeComponent();
            //Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            _resultList.Clear();
            _errResultList.Clear();

            int threadCount = (int)num_ThreadCount.Value;
            
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
            //Stopwatch totalStopwatch = new Stopwatch();
            //totalStopwatch.Start();
            //Task.WaitAll(tasks.ToArray());
            //totalStopwatch.Stop();

            //Console.ResetColor();
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine(string.Format("All task finished! Spend:{0}",totalStopwatch.ElapsedMilliseconds));

            //Console.WriteLine("TOP 10 slowest connects are :");

            //int rank = 1;

            //if (_resultList.Count > 0 && _resultList.All(x=>x!=null))
            //{
            //    foreach (var result in _resultList.OrderByDescending(x => x.TotalSpend).Take(10))
            //    {

            //        Console.WriteLine("{0}:{1},spend:{2}", rank++, result.CurrentIndex, result.TotalSpend);
            //    }
            //}
            

            //Console.WriteLine("Error connects count:{0}", _errResultList.Count);

            //rank = 1;
            //foreach (var result in _errResultList)
            //{
            //    Console.WriteLine("Error: at :{0},{1},{2}", rank++, result.CurrentIndex, result.TotalSpend);
            //}
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
                threadRunProgress.ClientSocket = clientSocket;

                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = (int)num_ThreadInterval.Value * 1000;
                timer.Elapsed += new ElapsedEventHandler((s, e) => timer_Elapsed(s, e, threadRunProgress));
                timer.Enabled = true;



            }
            catch
            {
                Console.WriteLine("Server Connect Failed");
                _errResultList.Add(new AccessRunesult { CurrentIndex = threadRunProgress.CurrentIndex});
            }
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e,ThreadRunProgress threadRunProgress)
        {
            SendtoServer(threadRunProgress);
        }

        private string SendtoServer(ThreadRunProgress threadRunProgress)
        {
            string receivedMsg = string.Empty;
            Stopwatch sp = new Stopwatch();
            //Sned message and receiced
            threadRunProgress.Message = GetSendMessage();
            try
            {
                sp.Start();
                //string sendMessage = "echo:" + txt_Conent.Text.TrimEnd() + "#";
                string sendMessage = threadRunProgress.Message;
                threadRunProgress.ClientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                //threadRunProgress.ClientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));

                //Receive Message
                int receiveLength = threadRunProgress.ClientSocket.Receive(_bufferBytes);
                receivedMsg = Encoding.ASCII.GetString(_bufferBytes, 0, receiveLength);
                receivedMsg.Replace("\r\n", "\r");

                //ResetRecText(receivedMsg + " was come from:" + threadRunProgress.CurrentIndex);

                sp.Stop();
                _resultList.Add(new AccessRunesult
                {
                    CurrentIndex = threadRunProgress.CurrentIndex,
                    TotalSpend = sp.ElapsedMilliseconds
                });
            }
            catch (Exception ex)
            {
                threadRunProgress.ClientSocket.Shutdown(SocketShutdown.Both);
                threadRunProgress.ClientSocket.Close();
                _errResultList.Add(new AccessRunesult {CurrentIndex = threadRunProgress.CurrentIndex});
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            finally
            {
                if (this.chk_CloseConnection.Checked)
                {
                    threadRunProgress.ClientSocket.Shutdown(SocketShutdown.Both);
                    threadRunProgress.ClientSocket.Close();
                }

                //Console.WriteLine(string.Format("Received:{0}",receivedMsg));
                Console.ForegroundColor = ConsoleColor.Yellow;
                //Console.WriteLine(string.Format("Thread:{0},Send and receice spend time:{1}", threadRunProgress.CurrentIndex, sp.ElapsedMilliseconds));
                Console.WriteLine("Message was come form server:{0}", receivedMsg);
            }

            return receivedMsg;
        }

       

        #region Private Method
        private void ResetRecText(string newText)
        {
            if (txt_Receive.InvokeRequired)
            {
                ResetTxetBox updater = new ResetTxetBox(ClearAndResetText);
                this.BeginInvoke(updater, newText);
            }
            else
            {
                this.txt_Receive.Clear();
                this.txt_Receive.Text = newText;
            }
        }

        private void ClearAndResetText(string newText)
        {
            this.txt_Receive.Clear();
            this.txt_Receive.Text = newText;
        }

        private void txt_Conent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.A))
            {
                txt_Conent.SelectAll();
            }
        }

        private string GetSendMessage()
        {
            var id = _rd.Next();

            return string.Format("I am : {0}", id);
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            _serverAddress = txt_Server.Text;
            _port = (int)num_Port.Value;
            IPAddress ip = IPAddress.Parse(_serverAddress);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(ip, _port));
 
            clientSocket.Send((Encoding.ASCII.GetBytes("11111")));
            
            Thread.Sleep(1000);
            clientSocket.Send((Encoding.ASCII.GetBytes("22222")));
           
            Thread.Sleep(1000);
            clientSocket.Send((Encoding.ASCII.GetBytes("33333")));
           
        }
       
    }

    public class ThreadRunProgress
    {
        public int CurrentIndex { get; set; }
        public int TotalCount { get; set; }
        public string Message { get; set; }
        public Socket ClientSocket { get; set; }
    }
    public class AccessRunesult
    {
        public int CurrentIndex { get; set; }
        public long TotalSpend { get; set; }
    }
}
