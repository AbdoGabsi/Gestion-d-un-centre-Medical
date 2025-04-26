using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Intelligente_d_un_Centre_Médical.secretaire
{
    public partial class gestion_rendez_vous : Form
    {

        private string cin_patient;

        

        
        public OleDbDataReader dataReader;
        public gestion_rendez_vous(string cin_patient)
        {
            InitializeComponent();

            
            this.cin_patient = cin_patient;

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "HH:mm";  // Affiche seulement l'heure et les minutes
            dateTimePicker2.ShowUpDown = true;  // Remplace le calendrier par un sélecteur d'heure


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void gestion_rendez_vous_Load(object sender, EventArgs e)
        {

            ChargerListeRendezVous();

            DataBase.connection.Open();

            //charger les comboboxs

            string req = @"SELECT sp.nom_spécialité FROM specialité as sp;";

            OleDbCommand cmd = new OleDbCommand(req, DataBase.connection);
            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                comboBox1.Items.Add(dataReader[0].ToString());
            }



            req = @"SELECT m.id_medcin, m.nom & ' ' & m.prenom FROM medcin as m;";
            cmd = new OleDbCommand(req, DataBase.connection);
            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                Medcin medcin = new Medcin();
                medcin.Id = dataReader[0].ToString();
                medcin.NomComplet = dataReader[1].ToString();

                comboBox2.Items.Add(medcin);
            }



            DataBase.connection.Close();
        }


        public class Medcin
        {
            public string Id { get; set; }
            public string NomComplet { get; set; }

            public override string ToString()
            {
                return NomComplet; // Ce qui sera affiché dans le combobox 
            }
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

                comboBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[3].Value);

                dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[4].Value);

                comboBox3.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Hide();
            
            comboBox3.SelectedIndex = 0;
            comboBox3.Enabled = false;

            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
            

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            textBox1.Show();
            
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
            comboBox3.Enabled = true;

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            textBox1.Show();

            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            comboBox3.Enabled = false;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataBase.connection.Open();
            string req;
            OleDbTransaction transaction;
            if (radioButton1.Checked)
            {
                transaction = DataBase.connection.BeginTransaction();

                try
                {



                    Medcin medcin = (Medcin)comboBox2.SelectedItem;
                    req = "INSERT INTO rendez_vous(id_medcin,cin_patient,nom_spécialité,[date],début,etat) Values('" + medcin.Id + "','" + cin_patient + "','" + comboBox1.SelectedItem + "',#" + dateTimePicker1.Value + "#,#" + dateTimePicker2.Value + "#,'" + comboBox3.SelectedItem + "');";
                    OleDbCommand cmd = new OleDbCommand(req, DataBase.connection, transaction);
                    cmd.ExecuteNonQuery();



                    //recuperer le numero de rendez vous
                    req = @"SELECT @@IDENTITY";  // Utiliser cette requête pour obtenir le dernier ID généré
                    cmd = new OleDbCommand(req, DataBase.connection, transaction);
                    int numero_rendez_vous = Convert.ToInt32(cmd.ExecuteScalar());


                    //generer automatiquement une facture

                    int montant = 60;
                    req = $"INSERT INTO facture(montant,etat) VALUES({montant},'impayé');";
                    cmd = new OleDbCommand(req, DataBase.connection, transaction);
                    cmd.ExecuteNonQuery();

                    //recuperer le numero de la facture
                    req = @"SELECT @@IDENTITY";  // Utiliser cette requête pour obtenir le dernier ID généré
                    cmd = new OleDbCommand(req, DataBase.connection, transaction);
                    int numero_facture = Convert.ToInt32(cmd.ExecuteScalar());

                    //ajouter le paiement
                    req = $"INSERT INTO Paiement(numero_facture,numero_rendez_vous) VALUES({numero_facture},{numero_rendez_vous});";
                    cmd = new OleDbCommand(req, DataBase.connection, transaction);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();

                    MessageBox.Show("L'operation d\'ajout est effectué avec succes !");
                }
                catch
                {

                    transaction.Rollback();
                    MessageBox.Show("L'operation d\'ajout est échoué");

                }
            }
            else if (radioButton2.Checked && dataGridView1.Rows.Count > 0)
            {

                try
                {
                    Medcin medcin = (Medcin)comboBox2.SelectedItem;
                    req = $"UPDATE rendez_vous SET id_medcin='{medcin.Id}',cin_patient='{cin_patient}',[nom_spécialité]='{comboBox1.SelectedItem}',[date]=#{dateTimePicker1.Value}#,[début]=#{dateTimePicker2.Value}#,etat='{comboBox3.SelectedItem}' WHERE numero_rendez_vous ={textBox1.Text};";
                    OleDbCommand cmd = new OleDbCommand(req, DataBase.connection);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("L'operation de modification est effectué avec succes !");
                }
                catch
                {
                    MessageBox.Show("L'operation de modification est échoué");
                }
            }
            else if (radioButton3.Checked && dataGridView1.Rows.Count > 0)
            {


                try
                {
                    Medcin medcin = (Medcin)comboBox2.SelectedItem;
                    req = $"DELETE * FROM rendez_vous WHERE numero_rendez_vous={textBox1.Text};";
                    OleDbCommand cmd = new OleDbCommand(req, DataBase.connection);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("L'operation de suppression est effectué avec succes !");
                }
                catch
                {
                    MessageBox.Show("L'operation de suppression est échoué");
                }
            }
            else
            {
                MessageBox.Show("Vous devez choisir l'operation à effectuer");
            }

            DataBase.connection.Close();

            ChargerListeRendezVous();
        }

        private void ChargerListeRendezVous()
        {
            dataGridView1.Rows.Clear();


            DataBase.connection.Open();
            //charger le data grid view
            string req = @"SELECT rv.numero_rendez_vous, sp.nom_spécialité, 
    (m.nom & ' ' & m.prenom) AS medcin, 
    rv.date, 
    rv.début, 
    rv.etat
FROM 
    rendez_vous AS rv, 
    medcin AS m, 
    specialité AS sp, 
    patient AS p
WHERE 
    p.cin_patient=rv.cin_patient AND sp.nom_spécialité=rv.nom_spécialité AND m.id_medcin=rv.ID_medcin AND p.cin_patient='" + cin_patient + "';";
            OleDbCommand cmd = new OleDbCommand(req, DataBase.connection);
            dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                dataGridView1.Rows.Add(
                    dataReader[0].ToString(),
                    dataReader[1].ToString(),
                    dataReader[2].ToString(),
                    dataReader[3].ToString().Substring(0, 10),
                    Convert.ToDateTime(dataReader[4].ToString()).ToString("HH:mm"),
                    dataReader[5].ToString()
                );
            }

            DataBase.connection.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            acceuil_secretaire acceuil_Secretaire = new acceuil_secretaire();
            acceuil_Secretaire.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBase.connection.Open();
            if (comboBox1.SelectedItem != null)
            {
                string req = $"SELECT m.id_medcin, m.nom & ' ' & m.prenom FROM medcin as m WHERE m.nom_spécialité='{comboBox1.SelectedItem}';";
                OleDbCommand cmd = new OleDbCommand(req, DataBase.connection);
                dataReader = cmd.ExecuteReader();
                comboBox2.Items.Clear();
                comboBox2.Text = "";
                while (dataReader.Read())
                {
                    Medcin medcin = new Medcin();
                    medcin.Id = dataReader[0].ToString();
                    medcin.NomComplet = dataReader[1].ToString();
                    comboBox2.Items.Add(medcin);
                }
            }

            DataBase.connection.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked && textBox1.Text.Length!=0) {
                DataBase.connection.Open();
                try
                {
                    
                    string req = $"SELECT count(*) FROM rendez_vous WHERE numero_rendez_vous={textBox1.Text.ToString()};";
                    OleDbCommand cmd = new OleDbCommand(req, DataBase.connection);
                    int result = Convert.ToInt32(cmd.ExecuteScalar());

                    supression_label.Hide();

                    if (result == 0)
                    {
                        supression_label.Show();
                        supression_label.Text = "Ce numero de rendez-vous\n n'existe pas";
                    }
                    else
                    {
                        int i=0;
                        while (dataGridView1.Rows[i].Cells[0].Value.ToString() != textBox1.Text)
                        {
                            i++;
                        }
                        dataGridView1.ClearSelection();
                        dataGridView1.Rows[i].Selected = true;
                    }
                    
                }
                catch (Exception)
                {
                    supression_label.Show();
                    supression_label.Text = "Numéro invalide !!";
                }
                DataBase.connection.Close();
                
            }
            else
            {
                supression_label.Hide();
            }
        }
    }

    }
