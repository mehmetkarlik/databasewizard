using System;
using System.Collections.Generic;
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

namespace DatabaseWizard.User_Controls
{
    /// <summary>
    /// Interaction logic for FirstScreen.xaml
    /// </summary>
    public partial class FirstScreen : UserControl
    {
        public FirstScreen()
        {

            InitializeComponent();

            lstDatabaseListing.Items.Add(new ListItemWithImage("/images/text.png", "Text File", "Supports various encodings and formats including CSV and fixed-width fields."));
            lstDatabaseListing.Items.Add(new ListItemWithImage("/images/access.png", "Microsoft Access", "Supports all versions of Access (includes *.mdb and *.accdb files)."));
            lstDatabaseListing.Items.Add(new ListItemWithImage("/images/excel.png", "Microsoft Excel", "Supports all versions od Excel (includes *.xls and *.xlsx files)."));
            lstDatabaseListing.Items.Add(new ListItemWithImage("/images/sqlserver.png", "Microsoft SQL Server", "Supports Microsoft SQL Server 2005 and later."));
            lstDatabaseListing.Items.Add(new ListItemWithImage("/images/oracle.png", "Oracle", "Supports Oracle 10g and later."));
            lstDatabaseListing.Items.Add(new ListItemWithImage("/images/mysql.png", "MySQL", "Supports MySQL 4.1 and later."));
            //lstDatabaseListing.Items.Add(new ListItemWithImage("/images/sap.png", "SAP IDoc", "Supports SAP Intermediate Document (IDoc) files."));
            //lstDatabaseListing.Items.Add(new ListItemWithImage("/images/ibm.png", "IBM DB2", "Supports IBM DB2 10.5.0 and later."));
            //lstDatabaseListing.Items.Add(new ListItemWithImage("/images/ibm.png", "IBM Informix", "Supports Informix 10.10 and later."));
            lstDatabaseListing.Items.Add(new ListItemWithImage("/images/postgresql.png", "PostgreSQL", "Supports PostgreSQL 9.0 and later."));
            lstDatabaseListing.Items.Add(new ListItemWithImage("/images/firebird.png", "Firebird", "Supports Firebird database."));
            lstDatabaseListing.Items.Add(new ListItemWithImage("/images/wsdl.png", "WSDL", "Supports Web Services."));
            //lstDatabaseListing.Items.Add(new ListItemWithImage("/images/oledb.png", "OLE DB Connection", "Connects to any database type with an installed OLE DB driver."));
            //lstDatabaseListing.Items.Add(new ListItemWithImage("/images/odbc.png", "ODBC Connection", "Connects to any database type with an installed ODBC driver."));

            gthis = C.gthis();
        }

        MainWindow gthis;

        private void lstDatabaseListing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnNext.IsEnabled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            gthis.Close();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            ListItemWithImage selected = (ListItemWithImage)lstDatabaseListing.SelectedItem;
            if (selected.Name == "Text File")
            {
                C.selectedDatabase = "Text File";
                gthis.firstScreen.Visibility = Visibility.Hidden;
                gthis.selectFile.Visibility = Visibility.Visible;
            }
            else if(selected.Name == "Microsoft Excel")
            {
                C.selectedDatabase = "Microsoft Excel";
                gthis.firstScreen.Visibility = Visibility.Hidden;
                gthis.selectFile.Visibility = Visibility.Visible;
            }
            else if (selected.Name == "Microsoft Access")
            {
                C.selectedDatabase = "Microsoft Access";
                gthis.firstScreen.Visibility = Visibility.Hidden;
                gthis.selectFile.Visibility = Visibility.Visible;
            }
            else if (selected.Name == "MySQL")
            {
                C.selectedDatabase = "MySQL";
                gthis.firstScreen.Visibility = Visibility.Hidden;
                gthis.mysqlScreen.Visibility = Visibility.Visible;
            }
            else if (selected.Name == "Microsoft SQL Server")
            {
                C.selectedDatabase = "Microsoft SQL Server";
                gthis.firstScreen.Visibility = Visibility.Hidden;
                gthis.sqlserverScreen.Visibility = Visibility.Visible;
            }
            else if (selected.Name == "Oracle")
            {
                C.selectedDatabase = "Oracle";
                gthis.firstScreen.Visibility = Visibility.Hidden;
                gthis.oracleScreen.Visibility = Visibility.Visible;
            }
            else if (selected.Name == "PostgreSQL")
            {
                C.selectedDatabase = "PostgreSQL";
                gthis.firstScreen.Visibility = Visibility.Hidden;
                gthis.postgresqlScreen.Visibility = Visibility.Visible;
            }
            else if (selected.Name == "Firebird")
            {
                C.selectedDatabase = "Firebird";
                gthis.firstScreen.Visibility = Visibility.Hidden;
                gthis.firebirdScreen.Visibility = Visibility.Visible;
            }
            else if (selected.Name == "WSDL")
            {
                C.selectedDatabase = "WSDL";
                gthis.firstScreen.Visibility = Visibility.Hidden;
                gthis.wsdlScreen.Visibility = Visibility.Visible;
            }
        }

        public int MyProperty { get; set; }
    }

    public class ListItemWithImage
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        //public string TableName { get; set; }
        //public string FieldName { get; set; }

        public ListItemWithImage(string path, string name, string description)
        {
            this.Name = name;
            this.Path = path;
            this.Description = description;
        }
        //public ListItemWithImage(string path, string name, string description, string tableName, string fieldName)
        //{
        //    this.Name = name;
        //    this.Path = path;
        //    this.Description = description;
        //    this.TableName = tableName;
        //    this.FieldName = fieldName;
        //}
    }
}
