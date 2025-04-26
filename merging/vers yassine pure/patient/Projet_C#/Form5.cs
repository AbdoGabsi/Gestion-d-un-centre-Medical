using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projet_C_
{
    public partial class Form5 : Form
    {
        OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=\"C:\\Users\\Victus\\Desktop\\C#\\BD_C#.accdb\";");

        int idMedecinSelectionne = -1;
        TimeSpan heureSelectionnee;

        public Form5()
        {
            InitializeComponent();

            try
            {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT nom_spécialité FROM specialité", connection);
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(0));
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
            ;
        }



        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                connection.Open();
                string specialiteChoisie = comboBox1.Text;
                string req = "SELECT P.id, P.nom, P.prenom " +
                             "FROM Patient P " +
                             "INNER JOIN Medcin M ON P.id = M.id " +
                             "WHERE M.specialite = " + specialiteChoisie + "'";
                OleDbCommand cmd = new OleDbCommand(req, connection);

                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int id = Convert.ToInt32(rdr["id"]);
                    string nom = rdr["nom"].ToString();
                    string prenom = rdr["prenom"].ToString();
                    listBox1.Items.Add(id + " - Dr. " + nom + " " + prenom);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                connection.Open();
                DateTime date = dateTimePicker1.Value.Date;

                // Lire les disponibilités
                string dispoReq = "SELECT heure_debut, heure_fin FROM Disponibilite " +
                                  "WHERE id_medecin = ? AND jour = ?";
                OleDbCommand cmdDispo = new OleDbCommand(dispoReq, connection);
                cmdDispo.Parameters.AddWithValue("?", idMedecinSelectionne);
                cmdDispo.Parameters.AddWithValue("?", date);

                OleDbDataReader rdDispo = cmdDispo.ExecuteReader();

                List<TimeSpan> creneaux = new List<TimeSpan>();
                if (rdDispo.Read())
                {
                    TimeSpan debut = (TimeSpan)rdDispo["heure_debut"];
                    TimeSpan fin = (TimeSpan)rdDispo["heure_fin"];
                    for (TimeSpan t = debut; t < fin; t = t.Add(TimeSpan.FromMinutes(30))) // créneaux de 30min
                    {
                        creneaux.Add(t);
                    }
                }
                rdDispo.Close();

                // Récupérer les créneaux déjà pris
                string rdvReq = "SELECT heure FROM RendezVous " +
                                "WHERE id_medecin = ? AND jour = ?";
                OleDbCommand cmdRdv = new OleDbCommand(rdvReq, connection);
                cmdRdv.Parameters.AddWithValue("?", idMedecinSelectionne);
                cmdRdv.Parameters.AddWithValue("?", date);

                OleDbDataReader rdv = cmdRdv.ExecuteReader();

                List<TimeSpan> dejaPris = new List<TimeSpan>();
                while (rdv.Read())
                {
                    TimeSpan heure = (TimeSpan)rdv["heure"];
                    dejaPris.Add(heure);
                }
                rdv.Close();

                // Afficher les créneaux disponibles
                foreach (TimeSpan h in creneaux)
                {
                    if (!dejaPris.Contains(h))
                    {
                        listBox1.Items.Add(h.ToString(@"hh\:mm"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                // Trouver l'id du patient connecté à partir de l'email unique
                string getId = "SELECT id FROM Patient WHERE login = ?";
                OleDbCommand cmdId = new OleDbCommand(getId, connection);
                cmdId.Parameters.AddWithValue("?", Form1.login);
                int idPatient = Convert.ToInt32(cmdId.ExecuteScalar());

                // Vérifier si une heure a été sélectionnée
                if (heureSelectionnee == TimeSpan.Zero)
                {
                    MessageBox.Show("Veuillez sélectionner une heure valide avant de réserver.");
                    return;
                }

                // Construire la date complète avec l'heure
                DateTime jour = dateTimePicker1.Value.Date;
                DateTime dateHeure = jour.Add(heureSelectionnee);

                // Insertion du rendez-vous dans la base
                string insert = "INSERT INTO RendezVous (id_patient, id_medecin,nom_specialité, date_heure, statut) " +
                                "VALUES (?, ?, ?, ?, ?)";
                OleDbCommand cmd = new OleDbCommand(insert, connection);
                cmd.Parameters.AddWithValue("?", idPatient);
                cmd.Parameters.AddWithValue("?", idMedecinSelectionne);
                cmd.Parameters.AddWithValue("?", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("?", dateHeure);
                cmd.Parameters.AddWithValue("?", "en attente");

                int r = cmd.ExecuteNonQuery();

                if (r > 0)
                    MessageBox.Show("Rendez-vous réservé avec succès !");
                else
                    MessageBox.Show("Erreur lors de la réservation.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selection = listBox1.SelectedItem.ToString();

                if (selection.Contains("Dr."))
                {
                    // Extraire l'ID du médecin
                    idMedecinSelectionne = Convert.ToInt32(selection.Split('-')[0].Trim());
                }
                else
                {
                    // Tenter de lire une heure valide
                    if (TimeSpan.TryParse(selection, out TimeSpan h))
                    {
                        heureSelectionnee = h;
                    }
                    else
                    {
                        MessageBox.Show("Veuillez sélectionner une heure valide au format HH:mm.");
                        heureSelectionnee = TimeSpan.Zero; // évite les bugs plus tard
                    }
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
