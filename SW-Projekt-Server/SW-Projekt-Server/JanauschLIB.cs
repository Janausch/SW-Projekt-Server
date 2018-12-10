using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms; //wird für die MessageBoxen (Fehlermeldungen) verwendet
using Microsoft.Win32; //wird für OpenFileDialog und SaveFileDialog benötigt
using System.IO; //wird für die Speicherung in einer Datei benötigt
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace SW_Projekt_Server
{
    struct JanauschLIB
    {
        #region ErrorMessage
        public void ErrorMessage (string Nachricht, string Fenstertitel)
        {
            MessageBox.Show(Nachricht, Fenstertitel, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        #endregion

        #region Speicherung einer Liste
        public static List<String> Speicher = new List<String>();//Deklaration der Liste

       // public List<string> MyList
        //{
        //    get { return Speicher; }
        //    set
        //    {
        //        foreach(string element in value)
        //        {
        //            Speicher.Add(element);
        //        }
        //    }
        //}
        private string Speicherpfad; //Deklaration der Variable Speicherpfad
        private string Speicherpfad2;//Deklaration der Variable Speicherpfad2
        
        public void DataToList(string Name, string Data, char Trennzeichen)//Methode, die Name mit Daten in die Liste speichert bzw. vorhandene Elemente updated
        {
            int counter = 0;//Deklaration der lokalen Variable counter
            foreach (string element in Speicher)//Schleife, die alle Einträge einer Liste einzeln durchgeht und in der Variable element speichert
            {
                string[] ListElement = element.Split(new char[] { Trennzeichen }, 2);//Teilt den Listeneintrag in Namen und Daten

                if (Name == ListElement[0])//Überprüft, ob der Name des Elementes mit dem Namen übereinstimmt
                {
                    Speicher.RemoveAt(counter);//Löscht den aktuellen Eintrag an der Stelle counter
                    Speicher.Insert(counter, Name + Trennzeichen + Data);//speichert das neue Element an der Stelle des alten
                    return;//beendet die Methode erfolgreich
                }
                counter++;//erhöht die Variable counter um 1
            }
                Speicher.Add(Name + Trennzeichen + Data);//speichert den Eintrag als neuen Eintrag
        }
        public List<string> DataFromListInList(char Trennzeichen)//Mehtode, die Namen in form einer Liste zurückgibt
        {
            List<string> returnList = new List<string>();//Deklariert die retunList
            foreach (string element in Speicher)//Schleife, die die Elemente der Liste einzeln durchgeht und in die Variable element speichert
            {
                string[] ListElement = element.Split(new char[] { Trennzeichen }, 2);//Teilt das Element in den Namen und die Daten
                returnList.Add(ListElement[0]);//speichert die Namen in die returnList
            }
            return returnList;//gibt die Liste returnList zurück
        }
        public List<string> SubDataFromListInList(string Name, char Trennzeichen, int Anzahl)//Methode, die die Daten eines Elementes geteilt als Liste zurück
        {
            List<string> returnList = new List<string>();//Deklaration der returnList
            foreach (string element in Speicher)//Schleife, die alle Elemente einer Liste einzeln durchgeht und in die Variable element speichert
            {
                string[] ListElement = element.Split(new char[] { Trennzeichen }, Anzahl);//Aufspaltung des Listeneintrags in Name und Daten

                if (Name == ListElement[0])//überprüft, ob der Name mit dem Namen des Listeneintrages übereinstimmt
                {
                    for (int i = 1; i < ListElement.Length; i++)//Schleife, die so oft durchläuft, wie das Array lang ist. i=1 -> 1.Wert ist der Name des Datensatzes
                        returnList.Add(ListElement[i]);//speichert jedes Element einzeln in die Liste
                    return returnList;//gibt die returnList an den Aufrufer zurück
                }
            }
            return null;//falls kein Wert in der Liste vorhanden ist
        }
        public void ListToList(List<string> Liste)//Methode, die eine übergebene Liste der lokalen Liste hinzufügt
        {
            foreach(string ListElement in Liste)//Schleife die alle Elemente einer Liste einzeln durchgeht und in die Variable ListElement speichert
            {
                Speicher.Add(ListElement);//füht ListElement der Liste hinzu
            }
        }
        public void SaveList(bool extSavePath)//Methode die die Liste in Abhägigkeit des Übergabewertes in eine Datei speichert
        {
            if (extSavePath == false)//Wenn kein externer Pfad verwendet werden soll
            {
                using (StreamWriter sw = new StreamWriter("Save.txt"))//unter der Verwendung des StreamWriters aus der Bibliothek System.IO werden die nachfolgenden Elemente in die vorher ermittelte Datei geschrieben
                {
                    sw.WriteLine("this");//schreibt in die erste Zeile der Standarddatei "this", damit beim nächsten Start des Programmes die Standarddatei als Speicher für die Elemente gewählt wird
                    foreach (string element in Speicher)//Schleife, die alle Elemente einzeln durchgeht und sie in die Variable element speichert
                    {
                        sw.WriteLine(element);//schreibt die Variable element in eine neue Zeile in die Datei
                    }

                }
            }
            else//falls ein externer Pfad für die Speicherung verwendet werden soll
            {
                if(Speicherpfad==null)//Überprüft, ob kein externer Speicherpfad gespeichert ist
                {
                    ErrorMessage("Kein Speicherpfad", null);//gibt eine Fehlermeldung aus, da kein Speicherpfad vorhanden ist
                    Speicherpfad = SaveFileDialog();//Fordert vom User auf einen neuen Speicherpfad zu wählen
                }
                using (StreamWriter sw = new StreamWriter("Save.txt"))//unter der Verwendung des StreamWriters aus der Bibliothek System.IO wird der Speicherpfad in die Standarddatei geschrieben
                {
                    sw.WriteLine(Speicherpfad);//Schreibt den Speicherpfad in die Standarddatei
                }
                using (StreamWriter sw = new StreamWriter(Speicherpfad))//unter der Verwendung des StreamWriters aus der Bibliothek System.IO werden die nachfolgenden Elemente in die vorher ermittelte Datei geschrieben
                {

                    foreach (string element in Speicher)//Schleife die alle Elemente der Liste einzeln durchgeht und in die Datei element speichert
                    {
                        sw.WriteLine(element);//schreibt das Element in eine neue Zeile in die Datei
                    }
                }
            }
        }
        public void LoadList()//Methode, die das Einlesen einer Liste bearbeitet
        {
            try//Fehlerbehandlung falls eine der Dateien nicht vorhanden ist
            {
                if (Speicherpfad == null||Speicherpfad == "")//überprüft, ob kein Speicherpfad vorhanden ist
                {
                    string s = null;//Deklaration der Variable s
                    string p = null;//Deklaration der Variable p

                    using (StreamReader sr = new StreamReader("Save.txt"))//unter der Verwendung des StreamReaders aus der Bibliothek System.IO wird das erste Elemente aus der Standarddatei gelesen
                    {
                        p = sr.ReadLine();//Die erste Zeile der Standarddatei wird ausgelesen
                    }

                    if (p == "this")//wenn in der ersten Zeile der Standarddatei this steht heißt das, dass die Elemente der Liste folgen
                    {
                        using (StreamReader sr = new StreamReader("Save.txt"))////unter der Verwendung des StreamReaders aus der Bibliothek System.IO werden die nachfolgenden Elemente aus der Standarddatei gelesen
                        {
                            sr.ReadLine();//einlesen der ersten Zeile -> Da hier this steht enthält diese Zeile keine Daten
                            while ((s = sr.ReadLine()) != null)//Schleife, die die Datei so lange ausliest, bis keine weitere Zeile der Datei beschrieben ist
                            {
                                Speicher.Add(s);//speichert das ausgelesene Element in die Liste
                            }
                        }
                    }                    
                    else//wenn die erste Zeile der Standarddatei kein this enthält, dann ist der Speicherpfad angegeben
                    {
                        Speicherpfad = p;//Speichern des Speicherpfades in die Variable Speicherpfad
                        using (StreamReader sp = new StreamReader(p))//unter der Verwendung des StreamReaders aus der Bibliothek System.IO werden die nachfolgenden Elemente aus der vorher ermittelte Datei gelesen
                        {
                            while ((s = sp.ReadLine()) != null)//Schleife, die die Datei so lange ausliest, bis keine weitere Zeile der Datei beschrieben ist
                            {
                                Speicher.Add(s);//Speichert das ausgelesene Element in die Liste
                            }
                        }
                    }
                }
                else//falls ein Speicherpfad in der Variable Speicherpfad vorhanden ist
                {
                    using (StreamReader sp = new StreamReader(Speicherpfad))//unter der Verwendung des StreamReaders aus der Bibliothek System.IO werden die nachfolgenden Elemente aus der vorher ermittelte Datei gelesen
                    {
                        string s = null;//Deklaration der Variable s
                        while ((s = sp.ReadLine()) != null)//Schleife, die die Datei so lange ausliest, bis keine weitere Zeile der Datei beschrieben ist
                        {
                            Speicher.Add(s);//Speichert das ausgelesene Element in die Liste
                        }
                    }
                }
            }
            catch (Exception)//Falls beim Auslesen der Dateien ein Fehler auftritt
            {
                //Meldung anpassen
                MessageBox.Show("ACHTUNG DEBUG FEHLER! \nDie Anwendung kann die Speicherungsdatei nicht finden! \nNur bei erstmaligem Starten normal. \nBitte neuen Speicherungsort auswählen."/*+ex.ToString()*/, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); //Fehlermeldung, dass die Standarddatei nicht gefunden wurde
                Speicherpfad = OpenFileDialog();//Der Nutzer kann nun eine bereits vorhandene Datei auswählen
                if (Speicherpfad == null)//Wenn der Nutzer keine Datei auswählt
                {
                    ErrorMessage("Speicherpfad Fehler", null);//wird eine Fehlermeldung ausgegeben
                    return;//beendet die Methode
                }
                LoadList();//falls eine Datei ausgewählt wurde, wird sie hier eingelesen
            }
        }
        public void LoadListFromExtFile()//Methode die eine Datei in die Liste einliest und überprüft, ob ein Element mit demselben Namen bereits vorhanden ist
        {
            Speicherpfad2 = OpenFileDialog();//Aufruf der Methode OpenFileDialog, welche den Speicherpfad der zu öffnenden Datei angibt
            string s;//Deklaration der Variable s, welche für die eingelesene Zeile aus der Datei verwendet wird
            using (StreamReader sp = new StreamReader(Speicherpfad2))//unter der Verwendung des StreamReaders aus der Bibliothek System.IO werden die nachfolgenden Elemente aus der vorher ermittelte Datei gelesen
            {
                while ((s = sp.ReadLine()) != null)//Schleife, welche die Datei so lange Zeile für Zeile ausliest bis sie keine weitere Zeile mehr enthält
                {
                    string[] ListElement = s.Split(new char[] { ';' }, 2);//Teilt das eingelesene Element in den Namen und die Daten auf
                    if (IsDataInList(ListElement[0], ';'))//überprüft, ob der Name des eingelesenen Elements in der Liste bereits vorhanden ist
                    {
                        DialogResult result = MessageBox.Show("Das Thermometer " + ListElement[0] + " ist bereits in der Liste.\nSollen die Werte überschrieben werden?", "Neues Thermometer hinzufügen.", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);//gibt eine Warnung aus, dass bereits ein Element mit demselben Namen in der Liste ist. Der Nutzer kann entscheiden, ob er die Daten überschreiben will oder ob er das Element überspringen will
                        if (result == DialogResult.Yes)//Wenn der Nutzer wählt, dass die Daten überschrieben werden sollen
                        {
                            DeleteItemFromList(ListElement[0], ';');//Löscht das Element das bereits in der Liste ist, mithilfe der Methode DeleteItemFormList, aus der Liste
                            Speicher.Add(s);//Speichert das eingelesene Element in die Liste
                        }
                        else//Wenn der Nutzer wählt, dass dieses Element übersprungen werden soll
                        { }
                    }
                    else
                        Speicher.Add(s);//Speichert das eingelesene Element in die Liste
                }
            }
        }
        public void SaveListToExtFile()//Methode die die Speicherung der Liste in eine externe Datei übernimmt
        {
            string exportPath = SaveFileDialog();//Aufruf der Methode SaveFileDialog, welche den Speicherpfad zurückgibt und speicherung des Pfades in exportPath
            using (StreamWriter sw = new StreamWriter(exportPath))//unter der Verwendung des StreamWriters aus der Bibliothek System.IO werden die nachfolgenden Elemente in die vorher ermittelte Datei geschrieben
            {
                foreach (string element in Speicher)//Schleife, welche die Elemente der Liste nacheinander durchgeht und in die Variable element speichert
                {
                    sw.WriteLine(element);//schreibt die Variable element in die nächste Zeile der Datei
                }
            }
        }
        public void DeleteItemFromList(string Name, char Trennzeichen)//Methode die einen Bestimmten Eintrag der Liste löscht
        {
            foreach (string element in Speicher)//Schleife, welche die Liste durchgeht und jeden Eintrag in die Variable element speichert
            {
                string[] ListElement = element.Split(new char[] { Trennzeichen }, 2);//Teilt die Variable element in den Namen des Eintrages und die Daten
                if (Name == ListElement[0])//überprüft, ob der gesuchte Name dem Namen des aktuellen Eintrages entspricht
                {
                    Speicher.Remove(element);//enfernt das element aus der Liste
                    return;//beendet den Suchvorgang
                }
            }
        }
        private string OpenFileDialog() //Methode die den OpenFileDialog öffnet und den Pfad zurückgibt
        {
            OpenFileDialog ofd = new OpenFileDialog();//Initialisierung des OpenFileDialogs
            Stream myStream;//Deklarierung der Variable myStream

            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";//Stellt den Filter für die verfügbaren Datentypen ein
            ofd.FilterIndex = 1;//Stellt den Standarddateityp ein
            ofd.RestoreDirectory = true;//öffnet den OpenFileDialog an der Stelle, wo zuletzt eine Datei gespeichert wurde

            if (ofd.ShowDialog() == DialogResult.OK)//Fragt ab, ob der OpenFileDialog erfolgreich geöffnet wurde
            {
                if ((myStream = ofd.OpenFile()) != null)//Sobald man auf den Button Öffnen drückt löst diese Abfrage aus
                {
                    string s = Path.GetFullPath(ofd.FileName);//Speichert den ausgewählten Pfad in die Variable S
                    myStream.Close();//Beendet den Datenöffnungsvorgang
                    return s;//gibt den ausgewwählen Speicherpfad zurück
                }
            }
            return null;//gibt nichts zurück im Falle, dass kein Pfad gewählt wurde
        }
        private string SaveFileDialog()//Methode die den SaveFileDialog öffnet und den Pfad zurückgibt
        {
            SaveFileDialog sfd = new SaveFileDialog();//Initialisierung des SaveFileDialogs
            Stream myStream;//Deklaration der Variable myStream

            sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";//Stellt den Filter für die verfügbaren Datentypen ein
            sfd.FilterIndex = 1;//Stellt den Standarddateityp ein
            sfd.RestoreDirectory = true;//öffnet den SaveFileDialog an der Stelle, wo zuletzt eine Datei gespeichert wurde

            if (sfd.ShowDialog() == DialogResult.OK)//Fragt ab, ob der SaveFileDialog erfolgreich geöffnet wurde
            {
                if ((myStream = sfd.OpenFile()) != null)//Sobald man auf den Button Speichern drückt löst diese Abfrage aus
                {
                    string S= Path.GetFullPath(sfd.FileName);//Speichert den ausgewählten Pfad in die Variable S
                    myStream.Close();//Beendet den Datenspeicherungsvorgang
                    return S;//gibt den ausgewwählen Speicherpfad zurück
                }
            }
            return null;//gibt nichts zurück im Falle, dass kein Pfad gewählt wurde
        }
        public bool ListisEmpty()//Methode, die zurückgibt, ob die Liste keine Elemente enthält
        {
            if (Speicher.Count == 0)
                return true;//gibt zurück, dass die Liste leer ist
            return false;//gibt zurück, dass die Liste Elemente enthält
        }
        public bool IsDataInList(string Name, char Trennzeichen) //Methode die untersucht, ob sich ein Wert in der Liste befindet
        {
            foreach(string element in Speicher) //Schleife die die ganze Liste durchgeht und jeden Wert in element zur Bearbeitung auslest
            {
                string[] ListElement = element.Split(new char[] { Trennzeichen }, 2); //zerlegt den Listeneintrag mithilfe des Trennzeichens in den Namen und die Daten des Eintrages
                if(Name == ListElement[0]) //Prüft ob das element dem gesuchten Namen entspricht
                {
                    return true;//gibt zurück, dass der Name in der Liste vorhanden ist
                }
            }
            return false;//gibt zurück, dass der Name nicht in der Liste vorhanden ist
        }
        #endregion

        #region Internetübertragungen
        private int Port;
        private bool PortListening;
        private Thread Start;

        public void TestPing(IPAddress IP)
        {
            Ping pinger = new Ping();
            PingOptions pingo = new PingOptions();

            pingo.DontFragment = true;
            byte[] Buffer = Encoding.ASCII.GetBytes("Test");
            int timeout = 1000;
            PingReply reply = pinger.Send(IP, timeout, Buffer, pingo);
            if(reply.Status == IPStatus.Success)
            {
                Console.WriteLine("IP: " + reply.Address.ToString());
                Console.WriteLine("Roadtime: "+ reply.RoundtripTime);
                Console.WriteLine("Buffer: " + reply.Buffer.ToString());
            }
            else
                Console.WriteLine("Error");
         }
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return null;
        }

        public void TestPingPort(IPAddress IP, int Port)
        {
            var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sock.Connect(IP, Port);
                //byte[] Buffer = Encoding.ASCII.GetBytes("Hi");
                //sock.Send(Buffer);
            }
            catch (Exception)
            {
                Console.WriteLine("No Connection available");
                return;
            }
            Console.WriteLine("Connected");
            sock.Close();
            Console.WriteLine("Disconnected");
        }
        public void Listener(int Port)
        {
            this.Port = Port;
            PortListening = true;
            Start = new Thread(new ThreadStart(TestPingPortListener));
        }
        public void EndListener()
        {
            PortListening = false;
            if (Start.ThreadState != ThreadState.Unstarted)
                Start.Join();
        }
        public void TestPingPortListener()
        {
            TcpListener Listener = new TcpListener(IPAddress.Parse(GetLocalIPAddress()), Port);
            Listener.Start();
            while (PortListening)
            {
                Socket client = Listener.AcceptSocket();
                var childSocketThread = new Thread(() =>
                {
                IPEndPoint end = client.RemoteEndPoint as IPEndPoint;
                Console.WriteLine("IP: " + end.Address);
                byte[] data = new byte[10];
                int size = client.Receive(data);
                Console.WriteLine("Recieved data: ");
                string Data = null;
                for (int i = 0; i < size; i++)
                    Data = Data + Convert.ToChar(data[i]).ToString();
                    Console.WriteLine(Data);
                client.Close();
                });
                childSocketThread.Start();
            }
        }

        #endregion
    }
}