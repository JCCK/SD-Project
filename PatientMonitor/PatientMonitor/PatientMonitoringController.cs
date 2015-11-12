using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace PatientMonitor
{
    internal class PatientMonitoringController
    {
        private readonly MainWindow _mainWindow = null;
        private readonly IPatientFactory _patientFactory = null;
        private DispatcherTimer _tickTimer = new DispatcherTimer();
        public Bays[] bayArray;
        string selection1;


        private PatientDataReader _dataReader1;
        private PatientData _patientData1;
        private PatientAlarmer _alarmer1;
        private PatientDataReader _dataReader2;
        private PatientData _patientData2;
        private PatientAlarmer _alarmer2;
        private PatientDataReader _dataReader3;
        private PatientData _patientData3;
        private PatientAlarmer _alarmer3;
        private PatientDataReader _dataReader4;
        private PatientData _patientData4;
        private PatientAlarmer _alarmer4;
        private PatientDataReader _dataReader5;
        private PatientData _patientData5;
        private PatientAlarmer _alarmer5;
        private PatientDataReader _dataReader6;
        private PatientData _patientData6;
        private PatientAlarmer _alarmer6;
        private PatientDataReader _dataReader7;
        private PatientData _patientData7;
        private PatientAlarmer _alarmer7;
        private PatientDataReader _dataReader8;
        private PatientData _patientData8;
        private PatientAlarmer _alarmer8;


        public PatientMonitoringController(MainWindow window, IPatientFactory patientFactory)
        {
            _patientFactory = patientFactory;
            for (int i = 0; i < 8; i++)
            {
                bayArray[i] = new Bays("Test"+i, "Pulse", "Breathing", "Temp", "Diastolic",
                    DefaultSettings.LOWER_PULSE_RATE,
                    DefaultSettings.LOWER_BREATHING_RATE,
                    DefaultSettings.LOWER_TEMPERATURE, DefaultSettings.LOWER_DIASTOLIC,
                    DefaultSettings.UPPER_PULSE_RATE, DefaultSettings.UPPER_BREATHING_RATE,
                    DefaultSettings.UPPER_TEMPERATURE, DefaultSettings.UPPER_DIASTOLIC);
            }
        



            for (int i =0;i<8;i++)

            bayArray[1].Module1 = _mainWindow.lblBed1Mod1;
            bayArray[1].Module2 = _mainWindow.lblBed1Mod2;
            bayArray[1].Module3 = _mainWindow.lblBed1Mod3;
            bayArray[1].Module4 = _mainWindow.lblBed1Mod4;
            //_alarmMuter1 = _mainWindow.AlarmMute;




        }

        public void RunMonitor()
        {
            setupComponents();
           setupUI();
        }

        private void setupUI()
        {

            _mainWindow.heartRateLower.AlarmValue = (int) DefaultSettings.LOWER_PULSE_RATE;
            _mainWindow.breathingRateLower.AlarmValue = (int) DefaultSettings.LOWER_BREATHING_RATE;
            _mainWindow.temperatureLower.AlarmValue = (int) DefaultSettings.LOWER_TEMPERATURE;
            _mainWindow.systolicLower.AlarmValue = (int) DefaultSettings.LOWER_SYSTOLIC;
            _mainWindow.diastolicLower.AlarmValue = (int) DefaultSettings.LOWER_DIASTOLIC;

            _mainWindow.heartRateUpper.AlarmValue = (int) DefaultSettings.UPPER_PULSE_RATE;
            _mainWindow.breathingRateUpper.AlarmValue = (int) DefaultSettings.UPPER_BREATHING_RATE;
            _mainWindow.temperatureUpper.AlarmValue = (int) DefaultSettings.UPPER_TEMPERATURE;
            _mainWindow.systolicUpper.AlarmValue = (int) DefaultSettings.UPPER_SYSTOLIC;
            _mainWindow.diastolicUpper.AlarmValue = (int) DefaultSettings.UPPER_DIASTOLIC;

            _mainWindow.heartRateLower.ValueChanged += new EventHandler(limitsChanged);
            _mainWindow.breathingRateLower.ValueChanged += new EventHandler(limitsChanged);
            _mainWindow.temperatureLower.ValueChanged += new EventHandler(limitsChanged);
            _mainWindow.systolicLower.ValueChanged += new EventHandler(limitsChanged);
            _mainWindow.diastolicLower.ValueChanged += new EventHandler(limitsChanged);

            _mainWindow.heartRateUpper.ValueChanged += new EventHandler(limitsChanged);
            _mainWindow.breathingRateUpper.ValueChanged += new EventHandler(limitsChanged);
            _mainWindow.temperatureUpper.ValueChanged += new EventHandler(limitsChanged);
            _mainWindow.systolicUpper.ValueChanged += new EventHandler(limitsChanged);
            _mainWindow.diastolicUpper.ValueChanged += new EventHandler(limitsChanged);
        }

        private void limitsChanged(object sender, EventArgs e)
        {
            _alarmer1.PulseRateTester.LowerLimit = _mainWindow.heartRateLower.AlarmValue;
            _alarmer1.BreathingRateTester.LowerLimit = _mainWindow.breathingRateLower.AlarmValue;
            _alarmer1.TemperatureTester.LowerLimit = _mainWindow.temperatureLower.AlarmValue;
            _alarmer1.SystolicBpTester.LowerLimit = _mainWindow.systolicLower.AlarmValue;
            _alarmer1.DiastolicBpTester.LowerLimit = _mainWindow.diastolicLower.AlarmValue;

            _alarmer1.PulseRateTester.UpperLimit = _mainWindow.heartRateUpper.AlarmValue;
            _alarmer1.BreathingRateTester.UpperLimit = _mainWindow.breathingRateUpper.AlarmValue;
            _alarmer1.TemperatureTester.UpperLimit = _mainWindow.temperatureUpper.AlarmValue;
            _alarmer1.SystolicBpTester.UpperLimit = _mainWindow.systolicUpper.AlarmValue;
            _alarmer1.DiastolicBpTester.UpperLimit = _mainWindow.diastolicUpper.AlarmValue;
        }

        private void setupComponents()
        {
            _patientData1 = (PatientData) _patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientData);
            _dataReader1 =
                (PatientDataReader) _patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientDataReader);
            _alarmer1 = (PatientAlarmer) _patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientAlarmer);

            _alarmer1.BreathingRateAlarm += new EventHandler(soundMutableAlarm);
            _alarmer1.DiastolicBloodPressureAlarm += new EventHandler(soundMutableAlarm);
            _alarmer1.PulseRateAlarm += new EventHandler(soundMutableAlarm);
            _alarmer1.SystolicBloodPressureAlarm += new EventHandler(soundMutableAlarm);
            _alarmer1.TemperatureAlarm += new EventHandler(soundMutableAlarm);
            _tickTimer.Stop();
            _tickTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _tickTimer.Tick += new EventHandler(updateReadings);
        }

        private void updateReadings(object sender, EventArgs e)
        {
            _patientData1.SetPatientData(_dataReader1.getData());
            bayArray[1].Module1.Content = selection1;

            switch (selection1)
            {
                case "Pulse":
                    bayArray[1].Module1.Content = _patientData1.PulseRate;
                    break;
                
                case "Breathing":
                    bayArray[1].Module2.Content = _patientData1.BreathingRate;
                    break;

                case "Temp":
                    bayArray[1].Module2.Content = _patientData1.Temperature;
                    break;

                case "Diastolic":

                    break;
                    
                case "Systolic":
                    bayArray[1].Module3.Content = _patientData1.SystolicBloodPressure;
                    break;

                default:

                    break;

            }
            
            
            
            ;
            bayArray[1].Module4.Content = _patientData1.DiastolicBloodPressure;
            
            _alarmer1.ReadingsTest(_patientData1);
        }

        private void SelectPatients(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _tickTimer.Stop();
            string fileName = @"..\..\..\" + _mainWindow.patientSelector.SelectedValue + ".csv";
            _dataReader1.Connect(fileName);
            _tickTimer.Start();
        }

        private void soundMutableAlarm(object sender, EventArgs e)
        {
            if (_alarmMuter.IsChecked == false)
            {
                _mainWindow.soundMutableAlarm();
            }
        }
    }
}
