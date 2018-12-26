using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;

namespace SW_Projekt_Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NetworkConnection nc = new NetworkConnection();
        string allIps = null;

        private void button1_Click(object sender, EventArgs e)
        {
            GetIPs();
            //string[] spittedString = nc.ActiveIPs.Split(new char[] { ';' });
            //MessageBox.Show(spittedString.Length.ToString());
        }
        List<IPAddress> theListOfIPs = new List<IPAddress>();
        string ActiveIPs = "";
        private async void GetIPs()
        {
            try
            {

                string[] mainIP = nc.getMyIP().Split(new char[] { '.' }, Convert.ToInt32(4));
                string MainIP = mainIP[0] + "." + mainIP[3] + "." + mainIP[2] + ".";

                for (int i = 1; i <= 15; i++)
                {
                    string PingIp = MainIP + i.ToString();
                    theListOfIPs.Add(IPAddress.Parse(PingIp));
                }
                //Task<List<PingReply>> PR = PingAsync();
                //List<PingReply> PR2 = await PR;
                //ActiveIPs = null; //Relevants?
                Ping pingSender = new Ping();
                var tasks = theListOfIPs.Select(ip => pingSender.SendPingAsync(ip)).ToList();
                var results = await Task.WhenAll(tasks);
                System.Windows.Forms.MessageBox.Show("Pings Done");

                foreach (PingReply pr in results)
                {
                    ActiveIPs += ";" + pr.Address;
                }
                //return ActiveIPs;


                //Task<string> GetIPs = nc.ServerPingStart(nc);
                //allIps = await nc.ServerPingStart(nc); ;
                string[] spittedString = ActiveIPs.Split(new char[] { ';' });
                ListIPs.BeginUpdate();
                foreach (string s in spittedString)
                {
                    ListIPs.Items.Add(s);
                }
                ListIPs.EndUpdate();
            }
            catch (Exception)
            {
                MessageBox.Show("Fehler");
                //throw;
            }
        }

        private void ListIPs_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Index = ListIPs.SelectedIndex.GetType().Name;
            //Aufruf einer Methode zum Auslesen der Daten (als Liste?)
            List<string> ListofData = new List<string>();
            DataBox.Clear();
            foreach (string s in ListofData)
            {
                //eventuell Farben einfügen (z.B. "<Error>Tracking lost" in Rot anzeigen "Tracking lost")
                DataBox.Text += s + "\n";
            }
        }

        private void Update_Button_Click(object sender, EventArgs e)
        {
            ListIPs.BeginUpdate();
            string[] spittedString = nc.ActiveIPs.Split(new char[] { ';' });
            foreach (string s in spittedString)
            {
                ListIPs.Items.Add(s);
            }
            ListIPs.EndUpdate();
        }
    }
}
