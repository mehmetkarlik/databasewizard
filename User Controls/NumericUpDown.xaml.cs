using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace DatabaseWizard.User_Controls
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        int minvalue = 1,
        maxvalue = 100000,
        startvalue = 1;

        //public static readonly DependencyProperty CopyCountProperty =
        //   DependencyProperty.Register("CopyCount",
        //       typeof(string),
        //       typeof(NumericUpDown),
        //       new PropertyMetadata("Not set"));

        //[Bindable(true)]
        //public string CopyCount
        //{
        //    get { return (string)this.GetValue(CopyCountProperty); }
        //    set { this.SetValue(CopyCountProperty, value); }
        //}


        //public static readonly DependencyProperty TextProperty;

        //static NumericUpDown()
        //{
        //    TextProperty = DependencyProperty.RegisterAttached("SelectedText",
        //      typeof(string), typeof(NumericUpDown),
        //      new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true });
        //}

        //public string SelectedText
        //{
        //    get { return NUDTextBox.Text; }
        //    set { NUDTextBox.Text = value; }
        //}

        public NumericUpDown()
        {
            InitializeComponent();
            NUDTextBox.Text = startvalue.ToString();

            //Binding b = new Binding("SelectedText");
            //b.Source = this;
            //b.Mode = BindingMode.TwoWay;

            //NUDTextBox.SetBinding(TextBox.TextProperty, b);
        }
        public string Text
        {
            get { return NUDTextBox.Text; }
            set { NUDTextBox.Text = value; }
        }

        public TextBox Child
        {
            get { return NUDTextBox; }
            set { NUDTextBox = value; }
        }

        public event TextChangedEventHandler TextBoxChanged
        {
            add { NUDTextBox.TextChanged += value; }
            remove { NUDTextBox.TextChanged -= value; }
        }

        private void NUDButtonUP_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox.Text != "") number = Convert.ToInt32(NUDTextBox.Text);
            else number = 0;
            if (number < maxvalue)
                NUDTextBox.Text = Convert.ToString(number + 1);
        }

        private void NUDButtonDown_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox.Text != "") number = Convert.ToInt32(NUDTextBox.Text);
            else number = 0;
            if (number > minvalue)
                NUDTextBox.Text = Convert.ToString(number - 1);
        }

        private void NUDTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                NUDButtonUP.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP, new object[] { true });
            }


            if (e.Key == Key.Down)
            {
                NUDButtonDown.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown, new object[] { true });
            }
        }

        private void NUDTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP, new object[] { false });

            if (e.Key == Key.Down)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown, new object[] { false });
        }

        private void NUDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (NUDTextBox.Text != "")
                if (!int.TryParse(NUDTextBox.Text, out number)) NUDTextBox.Text = startvalue.ToString();
            if (number > maxvalue) NUDTextBox.Text = maxvalue.ToString();
            if (number < minvalue) NUDTextBox.Text = minvalue.ToString();
            NUDTextBox.SelectionStart = NUDTextBox.Text.Length;

        }
    }
}
