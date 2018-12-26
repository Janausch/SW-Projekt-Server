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
        //public static NetworkConnection nc;
        private List<IPAddress> theListOfIPs = new List<IPAddress>();

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

        public async Task<string> ServerPingStart(NetworkConnection ncn)
        {
           // nc = ncn;
            string[] mainIP = getMyIP().Split(new char[] { '.' }, Convert.ToInt32(4));
            string MainIP = mainIP[0] + "." + mainIP[3] + "." + mainIP[2] + ".";

            for (int i = 1; i <= 155; i++)
            {
                string PingIp = MainIP + i.ToString();
                theListOfIPs.Add(IPAddress.Parse(PingIp));
            }
            //Task<List<PingReply>> PR = PingAsync();
            //List<PingReply> PR2 = await PR;
            //ActiveIPs = null; //Relevants?
            Ping pingSender = new Ping();
            var tasks = theListOfIPs.Select(ip => pingSender.SendPingAsync(ip, 5000)).ToList();
            var results = await Task.WhenAll(tasks);
            System.Windows.Forms.MessageBox.Show("Pings Done");

            foreach (PingReply pr in results)
            {
                ActiveIPs += ";" + pr.Address; 
            }
            return ActiveIPs;
        }
        public string getMyIP()
        {
            IPHostEntry hostInfo = Dns.GetHostByName(Dns.GetHostName());
            return hostInfo.AddressList[0].ToString();
        }
        private async Task<List<PingReply>> PingAsync()
        {
            Ping pingSender = new Ping();
            var tasks = theListOfIPs.Select(ip => pingSender.SendPingAsync(ip, 500));
            var results = await Task.WhenAll(tasks);
            System.Windows.Forms.MessageBox.Show("Pings Done");
            return results.ToList();
        }
    }
}
