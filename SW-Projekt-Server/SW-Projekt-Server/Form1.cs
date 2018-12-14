using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private async void GetIPs()
        {
            Task<string> GetIPs = nc.ServerPingStart(nc);
            allIps = await GetIPs;
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
