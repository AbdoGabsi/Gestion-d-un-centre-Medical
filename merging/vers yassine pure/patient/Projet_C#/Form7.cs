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

namespace Projet_C_
{
    public partial class Form7 : Form
    {
        OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=\"C:\\Users\\Victus\\Desktop\\C#\\BD_C#.accdb\";");
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

            listBox1.Items.Clear();
            try
            {
                connection.Open();

                // Récupérer l'id du patient connecté
                string getId = "SELECT id FROM Patient WHERE login = ?";
                OleDbCommand cmdId = new OleDbCommand(getId, connection);
                cmdId.Parameters.AddWithValue("?", Form1.login);
                int idPatient = Convert.ToInt32(cmdId.ExecuteScalar());

                // Préparer la requête pour récupérer les rendez-vous
                string req = "SELECT R.date_heure, M.nom, M.prenom " +
                             "FROM rendez_vous R " +
                             "INNER JOIN Medcin M ON R.id_medecin = M.id " +
                             "WHERE R.id_patient = ? " +
                             "ORDER BY R.date_heure DESC";

                OleDbCommand cmd = new OleDbCommand(req, connection);
                cmd.Parameters.AddWithValue("?", idPatient);

                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DateTime dt = Convert.ToDateTime(rdr["date_heure"]);
                    string nom = rdr["nom"].ToString();
                    string prenom = rdr["prenom"].ToString();
                    listBox1.Items.Add("Rendez-vous le " + dt.ToString("dd-MM-yyyy HH:mm") + " avec Dr. " + nom + " " + prenom);
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
    }
}
