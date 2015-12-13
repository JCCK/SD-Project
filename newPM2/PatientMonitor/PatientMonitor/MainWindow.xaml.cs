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
        SoundPlayer mutable = new SoundPlayer(PatientMonitor.Properties.Resources.Mutable);
        SoundPlayer nonMutable = new SoundPlayer(PatientMonitor.Properties.Resources.NonMutable);

        public List<bedLabels> ListOfBedsLabels = new List<bedLabels>();
        public List<BedUpDowns> ListOfBedUpDowns = new List<BedUpDowns>();
        public List<alarmRectangle> ListOfRectangles = new List<alarmRectangle>();


        public MainWindow()
        {
            InitializeComponent();
            PatientFactory factory = new PatientFactory();
            PatientMonitoringController controller = new PatientMonitoringController(this, factory);
            InitializeBedList();
            InitializeBedUpDowns();
            InitializeRectangles();

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

        private void bed1_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void InitializeBedList()
        {
            ListOfBedsLabels.Add(bed1Labels);
            ListOfBedsLabels.Add(bed2Labels);
            ListOfBedsLabels.Add(bed3Labels);
            ListOfBedsLabels.Add(bed4Labels);
            ListOfBedsLabels.Add(bed5Labels);
            ListOfBedsLabels.Add(bed6Labels);
            ListOfBedsLabels.Add(bed7Labels);
            ListOfBedsLabels.Add(bed8Labels);
        }
        public void InitializeBedUpDowns()
        {
            ListOfBedUpDowns.Add(bed1ud);
            ListOfBedUpDowns.Add(bed2ud);
            ListOfBedUpDowns.Add(bed3ud);
            ListOfBedUpDowns.Add(bed4ud);
            ListOfBedUpDowns.Add(bed5ud);
            ListOfBedUpDowns.Add(bed6ud);
            ListOfBedUpDowns.Add(bed7ud);
            ListOfBedUpDowns.Add(bed8ud);
        }

        public void InitializeRectangles()
        {
            ListOfRectangles.Add(alarm1);
            ListOfRectangles.Add(alarm2);
            ListOfRectangles.Add(alarm3);
            ListOfRectangles.Add(alarm4);
            ListOfRectangles.Add(alarm5);
            ListOfRectangles.Add(alarm6);
            ListOfRectangles.Add(alarm7);
            ListOfRectangles.Add(alarm8);
        }

        private void btnOpenLogin_Click(object sender, RoutedEventArgs e)
        {
            var lw = new LoginWindow();
            lw.Show();
        }
    }
}
