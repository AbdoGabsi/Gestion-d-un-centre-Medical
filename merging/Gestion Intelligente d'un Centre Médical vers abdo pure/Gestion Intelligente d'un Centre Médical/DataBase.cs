using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;
using System.IO;

namespace Gestion_Intelligente_d_un_Centre_Médical
{
  
    public static class DataBase
    {
        public static OleDbConnection connection { get; private set; }

        private static string src = Path.GetFullPath(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\BD_Centre-Médical.accdb"));

        private static string connectionString = $@"Provider=Microsoft.ACE.OLEDB.16.0;Data Source={src};";

        static DataBase()
        {
            connection = new OleDbConnection(connectionString);
        }
    }

}
