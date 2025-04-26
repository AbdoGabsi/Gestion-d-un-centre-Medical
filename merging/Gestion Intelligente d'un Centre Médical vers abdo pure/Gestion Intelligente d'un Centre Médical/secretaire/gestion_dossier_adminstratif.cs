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
using Org.BouncyCastle.Ocsp;
using static Gestion_Intelligente_d_un_Centre_Médical.secretaire.gestion_rendez_vous;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gestion_Intelligente_d_un_Centre_Médical.secretaire
{
    public partial class gestion_dossier_adminstratif : Form
    {
        
        public OleDbDataReader dataReader;

        private string patient_photo;
        
        private string cin_patient;
        public gestion_dossier_adminstratif(string cin_patient)
        {
            
            this.cin_patient = cin_patient;
            InitializeComponent();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            acceuil_secretaire acceuil_Secretaire = new acceuil_secretaire();
            acceuil_Secretaire.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DataBase.connection.Open();
            try
            {

                string req = $"UPDATE patient SET cin_patient='{textBox1.Text}',patient.nom='{textBox2.Text}',patient.prenom='{textBox3.Text}',patient.date_naissance=#{dateTimePicker1.Value}#,patient.Email='{textBox4.Text}',patient.Tel='{textBox5.Text}',patient.groupe_sanguin='{comboBox1.SelectedItem}',patient.photo='{patient_photo}' WHERE patient.cin_patient='{cin_patient}';";
                if (cin_patient == textBox1.Text)
                {
                    req = $"UPDATE patient SET patient.nom='{textBox2.Text}',patient.prenom='{textBox3.Text}',patient.date_naissance=#{dateTimePicker1.Value}#,patient.Email='{textBox4.Text}',patient.Tel='{textBox5.Text}',patient.groupe_sanguin='{comboBox1.SelectedItem}',patient.photo='{patient_photo}' WHERE patient.cin_patient='{cin_patient}';";
                }
                
                OleDbCommand cmd = new OleDbCommand(req, DataBase.connection);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Dossier mis à jour avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dossier non mis à jour :"+ex.Message);

            }



            DataBase.connection.Close();



        }

        private void gestion_dossier_adminstratif_Load(object sender, EventArgs e)
        {
            DataBase.connection.Open();
            string req = $"SELECT patient.cin_patient, patient.nom, patient.prenom, patient.date_naissance, patient.Email, patient.Tel, patient.groupe_sanguin, patient.photo FROM patient Where cin_patient='{cin_patient}';";
            OleDbCommand cmd = new OleDbCommand(req, DataBase.connection);
            dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                textBox1.Text = dataReader[0].ToString();
                textBox2.Text = dataReader[1].ToString();
                textBox3.Text = dataReader[2].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataReader[3]);
                textBox4.Text = dataReader[4].ToString();
                textBox5.Text = dataReader[5].ToString();
                comboBox1.SelectedItem = dataReader[6].ToString();


                photo_patient(dataReader[7], photo);
                patient_photo = dataReader[7].ToString();
            
                
            }

            



            DataBase.connection.Close();
        }

        private void photo_patient(object obj, PictureBox photo)
        {


            object photoFileName = obj;

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

        
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Ouvrir une boîte de dialogue pour sélectionner une nouvelle image
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Charger l'image sélectionnée dans le PictureBox
                photo.Image = Image.FromFile(openFileDialog.FileName);
                patient_photo = Path.GetFileName(openFileDialog.FileName);


                // Enregistrer le chemin de l'image dans la base de données ou effectuer d'autres actions nécessaires
            }

        }

        
    }
}
