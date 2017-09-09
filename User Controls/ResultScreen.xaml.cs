using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Dynamic;
using System.Collections;
using System.Windows.Controls.Primitives;

namespace DatabaseWizard.User_Controls
{
    public partial class ResultScreen : UserControl
    {
        public ResultScreen()
        {
            InitializeComponent();
            dt = new DataTable();
            dt2 = new DataTable();
            gthis = C.gthis();
        }
        MainWindow gthis;
        DataTable dt;
        DataTable dt2;
        Grid grdResult;
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UserControl kontrol = (UserControl)sender;

            if (kontrol.Visibility == Visibility.Visible)
            {
                #region DataTable Yükleme

                dt = C.modifiedDataTable;
                dt2 = new DataTable();
                grdResult = new Grid();

                grdResult.HorizontalAlignment = HorizontalAlignment.Left;
                grdResult.VerticalAlignment = VerticalAlignment.Top;
                grdResult.Width = 570;
                grdResult.Height = double.NaN;
                grdResult.MouseDown += grdResult_MouseDown;
                BrushConverter bc = new BrushConverter();
                grdResult.Background = (Brush)bc.ConvertFrom("#00000000");

                scrlGrdResult.Content = grdResult;
                int copyCount = C.copyCount;



                //dt = new DataTable();
                //dt.Columns.Add("Field 1");
                //dt.Columns.Add("Field 2");
                //dt.Columns.Add("Field 3");

                //for (int i = 0; i < 5; i++)
                //{
                //    DataRow dr = dt.NewRow();
                //    dr[0] = "Merhaba" + i.ToString();
                //    dr[1] = "Nasılsın" + i.ToString();
                //    dr[2] = "İyimisin" + i.ToString();
                //    dt.Rows.Add(dr);
                //}

                dt2.Columns.Add("pk", typeof(int));
                dt2.Columns.Add("IsSelected", typeof(bool));
                dt2.Columns.Add("Copies", typeof(int));

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt2.Columns.Add(dt.Columns[i].ColumnName, typeof(string));
                }

                //We import data from dt to dt2
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    object[] values = new object[3 + dt.Columns.Count];

                    values[0] = i;
                    values[1] = true;
                    values[2] = copyCount;

                    for (int j = 0; j < values.Length - 3; j++)
                    {
                        values[j + 3] = dt.Rows[i][j];
                    }

                    dt2.Rows.Add(values);
                }

                #endregion

                #region Gridin Kolonlarını Yükleme

                for (int i = 0; i < dt2.Columns.Count; i++)
                {
                    ColumnDefinition gridCol = new ColumnDefinition();
                    gridCol.Width = GridLength.Auto;
                    grdResult.ColumnDefinitions.Add(gridCol);
                }

                for (int i = 0; i < dt2.Rows.Count + 1; i++)
                {
                    RowDefinition gridRowBaslik = new RowDefinition();
                    gridRowBaslik.Height = new GridLength(25);
                    grdResult.RowDefinitions.Add(gridRowBaslik);
                }

                #endregion

                #region Grid Başlığı Yükleme

                TextBlock txtPkBaslik = new TextBlock();
                txtPkBaslik.Text = "PK";
                addControlToGrid(0, 0, 1, 1, 1, 1, txtPkBaslik, grdResult);

                CheckBox chkSelectAll = new CheckBox();
                chkSelectAll.IsChecked = true;
                chkSelectAll.Checked += ChkSelectAll_Checked;
                chkSelectAll.Unchecked += ChkSelectAll_Unchecked;
                addControlToGrid(0, 1, 0, 1, 1, 1, chkSelectAll, grdResult);

                TextBlock txtCopies = new TextBlock();
                txtCopies.Text = "Copies";
                addControlToGrid(0, 2, 0, 1, 1, 1, txtCopies, grdResult);


                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    TextBlock txtBaslik = new TextBlock();
                    txtBaslik.Text = dt.Columns[i].ColumnName;
                    addControlToGrid(0, i + 3, 0, 1, 1, 1, txtBaslik, grdResult);
                }

                #endregion

                #region Gridin İçeriğini Yükleme

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    for (int j = 0; j < dt2.Columns.Count; j++)
                    {
                        if (j == 0)
                        {
                            TextBlock txtPk = new TextBlock();
                            txtPk.Text = (int.Parse(dt2.Rows[i][j].ToString()) + 1).ToString();
                            addControlToGrid(i + 1, j, 1, 0, 1, 1, txtPk, grdResult);
                        }
                        else if (j == 1)
                        {
                            CheckBox chkSelect = new CheckBox();
                            chkSelect.IsChecked = true;
                            chkSelect.Checked += ChkSelect_Checked;
                            chkSelect.Unchecked += ChkSelect_Unchecked;
                            addControlToGrid(i + 1, j, 0, 0, 1, 1, chkSelect, grdResult);
                        }
                        else if (j == 2)
                        {
                            NumericUpDown nud = new NumericUpDown();
                            nud.TextBoxChanged += Nud_TextBoxChanged;
                            nud.Text = copyCount.ToString();
                            addControlToGrid(i + 1, j, 0, 0, 1, 1, nud, grdResult);
                        }
                        else
                        {
                            TextBlock txtFieldContent = new TextBlock();
                            txtFieldContent.Text = dt2.Rows[i][j].ToString();
                            addControlToGrid(i + 1, j, 0, 0, 1, 1, txtFieldContent, grdResult);
                        }
                    }
                }

                #endregion

                selectedRows = dt2.Columns.Count;
                
            }

        }

        int selectedRows;
        private void ChkSelect_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox c = (CheckBox)sender;

            for (int i = 0; i < grdResult.Children.Count; i++)
            {
                Border b = (Border)grdResult.Children[i];
                if (b.Child is CheckBox)
                {
                    CheckBox c2 = (CheckBox)b.Child;

                    if (c == c2)
                    {
                        int row = Grid.GetRow(b);

                        dt2.Rows[row - 1][1] = false;
                    }

                }
            }
        }
        private void ChkSelect_Checked(object sender, RoutedEventArgs e)
        {

            CheckBox c = (CheckBox)sender;

            for (int i = 0; i < grdResult.Children.Count; i++)
            {
                Border b = (Border)grdResult.Children[i];
                if (b.Child is CheckBox)
                {
                    CheckBox c2 = (CheckBox)b.Child;

                    if (c == c2)
                    {
                        int row = Grid.GetRow(b);

                        dt2.Rows[row - 1][1] = true;
                    }

                }
            }
        }
        private void ChkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            bool? check = c.IsChecked;

            for (int i = 0; i < grdResult.Children.Count; i++)
            {
                Border b = (Border)grdResult.Children[i];
                if (b.Child is CheckBox)
                {
                    CheckBox c2 = (CheckBox)b.Child;
                    if (check == true)
                    {
                        c2.IsChecked = true;
                    }
                    else
                    {
                        c2.IsChecked = false;
                    }
                }
            }

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                dt2.Rows[i][1] = false;
            }
        }
        private void ChkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            bool? check = c.IsChecked;
            for (int i = 0; i < grdResult.Children.Count; i++)
            {
                Border b = (Border)grdResult.Children[i];
                if (b.Child is CheckBox)
                {
                    CheckBox c2 = (CheckBox)b.Child;
                    if (check == true)
                    {
                        c2.IsChecked = true;
                    }
                    else
                    {
                        c2.IsChecked = false;
                    }
                }
            }
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                dt2.Rows[i][1] = true;
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            gthis.resultScreen.Visibility = Visibility.Hidden;
            gthis.dataSettings.Visibility = Visibility.Visible;
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            gthis.Close();
        }

        private void addControlToGrid(int row, int column, int leftBorder, int topBorder, int rightBorder, int bottomBorder, UIElement element, Grid grid)
        {
            UIElement genel = new UIElement();
            if (element is TextBlock)
            {
                TextBlock txt = (TextBlock)element;
                txt.HorizontalAlignment = HorizontalAlignment.Left;
                txt.VerticalAlignment = VerticalAlignment.Center;
                txt.Margin = new Thickness(5);
                genel = txt;
            }
            else if (element is CheckBox)
            {
                CheckBox chk = (CheckBox)element;
                chk.HorizontalAlignment = HorizontalAlignment.Left;
                chk.VerticalAlignment = VerticalAlignment.Center;
                chk.Margin = new Thickness(1);
                genel = chk;
            }
            else if (element is NumericUpDown)
            {
                NumericUpDown nud = (NumericUpDown)element;
                nud.HorizontalAlignment = HorizontalAlignment.Left;
                nud.VerticalAlignment = VerticalAlignment.Center;
                nud.Width = 45;
                genel = nud;
            }
            Border b = new Border();
            b.BorderThickness = new Thickness(leftBorder, topBorder, rightBorder, bottomBorder);
            b.BorderBrush = Brushes.DarkGray;
            Grid.SetColumn(b, column);
            Grid.SetRow(b, row);
            b.Child = genel;
            grid.Children.Add(b);
        }

        private void Nud_TextBoxChanged(object sender, TextChangedEventArgs e)
        {
            TextBox c = (TextBox)sender;

            for (int i = 0; i < grdResult.Children.Count; i++)
            {
                Border b = (Border)grdResult.Children[i];
                if (b.Child is NumericUpDown)
                {
                    NumericUpDown c2 = (NumericUpDown)b.Child;

                    if (c == c2.Child)
                    {
                        int row = Grid.GetRow(b);

                        dt2.Rows[row - 1][2] = c.Text;

                    }

                }
            }
        }

        private void grdResult_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = (Grid)sender;

            var point = Mouse.GetPosition(grid);

            int row = 0;
            int col = 0;
            double accumulatedHeight = 0.0;
            double accumulatedWidth = 0.0;

            // calc row mouse was over
            foreach (var rowDefinition in grid.RowDefinitions)
            {
                accumulatedHeight += rowDefinition.ActualHeight;
                if (accumulatedHeight >= point.Y)
                    break;
                row++;
            }

            // calc col mouse was over
            foreach (var columnDefinition in grid.ColumnDefinitions)
            {
                accumulatedWidth += columnDefinition.ActualWidth;
                if (accumulatedWidth >= point.X)
                    break;
                col++;
            }

            if (row != 0)
            {
                for (int i = 0; i < grid.Children.Count; i++)
                {
                    Border b = (Border)grid.Children[i];

                    if (Grid.GetRow(b) == row)
                    {
                        var bc = new BrushConverter();
                        b.Background = (Brush)bc.ConvertFrom("#b5d7ed");
                    }
                    else
                    {
                        b.Background = Brushes.Transparent;
                    }
                }
            }

        }

    }
}
