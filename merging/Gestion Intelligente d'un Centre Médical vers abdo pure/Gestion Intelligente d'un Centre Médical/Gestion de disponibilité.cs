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
    public partial class Gestion_de_disponibilité: Form
    {
        
     
        public Gestion_de_disponibilité()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataBase.connection.Open();
                    string req = "UPDATE disponibilite " +
                    "SET debut = '" + dateTimePicker1.Value.TimeOfDay + "',fin = '" + dateTimePicker2.Value.TimeOfDay + "' " +
                    "WHERE id_medcin = " + int.Parse(comboBox1.Text)+ " AND jour = '" + textBox3.Text + "'";
                    OleDbCommand oleDbCommand = new OleDbCommand(req, DataBase.connection);
                    oleDbCommand.ExecuteNonQuery();
                    DataBase.connection.Close();
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == textBox3.Text)
                        {
                            dataGridView1.Rows[i].Cells[1].Value = dateTimePicker1.Value.TimeOfDay;
                            dataGridView1.Rows[i].Cells[2].Value = dateTimePicker2.Value.TimeOfDay;
                        }
                    }
                    MessageBox.Show("Modification effectuée avec succès", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBox3.Clear();
                }else
                    MessageBox.Show("s'il vous plait de sélectionner un jour pour le modifier", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
                MessageBox.Show("s'il vous plait de sélectionner un medcin", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Gestion_de_disponibilité_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            DataBase.connection.Open();
            // Charger les ID des medcin 
            string req = "select id_medcin from medcin ";
            OleDbCommand cmd = new OleDbCommand(req, DataBase.connection);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
                comboBox1.Items.Add(rd.GetInt32(0));

            DataBase.connection.Close();

            
        }

       

        

        

        private void InitializeDataGridView()
        {
            // 1. Configuration de base


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

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            textBox3.Clear();

            DataBase.connection.Open();

            dataGridView1.Rows.Clear();
            string req = "SELECT* FROM disponibilite " +
             "WHERE id_medcin = " + int.Parse(comboBox1.Text);

            OleDbCommand cmd = new OleDbCommand(req, DataBase.connection);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
                dataGridView1.Rows.Add(rd.GetString(1), rd.GetDateTime(2).TimeOfDay, rd.GetDateTime(3).TimeOfDay);

            string req2 = "select nom,prenom from medcin where id_medcin = " + int.Parse(comboBox1.Text);
            OleDbCommand cmd2 = new OleDbCommand(req2, DataBase.connection);
            OleDbDataReader rd2 = cmd2.ExecuteReader();
            rd2.Read();
            textBox1.Text = rd2.GetString(0);
            textBox2.Text = rd2.GetString(1);
            DataBase.connection.Close();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow linge;
            linge = dataGridView1.Rows[e.RowIndex];
            textBox3.Text = linge.Cells[0].Value.ToString();
            dateTimePicker1.Text = linge.Cells[1].Value.ToString();
            dateTimePicker2.Text = linge.Cells[2].Value.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataBase.connection.Open();
                    string req = "UPDATE disponibilite " +
                    "SET debut = '" + dateTimePicker1.Value.TimeOfDay + "',fin = '" + dateTimePicker2.Value.TimeOfDay + "' " +
                    "WHERE id_medcin = " + int.Parse(comboBox1.Text) + " AND jour = '" + textBox3.Text + "'";
                    OleDbCommand oleDbCommand = new OleDbCommand(req, DataBase.connection);
                    oleDbCommand.ExecuteNonQuery();
                    DataBase.connection.Close();
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == textBox3.Text)
                        {
                            dataGridView1.Rows[i].Cells[1].Value = dateTimePicker1.Value.TimeOfDay;
                            dataGridView1.Rows[i].Cells[2].Value = dateTimePicker2.Value.TimeOfDay;
                        }
                    }
                    MessageBox.Show("Modification effectuée avec succès", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBox3.Clear();
                }
                else
                    MessageBox.Show("s'il vous plait de sélectionner un jour pour le modifier", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
                MessageBox.Show("s'il vous plait de sélectionner un medcin", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }

}
