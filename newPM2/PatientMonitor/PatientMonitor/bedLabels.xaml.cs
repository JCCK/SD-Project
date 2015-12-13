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

namespace PatientMonitor
{
    /// <summary>
    /// Interaction logic for bedLabels.xaml
    /// </summary>
    public partial class bedLabels : UserControl
    {
        public bedLabels()
        {
            InitializeComponent();
        }

        public Label LblBreathingRate
        {
            get { return lblBreathingRate; }
            set { lblBreathingRate = value; }
        }

        public Label LblPulseRate
        {
            get { return lblPulseRate; }
            set { lblPulseRate = value; }
        }

        public Label LblTemperature
        {
            get { return lblTemperature; }
            set { lblTemperature = value; }
        }

        public Label LblDiastolic
        {
            get { return lblDiastolic; }
            set { lblDiastolic = value; }
        }

        public Label LblSystolic
        {
            get { return lblSystolic; }
            set { lblSystolic = value; }
        }

        public void UpdateReadings(float breathing, float pulse, float temperature, float systolic, float diastolic)
        {
            LblBreathingRate.Content = breathing.ToString();
            lblPulseRate.Content = pulse.ToString();
            lblTemperature.Content = temperature.ToString();
            lblSystolic.Content = systolic.ToString();
            lblDiastolic.Content = diastolic.ToString();
            

        
        }

    }
}
