using Devart.Data.PostgreSql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatabaseWizard.User_Controls.Database
{
    /// <summary>
    /// Interaction logic for PostgreSqlScreen.xaml
    /// </summary>
    public partial class PostgreSqlScreen : UserControl
    {
        public PostgreSqlScreen()
        {
            InitializeComponent();
            gthis = C.gthis();
        }
        MainWindow gthis;

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            

            try
            {
                string server = txtServer.Text;
                string userName = txtUserName.Text;
                string password = txtPassword.Password;
                string database = txtDatabase.Text;
                string port = txtPort.Text;
                string connectionString = "";
                if (port == "")
                {
                    port = "5432";
                }
                connectionString = "User ID="+ userName +";Password="+password+";Host="+server+";Port="+port+";Database="+database+";Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";

                //Data Source=username/password@//myserver:1521/my.service.com;
                //Data Source=username/password@myserver/myservice:dedicated/instancename;

                PgSqlConnection con = new PgSqlConnection(connectionString);

                
                PgSqlCommand cmd = new PgSqlCommand("SELECT * FROM pg_catalog.pg_tables where schemaname != 'pg_catalog' AND schemaname != 'information_schema'", con);
                PgSqlDataAdapter adp = new PgSqlDataAdapter(cmd);

                DataTable tables = new DataTable();
                adp.Fill(tables);

                C.postgresqlConnection = con;
                C.dt = new DataTable();
                C.dt = tables;

                gthis.postgresqlScreen.Visibility = Visibility.Hidden;
                gthis.chooseFields.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            gthis.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            gthis.firstScreen.Visibility = Visibility.Visible;
            gthis.postgresqlScreen.Visibility = Visibility.Hidden;
        }
    }
}
