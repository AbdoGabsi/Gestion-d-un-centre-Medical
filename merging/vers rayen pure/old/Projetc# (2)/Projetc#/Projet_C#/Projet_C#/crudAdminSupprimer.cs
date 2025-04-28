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
    public partial class crudAdminSupprimer: Form
    {
        OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=\"C:\\Users\\User\\Desktop\\Projetc#\\BD_C#.accdb\";");
        OleDbDataAdapter daPatient;
        OleDbDataAdapter daMedcin;
        OleDbDataAdapter daSecretaire;
        OleDbCommandBuilder cbP;
        OleDbCommandBuilder cbS;
        OleDbCommandBuilder cbM;


        DataSet ds;
      
        public crudAdminSupprimer()
        {
            InitializeComponent();
        }

        private void crudAdminSupprimer_Load(object sender, EventArgs e)
        {
           InitializeDataGridView();
            //charger la combobox avec les type  d'utilisateur 
            comboBox1.Items.Add("Patient");
            comboBox1.Items.Add("Sécritaire");
            comboBox1.Items.Add("Medcin");

            //remplir la dataSet avec les table du base de données a manupiler
            ds = new DataSet("gestion_hopitale");

            // Ajouter la table Patient 
            daPatient = new OleDbDataAdapter("select * from patient", connection);
            daPatient.Fill(ds, "Patient");
            cbP = new OleDbCommandBuilder(daPatient);



            // Ajouter la table Medcin
            daMedcin = new OleDbDataAdapter("select * from medcin", connection);
            daMedcin.Fill(ds, "Medcin");
            cbM = new OleDbCommandBuilder(daMedcin);
            //Ajouter la table sécritaire
            daSecretaire = new OleDbDataAdapter("select * from secretaire", connection);
            daSecretaire.Fill(ds, "Secretaire");
            cbS = new OleDbCommandBuilder(daSecretaire);

            connection.Open();
            string req = "select * from specialité";
            OleDbCommand cmd = new OleDbCommand(req, connection);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
              comboBox2.Items.Add(rd.GetString(0));
            connection.Close();


           
            

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox6.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
           
            dateTimePicker1.Value = DateTime.Now;




         




            if (comboBox1.SelectedIndex == 0)
            {
                dataGridView1.DataSource = ds.Tables["Patient"];

                comboBox2.Visible = false;

            }

            else if (comboBox1.SelectedIndex == 1)
            {
                dataGridView1.DataSource = ds.Tables["Secretaire"];



                comboBox2.Visible = false;


            }

            else
            {
                dataGridView1.DataSource = ds.Tables["Medcin"];

              

                comboBox2.Visible = true;




            }
            dataGridView1.DataSource = ds.Tables["Medcin"];



            comboBox2.Text = "";



          
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow linge;
            if (comboBox1.SelectedIndex == 0)
            {
                linge = dataGridView1.Rows[e.RowIndex];
                textBox6.Text = linge.Cells[6].Value.ToString();
                textBox1.Text = linge.Cells[1].Value.ToString();
                textBox2.Text = linge.Cells[2].Value.ToString();
                dateTimePicker1.Text = linge.Cells[3].Value.ToString();
                textBox3.Text = linge.Cells[4].Value.ToString();
                textBox4.Text = linge.Cells[5].Value.ToString();

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                linge = dataGridView1.Rows[e.RowIndex];
                textBox6.Text = linge.Cells[6].Value.ToString();
                textBox1.Text = linge.Cells[1].Value.ToString();
                textBox2.Text = linge.Cells[2].Value.ToString();
                dateTimePicker1.Text = linge.Cells[3].Value.ToString();
                textBox3.Text = linge.Cells[4].Value.ToString();
                textBox4.Text = linge.Cells[5].Value.ToString();

            }
            else
            {
                linge = dataGridView1.Rows[e.RowIndex];
                textBox6.Text = linge.Cells[7].Value.ToString();
                textBox1.Text = linge.Cells[1].Value.ToString();
                textBox2.Text = linge.Cells[2].Value.ToString();
                dateTimePicker1.Text = linge.Cells[3].Value.ToString();
                textBox3.Text = linge.Cells[4].Value.ToString();
                textBox4.Text = linge.Cells[5].Value.ToString();
                comboBox2.Text = linge.Cells[6].Value.ToString();
            }
        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            string CIN;

            if (comboBox1.SelectedIndex == 0)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    CIN = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();

                    for (int i = 0; i < ds.Tables["Patient"].Rows.Count; i++)
                    {
                        if (ds.Tables["Patient"].Rows[i].ItemArray[6].ToString() == CIN)
                            ds.Tables["Patient"].Rows[i].Delete();

                    }
                    daPatient.Update(ds, "Patient");
                    MessageBox.Show("Suppression avec succés   ", "succés", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox6.Clear();



                }
                else
                    MessageBox.Show("Veuillez selectionner un patient à supprimer", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables["Secretaire"];
                    CIN = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();

                    for (int i = 0; i < ds.Tables["Secretaire"].Rows.Count; i++)
                    {
                        if (ds.Tables["Secretaire"].Rows[i].ItemArray[6].ToString() == CIN)
                            ds.Tables["Secretaire"].Rows[i].Delete();
                    }
                    daSecretaire.Update(ds, "Secretaire");
                    MessageBox.Show("Suppression avec succés  ", "succés", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);





                }
                else
                    MessageBox.Show("Veuillez selectionner un secretaire à supprimer", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {


                    CIN = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    for (int i = 0; i < ds.Tables["Medcin"].Rows.Count; i++)
                    {
                        if (ds.Tables["Medcin"].Rows[i].ItemArray[0].ToString() == CIN)
                            ds.Tables["Medcin"].Rows[i].Delete();
                    }
                    daMedcin.Update(ds, "Medcin");
                    MessageBox.Show("Suppression avec succés ", "succés", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    textBox6.ReadOnly = true;

                }
                else
                    MessageBox.Show("Veuillez selectionner un medcin à supprimer", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("s'il vous plait de sélectionner d'abord l'utilisateur", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);


            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();


            dateTimePicker1.Value = DateTime.Now;
            comboBox2.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            textBox6.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            dateTimePicker1.Value = DateTime.Now;









            if (comboBox1.SelectedIndex == 0)
            {
                dataGridView1.DataSource = ds.Tables["Patient"];

                comboBox2.Visible = false;
                label9.Visible = false;

            }

            else if (comboBox1.SelectedIndex == 1)
            {
                dataGridView1.DataSource = ds.Tables["Secretaire"];



                comboBox2.Visible = false;
                label9.Visible = false;


            }

            else
            {
                dataGridView1.DataSource = ds.Tables["Medcin"];


                comboBox2.Visible = true;
                label9.Visible = true;




            }
        }
    }
}
