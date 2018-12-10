using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace SW_Projekt_Server
{
    class Kreutzers_Zeugs
    {
        public static void Main()
        {
            string IP = "";
            string connString = "server=127.0.0.1;user=root;database=mir_pups_egal;port=3306;password=Martin123";
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                //Console.WriteLine("Connecting to MySQL...");
                MessageBox.Show("Connecting to the Database");
                conn.Open();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
            //Console.WriteLine("Done.");
            MessageBox.Show("Done");
            conn.Open();
            string query2 = "INSERT INTO fehlfunktion (Art) VALUE (@errorcode)";
            MySqlCommand cmd = new MySqlCommand(query2, conn);
            cmd.Parameters.AddWithValue("@errorcode", IP);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}