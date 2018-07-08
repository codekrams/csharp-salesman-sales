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
    public partial class Vertretereingabe : Form
    {
        List<Vertreter> vertreter = new List<Vertreter>();
        Datenbank db = new Datenbank();

        public Vertretereingabe()
        {
            InitializeComponent();
            vertreter = db.getAllVertreter();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Vertretereingabe_Load(object sender, EventArgs e)
        {
            foreach (Vertreter v in vertreter)
            {
                listBox1.Items.Add(v.getNname() + ", " + v.getVname());
            }
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

            listBox1.Items.Clear();

            vertreter = db.getAllVertreter();

            foreach (Vertreter v in vertreter)
            {
                listBox1.Items.Add(v.getNname() + ", " + v.getVname());
            }
        }

        private void safeUpdate()
        {
            if (listBox1.SelectedIndex == -1)
            {
                if (!String.IsNullOrEmpty(textBox1.Text)
                     && !String.IsNullOrEmpty(textBox2.Text))
                {

                    db.safeVertreter(textBox1.Text, textBox2.Text);
                    MessageBox.Show("Vertreter eingetragen");
                }
                else
                {
                    MessageBox.Show("Bitte alle Angaben eintragen");
                }
            }

            if (listBox1.SelectedIndex > -1)
            {
                if (!String.IsNullOrEmpty(textBox1.Text)
                     && !String.IsNullOrEmpty(textBox2.Text))
                {
                    int id = vertreter[listBox1.SelectedIndex].getID();
                    db.updateVertreter(textBox1.Text, textBox2.Text, id);
                    MessageBox.Show("Vertreterdaten aktualisiert");
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
                    int id = vertreter[listBox1.SelectedIndex].getID();
                    db.deleteVertreter(id);
                    MessageBox.Show("Vertreter gelöscht");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Schwerer Fehler aufgetreten: " + ex.Message);
            }
            listBox1.Items.Clear();

            vertreter = db.getAllVertreter();

            foreach (Vertreter v in vertreter)
            {
                listBox1.Items.Add(v.getNname() + ", " + v.getVname());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1) { 
            textBox1.Text = vertreter[listBox1.SelectedIndex].getNname();
            textBox2.Text = vertreter[listBox1.SelectedIndex].getVname();
            }
        
        }

        private void Vertretereingabe_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            listBox1.SelectedIndex = -1;
        }
    }
}
