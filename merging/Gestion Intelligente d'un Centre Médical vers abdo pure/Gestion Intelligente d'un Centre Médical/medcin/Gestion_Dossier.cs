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

namespace Gestion_Intelligente_d_un_Centre_Médical
{
    
    public partial class Gestion_Dossier: Form
    {
        
        private int id_rendezVous, id_Dossier,id_medcin,id_patient;
        DataSet Ds = new DataSet();
        OleDbDataAdapter Da;
        OleDbCommandBuilder cb;
        public Gestion_Dossier(int id_rendez_vous,int id_dossier,int id_medcin,int id_patient)
        {
            this.id_rendezVous = id_rendez_vous;
            this.id_Dossier = id_dossier;
            this.id_medcin = id_medcin;
            this.id_patient = id_patient;
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            DataBase.connection.Open();
            string req = "select * from  document_medicale  " +
                " where  numero_dossier  = "+id_Dossier;
            OleDbCommand cmd = new OleDbCommand(req, DataBase.connection);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                dataGridView1.Rows.Add(rd.GetInt32(0), rd.GetString(1), rd.GetString(2), rd.GetInt32(3), rd.GetInt32(4));
            }
            DataBase.connection.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow linge = dataGridView1.Rows[e.RowIndex];
            axAcroPDF1.src = linge.Cells[2].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mise_en_forme_medcin.pnl.Controls.Clear();
           Gerer_rendez_vous form = new Gerer_rendez_vous(id_rendezVous);
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            mise_en_forme_medcin.pnl.Controls.Add(form);
            form.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openFile.FileName;
                axAcroPDF1.src = textBox3.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
                MessageBox.Show("vous voulez sélectionner le document à ajouter", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (!textBox3.Text.Contains(comboBox1.Text.ToLower()) || comboBox1.Text.Length==0)
                    MessageBox.Show("Type de document incompatible", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DataRow dr = Ds.Tables["document_medical"].NewRow();
                   
                    dr[1] = comboBox1.Text;
                    dr[2] = textBox3.Text;
                    dr[3] = int.Parse(textBox1.Text);
                    dr[4] = int.Parse(textBox1.Text);
                    Ds.Tables["document_medical"].Rows.Add(dr);
                    Da.Update(Ds, "document_medical");
                    MessageBox.Show("Document ajouter avec succées", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox3.Text = "";
                }
                    
            }
        }

        private void Gestion_Dossier_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            // Ajouter les Type de document possible 
            comboBox1.Items.Add("radio");
            comboBox1.Items.Add("analyse");
            comboBox1.Items.Add("ordonnance");
            textBox1.Text = id_Dossier.ToString();
            textBox2.Text = id_rendezVous.ToString();
            Da = new OleDbDataAdapter("select * from document_medicale", DataBase.connection);
            Da.Fill(Ds, "document_medical");
            cb = new OleDbCommandBuilder(Da);

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
            dataGridView1.ColumnHeadersHeight = 80;

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
    }
}
