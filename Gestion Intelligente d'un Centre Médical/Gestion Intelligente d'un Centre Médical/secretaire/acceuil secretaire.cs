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
using Gestion_Intelligente_d_un_Centre_Médical.secretaire;

namespace Gestion_Intelligente_d_un_Centre_Médical
{
    
    public partial class acceuil_secretaire : Form
    {

        private string src = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\BD_Centre-Médical.accdb"));

        private OleDbConnection connection;
        public OleDbDataReader dataReader;

        
        public acceuil_secretaire()
        {
            connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + src + ";");
            InitializeComponent();
            
        }

        private void acceuil_secretaire_Load(object sender, EventArgs e)
        {

            
            connection.Open();
            //afficher touts les patient 
            string req1= @"SELECT patient.cin_patient,patient.nom,patient.prenom
FROM patient AS patient;";


            OleDbCommand cmd = new OleDbCommand(req1, connection);
            dataReader = cmd.ExecuteReader();
            
            
            
            while (dataReader.Read())
            {
                dataGridView1.Rows.Add(
                    dataReader[0].ToString(),
                    dataReader[1].ToString(),
                    dataReader[2].ToString()
                    
                    );
            }

            

            connection.Close();

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {

                string vcin = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string vnom = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                string vprenom = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();





                string req = @"SELECT 
    patient.cin_patient, 
    patient.photo, 
    (SELECT COUNT(*) FROM facture AS f, Paiement AS pa, rendez_vous AS rv 
     WHERE f.numero_facture = pa.numero_facture 
     AND pa.numero_rendez_vous = rv.numero_rendez_vous 
     AND rv.cin_patient = patient.cin_patient 
     AND f.etat = 'impayé') AS nbre_fact_impayé, 

    (SELECT SUM(f.montant) FROM facture AS f, Paiement AS pa, rendez_vous AS rv 
     WHERE f.numero_facture = pa.numero_facture 
     AND pa.numero_rendez_vous = rv.numero_rendez_vous 
     AND rv.cin_patient = patient.cin_patient 
     AND f.etat = 'impayé') AS total

FROM patient
WHERE patient.cin_patient ='" + vcin + "';";

                
                DataSet ds = new DataSet("table");
                OleDbDataAdapter adapter = new OleDbDataAdapter(req, connection);
                adapter.Fill(ds);

                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    cin.Text = "CIN : " + vcin;
                    nom.Text = "Nom : " + vnom;
                    prenom.Text = "Prenom : " + vprenom;

                    nbr_fact.Text = "Nombre des factures =     " + ds.Tables[0].Rows[0][2].ToString();
                    total.Text = "Total restant =      " + ds.Tables[0].Rows[0][3].ToString();

                    if (ds.Tables[0].Rows[0][2].ToString() == "0")
                    {
                        total.Hide();
                        btn_payer.Hide();
                    }
                    else
                    {
                        total.Show();
                        btn_payer.Show();
                    }

                    object attachmentData = ds.Tables[0].Rows[0][1];

                    object photoFileName = ds.Tables[0].Rows[0][1];

                    if (photoFileName != DBNull.Value && !string.IsNullOrEmpty(photoFileName.ToString()))
                    {
                        // Dossier fixe où sont stockées les images
                        string imageFolderPath = @"C:\Users\gabsi\source\repos\Project frame\Gestion Intelligente d'un Centre Médical\Images\patient";

                        // Construire le chemin de l'image
                        string imagePath = Path.Combine(imageFolderPath, photoFileName.ToString());

                        // Vérifier si le fichier image existe
                        if (File.Exists(imagePath))
                        {
                            photo.Image = Image.FromFile(imagePath);
                        }
                        else
                        {
                            MessageBox.Show("L'image n'existe pas : " + imagePath);
                            photo.Image = null; // Effacer l'image si elle est introuvable
                        }
                    }
                    else
                    {
                        
                        photo.Image = Image.FromFile(@"C:\Users\gabsi\source\repos\Project frame\Gestion Intelligente d'un Centre Médical\Images\patient\patient_inconnu.png");
                    }
                }
                
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            
            if (dataGridView1.SelectedRows.Count==1)
            {
                string cin_patient = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                this.Hide();
                paiement pa = new paiement(cin_patient); // cin de patient selectionnée 
                pa.Show();
                
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un patient de la liste");
            }
            
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_rendez_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                this.Hide();
                gestion_rendez_vous gestion_Rendez_Vous = new gestion_rendez_vous(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                gestion_Rendez_Vous.Show();
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un patient de la liste");
            }
            
        }
    }
}
