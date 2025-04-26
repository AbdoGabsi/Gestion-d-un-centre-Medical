using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projet_C_
{
    public partial class Form2 : Form
    {
        OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=\"C:\\Users\\Victus\\Desktop\\C#\\BD_C#.accdb\";");
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

       
       



        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            {
                // Vérification des champs vides
                if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0)
                {
                    MessageBox.Show("S'il vous plait remplir tout le champ  ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    connection.Open();
                    string req = "update patient set login ='" + textBox2.Text + "', mot_de_passe ='" + textBox1.Text + "' where Email ='" + textBox3.Text + "'";
                    OleDbCommand cmd = new OleDbCommand(req, connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Votre inscription est bien enregistrée", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    connection.Close();
                    this.Hide();
                    Form1 form = new Form1();
                    form.ShowDialog();
                }
            }

        }
    }
}
