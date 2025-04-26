using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projet_C_
{
    public partial class Form1 : Form
    {
 OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=\"C:\\Users\\Victus\\Desktop\\C#\\BD_C#.accdb\";");

        public static string login;
       
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            login = textBox1.Text;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || (textBox1.Text.Length == 0 && textBox2.Text.Length == 0))
            {
                MessageBox.Show("S'il vous plait remplir tout le champ  ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {


                connection.Open();
                String req = "select login ,mot_de_passe from  patient  " +
                    "where login ='" + textBox1.Text + "'";

                OleDbCommand cmd = new OleDbCommand(req, connection);
                OleDbDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    if (rd.GetString(1) == textBox2.Text)
                    {
                        this.Hide();
                        Form3 form = new Form3();
                        form.Show();
                    }
                    else
                        MessageBox.Show("mot de passe incorrect", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                else
                    MessageBox.Show("Pas d'inscription existe avec  " + textBox1.Text, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
            }

        }



  
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form2 form = new Form2();
            form.Show();
        }

    
    }

}
