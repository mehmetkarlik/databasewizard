using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Oracle.ManagedDataAccess.Client;

namespace DatabaseWizard.User_Controls.Database
{
    /// <summary>
    /// Interaction logic for SqlServerScreen.xaml
    /// </summary>
    public partial class OracleScreen : UserControl
    {
        public OracleScreen()
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
                string service = txtService.Text;
                string port = txtPort.Text;
                string connectionString = "";
                if (port == "")
                {
                    port = "1521";
                }
                connectionString = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST="+server+")(PORT="+port+")))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME="+service+")));User Id="+userName+";Password="+password+";";


                //Data Source=username/password@//myserver:1521/my.service.com;
                //Data Source=username/password@myserver/myservice:dedicated/instancename;

                OracleConnection con = new OracleConnection(connectionString);
                OracleCommand cmd = new OracleCommand("select table_name from user_tables", con);
                OracleDataAdapter adp = new OracleDataAdapter(cmd);

                DataTable tables = new DataTable();
                adp.Fill(tables);

                C.oracleConnection = con;
                C.dt = new DataTable();
                C.dt = tables;

                gthis.oracleScreen.Visibility = Visibility.Hidden;
                gthis.chooseFields.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                    MessageBox.Show(ex.Message);
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            gthis.firstScreen.Visibility = Visibility.Visible;
            gthis.oracleScreen.Visibility = Visibility.Hidden;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            gthis.Close();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

       
    }
}
