using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Interaction logic for FileFormat.xaml
    /// </summary>
    public partial class FileFormat : UserControl
    {
        public FileFormat()
        {
            InitializeComponent();

            gthis = C.gthis();
            dt = new DataTable();

        }
        MainWindow gthis;
        string fileName = "";
        string[] text;
        DataTable dt;
        bool sonKolonEklendimi = false;


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            gthis.Close();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            C.dt = dt;
            gthis.fileFormat.Visibility = Visibility.Hidden;
            gthis.chooseFields.Visibility = Visibility.Visible;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            gthis.selectFile.Visibility = Visibility.Visible;
            gthis.fileFormat.Visibility = Visibility.Hidden;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (((UserControl)sender).Visibility == Visibility.Visible)
            {

                if (C.selectedDatabase == "Text File" && C.enterFileFormat == false)
                {
                    fileName = C.fileName;
                    text = File.ReadAllLines(fileName);
                    bool tab = false, comma = false, semicolon = false;
                    char seperator = '\0';


                    //Ayraçı bul
                    #region Find Seperator

                    for (int i = 0; i < text.Length; i++)
                    {
                        if (text[i].Contains('\t'))
                        {
                            tab = true;
                            break;
                        }
                        else if (text[i].Contains(','))
                        {
                            comma = true;
                            break;
                        }
                        else if (text[i].Contains(';'))
                        {
                            semicolon = true;
                            break;
                        }
                    }

                    if (tab)
                    {
                        cmbSeperator.SelectedIndex = 0;
                        seperator = '\t';


                    }
                    else if (comma)
                    {
                        cmbSeperator.SelectedIndex = 1;
                        seperator = ',';
                    }
                    else if (semicolon)
                    {
                        cmbSeperator.SelectedIndex = 2;
                        seperator = ';';
                    }
                    else
                    {
                        cmbSeperator.SelectedIndex = 3;
                        lblSeperator.IsEnabled = true;
                        txtSeperator.IsEnabled = true;
                    }

                    #endregion


                    createTable(seperator, text, ref dt);
                    fillDataGrid(dt);
                    C.enterFileFormat = true;

                }

            }




        }

        private void createTable(char seperator, string[] text,ref DataTable dt)
        {
            int maxColumnCountInLine = 0;

            //Kolon Sayısını Hesapla
            #region Find Number of Column
            if (seperator != '\0')
            {
                for (int i = 0; i < text.Length; i++)
                {
                    string[] line = text[i].Split(seperator);
                    if (maxColumnCountInLine < line.Length)
                    {
                        maxColumnCountInLine = line.Length;
                    }
                }

                dt = new DataTable();
                for (int i = 0; i < maxColumnCountInLine; i++)
                {
                    DataColumn dr = new DataColumn("Field" + (i + 1).ToString());
                    dt.Columns.Add(dr);
                }
            }
            #endregion


            //DataTable'ı Doldur
            #region Fill DataTable
            for (int i = 0; i < text.Length; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < maxColumnCountInLine; j++)
                {
                    try
                    {
                        dr[j] = text[i].Split(seperator)[j].ToString();
                    }
                    catch (Exception)
                    {

                        dr[j] = "";
                    }

                }
                dt.Rows.Add(dr);
            }
            #endregion
            

        }

        private void cmbSeperator_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            C.enterChooseFields = false;
            if (cmbSeperator.SelectedIndex == 3)
            {
                lblSeperator.IsEnabled = true;
                txtSeperator.IsEnabled = true;
            }
            else
            {
                try
                {
                    lblSeperator.IsEnabled = false;
                    txtSeperator.IsEnabled = false;
                    txtSeperator.Text = "";
                }
                catch (Exception ex)
                {
                }
            }

            try
            {
                if (cmbSeperator.SelectedIndex == 0)
                {
                    createTable('\t', text, ref dt);
                    fillDataGrid(dt);
                    
                    btnNext.IsEnabled = true;
                }
                else if (cmbSeperator.SelectedIndex == 1)
                {
                    createTable(',', text, ref dt);
                    fillDataGrid(dt);
                    btnNext.IsEnabled = true;
                }
                else if (cmbSeperator.SelectedIndex == 2)
                {
                    createTable(';', text, ref dt);
                    fillDataGrid(dt);
                    btnNext.IsEnabled = true;
                }
                else if (cmbSeperator.SelectedIndex == 3)
                {
                    if (txtSeperator.Text != "")
                    {
                        createTable(txtSeperator.Text[0], text, ref dt);
                        fillDataGrid(dt);
                        btnNext.IsEnabled = true;
                    }
                    
                }
            }
            catch (Exception ex)
            {
            }
           
        }

        private void txtSeperator_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSeperator.Text != "")
            {
                createTable(txtSeperator.Text[0], text, ref dt);
                fillDataGrid(dt);
                btnNext.IsEnabled = true;
            }
            
        }

        private void fillDataGrid(DataTable dt)
        {
            dataGrid.ItemsSource = dt.DefaultView;
            if (sonKolonEklendimi == false)
            {
                DataGridTextColumn textColumn = new DataGridTextColumn();
                textColumn.Header = "";
                textColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                dataGrid.Columns.Add(textColumn);
                sonKolonEklendimi = true;
            }
            
        }

        private void dataGrid_AutoGeneratedColumns(object sender, EventArgs e)
        {
            var grid = (DataGrid)sender;
            foreach (var item in grid.Columns)
            {
                if (item.Header.ToString() == "")
                {
                    item.DisplayIndex = grid.Columns.Count - 1;
                    break;
                }
            }
        }
    }
}
