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
using System.Media;


namespace PatientMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SoundPlayer mutable = new SoundPlayer(PatientMonitor.Properties.Resources.Mutable); //creating a 
        SoundPlayer nonMutable = new SoundPlayer(PatientMonitor.Properties.Resources.NonMutable);

        public MainWindow()
        {
            InitializeComponent();
            PatientFactory factory = new PatientFactory(); //creating a new instance of patient factory
            PatientMonitoringController controller = new PatientMonitoringController(this, factory);
            controller.RunMonitor();
        }
        
        public void soundMutableAlarm()
        {
            mutable.Stop();
            mutable.Play();
        }

        public void soundNonMutableAlarm()
        {
            nonMutable.Stop();
            nonMutable.Play();
        }

        private void heartRateLower_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void systolicLower_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
