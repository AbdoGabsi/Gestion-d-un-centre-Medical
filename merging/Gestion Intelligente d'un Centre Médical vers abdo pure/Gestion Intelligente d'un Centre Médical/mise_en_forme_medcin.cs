using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Intelligente_d_un_Centre_Médical
{
    public partial class mise_en_forme_medcin: Form
    {
        public static Panel pnl;
        private int idMed;
        private string pseudo;
        public mise_en_forme_medcin(int id_med,string pseudo)
        {
            InitializeComponent();
            pnl = panel4;
            this.idMed = id_med;
            this.pseudo = pseudo;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Top = button2.Top;
            panel3.Height = button2.Height;
            button2.BackColor = Color.FromArgb(46, 51, 73);
            button1.BackColor = Color.FromArgb(3, 169, 244);
            panel4.Controls.Clear();
            EmploiMedcin emploi = new EmploiMedcin(idMed);
            emploi.TopLevel = false;
            emploi.FormBorderStyle = FormBorderStyle.None;
            emploi.Dock = DockStyle.Fill;
            panel4.Controls.Add(emploi);
            emploi.Show();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Top = button1.Top;
            panel3.Height = button1.Height;
            button1.BackColor = Color.FromArgb(46, 51, 73);
            button2.BackColor = Color.FromArgb(3, 169, 244);
            panel4.Controls.Clear();
            Dashboard_Medcin stat = new Dashboard_Medcin(idMed);
            stat.TopLevel = false;
            stat.FormBorderStyle = FormBorderStyle.None;
            stat.Dock = DockStyle.Fill;
            panel4.Controls.Add(stat);
            stat.Show();
        }

        private void mise_en_forme_medcin_Load(object sender, EventArgs e)
        {
            label1.Text = this.pseudo;
            panel4.Controls.Clear();
            Dashboard_Medcin stat = new Dashboard_Medcin(idMed);
            stat.TopLevel = false;
            stat.FormBorderStyle = FormBorderStyle.None;
            stat.Dock = DockStyle.Fill;
            panel4.Controls.Add(stat);
            stat.Show();
        }
    }
}
