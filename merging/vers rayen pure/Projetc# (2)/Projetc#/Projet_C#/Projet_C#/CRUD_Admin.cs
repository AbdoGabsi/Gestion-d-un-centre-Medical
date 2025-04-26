using System;
using System.Collections;
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
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Projet_C_
{
    public partial class CRUD_Admin: Form
    {
        OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=\"C:\\Users\\User\\Desktop\\Projetc#\\BD_C#.accdb\";");
        OleDbDataAdapter daPatient;
        OleDbDataAdapter daMedcin;
        OleDbDataAdapter daSecretaire;
        OleDbCommandBuilder cbP;
        OleDbCommandBuilder cbS;
        OleDbCommandBuilder cbM;
    

        DataSet ds;
       


        public CRUD_Admin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();

            //charger la combobox avec les type  d'utilisateur 
            comboBox1.Items.Add("Patient");
            comboBox1.Items.Add("Sécritaire");
            comboBox1.Items.Add("Medcin");

            //remplir la dataSet avec les table du base de données a manupiler
             ds = new DataSet("gestion_hopitale");

            // Ajouter la table Patient 
            daPatient = new OleDbDataAdapter("select * from patient", connection);
            daPatient.Fill(ds, "Patient" );
            cbP = new OleDbCommandBuilder(daPatient);
           
            

            // Ajouter la table Medcin
            daMedcin = new OleDbDataAdapter("select * from medcin", connection);
            daMedcin.Fill(ds, "Medcin");
            cbM = new OleDbCommandBuilder(daMedcin);
            //Ajouter la table sécritaire
            daSecretaire = new OleDbDataAdapter("select * from secretaire", connection);
            daSecretaire.Fill(ds, "Secretaire");
            cbS = new OleDbCommandBuilder(daSecretaire);
        
            connection.Open();
            string req = "select * from specialité";
            OleDbCommand cmd = new OleDbCommand(req, connection);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
                comboBox2.Items.Add(rd.GetString(0));
            connection.Close();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow ligne;
            Boolean existe = false;

           
               
            if (comboBox1.SelectedIndex == 0)
            {
                
                
                    for (int i = 0; i < ds.Tables["Patient"].Rows.Count && !existe; i++)
                    {
                        ligne = ds.Tables["Patient"].Rows[i];
                        if (ligne["CIN"].ToString() == textBox6.Text || ligne["Email"].ToString() == textBox3.Text)
                            existe = true;
                    }

                    if (!existe)
                    {
                    if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0 || textBox4.Text.Length == 0 || textBox6.Text.Length ==0)
                    {
                        MessageBox.Show("Veuillez remplir tous les champs", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        DataRow dr = ds.Tables["Patient"].NewRow();
                        dr["CIN"] = textBox6.Text;
                        dr["nom"] = textBox1.Text;
                        dr["prenom"] = textBox2.Text;
                        dr["date_naissance"] = dateTimePicker1.Value;
                        dr["Email"] = textBox3.Text;
                        dr["Tel"] = textBox4.Text;
                   
                        dr["Login"] = "default";
                        dr["mot_de_passe"] = "default";





                        ds.Tables["Patient"].Rows.Add(dr);
                        daPatient.Update(ds, "Patient");
                        connection.Open();
                        string req = "select max(id_patient) from patient";
                        OleDbCommand cmd = new OleDbCommand(req, connection);

                        int id_patient = (int)cmd.ExecuteScalar();
                        dataGridView1.Rows[ds.Tables["Patient"].Rows.Count - 1].Cells[0].Value = id_patient;
                        connection.Close();

                      

                        connection.Close();

                        MessageBox.Show("Patient ajouté avec succès !");
                        
                    }

                        
                    }
                    else
                        MessageBox.Show("Ce patient déjà existe dans le système   ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                

                    


            }else if(comboBox1.SelectedIndex == 1)
            {
                for (int i = 0; i < ds.Tables["Secretaire"].Rows.Count && !existe; i++)
                {
                    ligne = ds.Tables["Secretaire"].Rows[i];
                    if (ligne["CIN"].ToString() == textBox6.Text || ligne["Email"].ToString() == textBox3.Text)
                        existe = true;
                }

                if (!existe)
                {
                    if(textBox1.Text.Length==0 ||  textBox2.Text.Length==0|| textBox3.Text.Length==0 || textBox4.Text.Length == 0 || textBox6.Text.Length == 0)
                    {
                        MessageBox.Show("Veuillez remplir tous les champs", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DataRow dr = ds.Tables["Secretaire"].NewRow();
                        dr["CIN"] = textBox6.Text;
                        dr["nom"] = textBox1.Text;
                        dr["prenom"] = textBox2.Text;
                        dr["date_naissance"] = dateTimePicker1.Value.Date;
                        dr["Email"] = textBox3.Text;
                        dr["Tel"] = textBox4.Text;
                        dr["Login"] = "default";
                        dr["mot_de_passe"] = "default";

                        ds.Tables["Secretaire"].Rows.Add(dr);
                        daSecretaire.Update(ds, "Secretaire");
                        connection.Open();
                        string req = "select max(id_secretaire) from secretaire";
                        OleDbCommand cmd = new OleDbCommand(req, connection);

                        int id_secretaire =(int)cmd.ExecuteScalar();
                        dataGridView1.Rows[ds.Tables["Secretaire"].Rows.Count - 1].Cells[0].Value = id_secretaire;
                        connection.Close();

                        MessageBox.Show("Secretaire ajouté avec succès !");

                        connection.Close();
                        
                    }
                      
                }
                else
                    MessageBox.Show("Ce Secrétaire déjà existe dans le système   ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (comboBox1.SelectedIndex == 2)
            {
                for (int i = 0; i < ds.Tables["Medcin"].Rows.Count && !existe; i++)
                {
                    ligne = ds.Tables["Medcin"].Rows[i];
                    if (ligne["CIN"].ToString() == textBox6.Text || ligne["Email"].ToString() == textBox3.Text)
                        existe = true;
                }

                if (!existe)
                {
                    if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0 || textBox4.Text.Length == 0 ||comboBox2.Text.Length ==0 || textBox6.Text.Length == 0)
                    {
                        MessageBox.Show("Veuillez remplir tous les champs", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //Si la specialité n existe pas on va l'ajouter
                        connection.Open();
                        OleDbCommand cmd = new OleDbCommand("select * from specialité", connection);
                        OleDbDataReader spec = cmd.ExecuteReader();


                        Boolean contain = false;
                        while (spec.Read() && !contain)
                        {
                            if (spec.GetString(0).ToLower() == comboBox2.Text.ToLower()) 
                                contain = true;
                        }
                        spec.Close();
                        if (!contain)
                        {
                            cmd.CommandText = "insert into specialité  " +
                            "values('" + comboBox2.Text + "')";
                            cmd.ExecuteNonQuery();
                        }
                        connection.Close();

                        DataRow dr = ds.Tables["Medcin"].NewRow();
                        dr["CIN"] = textBox6.Text;
                        dr["nom"] = textBox1.Text;
                        dr["prenom"] = textBox2.Text;
                        dr["date_naissance"] = dateTimePicker1.Value;
                        dr["Email"] = textBox3.Text;
                        dr["Tel"] = textBox4.Text;
                        dr["nom_spécialité"] = comboBox2.Text;
                        dr["Login"] = "default";
                        dr["mot_de_passe"] = "default";
                        ds.Tables["Medcin"].Rows.Add(dr);
                        daMedcin.Update(ds, "Medcin");

                        //disponibilte par defaut
                        string[] joursSemaine = new string[] { "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi", "Dimanche" };
                        connection.Open();
                        string req ,req2;
                        OleDbCommand comande,cmd2;
                        DataRow derniereLigne = ds.Tables["medcin"].Rows[ds.Tables["medcin"].Rows.Count - 1];
                        String cin = derniereLigne[7].ToString();
                        req = "select id_medcin from medcin where CIN = '" + cin + "'";
                        comande = new OleDbCommand(req, connection);
                        OleDbDataReader reader = comande.ExecuteReader();
                        reader.Read();
                        int id_med = reader.GetInt32(0);
             

                        foreach (string jour in joursSemaine)
                        {
                            req2= "insert into disponibilite (id_medcin, jour, debut, fin) " +
                                "values(" + id_med + ",'" + jour + "', '00:00', '00:00')";
                            cmd2 = new OleDbCommand(req2, connection);
                            cmd2.ExecuteNonQuery();
                        }
                        connection.Close();


                        MessageBox.Show("Medcin ajouté avec succès !");
                      





                    }

                      
                }
                else
                    MessageBox.Show("Ce Medcin déjà existe dans le système   ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

          
            }else
                MessageBox.Show("s'il vous plait de sélectionner d'abord l'utilisateur", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

           
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            
            dateTimePicker1.Value = DateTime.Now;
            comboBox2.Text = "";
            textBox6.Clear();



        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox6.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            dateTimePicker1.Value = DateTime.Now;



            if (comboBox1.SelectedIndex == 0)
            {
                dataGridView1.DataSource = ds.Tables["Patient"];

                comboBox2.Visible = false;

            }

            else if (comboBox1.SelectedIndex == 1)
            {
                dataGridView1.DataSource = ds.Tables["Secretaire"];

                label7.Visible = false;


                comboBox2.Visible = false;


            }

            else
            {
                dataGridView1.DataSource = ds.Tables["Medcin"];

                label7.Visible = true;

                comboBox2.Visible = true;




            }



            comboBox2.Text = "";
        }

      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }
        

       


       
        

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Gestion_de_disponibilité form = new Gestion_de_disponibilité();
            form.Show();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
          textBox6.MaxLength = 8;
  
           
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.keyChar est une propriété qui permet de récupérer le caractère tapé par l'utilisateur
            //Keys.back represente effacer  ( <----- )
            
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
           
        }

        private void textBox6_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void textBox6_MouseLeave_1(object sender, EventArgs e)
        {
          
        }

        // Dans votre méthode d'initialisation ou de chargement du formulaire
        private void InitializeDataGridView()
        {
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.MaxLength = 8;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
    }

