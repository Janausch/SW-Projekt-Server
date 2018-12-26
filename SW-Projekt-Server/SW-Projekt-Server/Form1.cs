using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace SW_Projekt_Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Start();
        }
        NetworkConnection nc = new NetworkConnection();
        Stopwatch sw = new Stopwatch();

        private async void button1_Click(object sender, EventArgs e)
        {
            sw.Start();
            await GetIPs();
        }
        List<string> ActiveIPs = new List<string>();

        private async Task GetIPs()
        {
            try
            {

                string[] mainIP = nc.getMyIP().Split(new char[] { '.' }, Convert.ToInt32(4));
                string MainIP = mainIP[0] + "." + mainIP[1] + "." + mainIP[2] + ".";
                List<IPAddress> theListOfIPs = new List<IPAddress>();

                for (int i = 1; i <= 254; i++)
                {
                    string PingIp = MainIP + i.ToString();
                    theListOfIPs.Add(IPAddress.Parse(PingIp));
                }
                var tasks = theListOfIPs.Select(ip => PingAndProcessAsync(ip)).ToList();
                var results = await Task.WhenAll(tasks);
                ActiveIPs.Clear();
                ActiveIPs = results.ToList();
                ActiveIPs.RemoveAll(DeleteRules);
                UpdateDropDownMenu();
                sw.Stop();//Debugstuff start
                Console.WriteLine(sw.ElapsedMilliseconds.ToString());
                sw.Reset();//Debugstuff ende

            }
            catch (Exception)
            {
                MessageBox.Show("Ping Fehler");
            }
        }
        private static bool DeleteRules(string Element)
        {
            return (Element == string.Empty);
        }
        private async Task<string> PingAndProcessAsync(IPAddress ip)
        {
            Ping PingSender = new Ping();
            var result = await PingSender.SendPingAsync(ip,1000);
            if (result.Status.ToString().Equals("Success"))
                return result.Address.ToString();
            else
                return string.Empty;
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
        private void UpdateDropDownMenu()
        {
            ListIPs.BeginUpdate();
            ListIPs.Items.Clear();
            foreach (string s in ActiveIPs)
            {
                ListIPs.Items.Add(s);
            }
            ListIPs.EndUpdate();
        }

        private void Update_Button_Click(object sender, EventArgs e)
        {
            UpdateDropDownMenu();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }
    }
}
