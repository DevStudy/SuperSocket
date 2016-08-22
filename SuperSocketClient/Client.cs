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

        public delegate void ResetTxetBox(string text);

        private string _serverAddress = String.Empty;
        private int _port = 0;
        private static byte[] _bufferBytes = new byte[1024];  

        public Client()
        {
            InitializeComponent();
            //Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
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
                string sendMessage = "echo:" + txt_Conent.Text.TrimEnd() + "#";
                clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));

                //Receive Message
                int receiveLength = clientSocket.Receive(_bufferBytes);
                receivedMsg = Encoding.ASCII.GetString(_bufferBytes, 0, receiveLength);
                receivedMsg.Replace("\r\n", "\r");

                ResetRecText(receivedMsg);

                sp.Stop();
            }
            catch (Exception ex)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            finally
            {
                if (this.chk_CloseConnection.Checked)
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();    
                }
            }
            
            //Console.WriteLine(string.Format("Received:{0}",receivedMsg));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(string.Format("Thread:{0},Send and receice spend time:{1}", threadRunProgress.CurrentIndex, sp.ElapsedMilliseconds));
        }

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

        
    }

    public class ThreadRunProgress
    {
        public int CurrentIndex { get; set; }
        public int TotalCount { get; set; }
    }
}
