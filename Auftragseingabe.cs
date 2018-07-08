using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VertreterUmsaetze
{
    public partial class Auftragseingabe : Form
    {
        List<Vertreter> vertreter = new List<Vertreter>();
        List<Auftrag> auftraege = new List<Auftrag>();
        Datenbank db = new Datenbank();

        public Auftragseingabe()
        {
            InitializeComponent();
            auftraege = db.getAllAuftraege();
            vertreter = db.getAllVertreter();
        }

        private void Auftragseingabe_Load(object sender, EventArgs e)
        {
            foreach (Vertreter v in vertreter)
            {
                comboBox1.Items.Add(v.getNname() + ", " + v.getVname());
            }

            foreach (Auftrag a in auftraege)
            {
                listBox1.Items.Add(a.getAuftragsnummer());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                safeUpdate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Schwerer Fehler aufgetreten: " + ex.Message);
            }

            showAuftraege();
        }

        private void safeUpdate()
        {
            if (listBox1.SelectedIndex == -1)
            {
                if (!String.IsNullOrEmpty(textBox1.Text)
                     && comboBox1.SelectedIndex > -1)
                {
                    int vertreter_id = vertreter[comboBox1.SelectedIndex].getID();
                    db.safeAuftraege(textBox1.Text, vertreter_id, textBox2.Text.Replace(",", "."));
                    MessageBox.Show("Auftrag angelegt");
                }
                else
                {
                    MessageBox.Show("Bitte alle Angaben eintragen");
                }
            }

            if (listBox1.SelectedIndex > -1)
            {
                if (!String.IsNullOrEmpty(textBox1.Text)
                    && !String.IsNullOrEmpty(textBox2.Text)
                     && comboBox1.SelectedIndex > -1)
                {
                    int id = auftraege[comboBox1.SelectedIndex].getID();
                    int vertreter_id = vertreter[comboBox1.SelectedIndex].getID();                 
                    db.updateAuftraege(textBox1.Text, vertreter_id, textBox2.Text.Replace(",", "."), id);
                    MessageBox.Show("Auftragsdaten aktualisiert");
                }
                else
                {
                    MessageBox.Show("Bitte alle Angaben eintragen");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex > -1)
                {
                    int id = auftraege[listBox1.SelectedIndex].getID();
                    int vertreter_id = auftraege[listBox1.SelectedIndex].getVertreter_ID();
                    db.deleteAuftraege(id, vertreter_id);
                    MessageBox.Show("Auftrag gelöscht");
                }
                else
                {
                    MessageBox.Show("Bitte Auftrag auswählen");
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Schwerer Fehler aufgetreten: " + ex.Message);
            }

            showAuftraege();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = auftraege[listBox1.SelectedIndex].getAuftragsnummer();

            int vertreter_id = auftraege[listBox1.SelectedIndex].getVertreter_ID();
            int vid=1;

            for (int i=0; i<vertreter.Count; i++) {
                if (vertreter[i].getID() == vertreter_id) {
                    vid = i;
                    break;
                }
            }

            comboBox1.SelectedIndex = vid;
            textBox2.Text= auftraege[listBox1.SelectedIndex].getUmsatz().ToString();

        }

        private void showAuftraege()
        {
            listBox1.Items.Clear();

            auftraege = db.getAllAuftraege();

            foreach (Auftrag a in auftraege)
            {
                listBox1.Items.Add(a.getAuftragsnummer());
            }

        }
    }
}
