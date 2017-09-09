using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for SelectTextFile.xaml
    /// </summary>
    public partial class SelectFile : UserControl
    {
        public SelectFile()
        {
            InitializeComponent();

            gthis = C.gthis();
        }
        MainWindow gthis;
        string fileName = "";
        string safeFileName = "";
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            gthis.Close();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (fileName != "")
            {
                C.fileName = fileName;
                C.safeFileName = safeFileName;
            }
            else
            {
                fileName = C.fileName;
                safeFileName = C.safeFileName;
            }
            
            
            gthis.selectFile.Visibility = Visibility.Hidden;

            if (C.selectedDatabase == "Text File")
            {
                gthis.fileFormat.Visibility = Visibility.Visible;
            }
            else
            {
                gthis.chooseFields.Visibility = Visibility.Visible;
            }
            
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            gthis.firstScreen.Visibility = Visibility.Visible;
            gthis.selectFile.Visibility = Visibility.Hidden;
        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            if (C.selectedDatabase == "Text File")
            {
                try
                {
                    OpenFileDialog file = new OpenFileDialog();
                    file.Filter = "CSV Files (*.csv) and TXT Files (*.txt)  |*.csv; *.txt";
                    file.ShowDialog();
                    fileName = file.FileName;
                    safeFileName = file.SafeFileName;
                    lblFilePreview.Text = File.ReadAllText(fileName);
                    btnNext.IsEnabled = true;
                    txtFileName.Text = fileName;
                    C.enterChooseFields = false;
                    C.enterFileFormat = false;
                }
                catch (Exception)
                {
                    MessageBox.Show("You didn't choose any file.");
                }
            }
            else if(C.selectedDatabase == "Microsoft Excel")
            {
                try
                {
                    
                    OpenFileDialog file = new OpenFileDialog();
                    file.Filter = "Excel Files (*.xls, *.xlsx) |*.xls; *.xlsx";
                    file.ShowDialog();
                    fileName = file.FileName;
                    safeFileName = file.SafeFileName;
                    btnNext.IsEnabled = true;
                    txtFileName.Text = fileName;
                    C.enterChooseFields = false;
                    C.enterFileFormat = false;
                }
                catch (Exception)
                {
                    MessageBox.Show("You didn't choose any file.");
                }
            }
            else if (C.selectedDatabase == "Microsoft Access")
            {
                try
                {




                    OpenFileDialog file = new OpenFileDialog();
                    file.Filter = "Access Files (*.mdb), (*.accdb) |*.mdb; *.accdb";
                    file.ShowDialog();
                    fileName = file.FileName;
                    safeFileName = file.SafeFileName;
                    lblFilePreview.Text = File.ReadAllText(fileName);
                    btnNext.IsEnabled = true;
                    txtFileName.Text = fileName;
                    C.enterChooseFields = false;
                    C.enterFileFormat = false;
                }
                catch (Exception)
                {
                    MessageBox.Show("You didn't choose any file.");
                }
            }
            
            
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (C.selectedDatabase == "Microsoft Excel")
            {
                groupBox.Visibility = Visibility.Hidden;
                lblTextFile.Content = "Excel File";
            }
            else if (C.selectedDatabase == "Microsoft Access")
            {
                groupBox.Visibility = Visibility.Hidden;
                lblTextFile.Content = "Access File";
            }
        }
    }
}
