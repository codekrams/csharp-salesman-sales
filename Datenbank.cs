using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VertreterUmsaetze
{
    class Datenbank
    {
        private MySqlConnection dbConnection;

        public void dbOeffnen()
        {
            try
            {
                dbConnection = new MySqlConnection("SERVER=127.0.0.1; DATABASE=vertretermanagement;UID=root; PASSWORD=root;  SSLMode=None;");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void dbSchliessen()
        {
            dbConnection.Close();
        }

        public List<Vertreter> getAllVertreter()
        {
            List<Vertreter> liste = new List<Vertreter>();

            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE personal; SELECT * FROM vertretermanagement.vertreter;";
            dbConnection.Open();
            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                Vertreter v = new Vertreter(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                liste.Add(v);
            }
            reader.Close();
            dbConnection.Close();
            dbSchliessen();

            return liste;
        }

        public void safeVertreter(string nachname, string vorname)
        {
            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE vertretermanagement; INSERT INTO vertretermanagement.vertreter VALUES (NULL, '" + nachname + "', '" + vorname + "');";
            dbConnection.Open();
            comm.ExecuteNonQuery();

            dbConnection.Close();
            dbSchliessen();
        }

        public void updateVertreter(string nachname, string vorname, int id)
        {
            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE vertretermanagement; UPDATE vertretermanagement.vertreter SET nname = '" + nachname + "', vname = '" + vorname + "' WHERE id = " + id + ";";

            dbConnection.Open();
            comm.ExecuteNonQuery();

            dbConnection.Close();
            dbSchliessen();
        }

        public void deleteVertreter(int id)
        {
            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE vertretermanagement;  DELETE from vertretermanagement.auftraege WHERE vertreter_id = " + id + "; DELETE from vertretermanagement.vertreter WHERE id = " + id + ";";

            dbConnection.Open();
            comm.ExecuteNonQuery();

            dbConnection.Close();
            dbSchliessen();
        }

        public List<Auftrag> getAllAuftraege()
        {
            List<Auftrag> liste = new List<Auftrag>();

            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE vertretermanagement; SELECT * FROM vertretermanagement.auftraege;";
            dbConnection.Open();
            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                Auftrag v = new Auftrag(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3));
                liste.Add(v);
            }
            reader.Close();
            dbConnection.Close();
            dbSchliessen();

            return liste;
        }

        public void safeAuftraege(string auftragsnummer, int vertreter_id, string umsatz)
        {
            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE vertretermanagement; INSERT INTO vertretermanagement.auftraege VALUES (NULL, '" + auftragsnummer + "', '" + vertreter_id + "', '" + umsatz + "');";
            dbConnection.Open();
            comm.ExecuteNonQuery();

            dbConnection.Close();
            dbSchliessen();
        }

        public void updateAuftraege(string auftragsnummer, int vertreter_id, string umsatz, int id)
        {
            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE vertretermanagement; UPDATE vertretermanagement.auftraege SET auftragsnummer = '" + auftragsnummer + "', vertreter_id = '" + vertreter_id + "', umsatz = '" + umsatz + "' WHERE id = " + id + ";";

            dbConnection.Open();
            comm.ExecuteNonQuery();

            dbConnection.Close();
            dbSchliessen();
        }

        public void deleteAuftraege(int id, int vertreter_id)
        {
            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE vertretermanagement;  DELETE from vertretermanagement.auftraege WHERE vertreter_id = " + id + "; DELETE from vertretermanagement.vertreter WHERE id = " + id + ";";

            dbConnection.Open();
            comm.ExecuteNonQuery();

            dbConnection.Close();
            dbSchliessen();
        }

        public string getGesamtUmsatz()
        {
            string umsatz = "";
            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE vertretermanagement; SELECT SUM(vertretermanagement.auftraege.umsatz) FROM vertretermanagement.auftraege;";
            dbConnection.Open();
            MySqlDataReader reader = comm.ExecuteReader();
           

            while (reader.Read())
            {
               umsatz = reader.GetString(0);
            }
            reader.Close();
            dbConnection.Close();
            dbSchliessen();

            return umsatz;
        }

        public string getEinzelneUmsaetz()
        {
            string umsatz = "";
            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE vertretermanagement; SELECT vertretermanagement.auftraege.umsatz FROM vertretermanagement.auftraege;";

            dbConnection.Open();
            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                string zeile = reader.GetString(0);
                umsatz += Environment.NewLine +  zeile;
            }
            reader.Close();
            dbConnection.Close();
            dbSchliessen();

            return umsatz;
        }

        public string getEinzelneUmsaetzenachMitarbeiter(int id) {
            string umsatz = "";
            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE vertretermanagement; SELECT vertretermanagement.auftraege.umsatz FROM vertretermanagement.auftraege WHERE vertreter_id ="  + id +";";

            dbConnection.Open();
            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                string zeile = reader.GetString(0);
                umsatz += Environment.NewLine + zeile;
            }
            reader.Close();
            dbConnection.Close();
            dbSchliessen();
            return umsatz;
        }

        public string getGesamtUmsatznachMitarbeiter(int id)
        {
            string umsatz = "";
            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE vertretermanagement; SELECT SUM(vertretermanagement.auftraege.umsatz) FROM vertretermanagement.auftraege WHERE vertreter_id =" + id + ";";

            dbConnection.Open();
            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                umsatz = reader.GetString(0);
            }
            reader.Close();
            dbConnection.Close();
            dbSchliessen();
            return umsatz;
        }

        public string getGesamtUmsatznachAuftrag(int id)
        {
            string umsatz = "";
            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE vertretermanagement; SELECT vertretermanagement.auftraege.umsatz FROM vertretermanagement.auftraege WHERE id =" + id + ";";

            dbConnection.Open();
            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                umsatz = reader.GetString(0);
            }
            reader.Close();
            dbConnection.Close();
            dbSchliessen();
            return umsatz;
        }

        public string getEinzelneUmsaetzenachAuftrag(int id)
        {
            string umsatz = "";
            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE vertretermanagement; SELECT vertretermanagement.auftraege.umsatz FROM vertretermanagement.auftraege WHERE id =" + id + ";";

            dbConnection.Open();
            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                umsatz = reader.GetString(0);
            }
            reader.Close();
            dbConnection.Close();
            dbSchliessen();
            return umsatz;
        }
    }
}
