
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PatientMonitor
{
    /// <summary>
    /// Interaction logic for UpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public event EventHandler ValueChanged;

        private int _numValue = 0;
        public int bay;
        public string module;
        public int AlarmValue
        
        {
            get{return _numValue;}
            set
            {
                _numValue = value;
                textValue.Content = value.ToString();
            }
        }

        public NumericUpDown()
        {
            InitializeComponent();
            textValue.Content = "0";
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            textValue.Content = ++AlarmValue;
            if (ValueChanged != null) ValueChanged(this, null);
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            textValue.Content = --AlarmValue;
            if (ValueChanged != null) ValueChanged(this, null);
        }

        private void recordSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bay = Convert.ToInt32(recordSelector.Text);
        }

        private void moduleSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            moduleSelector.Text = module;
        }
    }
}
