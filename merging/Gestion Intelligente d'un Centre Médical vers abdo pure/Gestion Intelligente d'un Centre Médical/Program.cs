using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gestion_Intelligente_d_un_Centre_Médical.secretaire;

namespace Gestion_Intelligente_d_un_Centre_Médical
{
    internal static class Program
    {

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]



        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new acceuil_secretaire());
        }
    }
    public class update
    {
        


        private static void mise()
        {
            if (gestion_rendez_vous.ActiveForm.Visible)
            {
                update_rendezvous();
            }
            
        }
        private static void update_rendezvous()
        {
            DataBase.connection.Open();

            string req = @"UPDATE rendez_vous SET etat='encours' WHERE [date]=Date() AND début>Time();";
            OleDbCommand cmd = new OleDbCommand(req, DataBase.connection);
            cmd.ExecuteNonQuery();


            req = @"UPDATE rendez_vous SET etat='terminé' WHERE [date]<Date();";
            cmd = new OleDbCommand(req, DataBase.connection);
            cmd.ExecuteNonQuery();

            req = @"UPDATE rendez_vous SET etat='terminé' WHERE [date]=Date() AND fin<Time();";

            DataBase.connection.Close();
        }
        private static void update_patient()
        {
            DataBase.connection.Open();
            string req = @"UPDATE patient SET etat='encours' WHERE [date]=Date() AND début>Time();";
            OleDbCommand cmd = new OleDbCommand(req, DataBase.connection);
            cmd.ExecuteNonQuery();

        }
        private static void update_medecin()
        {
            DataBase.connection.Open();
            string req = @"UPDATE medecin SET etat='encours' WHERE [date]=Date() AND début>Time();";
            OleDbCommand cmd = new OleDbCommand(req, DataBase.connection);
            cmd.ExecuteNonQuery();
        }

    }
}