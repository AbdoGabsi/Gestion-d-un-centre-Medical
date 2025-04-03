using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Intelligente_d_un_Centre_Médical.secretaire
{
    public partial class gestion_rendez_vous : Form
    {

        private string cin_patient;

        private string src = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\BD_Centre-Médical.accdb"));

        private OleDbConnection connection;
        public OleDbDataReader dataReader;
        public gestion_rendez_vous(string cin_patient)
        {
            InitializeComponent();

            connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + src + ";");
            this.cin_patient = cin_patient;

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "HH:mm:ss";  // Affiche seulement l'heure et les minutes
            dateTimePicker2.ShowUpDown = true;  // Remplace le calendrier par un sélecteur d'heure

            



        }
      
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void gestion_rendez_vous_Load(object sender, EventArgs e)
        {
            connection.Open();
            
            //charger le data grid view
            string req = @"
SELECT 
    rv.numero_rendez_vous, 
    sp.nom_spécialité, 
    (m.nom & ' ' & m.prenom) AS medcin, 
    rv.date, 
    rv.durée, 
    rv.etat
FROM 
    rendez_vous AS rv, 
    medcin AS m, 
    specialité AS sp, 
    patient AS p
WHERE 
    p.cin_patient=rv.cin_patient AND sp.nom_spécialité=rv.Nom_specialité AND m.id_medcin=rv.ID_medcin;

";
            OleDbCommand cmd = new OleDbCommand(req, connection);
            dataReader= cmd.ExecuteReader();

            while (dataReader.Read())
            {
                dataGridView1.Rows.Add(
                    dataReader[0].ToString(),
                    dataReader[1].ToString(),
                    dataReader[2].ToString(),
                    dataReader[3].ToString(),
                    dataReader[4].ToString(),
                    dataReader[5].ToString()
                );
            }

            //charger les comboboxs

            req = @"SELECT sp.nom_spécialité FROM specialité as sp;";

            cmd = new OleDbCommand(req, connection);
            dataReader= cmd.ExecuteReader();
            while (dataReader.Read())
            {
                comboBox1.Items.Add(dataReader[0].ToString());
            }


            req = @"SELECT m.nom & ' ' & m.prenom FROM medcin as m;";
            cmd = new OleDbCommand(req, connection);
            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                comboBox2.Items.Add(dataReader[0].ToString());
            }

            
            
            connection.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                comboBox2.Text= dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[3].Value);
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                comboBox3.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            }

        }

        
    }
    
}
