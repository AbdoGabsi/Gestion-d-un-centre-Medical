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
            //afficher les patient qui ont des factures à payé
            string req1= @"SELECT DISTINCT patient.cin_patient,patient.nom,patient.prenom
FROM patient AS patient,Paiement as Paiement,rendez_vous as rendez_vous,facture AS facture
WHERE rendez_vous.numero_rendez_vous=Paiement.numero_rendez_vous AND Paiement.numero_facture=facture.numero_facture AND patient.cin_patient=rendez_vous.cin_patient AND facture.etat='impayé';";


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





                string req = @"SELECT patient.cin_patient,patient.photo AS photo,Count(facture.numero_facture) AS nbre_fact_impayé ,SUM(facture.montant) AS total
FROM patient AS patient,Paiement as Paiement,rendez_vous as rendez_vous,facture AS facture
WHERE rendez_vous.numero_rendez_vous=Paiement.numero_rendez_vous AND Paiement.numero_facture=facture.numero_facture AND patient.cin_patient=rendez_vous.cin_patient AND patient.cin_patient='" + vcin + "' AND facture.etat='impayé' GROUP BY patient.cin_patient;";

                
                DataSet ds = new DataSet("table");
                OleDbDataAdapter adapter = new OleDbDataAdapter(req, connection);
                adapter.Fill(ds);

                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    cin.Text = "CIN : " + vcin;
                    nom.Text = "Nom : " + vnom;
                    prenom.Text = "Prenom : " + vprenom;

                    nbr_fact.Text = "Nombre des factures =     " + ds.Tables[0].Rows[0][2].ToString();
                    total.Text = "Total montant =      " + ds.Tables[0].Rows[0][3].ToString();

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
                MessageBox.Show("Veuillez choisir une ligne");
            }
            
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
