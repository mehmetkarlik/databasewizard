using MySql.Data.MySqlClient;
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
    /// Interaction logic for MySQLScreen.xaml
    /// </summary>
    public partial class MySQLScreen : UserControl
    {
        public MySQLScreen()
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
                string port = txtPort.Text;
                string database = txtDatabase.Text;
                string connectionString = "";

                
                connectionString = "Server=" + server + ";Port=" + port + ";Database=" + database + ";Uid=" + userName + ";Pwd = " + password + "; ";
                

                MySqlConnection con = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand("select * from information_schema.tables WHERE table_schema = '" + database + "';", con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

                DataTable tables = new DataTable();
                adp.Fill(tables);

                C.mysqlConnection = con;
                C.dt = new DataTable();
                C.dt = tables;

                gthis.mysqlScreen.Visibility = Visibility.Hidden;
                gthis.chooseFields.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unable to connect to any of the specified MySQL hosts."))
                {
                    MessageBox.Show("Server name or Port number is written wrong or MySQL Service was not started.");
                }
                else if (ex.Message.Contains("Access denied for user"))
                {
                    MessageBox.Show("User name or Password is written wrong.");
                }
                else if (ex.Message.Contains("Unknown database"))
                {
                    MessageBox.Show("Unknown database. Mysql doesnt have this name database.");
                }
                else if(ex.Message.Contains("Object reference not set to an instance of an object."))
                {
                    MessageBox.Show("Operation is terminated, please control fields.");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            gthis.firstScreen.Visibility = Visibility.Visible;
            gthis.mysqlScreen.Visibility = Visibility.Hidden;
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
