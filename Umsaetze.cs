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
    public partial class Umsaetze : Form
    {
        Datenbank db = new Datenbank();
        double endsumme;

        List<Vertreter> vertreter = new List<Vertreter>();
        List<Auftrag> auftraege = new List<Auftrag>();

        public Umsaetze()
        {
            InitializeComponent();
            vertreter = db.getAllVertreter();
            auftraege = db.getAllAuftraege();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    richTextBox1.Text = "";
                    richTextBox1.Text = "Einzelne Umsätze: " + Environment.NewLine + db.getEinzelneUmsaetz() + Environment.NewLine + "Gesamtumsatz:" + Environment.NewLine + db.getGesamtUmsatz();
                }
                if (radioButton2.Checked == true)
                {
                    if (comboBox1.SelectedIndex > -1)
                    {
                        richTextBox1.Text = "";
                        int id = vertreter[comboBox1.SelectedIndex].getID();
                        richTextBox1.Text = "Einzelne Umsätze: " + Environment.NewLine + db.getEinzelneUmsaetzenachMitarbeiter(id) + Environment.NewLine + "Gesamtumsatz:" + Environment.NewLine + db.getGesamtUmsatznachMitarbeiter(id);
                    }
                    else
                    {
                        MessageBox.Show("Bitte Vertreter auswählen");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Schwerer Fehler auftreten: " + ex.Message);
            }
            try
            {
                if (radioButton3.Checked == true)
                {
                    if (comboBox2.SelectedIndex > -1)
                    {
                        richTextBox1.Text = "";
                        richTextBox1.Text = "Gesamtumsatz:" + Environment.NewLine + db.getGesamtUmsatznachAuftrag(auftraege[comboBox2.SelectedIndex].getID());
                    }
                    else
                    {
                        MessageBox.Show("Bitte Auftrag auswählen");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Schwerer Fehler auftreten: " + ex.Message);
            }
            try
            {
                if (radioButton4.Checked == true)
                {

                    richTextBox1.Text = "";
                    richTextBox1.Text = "Gesamtumsatz:" + Environment.NewLine + endsumme;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Schwerer Fehler auftreten: " + ex.Message);
            }
        }



    private void Umsaetze_Load(object sender, EventArgs e)
        {
            foreach (Vertreter v in vertreter)
            {
                comboBox1.Items.Add(v.getNname() + ", " + v.getVname());
            }
            foreach (Auftrag a in auftraege)
            {
                comboBox2.Items.Add(a.getAuftragsnummer());
                comboBox3.Items.Add(a.getAuftragsnummer());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox3.SelectedIndex > -1)
                {
                    
                    int id = auftraege[comboBox3.SelectedIndex].getID();
                    richTextBox1.Text = "";
                    richTextBox1.Text = auftraege[comboBox3.SelectedIndex].getAuftragsnummer() + ": " + db.getEinzelneUmsaetzenachAuftrag(id);
                    endsumme += Convert.ToDouble(auftraege[comboBox3.SelectedIndex].getUmsatz());
                }
                else
                {
                    MessageBox.Show("Bitte Auftrag auswählen");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Schwerer Fehler auftreten: " + ex.Message);
            }
        }
    }
}
