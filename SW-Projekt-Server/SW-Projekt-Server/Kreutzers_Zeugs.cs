using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
namespace SW_Projekt_Server
{
    class Kreutzers_Zeugs
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        public void ConnectToDatabase()
        {
            //---------------------------------------------------------------!!!WICHTIG FÜR DICH GREGS!!!---------------------------------------------------------------
            string IP = "127.0.0.1"; // IP adresse vom Server
            //----------------------------------------------------------------------------------------------------------------------------------------------------------



            string connString = "server=" + IP + ";user=root;database=mir_pups_egal;port=3306;password=Martin123"; //Datenbankparameter also wohin man Verbinden soll und welcher login
            conn = new MySqlConnection(connString);
            //-------------------------------------------------------------------------------------------Test Anfang
            try
            {
                //MessageBox.Show("Connecting to the Database");
                conn.Open();//Versuche die Verbindung zu öffnen
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); //Fehlercode Ausgabe falls keine Verbindung hergestellt werden kann
            }
            conn.Close();
            //-------------------------------------------------------------------------------------------Test Ende
        }
        public List<string> DataFromDB()
        {
            List<string> returnvalue = new List<string>();
            MySqlDataReader rdr;
            //-------------------------------------------------------------------------------------------Auslesen
            conn.Open();
            try
            {
                rdr = cmd.ExecuteReader();
            }
            catch(NullReferenceException)
            {
                returnvalue.Add("Keine Fehler vorhanden");
                return returnvalue;
            }
            //catch
            //{
            //    MessageBox.Show("Datenbankfehler!");
            //}
            
            while (rdr.Read())
            {
                returnvalue.Add(rdr.GetInt32(0) + ":" + rdr.GetString(2) + ":" + rdr.GetMySqlDateTime(1) + "\n");
            }
            conn.Close();
            return returnvalue;
        }
        public void DataToDatabase(byte IPteil)
        {
            conn.Open();
            string query2 = "INSERT INTO fehlfunktion (Art) VALUE (" + IPteil.ToString() + ")"; //Befehl für die Datenbank
            cmd = new MySqlCommand(query2, conn);
            //cmd.Parameters.AddWithValue("@errorcode", IPteil.ToString()); // Die IP adresse die wir vom Gregs weitergeben wollen
            cmd.ExecuteNonQuery();//Ausführen des Befehls
            conn.Close(); //Verbindung unterbrechen wegen Netzwerkauslastung
        }
    }
}