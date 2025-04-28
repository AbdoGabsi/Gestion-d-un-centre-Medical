using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_C_
{
    public partial class mise_en_forme_admin: Form
    {
       private string login;
        public mise_en_forme_admin(string pseudo)
        {
            this.login = pseudo;
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mise_en_forme_admin_Load(object sender, EventArgs e)
        {
            label1.Text = login;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Top=button2.Top;
            panel3.Height = button2.Height;
            button2.BackColor = Color.FromArgb(46, 51, 73);
            button1.BackColor = Color.FromArgb(3, 169, 244);
            button3.BackColor = Color.FromArgb(3, 169, 244);
            button4.BackColor = Color.FromArgb(3, 169, 244);
            button5.BackColor = Color.FromArgb(3, 169, 244);
            panel4.Controls.Clear();
            CRUD_Admin admin = new CRUD_Admin();
            admin.TopLevel = false;
            admin.FormBorderStyle = FormBorderStyle.None;
            admin.Dock = DockStyle.Fill;
            panel4.Controls.Add(admin);
            admin.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Top = button3.Top;
            panel3.Height = button3.Height;
            button3.BackColor = Color.FromArgb(46, 51, 73);
            button1.BackColor = Color.FromArgb(3, 169, 244);
            button2.BackColor = Color.FromArgb(3, 169, 244);
            button4.BackColor = Color.FromArgb(3, 169, 244);
            button5.BackColor = Color.FromArgb(3, 169, 244);
           crudAdminModif admin = new crudAdminModif();
            panel4.Controls.Clear();
            admin.TopLevel = false;
            admin.FormBorderStyle = FormBorderStyle.None;
            admin.Dock = DockStyle.Fill;
            panel4.Controls.Add(admin);
            admin.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Top = button4.Top;
            panel3.Height = button4.Height;
            button4.BackColor = Color.FromArgb(46, 51, 73);
            button1.BackColor = Color.FromArgb(3, 169, 244);
            button3.BackColor = Color.FromArgb(3, 169, 244);
            button2.BackColor = Color.FromArgb(3, 169, 244);
            button5.BackColor = Color.FromArgb(3, 169, 244);
            crudAdminSupprimer admin = new crudAdminSupprimer();
            panel4.Controls.Clear();
            admin.TopLevel = false;
            admin.FormBorderStyle = FormBorderStyle.None;
            admin.Dock = DockStyle.Fill;
            panel4.Controls.Add(admin);
            admin.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel3.Top = button5.Top;
            panel3.Height = button5.Height;
            button5.BackColor = Color.FromArgb(46, 51, 73);
            button1.BackColor = Color.FromArgb(3, 169, 244);
            button3.BackColor = Color.FromArgb(3, 169, 244);
            button4.BackColor = Color.FromArgb(3, 169, 244);
            button2.BackColor = Color.FromArgb(3, 169, 244);
            panel4.Controls.Clear();
            Gestion_de_disponibilité admin =new  Gestion_de_disponibilité();
            admin.TopLevel = false;
            admin.FormBorderStyle = FormBorderStyle.None;
            admin.Dock = DockStyle.Fill;
            panel4.Controls.Add(admin);
            admin.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Top = button1.Top;
            panel3.Height = button1.Height;
            button1.BackColor = Color.FromArgb(46, 51, 73);
            button5.BackColor = Color.FromArgb(3, 169, 244);
            button3.BackColor = Color.FromArgb(3, 169, 244);
            button4.BackColor = Color.FromArgb(3, 169, 244);
            button2.BackColor = Color.FromArgb(3, 169, 244);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormPrincipale form = new FormPrincipale();
            form.Show();

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
