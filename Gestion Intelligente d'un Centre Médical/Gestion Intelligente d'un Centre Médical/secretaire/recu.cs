using System;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;

namespace Gestion_Intelligente_d_un_Centre_Médical.secretaire
{
    public partial class recu : Form
    {
        public recu()
        {
            InitializeComponent();
        }

        public void GenererPdf()
        {
            //code pour générer le pdf
            string filePath = "example.pdf";

            using (PdfWriter writer = new PdfWriter(filePath))
            {
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);
                    document.Add(new Paragraph("Bonjour, ceci est un PDF généré en C#!"));
                    document.Close();


                }
            }
        }
    }
}
