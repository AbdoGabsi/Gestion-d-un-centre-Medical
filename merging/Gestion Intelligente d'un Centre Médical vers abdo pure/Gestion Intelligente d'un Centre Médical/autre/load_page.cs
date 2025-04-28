using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Intelligente_d_un_Centre_Médical
{
    public partial class load_page : Form
    {
        public load_page()
        {
            InitializeComponent();
        }

        private void load_page_Load(object sender, EventArgs e)
        {
            

            
            progressBar1.Show();
            progressBar1.Maximum = 20;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            timer1.Start();


        }

        private void label2_Click(object sender, EventArgs e)
        {
            
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value += 1;
            }
            else
            {
                
                timer1.Stop();
                Thread.Sleep(1000);
                this.Hide();
                login_page login_Page=new login_page();
                login_Page.ShowDialog();  
            }
        }
    }
}
