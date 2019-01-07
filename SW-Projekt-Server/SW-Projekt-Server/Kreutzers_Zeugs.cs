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
        public static void Main()
        {
            //---------------------------------------------------------------!!!WICHTIG FÜR DICH GREGS!!!---------------------------------------------------------------
            string IP = ""; // IP adresse vom Gregs zum eintragen in die Datenbank
            //----------------------------------------------------------------------------------------------------------------------------------------------------------



            string connString = "server=127.0.0.1;user=root;database=mir_pups_egal;port=3306;password=Martin123"; //Datenbankparameter also wohin man Verbinden soll und welcher login
            MySqlConnection conn = new MySqlConnection(connString);
            //-------------------------------------------------------------------------------------------Test Anfang
            try
            {
                MessageBox.Show("Connecting to the Database");
                conn.Open();//Versuche die Verbindung zu öffnen
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); //Fehlercode Ausgabe falls keine Verbindung hergestellt werden kann
            }
            conn.Close();
            MessageBox.Show("Done");
            //-------------------------------------------------------------------------------------------Test Ende
            conn.Open();
            string query2 = "INSERT INTO fehlfunktion (Art) VALUE (@errorcode)"; //Befehl für die Datenbank
            MySqlCommand cmd = new MySqlCommand(query2, conn);
            cmd.Parameters.AddWithValue("@errorcode", IP); // Die IP adresse die wir vom Gregs weitergeben wollen
            cmd.ExecuteNonQuery();//Ausführen des Befehls
            conn.Close(); //Verbindung unterbrechen wegen Netzwerkauslastung
        }
    }
}