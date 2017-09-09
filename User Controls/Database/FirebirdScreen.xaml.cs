using FirebirdSql.Data.FirebirdClient;
using Microsoft.Win32;
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
    /// Interaction logic for FirebirdScreen.xaml
    /// </summary>
    public partial class FirebirdScreen : UserControl
    {
        public FirebirdScreen()
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
                    port = "3050";
                }
                connectionString = "User = "+ userName+"; Password = "+password+"; Database = "+ database + "; DataSource = "+server+"; Port = "+port+"; Dialect = 3; Charset = UTF8; Role =; Connection lifetime = 15; Pooling = true; MinPoolSize = 0; MaxPoolSize = 50; Packet Size = 8192; ServerType = 0;";


                FbConnection con = new FbConnection(connectionString);


                FbCommand cmd = new FbCommand("SELECT a.RDB$RELATION_NAME FROM RDB$RELATIONS a WHERE RDB$SYSTEM_FLAG = 0 AND RDB$RELATION_TYPE = 0", con);
                FbDataAdapter adp = new FbDataAdapter(cmd);

                DataTable tables = new DataTable();
                adp.Fill(tables);

                C.firebirdConnection = con;
                C.dt = new DataTable();
                C.dt = tables;

                gthis.firebirdScreen.Visibility = Visibility.Hidden;
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
            gthis.firebirdScreen.Visibility = Visibility.Hidden;
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Firebird Database Files |*.fdb;*.gdb";
            file.ShowDialog();
            if (file.FileName != null && file.FileName != "")
            {
                txtDatabase.Text = file.FileName;
            }
        }
    }
}
