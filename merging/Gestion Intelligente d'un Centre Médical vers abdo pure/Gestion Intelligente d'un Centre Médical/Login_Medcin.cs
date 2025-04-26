using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Gestion_Intelligente_d_un_Centre_Médical
{
    public partial class Login_Medcin: Form
    {
        
        public Login_Medcin()
        {
            InitializeComponent();
        }

        private void Login_Medcin_Load(object sender, EventArgs e)
        {

        }

       
      
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int id_med;
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || (textBox1.Text.Length == 0 && textBox2.Text.Length == 0))
            {
                MessageBox.Show("S'il vous plait remplir tout le champ  ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                DataBase.connection.Open();
                String req = "select ID_medcin,mot_de_passe from  medcin  " +
                    "where login ='" + textBox1.Text + "'";
                OleDbCommand cmd = new OleDbCommand(req, DataBase.connection);
                OleDbDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    if (rd.GetString(1) == textBox2.Text)
                    {
                        id_med = rd.GetInt32(0);
                        this.Hide();
                        mise_en_forme_medcin form = new mise_en_forme_medcin(id_med,textBox1.Text);
                        form.Show();
                    }
                    else
                        MessageBox.Show("mot de passe incorrect", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                else
                    MessageBox.Show("Pas d'inscription existe avec  " + textBox1.Text, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DataBase.connection.Close();
            }
            }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            SIGN_UP_medcin form = new SIGN_UP_medcin();
            form.Show();
        }
    }
}
