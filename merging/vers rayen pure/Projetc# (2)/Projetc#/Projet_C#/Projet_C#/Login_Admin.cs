using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_C_
{
    public partial class Login_Admin: Form
    {
        OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=\"C:\\Users\\User\\Desktop\\Projetc#\\BD_C#.accdb\";");
        public Login_Admin()
        {
            
            InitializeComponent();

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            Panel shadowPanel = new Panel();
            shadowPanel.Size = new Size(panel1.Width, panel1.Height);
            shadowPanel.Location = new Point(panel1.Left + 5, panel1.Top + 5);
            shadowPanel.BackColor = Color.FromArgb(80, 0, 0, 0); // gris foncé avec transparence

            // Insère l’ombre dans le même conteneur que panel1
            panel1.Parent.Controls.Add(shadowPanel);
            shadowPanel.SendToBack();
            panel1.BringToFront();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || (textBox1.Text.Length == 0 && textBox2.Text.Length == 0))
            {
                MessageBox.Show("S'il vous plait remplir tout le champ  ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }else
            {
                connection.Open();
                String req = "select login,password from  adminstrateur  " +
                    "where login ='" + textBox1.Text + "'";
                OleDbCommand cmd = new OleDbCommand(req, connection);
                OleDbDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    if (rd.GetString(1) == textBox2.Text)
                    {
                        this.Hide();
                        mise_en_forme_admin form = new mise_en_forme_admin(rd.GetString(0));
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
