using Devart.Data.PostgreSql;
using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;

namespace DatabaseWizard
{
    public static class C
    {
        public static MainWindow gthis()
        {
            MainWindow m = null;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    m = (window as MainWindow);
                }
            }
            return m;
        }


        public static int copyCount;
        public static string fileName;
        public static string safeFileName;
        public static string selectedDatabase;
        public static DataTable dt;
        public static bool enterChooseFields = false;
        public static bool enterFileFormat = false;
        public static DataTable selectedFieldsDataTable;
        internal static DataTable modifiedDataTable;
        internal static bool isResultScreen = false;
        public static MySqlConnection mysqlConnection;
        public static SqlConnection sqlserverConnection;
        public static OracleConnection oracleConnection;
        public static PgSqlConnection postgresqlConnection;
        public static FbConnection firebirdConnection;
    }
}
