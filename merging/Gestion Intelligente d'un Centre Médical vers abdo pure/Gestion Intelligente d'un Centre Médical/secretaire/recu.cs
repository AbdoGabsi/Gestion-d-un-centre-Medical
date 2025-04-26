using System;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;



namespace Gestion_Intelligente_d_un_Centre_Médical.secretaire
{
    public partial class recu : Form
    {
        private int numero_facture;
        
        
        
        public recu(int numero_facture)
        {
            InitializeComponent();
            this.numero_facture = numero_facture;
            

        }



public void GenererRecuPDF(int montant,string mode_paiement,string NPClient,string NPMedcin, string date_paiement, string fichierPath)
    {
           

            // Create the document with custom size
            Document doc = new Document(PageSize.A5);
            PdfWriter.GetInstance(doc, new FileStream(fichierPath, FileMode.Create));
        doc.Open();

        // Titre
        var titre = new Paragraph("Reçu de Paiement")
        {
            Alignment = Element.ALIGN_CENTER,
            SpacingAfter = 20f
        };
        doc.Add(titre);

            // Contenu
            doc.Add(new Paragraph("Numero facture payé : " + numero_facture));
            doc.Add(new Paragraph("Nom et Prenom du client : " + NPClient));
        doc.Add(new Paragraph("Nom et Prenom du medcin : " + NPMedcin));
            doc.Add(new Paragraph("Date de paiement: " + date_paiement));
        doc.Add(new Paragraph("Montant payé : " + montant + " DT"));
        doc.Add(new Paragraph("Merci pour votre confiance !"));

        doc.Close();
    }

        private void recu_Load(object sender, EventArgs e)
        {
            DataBase.connection.Open();
            // Récupérer les informations de la facture
            string req1 = $"SELECT facture.montant,Paiement.mode_de_paiement,patient.nom&' '&patient.prenom,medcin.nom &' '&medcin.prenom FROM Paiement,facture,patient,rendez_vous,medcin WHERE Paiement.numero_facture=facture.numero_facture AND Paiement.numero_rendez_vous=rendez_vous.numero_rendez_vous AND rendez_vous.cin_patient=patient.cin_patient AND rendez_vous.ID_medcin=medcin.ID_medcin AND facture.numero_facture=@numero_facture; ";
            OleDbCommand cmd = new OleDbCommand(req1, DataBase.connection);
            cmd.Parameters.AddWithValue("@numero_facture", numero_facture);
            OleDbDataReader dataReader = cmd.ExecuteReader();

            dataReader.Read();
            string fichierPath =  Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\recus\Recu_"+numero_facture.ToString()+".pdf"));
            
            GenererRecuPDF(Convert.ToInt32(dataReader[0]), dataReader[1].ToString(), dataReader[2].ToString(), dataReader[3].ToString(), DateTime.Now.ToString(),fichierPath);

            
            


            if (File.Exists(fichierPath))
            {
                
                axAcroPDF1.LoadFile(fichierPath);
                
                axAcroPDF1.setView("Fit");
                axAcroPDF1.setZoom(100); // optional
                axAcroPDF1.setShowToolbar(false); // hide toolbar
                axAcroPDF1.setShowScrollbars(true); // show scrollbars

                axAcroPDF1.Dock = DockStyle.Fill;
                this.Size = axAcroPDF1.Size;
                

            }
            else
            {
                MessageBox.Show("Fichier introuvable : " + fichierPath);
            }




            DataBase.connection.Close();

            
        }
    }
}
