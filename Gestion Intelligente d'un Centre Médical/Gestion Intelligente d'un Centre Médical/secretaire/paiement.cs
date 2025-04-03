using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
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
            panel1.Hide();
            
            connection.Open();

            ChargerFacturesImpayé();

            string req2 = @"SELECT patient.nom,patient.prenom FROM patient WHERE patient.cin_patient='" + cin_patient + "';";

            OleDbCommand cmd = new OleDbCommand(req2, connection);
            OleDbDataReader dataReader = cmd.ExecuteReader();


            dataReader.Read();

            cin.Text = "CIN :     " + cin_patient;
            nom.Text = "Nom :     " + dataReader.GetString(0);
            prenom.Text = "Prenom :     " + dataReader[1].ToString();



            connection.Close();

            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            clear();

            panel1.Show();

            payer.Show();

            text1.Hide();
            text2.Hide();
            text3.Hide();
            text4.Hide();
            

            textBox1.Hide();
            textBox2.Hide();

            textBox3.Hide();
            dateTimePicker1.Hide();

            textBox4.Hide();
            chemin.Hide();
            Choisir_un_fichier.Hide();


        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            clear();

            panel1.Show();

            payer.Show();

            text1.Show();
            text2.Show();
            text3.Show();
            text4.Show();
            

            textBox1.Show();
            textBox2.Show();

            textBox3.Hide();
            dateTimePicker1.Show();

            textBox4.Hide();
            chemin.Show();
            Choisir_un_fichier.Show();

            
            

            // Remplacer TextBox par DateTimePicker
            dateTimePicker1.Location = textBox3.Location;

            text1.Text = "Nom de l'assureur : ";
            text2.Text = "Numéro de police d’assurance : ";
            text3.Text = "Date de validité de l’assurance : ";
            text4.Text = "Attestation d’assurance : ";

            SetPlaceholder(textBox1, "Ex: Allianz, AXA...");
            SetPlaceholder(textBox2, "Ex: 123456789XYZ");
            

            
            


        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            clear();

            panel1.Show();

            payer.Show();

            text1.Show();
            text2.Show();
            text3.Show();
            text4.Show();
            

            textBox1.Show();
            textBox2.Show();

            textBox3.Hide();
            dateTimePicker1.Show();

            textBox4.Show();
            chemin.Hide();
            Choisir_un_fichier.Hide();

            
            

            // Remplacer TextBox par DateTimePicker
            dateTimePicker1.Location = textBox3.Location;

            text1.Text = "Numéro de carte bancaire : ";
            text2.Text = "Nom du titulaire de la carte : ";
            text3.Text = "Date d'expiration : ";
            text4.Text = "CVV/CVC : ";

            SetPlaceholder(textBox1, "1234 5678 9012 3456");
            SetPlaceholder(textBox2, "Jean Dupont");
            SetPlaceholder(textBox4, "123,1234");
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

     
        private void ChargerFacturesImpayé()
        {
            dataGridView1.Rows.Clear();

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
            
        }

        private void clear()
        {
            
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            dateTimePicker1.Value = DateTime.Now;
            DialogResult= DialogResult.No;

        }
        void SetPlaceholder(TextBox textBox, string placeholder)
        {
            
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.GotFocus += (s, e) => {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };
            
            textBox.LostFocus += (s, e) => {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };
            
        }

        private void Choisir_un_fichier_Click(object sender, EventArgs e)
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
        private void valider_payment()
        {
            string req = "";
            OleDbCommand cmd;
            OleDbDataAdapter adapter ;
            DataSet ds = new DataSet();
            
            connection.Open();
            
                if (radioButton2.Checked)
                {
                    req = @"INSERT INTO assurance VALUES('" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "','" + textBox1.Text + "','" + textBox2.Text + "',#" + dateTimePicker1.Value.ToString() + "#,'" + chemin.Text + "');";
                 cmd = new OleDbCommand(req, connection);
                cmd.ExecuteNonQuery();

            }
                else if (radioButton3.Checked)
                {
                    req = @"INSERT Into  carte_bancaire VALUES('" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "','" + textBox1.Text + "','" + textBox2.Text + "',#" + dateTimePicker1.Value.ToString() + "#,'" + textBox4.Text + "');";
                 cmd = new OleDbCommand(req, connection);
                cmd.ExecuteNonQuery();



            }
           
            connection.Close();

            connection.Open();

            adapter = new OleDbDataAdapter($"SELECT numero_facture,etat,montant FROM facture WHERE numero_facture='{dataGridView1.SelectedRows[0].Cells[0].Value.ToString()}';", connection);

            adapter.Fill(ds,"facture");


            DataRow row = ds.Tables[0].Rows[0];


            // Début de l'édition
            row.BeginEdit();

            row[1] = "payé";

            row.EndEdit(); // Fin de l'édition

            // Mise à jour de la base de données
            OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter);
            adapter.Update(ds, "facture");

            MessageBox.Show("Payement efféctué avec succés !");
            connection.Close();

            connection.Open();
            //actualiser le data grid view
            ChargerFacturesImpayé();
            
            connection.Close();

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Ce patient n'a plus de factures impayées");
                this.Close();
                
            }
        }
        private void payer_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.SelectedRows.Count != 1)
                {
                    throw new Exception("Vous devez seléctionné une facture à payer ");
                }
                else if (radioButton1.Checked)
                {

                    valider_payment();

                }
                else if (radioButton2.Checked)
                {
                    if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || !dateTimePicker1.Checked || chemin.Text=="fichier pdf/jpg/jpeg/png")
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
                    else if (dateTimePicker1.Value < DateTime.Now)
                    {
                        throw new Exception("Date de validité de l’assurance est expirée");
                    }
                    else
                    {
                        valider_payment();

                        
                    }



                }
                else if (radioButton3.Checked)
                {
                    if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || !dateTimePicker1.Checked || textBox4.Text.Length == 0)
                    {
                        throw new Exception("l'un des champs est vide");
                    }
                    else if (!IsNumeric(textBox1.Text) || textBox1.Text.Length < 13 || textBox1.Text.Length > 19)
                    {//Composé de 13 à 19 chiffres.
                        throw new Exception("Numéro de carte bancaire doit etre composé de 13 à 19 chiffres");
                    }
                    else if (!IsAlphabetic(textBox2.Text))
                    {
                        throw new Exception("Nom du titulaire de la carte doit etre alphabetique");
                    }
                    else if (dateTimePicker1.Value < DateTime.Now)
                    {
                        throw new Exception("la carte est expirée");
                    }
                    else if (!IsNumeric(textBox4.Text) || (textBox4.Text.Length != 3 && textBox4.Text.Length != 4))
                    {
                        throw new Exception("CVV/CVC doit etre composé de 3 ou 4 chiffres");
                    }
                    else
                    {
                        valider_payment();
                    }


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

       
    }
}

