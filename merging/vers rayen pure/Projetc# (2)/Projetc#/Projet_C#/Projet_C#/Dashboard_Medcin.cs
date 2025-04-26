using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel; // un alia qui represente la bibliotheque excel

namespace Projet_C_
{
    public partial class Dashboard_Medcin: Form
    {
        OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=\"C:\\Users\\User\\Desktop\\Projetc#\\BD_C#.accdb\";");
        DataSet ds = new DataSet();
        OleDbDataAdapter adapter = new OleDbDataAdapter();
        OleDbDataAdapter adapter2 = new OleDbDataAdapter();
        private int id_medcin = 0; // variable pour stocker l'id du medecin
        public Dashboard_Medcin(int id_med)
        {
            InitializeComponent();
            id_medcin = id_med; // initialiser l'id du medecin
        }

        private void Dashboard_Medcin_Load(object sender, EventArgs e)
        {
           
            string req = "select  Format(date_rendez_vous, 'mm' ) AS NumMois,Format(date_rendez_vous, 'mmmm') ,count(*)" +
                " from rendez_vous " +
                " where  id_medcin = 1 and  Year(date_rendez_vous) =  Year(Date()) and etat ='Terminer' " +
                "group by  Format(date_rendez_vous, 'mm' ),Format(date_rendez_vous,'mmmm') " +
                "order by    Format(date_rendez_vous, 'mm' ) ";
            adapter = new OleDbDataAdapter(req, connection);
            adapter.Fill(ds, "rendez_vous");

            req= "select Year(date_rendez_vous) as Annee, count(*) " +
                "from rendez_vous" +
                " where id_medcin = 1 and etat ='Terminer' " +
                " group by Year(date_rendez_vous)" +
                " order by Year(date_rendez_vous) desc";
            adapter2 = new OleDbDataAdapter(req, connection);
            adapter2.Fill(ds, "rendez_vousY");




        }

        private void button1_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp = new Excel.Application();
            xlApp.Visible = false;  // Laisser Excel invisible pendant l'exportation

            string shema = "C:\\Users\\User\\Desktop\\Projetc#\\Stat_medcin.xlsx";
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(shema); // ouvrir le fichier excel
            Excel.Worksheet xlWorksheet = xlWorkbook.Sheets[1]; // ouvrir la premiere feuille de calcul
            Excel.Worksheet xlWorksheet2 = xlWorkbook.Sheets[2]; // ouvrir la deuxieme feuille de calcul

            xlWorksheet.Cells.ClearContents(); // Effacer les anciennes données
            xlWorksheet.Cells[1, 1].Value = "Mois"; // Écrire dans la cellule A1
            xlWorksheet.Cells[1, 2].Value = "Nombre de rendez-vous"; // Écrire dans la cellule B1


            // Remplir les données à partir de la source
            for (int i = 0; i < ds.Tables["rendez_vous"].Rows.Count; i++)
            {
                
                xlWorksheet.Cells[i + 2, 1].Value = ds.Tables["rendez_vous"].Rows[i].ItemArray[1];
                xlWorksheet.Cells[i + 2, 2].Value = ds.Tables["rendez_vous"].Rows[i].ItemArray[2];
            }

            xlWorksheet2.Cells.ClearContents(); // Effacer les anciennes données
            xlWorksheet2.Cells[1, 1].Value = "Année"; // Écrire dans la cellule A1
            xlWorksheet2.Cells[1, 2].Value = "Nombre de rendez-vous"; // Écrire dans la cellule B1
            // Remplir les données à partir de la source
            for (int i = 0; i < ds.Tables["rendez_vousY"].Rows.Count; i++)
            {

                xlWorksheet2.Cells[i + 2, 1].Value = ds.Tables["rendez_vousY"].Rows[i].ItemArray[0];
                xlWorksheet2.Cells[i + 2, 2].Value = ds.Tables["rendez_vousY"].Rows[i].ItemArray[1];
            }
            // Message de confirmation
            MessageBox.Show("Les données ont été exportées avec succès vers Excel.", "Exportation réussie", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Sauvegarder et fermer le fichier
            xlWorkbook.Save();
            xlWorkbook.Close(false);

            // Quitter l'application Excel
            xlApp.Quit();

            // Libérer les objets COM de manière ordonnée
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorksheet2);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);

            // Libération de la mémoire
            GC.Collect();
            GC.WaitForPendingFinalizers();




        }

        private void button2_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp = new Excel.Application(); // ouvrir l app excel (creer une instance de l application excel)
            string shema = "C:\\Users\\User\\Desktop\\Projetc#\\Stat_medcin.xlsx";
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(shema); // ouvrir le fichier excel
            Excel.Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Worksheet xlWorksheet2 = xlWorkbook.Sheets[2];

            Excel.ChartObjects chartObjects = (Excel.ChartObjects)xlWorksheet.ChartObjects();
            Excel.ChartObjects chartObjects2 = (Excel.ChartObjects)xlWorksheet2.ChartObjects();
            Excel.ChartObject chartObject1 = chartObjects.Item(1);
            Excel.ChartObject chartObject2 = chartObjects2.Item(1);
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose(); // Libérer la ressource de l'image précédente
                pictureBox1.Image = null;    // Supprimer la référence à l'image précédente
            }
            string path = @"C:\Users\User\Desktop\Projetc#\source_img\rendezVousParMois.png";
            chartObject1.Chart.Export(path, "PNG", false);
            pictureBox1.Image = Image.FromFile(path);

            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.Dispose(); // Libérer la ressource de l'image précédente
                pictureBox2.Image = null;    // Supprimer la référence à l'image précédente
            }
            string path2 = @"C:\Users\User\Desktop\Projetc#\source_img\rendezVousParAnnee.png";
            chartObject2.Chart.Export(path2, "PNG", false);
            pictureBox2.Image = Image.FromFile(path2);
            // 5. Fermer le classeur et l'application Excel
            xlWorkbook.Close(false);
            xlApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(chartObject1);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(chartObjects);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(chartObject1);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(chartObjects);

        }
    }
}
