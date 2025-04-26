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
   
    public partial class EmploiMedcin: Form
    {
        
        DataSet ds;
        OleDbDataAdapter da;

        private int id_med;
        public EmploiMedcin(int id)
        {
           this.id_med = id;
            InitializeComponent();
        }

        private void EmploiMedcin_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            string req = "SELECT r.numero_rendez_vous,Format(r.heure_rendez_vous, 'hh:nn') as Heure,p.CIN, p.nom, p.prenom \r\nFROM rendez_vous AS r  \r\nINNER JOIN patient AS p ON (r.id_patient = p.id_patient)\r\nWHERE DateValue(r.[date_rendez_vous]) = Date() AND r.etat = 'Confirmé' AND r.id_medcin =" + id_med+"  " +
                " order by Format(r.heure_rendez_vous, 'hh:nn') ";


            DataBase.connection.Open();
            ds = new DataSet();
            da = new OleDbDataAdapter(req, DataBase.connection);
            da.Fill(ds, "rendez_vous");
            dataGridView1.DataSource = ds.Tables["rendez_vous"];
            DataBase.connection.Close();

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            mise_en_forme_medcin.pnl.Controls.Clear();
            int id_rendez_vous =int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            Gerer_rendez_vous form= new Gerer_rendez_vous(id_rendez_vous);
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            mise_en_forme_medcin.pnl.Controls.Add(form);
            form.Show();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
