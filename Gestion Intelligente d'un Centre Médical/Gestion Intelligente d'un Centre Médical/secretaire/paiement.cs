using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Intelligente_d_un_Centre_Médical.secretaire
{
    public partial class paiement : Form
    {
        private string src = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\BD_Centre-Médical.accdb"));

        private OleDbConnection connection;


        private string cin_patient;

        public paiement(string cin_patient)
        {
            this.cin_patient = cin_patient;

            connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + src + ";");
            InitializeComponent();
        }



        private void paiement_Load(object sender, EventArgs e)
        {
            text1.Hide();
            text2.Hide();
            text3.Hide();
            text4.Hide();
            text5.Hide();

            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            dateTimePicker1.Hide();
            textBox4.Hide();
            textBox5.Hide();
            button2.Hide();
            button1.Hide();

            connection.Open();

            string req = @"SELECT f.numero_facture, r.date, f.montant
FROM facture AS f, Paiement, rendez_vous AS r, patient
WHERE Paiement.numero_facture = f.numero_facture AND Paiement.numero_rendez_vous = r.numero_rendez_vous AND r.cin_patient = patient.cin_patient and patient.cin_patient='" + cin_patient + "' AND f.etat='impayé';";

            OleDbCommand cmd = new OleDbCommand(req, connection);
            OleDbDataReader dataReader = cmd.ExecuteReader();


            while (dataReader.Read())
            {
                dataGridView1.Rows.Add(
                    dataReader[0].ToString(), dataReader[1].ToString(), dataReader[2].ToString()
                );
            }


            string req2 = @"SELECT patient.nom,patient.prenom FROM patient WHERE patient.cin_patient='" + cin_patient + "';";

            cmd = new OleDbCommand(req2, connection);
            dataReader = cmd.ExecuteReader();


            dataReader.Read();

            cin.Text = "CIN :     " + cin_patient;
            nom.Text = "Nom :     " + dataReader.GetString(0);
            prenom.Text = "Prenom :     " + dataReader[1].ToString();



            connection.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {





        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Show();

            text1.Show();
            text2.Hide();
            text3.Hide();
            text4.Hide();
            text5.Hide();

            textBox1.Show();
            textBox2.Hide();
            textBox3.Hide();
            dateTimePicker1.Hide();
            textBox4.Hide();
            textBox5.Hide();
            button2.Hide();



            text1.Text = "montant : ";

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Show();

            text1.Show();
            text2.Show();
            text3.Show();
            text4.Show();
            text5.Show();


            textBox1.Show();
            textBox2.Show();

            // Remplacer TextBox par DateTimePicker
            dateTimePicker1.Location = textBox3.Location;

            textBox3.Hide();
            dateTimePicker1.Show();

            textBox4.Show();



            textBox5.Hide();
            button2.Show();

            text1.Text = "Nom de l'assureur : ";
            text2.Text = "Numéro de police d’assurance : ";
            text3.Text = "Date de validité de l’assurance : ";
            text4.Text = "Montant de la prise en charge : ";
            text5.Text = "Attestation d’assurance : ";


        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            button1.Show();

            text1.Show();
            text2.Show();
            text3.Show();
            text4.Show();
            text5.Hide();


            textBox1.Show();
            textBox2.Show();
            textBox3.Show();
            dateTimePicker1.Hide();
            textBox4.Show();
            textBox5.Hide();
            button2.Hide();


            text1.Text = "Numéro de carte bancaire : ";
            text2.Text = "Nom du titulaire de la carte : ";
            text3.Text = "Date d'expiration : ";
            text4.Text = "CVV/CVC : ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked)
                {
                    if (textBox1.Text.Length == 0)
                    {
                        throw new Exception("l'un des champs est vide");
                    }
                    else if (!IsNumeric(text1.Text))
                    {
                        throw new Exception("montant doit etre numerique");
                    }

                }
                else if (radioButton2.Checked)
                {
                    if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0 || textBox4.Text.Length == 0 || textBox5.Text.Length == 0)
                    {
                        throw new Exception("l'un des champs est vide");
                    }
                    else if (!IsAlphabetic(textBox1.Text))
                    {
                        throw new Exception("Nom de l'assureur doit etre alphabetique");
                    }
                    else if (!IsAlphanumeric(textBox2.Text))
                    {
                        throw new Exception("Numéro de police d’assurance doit etre alphaNumerique");
                    }
                    else if (dateTimePicker1.Value <= DateTime.Now)
                    {
                        throw new Exception("Date de validité de l’assurance est expirée");
                    }
                    else if (!IsNumeric(textBox4.Text))
                    {
                        throw new Exception("Montant de la prise en charge doit etre numerique");
                    }


                }
                else if (radioButton3.Checked)
                {
                }
                else
                {
                    MessageBox.Show("Vous devez choisir la methode de paiement");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private bool IsNumeric(string text)
        {
            return text.All(char.IsDigit);
        }

        private bool IsAlphabetic(string text)
        {
            return text.All(char.IsLetter);
        }

        private bool IsAlphanumeric(string text)
        {
            return text.All(char.IsLetterOrDigit);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Sélectionner un fichier";
            ofd.Filter = "Fichiers PDF|*.pdf|Images|*.jpg;*.png;*.jpeg";
            ofd.Multiselect = false; // Permettre un seul fichier

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                chemin.Text = Path.GetFileName(ofd.FileName); // Afficher le nom de fichier dans le label
            }


        }
    }
}

