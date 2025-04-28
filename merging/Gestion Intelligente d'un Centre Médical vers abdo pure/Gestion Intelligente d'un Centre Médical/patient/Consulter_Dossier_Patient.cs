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
    
    public partial class Consulter_Dossier_Patient: Form
    {
        
        private int id_rendezVous, id_Dossier,id_medcin,id_patient;
        DataSet Ds = new DataSet();
        OleDbDataAdapter Da;
        OleDbCommandBuilder cb;
        public Consulter_Dossier_Patient(int id_dossier,int id_medcin,int id_patient)
        {
       
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

    

        private void button2_Click(object sender, EventArgs e)
        {
          DataBase.connection.Open();

            string req = $"SELECT Document_medicale.numero_docuemnt,Document_medicale.Lien,Document_medicale.numero_rendez_vous,rendez_vous.date_rendez_vous from Document_medicale, rendez_vous WHERE Document_medicale.numero_rendez_vous = rendez_vous.numero_rendez_vous AND rendez_vous.id_medcin ={id_medcin} AND rendez_vous.id_patient ={id_patient} AND Document_medicale.numero_dossier ={id_Dossier} AND Document_medicale.Type_Document ='{comboBox1.SelectedItem.ToString()}' AND rendez_vous.date_rendez_vous BETWEEN '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' AND '{dateTimePicker2.Value.ToString("yyyy-MM-dd")}' ; ";
            OleDbCommand cmd=new OleDbCommand(req,DataBase.connection);
            OleDbDataReader rd = cmd. ExecuteReader();

            dataGridView1.Rows.Clear();
            while (rd.Read())
            {
                dataGridView1.Rows.Add(
                     rd[0].ToString(),  // numero_document
            rd[1].ToString(),  // Lien
            rd[2].ToString(),  // numero_rendez_vous
            rd[3].ToString()   // date_rendez_vous
                    );

            }
            DataBase.connection.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            
        }

        private void Consulter_Dossier_Patient_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            // Ajouter les Type de document possible 
            comboBox1.Items.Add("radio");
            comboBox1.Items.Add("analyse");
            comboBox1.Items.Add("ordonnance");
            textBox1.Text = id_Dossier.ToString();
            
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
