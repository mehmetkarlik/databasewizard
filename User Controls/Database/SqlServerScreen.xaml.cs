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

namespace DatabaseWizard.User_Controls.Database
{
    /// <summary>
    /// Interaction logic for SqlServerScreen.xaml
    /// </summary>
    public partial class SqlServerScreen : UserControl
    {
        public SqlServerScreen()
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
                string connectionString = "";
                if (rdbWinAut.IsChecked == true)
                {
                    connectionString = "Server=" + server +";Database=" + database + "; Trusted_Connection = True;";
                }
                else
                {
                    connectionString = "Server=" + server + ";Database=" + database + ";User Id=" + userName + ";Password =" + password + "; ";
                }

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = '" + database + "';", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable tables = new DataTable();
                adp.Fill(tables);

                C.sqlserverConnection = con;
                C.dt = new DataTable();
                C.dt = tables;

                gthis.sqlserverScreen.Visibility = Visibility.Hidden;
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
            gthis.sqlserverScreen.Visibility = Visibility.Hidden;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void rdbWinAut_Checked(object sender, RoutedEventArgs e)
        {
            txtUserName.IsEnabled = false;
            txtPassword.IsEnabled = false;

        }

        private void rdbWinAut_Unchecked(object sender, RoutedEventArgs e)
        {
            txtUserName.IsEnabled = true;
            txtPassword.IsEnabled = true;
        }
    }
}
