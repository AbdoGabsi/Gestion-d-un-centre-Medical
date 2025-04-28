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

namespace Gestion_Intelligente_d_un_Centre_Médical.secretaire
{
    public partial class SIGN_UP_secretaire : Form
    {
        public SIGN_UP_secretaire()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0)
            {
                MessageBox.Show("S'il vous plait remplir tout le champ  ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                DataBase.connection.Open();
                String req = "select * from  secretaire  " +
                    "where Email ='" + textBox1.Text + "'";
                OleDbCommand cmd = new OleDbCommand(req, DataBase.connection);
                OleDbDataReader rd = cmd.ExecuteReader();
                if (rd != null)
                {
                    string req1 = "select * from  secretaire where login ='" + textBox2.Text + "";
                    OleDbCommand cmd1 = new OleDbCommand(req1, DataBase.connection);
                    OleDbDataReader rd1 = cmd1.ExecuteReader();
                    if (rd1.Read())
                    {
                        MessageBox.Show("Ce login existe déjà", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DataBase.connection.Close();
                        return;
                    }
                    else
                    {
                        string req2 = "update secretaire set login ='" + textBox2.Text + "', mot_de_passe ='" + textBox3.Text + "' where Email ='" + textBox1.Text + "'";
                        OleDbCommand cmd2 = new OleDbCommand(req2, DataBase.connection);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Votre inscription est bien enregistrée", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DataBase.connection.Close();
                        this.Hide();
                        Login_Medcin form = new Login_Medcin();
                        form.ShowDialog();
                    }

                }
                else
                {
                    MessageBox.Show("Pas d'inscription existe avec  " + textBox1.Text, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
