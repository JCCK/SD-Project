using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PatientMonitor
{
    class PatientMonitoringController //Main Controller class for the program. 
    {
        readonly MainWindow _mainWindow = null;
        readonly IPatientFactory _patientFactory = null;
        DispatcherTimer _tickTimer = new DispatcherTimer();
        PatientDataReader _dataReader;
        PatientData _patientData;
        PatientAlarmer _alarmer;

        CheckBox _alarmMuter;
        Label _pulseRate;
        Label _breathingRate;
        Label _systolicPressure;
        Label _diastolicPressure;
        Label _temperature;

        public PatientMonitoringController(MainWindow window, IPatientFactory patientFactory)
        {
            _patientFactory = patientFactory;
            _mainWindow = window;
            _pulseRate = _mainWindow.pulseRate;
            _breathingRate = _mainWindow.breathingRate;
            _systolicPressure = _mainWindow.systolic;
            _diastolicPressure = _mainWindow.diastolic;
            _temperature = _mainWindow.temperature;
            _alarmMuter = _mainWindow.AlarmMute;
        }

        public void RunMonitor()
        {
            setupComponents(); //Running SetupComponents method 
            setupUI(); //Running Setup UI method.
        }

        void setupUI() //This class is setting the default values for the upper and lower limits of patient's vitals, created in the DefaultSettings class to the MainWindow text boxes
        {
            _mainWindow.patientSelector.SelectionChanged
                += new System.Windows.Controls.SelectionChangedEventHandler(newPatientSelected);

            _mainWindow.heartRateLower.AlarmValue = (int)DefaultSettings.LOWER_PULSE_RATE;
            _mainWindow.breathingRateLower.AlarmValue = (int)DefaultSettings.LOWER_BREATHING_RATE;
            _mainWindow.temperatureLower.AlarmValue = (int)DefaultSettings.LOWER_TEMPERATURE;
            _mainWindow.systolicLower.AlarmValue = (int)DefaultSettings.LOWER_SYSTOLIC;
            _mainWindow.diastolicLower.AlarmValue = (int)DefaultSettings.LOWER_DIASTOLIC;

            _mainWindow.heartRateUpper.AlarmValue = (int)DefaultSettings.UPPER_PULSE_RATE;
            _mainWindow.breathingRateUpper.AlarmValue = (int)DefaultSettings.UPPER_BREATHING_RATE;
            _mainWindow.temperatureUpper.AlarmValue = (int)DefaultSettings.UPPER_TEMPERATURE;
            _mainWindow.systolicUpper.AlarmValue = (int)DefaultSettings.UPPER_SYSTOLIC;
            _mainWindow.diastolicUpper.AlarmValue = (int)DefaultSettings.UPPER_DIASTOLIC;

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

        void limitsChanged(object sender, EventArgs e) 
        {
            _alarmer.PulseRateTester.LowerLimit = _mainWindow.heartRateLower.AlarmValue;
            _alarmer.BreathingRateTester.LowerLimit = _mainWindow.breathingRateLower.AlarmValue;
            _alarmer.TemperatureTester.LowerLimit = _mainWindow.temperatureLower.AlarmValue;
            _alarmer.SystolicBpTester.LowerLimit = _mainWindow.systolicLower.AlarmValue;
            _alarmer.DiastolicBpTester.LowerLimit = _mainWindow.diastolicLower.AlarmValue;

            _alarmer.PulseRateTester.UpperLimit = _mainWindow.heartRateUpper.AlarmValue;
            _alarmer.BreathingRateTester.UpperLimit = _mainWindow.breathingRateUpper.AlarmValue;
            _alarmer.TemperatureTester.UpperLimit = _mainWindow.temperatureUpper.AlarmValue;
            _alarmer.SystolicBpTester.UpperLimit = _mainWindow.systolicUpper.AlarmValue;
            _alarmer.DiastolicBpTester.UpperLimit = _mainWindow.diastolicUpper.AlarmValue;
        }

        void setupComponents()
        {
            _patientData = (PatientData)_patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientData);
            _dataReader = (PatientDataReader)_patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientDataReader);
            _alarmer = (PatientAlarmer)_patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientAlarmer);

            _alarmer.BreathingRateAlarm += new EventHandler(soundMutableAlarm);
            _alarmer.DiastolicBloodPressureAlarm += new EventHandler(soundMutableAlarm);
            _alarmer.PulseRateAlarm += new EventHandler(soundMutableAlarm);
            _alarmer.SystolicBloodPressureAlarm += new EventHandler(soundMutableAlarm);
            _alarmer.TemperatureAlarm += new EventHandler(soundMutableAlarm);
            _tickTimer.Stop();
            _tickTimer.Interval= TimeSpan.FromMilliseconds(1000);  //changes patient details every second.
            _tickTimer.Tick += new EventHandler(updateReadings); //updates patiend readings. 
        }

        void updateReadings(object sender, EventArgs e) //class is updating the readings of patient vitals.
        {            
            _patientData.SetPatientData(_dataReader.getData());
            _pulseRate.Content = _patientData.PulseRate;
            _breathingRate.Content = _patientData.BreathingRate;
            _systolicPressure.Content = _patientData.SystolicBloodPressure;
            _diastolicPressure.Content = _patientData.DiastolicBloodPressure;
            _temperature.Content = _patientData.Temperature;
            _alarmer.ReadingsTest(_patientData);
        }

        void newPatientSelected(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _tickTimer.Stop();
            string fileName = @"..\..\..\" + _mainWindow.patientSelector.SelectedValue + ".csv"; //Reading patient's vitals from csv file
            _dataReader.Connect(fileName); 
            _tickTimer.Start();
        }

        void soundMutableAlarm(object sender, EventArgs e)
        {
            if(_alarmMuter.IsChecked == false)
            {
                _mainWindow.soundMutableAlarm(); //sounding alarm upon user input.
            }
        }
    }
}
