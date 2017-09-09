using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
using System.Text.RegularExpressions;
using System.Globalization;

namespace DatabaseWizard.User_Controls.Database
{
    /// <summary>
    /// Interaction logic for WSDLScreen.xaml
    /// </summary>
    public partial class WSDLScreen : UserControl
    {
        public WSDLScreen()
        {
            InitializeComponent();
            gthis = C.gthis();
        }
        MainWindow gthis;

        bool methodWithParameter = false;


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
            e.Handled = !regex.IsMatch(e.Text);
        }
        private void cmbMethodNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stckControls.Children.Clear();
            stckLabel.Children.Clear();

            if (cmbMethodNames.SelectedIndex != 0)
            {
                ParameterInfo[] parameters = ReadWebService.GetWebServiceMethodsParameters(txtServer.Text, cmbMethodNames.SelectedItem.ToString());

                if (parameters.Length > 0)
                {
                    methodWithParameter = true;

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        Label lbl = new Label();
                        lbl.Height = 25;
                        lbl.Margin = new Thickness(3);
                        lbl.Content = parameters[i].Name + ":";
                        string bas = lbl.Content.ToString().ToCharArray()[0].ToString().ToUpper();
                        for (int j = 1; j < lbl.Content.ToString().ToCharArray().Length; j++)
                        {
                            bas += lbl.Content.ToString().ToCharArray()[j].ToString();
                        }

                        lbl.Content = bas;

                        string type = parameters[i].ParameterType.ToString();

                        if (type == "System.Int32")
                        {
                            TextBox txt = new TextBox();
                            txt.Width = 300;
                            txt.Height = 25;
                            txt.Margin = new Thickness(3);
                            txt.PreviewTextInput += NumberValidationTextBox;
                            txt.Tag = "Int";
                            stckControls.Children.Add(txt);

                        }
                        else if (type == "System.Boolean")
                        {
                            ComboBox cmb = new ComboBox();
                            cmb.Items.Add("True");
                            cmb.Items.Add("False");
                            cmb.SelectedIndex = 0;
                            cmb.Tag = "Bool";
                            cmb.Height = 25;
                            cmb.Margin = new Thickness(3);
                            stckControls.Children.Add(cmb);
                        }
                        else if (type == "System.String")
                        {
                            TextBox txt = new TextBox();
                            txt.Width = 300;
                            txt.Tag = "Text";
                            txt.Height = 25;
                            txt.Margin = new Thickness(3);
                            stckControls.Children.Add(txt);
                        }
                        else if (type == "System.Double")
                        {
                            TextBox txt = new TextBox();
                            txt.Width = 300;
                            txt.PreviewTextInput += NumberValidationTextBox;
                            txt.Tag = "Double";
                            txt.Height = 25;
                            txt.Margin = new Thickness(3);
                            stckControls.Children.Add(txt);
                        }
                        else if (type == "System.Int16")
                        {
                            TextBox txt = new TextBox();
                            txt.Width = 300;
                            txt.PreviewTextInput += NumberValidationTextBox;
                            txt.Tag = "Int16";
                            txt.Height = 25;
                            txt.Margin = new Thickness(3);
                            stckControls.Children.Add(txt);
                        }
                        else if (type == "System.Int64")
                        {
                            TextBox txt = new TextBox();
                            txt.Width = 300;
                            txt.PreviewTextInput += NumberValidationTextBox;
                            txt.Tag = "Int64";
                            txt.Height = 25;
                            txt.Margin = new Thickness(3);
                            stckControls.Children.Add(txt);
                        }
                        else if (type == "System.Decimal")
                        {
                            TextBox txt = new TextBox();
                            txt.Width = 300;
                            txt.PreviewTextInput += NumberValidationTextBox;
                            txt.Tag = "Decimal";
                            txt.Height = 25;
                            txt.Margin = new Thickness(3);
                            stckControls.Children.Add(txt);
                        }
                        else if (type == "System.DateTime")
                        {
                            DatePicker dp = new DatePicker();

                            dp.Width = 300;
                            dp.Tag = "DateTime";
                            dp.Height = 25;
                            dp.Margin = new Thickness(3);
                            stckControls.Children.Add(dp);

                        }
                        else if (type == "System.Char")
                        {
                            TextBox txt = new TextBox();
                            txt.Width = 300;
                            txt.MaxLength = 1;
                            txt.Tag = "Char";
                            txt.Height = 25;
                            txt.Margin = new Thickness(3);
                            stckControls.Children.Add(txt);
                        }

                        stckLabel.Children.Add(lbl);


                    }

                }
            }
        }
        

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            DataSet ds = new DataSet();
            if (txtServer.Text != "")
            {


                if (cmbMethodNames.SelectedIndex != 0)
                {
                    if (methodWithParameter)
                    {
                        List<object> args = new List<object>();

                        for (int i = 0; i < stckControls.Children.Count; i++)
                        {

                            Control cntrl = (Control)stckControls.Children[i];


                            if (cntrl.Tag.ToString() == "Text")
                            {
                                TextBox txt = (TextBox)cntrl;
                                string text = txt.Text;

                                if (text == "")
                                {
                                    MessageBox.Show("You must enter parameters.");
                                    return;
                                }
                                else
                                {
                                    args.Add(text);
                                }
                            }
                            else if (cntrl.Tag.ToString() == "Bool")
                            {
                                ComboBox cmb = (ComboBox)cntrl;
                                bool boolean;
                                if (cmb.SelectedIndex == 0)
                                {
                                    boolean = true;
                                }
                                else
                                {
                                    boolean = false;
                                }

                                args.Add(boolean);
                            }
                            else if (cntrl.Tag.ToString() == "Int")
                            {
                                TextBox txt = (TextBox)cntrl;
                                try
                                {
                                    int number = int.Parse(txt.Text);
                                    args.Add(number);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("You must enter integer value.");
                                    return;
                                }
                            }
                            else if (cntrl.Tag.ToString() == "Int64")
                            {
                                TextBox txt = (TextBox)cntrl;
                                try
                                {
                                    long number = Int64.Parse(txt.Text);
                                    args.Add(number);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("You must enter long integer value.");
                                    return;
                                }
                            }
                            else if (cntrl.Tag.ToString() == "Int16")
                            {
                                TextBox txt = (TextBox)cntrl;
                                try
                                {
                                    short number = Int16.Parse(txt.Text);
                                    args.Add(number);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("You must enter short integer value.");
                                    return;
                                }
                            }
                            else if (cntrl.Tag.ToString() == "Double")
                            {
                                TextBox txt = (TextBox)cntrl;
                                try
                                {
                                    double number = Double.Parse(txt.Text, CultureInfo.InvariantCulture);
                                    args.Add(number);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("You must enter double value.");
                                    return;
                                }
                            }
                            else if (cntrl.Tag.ToString() == "Char")
                            {
                                TextBox txt = (TextBox)cntrl;
                                try
                                {
                                    char ch = Char.Parse(txt.Text);
                                    args.Add(ch);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("You must enter character value.");
                                    return;
                                }
                            }
                            else if (cntrl.Tag.ToString() == "Decimal")
                            {
                                TextBox txt = (TextBox)cntrl;
                                try
                                {
                                    decimal number = Decimal.Parse(txt.Text, CultureInfo.InvariantCulture);
                                    args.Add(number);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("You must enter decimal number value.");
                                    return;
                                }
                            }
                            else if (cntrl.Tag.ToString() == "DateTime")
                            {
                                DatePicker txt = (DatePicker)cntrl;
                                try
                                {
                                    DateTime date = DateTime.Parse(txt.Text);
                                    args.Add(date);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("You must enter Date value");
                                    return;
                                }
                            }
                        }

                        ds = ReadWebService.WebservicecallWithArguments(txtServer.Text, cmbMethodNames.SelectedItem.ToString(), args.ToArray());

                    }
                    else
                    {
                        ds = ReadWebService.WebservicecallWithNoArguments(txtServer.Text, cmbMethodNames.SelectedItem.ToString());
                    }

                    try
                    {
                        C.dt = ds.Tables[0];
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ReadWebService.hata);
                        ReadWebService.hata = "";
                        return;
                    }

                    gthis.wsdlScreen.Visibility = Visibility.Hidden;
                    gthis.chooseFields.Visibility = Visibility.Visible;

                }
                else
                {
                    MessageBox.Show("Please firstly select a web service method..");
                }
            }
            else
            {
                MessageBox.Show("Please firstly write Web Service Address then Read Web Service.");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            gthis.firstScreen.Visibility = Visibility.Visible;
            gthis.wsdlScreen.Visibility = Visibility.Hidden;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            gthis.Close();
        }

        private void btnReadService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] methods = ReadWebService.GetWebServiceMethods(txtServer.Text);

                cmbMethodNames.Items.Clear();

                cmbMethodNames.Items.Add("Choose Method");
                for (int i = 0; i < methods.Length; i++)
                {
                    cmbMethodNames.Items.Add(methods[i]);
                }
                cmbMethodNames.SelectedIndex = 0;

                //DataTable dt = ReadService.WebservicecallWithNoArguments(txtMethodName.Text, txtAdress.Text).Tables[0];
                //grdDataGrid.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show(ReadWebService.hata);
                ReadWebService.hata = "";
            }
        }

        private void Txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsControl(e.Key.ToString().ToCharArray()[0]) && !char.IsDigit(e.ToString().ToCharArray()[0]) &&
                (e.Key.ToString() != "."))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.Key.ToString() == ".") && ((sender as TextBox).Text.IndexOf(".") > -1))
            {
                e.Handled = true;
            }

        }
    }
}
