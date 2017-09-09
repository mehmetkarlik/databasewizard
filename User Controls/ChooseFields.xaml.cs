using DatabaseWizard.Warnings;
using Devart.Data.PostgreSql;
using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

namespace DatabaseWizard.User_Controls
{
    /// <summary>
    /// Interaction logic for ChooseFields.xaml
    /// </summary>
    public partial class ChooseFields : UserControl
    {
        public ChooseFields()
        {
            InitializeComponent();
            gthis = C.gthis();
        }
        MainWindow gthis;
        string fileName = "";
        DataTable dt;
        OleDbConnection objConn = null;
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            gthis.Close();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt2 = new DataTable();

            for (int i = 0; i < lstSelectedFields.Items.Count; i++)
            {
                string columnName = ((ListItemWithImage)lstSelectedFields.Items[i]).Name;
                DataColumn dc = new DataColumn(columnName);
                dt2.Columns.Add(dc);
            }


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt2.NewRow();
                for (int j = 0; j < dt2.Columns.Count; j++)
                {
                    string columnName = dt2.Columns[j].ColumnName;
                    dr[columnName] = dt.Rows[i][columnName];
                }
                dt2.Rows.Add(dr);
            }

            C.selectedFieldsDataTable = dt2;

            gthis.chooseFields.Visibility = Visibility.Hidden;
            gthis.dataSettings.Visibility = Visibility.Visible;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (C.selectedDatabase == "Text File")
            {
                gthis.fileFormat.Visibility = Visibility.Visible;
            }
            else if (C.selectedDatabase == "Microsoft Excel" || C.selectedDatabase == "Microsoft Access")
            {
                gthis.selectFile.Visibility = Visibility.Visible;
            }
            else
            {
                gthis.firstScreen.Visibility = Visibility.Visible;
            }
            gthis.chooseFields.Visibility = Visibility.Hidden;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (((UserControl)sender).Visibility == Visibility.Visible)
            {
                if (C.selectedDatabase == "Text File" && C.enterChooseFields == false)
                {
                    dt = C.dt;
                    cmbTables.Items.Clear();
                    cmbTables.Items.Add(C.safeFileName);
                    cmbTables.SelectedIndex = 0;
                    lstSelectedFields.Items.Clear();
                    lstAvailableFields.Items.Clear();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        lstAvailableFields.Items.Add(new ListItemWithImage("/images/table.png", dt.Columns[i].Caption, " " + "(" + C.safeFileName + ">" + dt.Columns[i].Caption + ")"));
                    }
                }
                else if (C.selectedDatabase == "Microsoft Excel" && C.enterChooseFields == false)
                {
                    List<string> pages = GetExcelOrAccessTablesNames(C.fileName, true);
                    if (pages == null)
                    {
                        gthis.Close();
                    }
                    cmbTables.Items.Clear();
                    for (int i = 0; i < pages.Count; i++)
                    {
                        cmbTables.Items.Add(pages[i]);
                    }
                    cmbTables.SelectedIndex = 0;

                    cmbTables.IsEnabled = true;

                    dt = ImportExcelorAccesstoDatatable(C.fileName, cmbTables.SelectedItem.ToString(), true);

                    lstSelectedFields.Items.Clear();
                    lstAvailableFields.Items.Clear();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        lstAvailableFields.Items.Add(new ListItemWithImage("/images/table.png", dt.Columns[i].Caption, " " + "(" + C.safeFileName + ">" + dt.Columns[i].Caption + ")"));
                    }

                }
                else if (C.selectedDatabase == "Microsoft Access" && C.enterChooseFields == false)
                {
                    List<string> pages = GetExcelOrAccessTablesNames(C.fileName, false);
                    if (pages == null)
                    {
                        gthis.Close();
                    }
                    cmbTables.Items.Clear();
                    for (int i = 0; i < pages.Count; i++)
                    {
                        cmbTables.Items.Add(pages[i]);
                    }
                    cmbTables.SelectedIndex = 0;

                    cmbTables.IsEnabled = true;

                    dt = ImportExcelorAccesstoDatatable(C.fileName, cmbTables.SelectedItem.ToString(), false);

                    lstSelectedFields.Items.Clear();
                    lstAvailableFields.Items.Clear();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        lstAvailableFields.Items.Add(new ListItemWithImage("/images/table.png", dt.Columns[i].Caption, " " + "(" + C.safeFileName + ">" + dt.Columns[i].Caption + ")"));
                    }
                }
                else if (C.selectedDatabase == "MySQL")
                {
                    dt = C.dt;
                    try
                    {
                        cmbTables.Items.Clear();
                    }
                    catch (Exception ex)
                    {
                    }
                    
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmbTables.Items.Add(dt.Rows[i]["TABLE_NAME"]);
                    }
                    cmbTables.SelectedIndex = 0;

                    cmbTables.IsEnabled = true;

                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM "+ cmbTables.SelectedItem.ToString(), C.mysqlConnection);

                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    adp.Fill(dt);
                    lstSelectedFields.Items.Clear();
                    lstAvailableFields.Items.Clear();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        lstAvailableFields.Items.Add(new ListItemWithImage("/images/table.png", dt.Columns[i].Caption, ""));
                    }

                }
                else if (C.selectedDatabase == "Microsoft SQL Server")
                {
                    dt = C.dt;
                    try
                    {
                        cmbTables.Items.Clear();
                    }
                    catch (Exception ex)
                    {
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmbTables.Items.Add(dt.Rows[i]["TABLE_NAME"]);
                    }
                    cmbTables.SelectedIndex = 0;

                    cmbTables.IsEnabled = true;

                    SqlCommand cmd = new SqlCommand("SELECT * FROM " + cmbTables.SelectedItem.ToString(), C.sqlserverConnection);

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adp.Fill(dt);
                    lstSelectedFields.Items.Clear();
                    lstAvailableFields.Items.Clear();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        lstAvailableFields.Items.Add(new ListItemWithImage("/images/table.png", dt.Columns[i].Caption, ""));
                    }

                }
                else if (C.selectedDatabase == "Oracle")
                {
                    dt = C.dt;
                    try
                    {
                        cmbTables.Items.Clear();
                    }
                    catch (Exception ex)
                    {
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmbTables.Items.Add(dt.Rows[i]["TABLE_NAME"]);
                    }
                    cmbTables.SelectedIndex = 0;

                    cmbTables.IsEnabled = true;

                    OracleCommand cmd = new OracleCommand("SELECT * FROM " + cmbTables.SelectedItem.ToString(), C.oracleConnection);

                    OracleDataAdapter adp = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    adp.Fill(dt);
                    lstSelectedFields.Items.Clear();
                    lstAvailableFields.Items.Clear();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        lstAvailableFields.Items.Add(new ListItemWithImage("/images/table.png", dt.Columns[i].Caption, ""));
                    }

                }
                else if (C.selectedDatabase == "PostgreSQL")
                {
                    dt = C.dt;
                    try
                    {
                        cmbTables.Items.Clear();
                    }
                    catch (Exception ex)
                    {
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmbTables.Items.Add(dt.Rows[i]["tablename"]);
                    }
                    cmbTables.SelectedIndex = 0;

                    cmbTables.IsEnabled = true;

                    //SELECT * FROM pg_catalog.pg_tables where schemaname != 'pg_catalog' AND schemaname != 'information_schema'
                    PgSqlCommand cmd = new PgSqlCommand("SELECT * FROM " + cmbTables.SelectedItem.ToString(), C.postgresqlConnection);

                    PgSqlDataAdapter adp = new PgSqlDataAdapter(cmd);
                    dt = new DataTable();
                    adp.Fill(dt);
                    lstSelectedFields.Items.Clear();
                    lstAvailableFields.Items.Clear();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        lstAvailableFields.Items.Add(new ListItemWithImage("/images/table.png", dt.Columns[i].Caption, ""));
                    }

                }
                else if (C.selectedDatabase == "Firebird")
                {
                    dt = C.dt;
                    try
                    {
                        cmbTables.Items.Clear();
                    }
                    catch (Exception ex)
                    {
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmbTables.Items.Add(dt.Rows[i]["RDB$RELATION_NAME"]);
                    }
                    cmbTables.SelectedIndex = 0;

                    cmbTables.IsEnabled = true;

                    FbCommand cmd = new FbCommand("SELECT * FROM " + cmbTables.SelectedItem.ToString(), C.firebirdConnection);

                    FbDataAdapter adp = new FbDataAdapter(cmd);
                    dt = new DataTable();
                    adp.Fill(dt);
                    lstSelectedFields.Items.Clear();
                    lstAvailableFields.Items.Clear();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        lstAvailableFields.Items.Add(new ListItemWithImage("/images/table.png", dt.Columns[i].Caption, ""));
                    }

                }
                else if (C.selectedDatabase == "WSDL")
                {
                    dt = C.dt;
                    try
                    {
                        cmbTables.Items.Clear();
                    }
                    catch (Exception ex)
                    {
                    }
                    
                    lstSelectedFields.Items.Clear();
                    lstAvailableFields.Items.Clear();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        lstAvailableFields.Items.Add(new ListItemWithImage("/images/table.png", dt.Columns[i].Caption, ""));
                    }

                }

                C.enterChooseFields = true;
                gthis.chooseFields.btnAdd.IsEnabled = true;
                gthis.chooseFields.btnAddAll.IsEnabled = true;
                gthis.chooseFields.btnRemove.IsEnabled = false;
                gthis.chooseFields.btnRemoveAll.IsEnabled = false;
                gthis.chooseFields.btnNext.IsEnabled = false;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (lstAvailableFields.SelectedIndex >= 0)
            {
                btnNext.IsEnabled = true;
                lstSelectedFields.Items.Add(lstAvailableFields.SelectedItem);
                lstAvailableFields.Items.Remove(lstAvailableFields.SelectedItem);

                btnRemove.IsEnabled = true;
                btnRemoveAll.IsEnabled = true;

                if (lstAvailableFields.Items.Count == 0)
                {
                    btnAdd.IsEnabled = false;
                    btnAddAll.IsEnabled = false;
                }
            }

        }
        private void btnAddAll_Click(object sender, RoutedEventArgs e)
        {
            int count = lstAvailableFields.Items.Count;

            if (count > 0)
            {
                btnNext.IsEnabled = true;
                for (int i = 0; i < count; i++)
                {
                    lstSelectedFields.Items.Add(lstAvailableFields.Items[0]);
                    lstAvailableFields.Items.Remove(lstAvailableFields.Items[0]);
                }

                btnRemove.IsEnabled = true;
                btnRemoveAll.IsEnabled = true;

                btnAdd.IsEnabled = false;
                btnAddAll.IsEnabled = false;
            }


        }
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lstSelectedFields.SelectedIndex >= 0)
            {
                lstAvailableFields.Items.Add(lstSelectedFields.SelectedItem);
                lstSelectedFields.Items.Remove(lstSelectedFields.SelectedItem);

                btnAdd.IsEnabled = true;
                btnAddAll.IsEnabled = true;

                if (lstSelectedFields.Items.Count == 0)
                {
                    btnRemove.IsEnabled = false;
                    btnRemoveAll.IsEnabled = false;
                    btnNext.IsEnabled = false;
                }
            }
        }
        private void btnRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            int count = lstSelectedFields.Items.Count;
            btnNext.IsEnabled = false;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    lstAvailableFields.Items.Add(lstSelectedFields.Items[0]);
                    lstSelectedFields.Items.Remove(lstSelectedFields.Items[0]);
                }

                btnAdd.IsEnabled = true;
                btnAddAll.IsEnabled = true;

                btnRemove.IsEnabled = false;
                btnRemoveAll.IsEnabled = false;
            }


        }
        private DataTable ImportExcelorAccesstoDatatable(string filepath, string sheetName, bool isExcelOrAccess)
        {

            dt = null;
            string sqlquery = "Select * From [" + sheetName + "]";
            DataSet ds = new DataSet();

            OleDbDataAdapter da = new OleDbDataAdapter(sqlquery, objConn);
            da.Fill(ds);
            dt = ds.Tables[0];
            return dt;
        }
        private List<string> GetExcelOrAccessTablesNames(string fileName, bool isExcelOrAccess)
        {
            string excel = "Excel 12.0 Xml";
            string access = "";
            dt = null;

            try
            {

                // Connection String. Change the excel file to the file you
                // will search.

                String connString = "";

                if (isExcelOrAccess)
                {
                    connString = @"Provider= Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + "; Extended Properties='" + excel + ";HDR=YES'";
                }
                else
                {
                    connString = @"Provider= Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Jet OLEDB:Database Password = " + access;
                }

                // Create connection object by using the preceding connection string.
                objConn = new OleDbConnection(connString);
                // Open connection with the database.
                objConn.Open();
                // Get the data table containg the schema guid.
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                List<string> excelSheets = new List<string>();
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    if (!row["TABLE_NAME"].ToString().Contains("MSys"))
                    {
                        excelSheets.Add(row["TABLE_NAME"].ToString());
                    }
                    i++;
                }


                return excelSheets;
            }
            catch (Exception ex)
            {
                OfficeAddOnWarning off = new OfficeAddOnWarning();
                off.ShowDialog();
                gthis.Close();
                return null;
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        private void cmbTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((C.selectedDatabase == "Microsoft Excel" || C.selectedDatabase == "Microsoft Access") && C.enterChooseFields == true)
            {
                dt = ImportExcelorAccesstoDatatable(C.fileName, cmbTables.SelectedItem.ToString(), true);
            }
            else if (C.selectedDatabase == "MySQL")
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM " + cmbTables.SelectedItem.ToString(), C.mysqlConnection);

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);
                
            }
            else if (C.selectedDatabase == "Microsoft SQL Server")
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM " + cmbTables.SelectedItem.ToString(), C.sqlserverConnection);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);

            }
            else if (C.selectedDatabase == "Oracle")
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM " + cmbTables.SelectedItem.ToString(), C.oracleConnection);

                OracleDataAdapter adp = new OracleDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);

            }
            else if (C.selectedDatabase == "PostgreSQL")
            {
                PgSqlCommand cmd = new PgSqlCommand("SELECT * FROM " + cmbTables.SelectedItem.ToString(), C.postgresqlConnection);

                PgSqlDataAdapter adp = new PgSqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);

            }
            else if (C.selectedDatabase == "Firebird")
            {
                FbCommand cmd = new FbCommand("SELECT * FROM " + cmbTables.SelectedItem.ToString(), C.firebirdConnection);

                FbDataAdapter adp = new FbDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);

            }

            lstSelectedFields.Items.Clear();
            lstAvailableFields.Items.Clear();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                lstAvailableFields.Items.Add(new ListItemWithImage("/images/table.png", dt.Columns[i].Caption, ""));
            }

            gthis.chooseFields.btnAdd.IsEnabled = true;
            gthis.chooseFields.btnAddAll.IsEnabled = true;
            gthis.chooseFields.btnRemove.IsEnabled = false;
            gthis.chooseFields.btnRemoveAll.IsEnabled = false;
            gthis.chooseFields.btnNext.IsEnabled = false;


        }
    }


}
