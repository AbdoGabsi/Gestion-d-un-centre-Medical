using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projet_C_
{
    public partial class Gerer_rendez_vous: Form
    {
        OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=\"C:\\Users\\User\\Desktop\\Projetc#\\BD_C#.accdb\";");
        private int ID_rendez_vous;
      int id_medcin, id_patient;
        DataSet ds=new DataSet("gestion_rendez_vous");
        OleDbDataAdapter da;
        OleDbDataAdapter hist;
        OleDbCommandBuilder cb;

        public Gerer_rendez_vous(int ID)
        {
            this.ID_rendez_vous = ID;
            InitializeComponent();
        }

        public Gerer_rendez_vous()
        {
        }

        private void Gerer_rendez_vous_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            Boolean exist = false;
            int i = 0;

           
            


            //recuperer l'id du medcin et l'id du patient
            connection.Open();
            string req = "select id_patient ,id_medcin from rendez_vous where numero_rendez_vous =" + ID_rendez_vous;
            OleDbCommand cmd = new OleDbCommand(req, connection);
            OleDbDataReader rd= cmd.ExecuteReader();
             rd.Read();
            id_medcin = rd.GetInt32(1);
            id_patient = rd.GetInt32(0);

            //charger les donnees du patient
            req= "select nom,prenom,Year(Date())-Year(date_naissance),CIN" +
                " from patient where id_patient =" + id_patient;
            cmd = new OleDbCommand(req, connection);
            rd = cmd.ExecuteReader();
            rd.Read();
            textBox1.Text = rd.GetString(0);
            textBox2.Text = rd.GetString(1);
            textBox3.Text = rd.GetInt32(2).ToString();
            textBox4.Text = rd.GetString(3);

            // verifier si le dossier medical existe

            string req2 = "select * from dossier_medicale ";
            OleDbCommand cmd2 = new OleDbCommand(req2, connection);
            OleDbDataReader r2 = cmd2.ExecuteReader();
            while (r2.Read() && !exist)
            {
                if(r2.GetInt32(1) == id_patient && r2.GetInt32(2) == id_medcin)
                {
                    textBox5.Text = r2.GetInt32(0).ToString();
                    exist = true;
                }
            }
            //charger l'historique des rendez-vous
            if (exist)
            {
                hist = new OleDbDataAdapter("select numero_rendez_vous, date_rendez_vous from rendez_vous where id_patient = " + id_patient + " and id_medcin ="+id_medcin+ " and etat ='Terminer'  order by date_rendez_vous", connection);
                hist.Fill(ds, "historique");
                dataGridView1.DataSource = ds.Tables["historique"];
            }



            connection.Close();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
                MessageBox.Show("il faut créer d'abord le dossier médicale", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                mise_en_forme_medcin.pnl.Controls.Clear();
                Gestion_Dossier form = new Gestion_Dossier(ID_rendez_vous, int.Parse(textBox5.Text),id_medcin,id_patient);
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                mise_en_forme_medcin.pnl.Controls.Add(form);
                form.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox5.Text=="")
                MessageBox.Show("il faut créer d'abord le dossier médicale", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                // changer l' etat l rendez vous 
                connection.Open();
                string req = "update rendez_vous  " +
                            "set etat = 'Terminer'  " +
                            "where numero_rendez_vous =" + ID_rendez_vous;
                OleDbCommand cmd = new OleDbCommand(req, connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("consultation Términer ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                // le medcin retourne au 1 er interface pour commencer le rendez-vous suivant 
                mise_en_forme_medcin.pnl.Controls.Clear();
                EmploiMedcin form = new EmploiMedcin(id_medcin);
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                mise_en_forme_medcin.pnl.Controls.Add(form);
                form.Show();
            }
               

        }

        private void button1_Click(object sender, EventArgs e)
        {

            connection.Open();
                // ajouter un dossier medical
                if (textBox5.Text == "")
                {
                string req = "insert into dossier_medicale (id_patient,id_medcin) " +
                 " values(" + id_patient + "," + id_medcin + ")";
                OleDbCommand cmd =new OleDbCommand(req,connection); 
                cmd.ExecuteNonQuery();

                req = "select max(numero_dossier) from dossier_medicale";
                OleDbCommand cmd2=new OleDbCommand(req,connection);
                textBox5.Text=cmd2.ExecuteScalar().ToString();


                    MessageBox.Show("le dossier médicale a été ajouté avec succès", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
                else 
                    MessageBox.Show ("le dossier médicale déjà existe ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void InitializeDataGridView()
        {
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // 2. Style des en-têtes de colonnes
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Gill Sans Nova Cond", 14, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.ColumnHeadersHeight = 35;

            // 3. Style des cellules
            dataGridView1.DefaultCellStyle.Font = new Font("Gill Sans Nova Cond", 11, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 255);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // 4. Effet 3D avec ombres et dégradés
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.CellPainting += DataGridView1_CellPainting;

            // 5. Personnalisation des lignes
            dataGridView1.RowTemplate.Height = 30;


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Ombre sur les cellules sélectionnées pour effet 3D
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
                {
                    using (var brush = new LinearGradientBrush(e.CellBounds,
                           Color.FromArgb(220, 240, 255),
                           Color.FromArgb(180, 220, 255),
                           LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(brush, e.CellBounds);
                    }

                    e.PaintContent(e.CellBounds);
                    e.Handled = true;
                }
            }
        }
    }

}
