using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VertreterUmsaetze
{
    public partial class Form1 : Form
    {
        List<Auswahl> auswahl = new List<Auswahl>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getAuswahl();
        }

        private void getAuswahl()
        {
            if (comboBox1.Text == "Auftraege eingeben, aendern oder loeschen")
            {
                Auftragseingabe f2 = new Auftragseingabe();
                f2.ShowDialog();

            }
            else if (comboBox1.Text == "Vertreter eingeben, aendern oder loeschen")
            {
                Vertretereingabe f2 = new Vertretereingabe();
                f2.ShowDialog();
            }
            else if (comboBox1.Text == "Umsaetze anzeigen")
            {
                Umsaetze f2 = new Umsaetze();
                f2.ShowDialog();
            }
            else if (String.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Bitte Aktion auswählen");
            }
        }

        public void listeErstellen()
        {
            comboBox1.Items.Clear();

            FileStream fs = new FileStream("auswahl.txt", FileMode.OpenOrCreate, FileAccess.Read);

            StreamReader sr = new StreamReader(fs);

            while (sr.Peek() != -1)
            {
                    Auswahl a1 = new Auswahl(sr.ReadLine());
                    auswahl.Add(a1);
                    Auswahl a2 = new Auswahl(sr.ReadLine());
                    auswahl.Add(a2);
                    Auswahl a3 = new Auswahl(sr.ReadLine());
                    auswahl.Add(a3);
            }
            sr.Close();
            fs.Close();

            listeAnzeigen();

        }

        public void listeAnzeigen()
        {
            comboBox1.Items.Clear();

            foreach (Auswahl a in auswahl)
            {
                comboBox1.Items.Add(a.auswahl);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listeErstellen();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
