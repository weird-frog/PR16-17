using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortScanner
{
    public partial class Form1 : Form
    {
        public static ManualResetEvent connectDone = new ManualResetEvent(false);
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int beginPort = Convert.ToInt32(nBeginPort.Value),
                endPort = Convert.ToInt32(nEndPort.Value);
            int i;

            progressBar1.Maximum = endPort - beginPort + 1;
            progressBar1.Value = 0;

            listView1.Items.Clear();

            IPAddress addr = IPAddress.Parse(tIPAddress.Text);

            for (i = beginPort; i <= endPort; i++)
            {
                IPEndPoint ep = new IPEndPoint(addr, i);
                Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IAsyncResult asyncResult = soc.BeginConnect(ep, new AsyncCallback(ConnectCallback), soc);

                if (!asyncResult.AsyncWaitHandle.WaitOne(30, false))
                {
                    soc.Close();
                    listView1.Items.Add("Порт " + i.ToString());
                    listView1.Items[i - beginPort].SubItems.Add("");
                    listView1.Items[i - beginPort].SubItems.Add("Закрыт");
                    listView1.Refresh();
                    progressBar1.Value += 1;
                }
                else
                {
                    soc.Close();
                    listView1.Items.Add("Порт " + i.ToString());
                    listView1.Items[i - beginPort].SubItems.Add("Открыт");
                    progressBar1.Value += 1;
                }

            }
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket) ar.AsyncState;
                client.EndConnect(ar);
                connectDone.Set();
            }
            catch (Exception)
            {
            }
        }
    }
}
