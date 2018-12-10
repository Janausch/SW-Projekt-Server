using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SW_Projekt_Server
{
    class NetworkConnection
    {
        private List<string> ActiveAdresses = new List<string>();

        public string ActiveIPs
        {
            get
            {
                string returnvalue = null;
                foreach (string ip in ActiveAdresses)
                    returnvalue += ip+";";
                return returnvalue;
            }
            set { ActiveAdresses.Add(value); }
        }

        public List<string> ServerPingStart()
        {
            List<string> returnList = new List<string>();
            string[] mainIP = getMyIP().Split(new char[] { '.' }, Convert.ToInt32(4));
            string MainIP = mainIP[0] + "." + mainIP[3] + "." + mainIP[2] + ".";

            //System.Windows.Forms.MessageBox.Show(getMyIP() +" " + MainIP);
            for (int i = 0; i <= 255; i++)
            {
                string PingIp = MainIP + i.ToString();
                Ping p = new Ping();
                p.PingCompleted += new PingCompletedEventHandler(PingCompleted);
                p.SendAsync(PingIp, 500, PingIp);
                //PingReply rep = p.Send(IPAddress.Parse(PingIp),100);
                //if (rep.Status == IPStatus.Success)
                //    returnList.Add(PingIp);
            }
            return returnList;
        }
        private string getMyIP()
        {
            IPHostEntry hostInfo = Dns.GetHostByName(Dns.GetHostName());
            return hostInfo.AddressList[0].ToString();
        }
        static void PingCompleted(object sender, PingCompletedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(e.UserState.ToString());
            if(e.Reply.Status == IPStatus.Success)
            {
                //ActiveIPs = e.UserState.ToString();
            }
        }
    }
}
