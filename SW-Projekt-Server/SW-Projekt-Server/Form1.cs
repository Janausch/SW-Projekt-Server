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
        Stopwatch sw = new Stopwatch();
        List<string> AllIPs = new List<string>();
        bool[,] Speicher = new bool[256, 300];
        private void Speicherfüllen(List<string> IPs)
        {
            WeiterSchieben();
            foreach (string s in IPs)
            {
                string[] Splitted = s.Split(new char[] { '.' });
                string a = Splitted[3];
                if (!AllIPs.Contains(s))
                    AllIPs.Add(s);
                AllIPs.Sort();
                Speicher[Convert.ToByte(a), 0] = true;
            }
        }

        private void WeiterSchieben()
        {
                for (int i = 0; i < 256; i++)
                {
                    for (int o = 299; o > 0; o--)
                    {
                        Speicher[i, o] = Speicher[i, o - 1];
                    }
                }
                for (int i = 0; i < 256; i++)
                    Speicher[i, 0] = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            sw.Start();
            await GetIPs();
        }
        List<string> ActiveIPs = new List<string>();

        public string getMyIP()
        {
            IPHostEntry hostInfo = Dns.GetHostByName(Dns.GetHostName());
            return hostInfo.AddressList[0].ToString();
        }

        private async Task GetIPs()
        {
            try
            {

                string[] mainIP = getMyIP().Split(new char[] { '.' }, Convert.ToInt32(4));
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
                Speicherfüllen(ActiveIPs);
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

        private Color CheckOnline(byte IPTeil)
        {
            bool Green = true;
            for (int I = 0; I < 300; I++)
                if (!Speicher[IPTeil, I])
                {
                    Green = false;
                    break;
                }
            if (Green)
                return Color.Green;
            for (int I = 0; I <= 300; I++)
                if (Speicher[IPTeil, I])
                    return Color.Orange;
            return Color.Red;
        }
        private int Zwischenspeicher = 0;
        private void ListIPs_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ListipsItem = null;
            try
            {
                ListipsItem = ListIPs.SelectedItem.ToString();
            }
            catch (Exception){}

            if (ListipsItem == null)
                ListipsItem = AllIPs.ElementAt(Zwischenspeicher);
            else
                Zwischenspeicher = ListIPs.Items.IndexOf(ListipsItem);

            string[] IndexAll = ListipsItem.Split(new char[] { '.' });
            string Index = IndexAll[3];
            //Aufruf einer Methode zum Auslesen der Daten (als Liste?)
            FarbPanel.BackColor = CheckOnline(Convert.ToByte(Index));
            int Zähler = 0;
            string Folge = "Folgende Pings fehlen: ";
            if (FarbPanel.BackColor == Color.Orange)
            {
                for (int i = 0; i < 300; i++)
                    if (Speicher[Convert.ToByte(Index), i])
                        Zähler++;
                    else
                        Folge += i.ToString() + ",";
            }
            DataBox.Clear();
            if (Zähler == 300)
                DataBox.Text = Zähler.ToString() + "/300\n";
            else
                DataBox.Text = Zähler.ToString() + "/300\n" + Folge;
        }
        private void UpdateDropDownMenu()
        {
            ListIPs.BeginUpdate();
            ListIPs.Items.Clear();
            foreach (string s in AllIPs)
            {
                ListIPs.Items.Add(s);               
            }
            ListIPs.EndUpdate();
        } 

        private void Update_Button_Click(object sender, EventArgs e) //Kommt noch weg
        {
            UpdateDropDownMenu();
            ListIPs_SelectedIndexChanged(null, null);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }
    }
}
