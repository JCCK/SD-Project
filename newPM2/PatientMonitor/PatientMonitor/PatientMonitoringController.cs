using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using NUnit.Framework;

namespace PatientMonitor //ALL NEW AND UPDATED CODE IN THE PATIENT MONITORING CONTROLLER WAS COMPLETED BY JAKE BETTLES AND CHRIS SCOTNEY.
{
    public interface IPatientMonitoringController
    {
        void RunMonitor();
        void setupUI();
        void limitsChanged(object sender, EventArgs e);
        void setupComponents();
        void updateReadings(object sender, EventArgs e);
        void UpdateAllReadingsToScreen(); 
        void soundMutableAlarm(object sender, EventArgs e);
        void AddtoReadersAlarmers();  
    }
    
    class PatientMonitoringController : IPatientMonitoringController
    {
        readonly MainWindow _mainWindow = null;
        readonly IPatientFactory _patientFactory = null;
        DispatcherTimer _tickTimer = new DispatcherTimer();




        List<PatientData> readings = new List<PatientData>(); // creates list of patient data type
        List<PatientDataReader> readers = new List<PatientDataReader>(); // creates list of patient data readers
        List<PatientAlarmer> alarmers = new List<PatientAlarmer>(); // creates list of patient alarms


        CheckBox _alarmMuter;



        public PatientMonitoringController(MainWindow window, IPatientFactory patientFactory)
        {
            _patientFactory = patientFactory;
            _mainWindow = window;
            _alarmMuter = _mainWindow._alarmMuter;

        }




        public void RunMonitor()
        {
            
            

            AddtoReadersAlarmers();
            setupComponents();// can't  have this first because create and return obj's have nothing to work into
            setupUI();
        }

        public void setupUI()
        {

            for (int i = 0; i < 8; i++) //sets up patient readings with default settings when loaded stored in DefaultSettings.
            {
                _mainWindow.ListOfBedUpDowns[i].udBreathingDown.AlarmValue = (int) DefaultSettings.LOWER_BREATHING_RATE;
                _mainWindow.ListOfBedUpDowns[i].udPulseDown.AlarmValue = (int) DefaultSettings.LOWER_PULSE_RATE;
                _mainWindow.ListOfBedUpDowns[i].udTempDown.AlarmValue = (int) DefaultSettings.LOWER_TEMPERATURE;
                _mainWindow.ListOfBedUpDowns[i].udDiastolicDown.AlarmValue = (int) DefaultSettings.LOWER_DIASTOLIC;
                _mainWindow.ListOfBedUpDowns[i].udSystolicDown.AlarmValue = (int) DefaultSettings.LOWER_SYSTOLIC;

                _mainWindow.ListOfBedUpDowns[i].udBreathingUp.AlarmValue = (int) DefaultSettings.UPPER_BREATHING_RATE;
                _mainWindow.ListOfBedUpDowns[i].udPulseUp.AlarmValue = (int) DefaultSettings.UPPER_PULSE_RATE;
                _mainWindow.ListOfBedUpDowns[i].udTempUp.AlarmValue = (int) DefaultSettings.UPPER_TEMPERATURE;
                _mainWindow.ListOfBedUpDowns[i].udDiastolicUp.AlarmValue = (int) DefaultSettings.UPPER_DIASTOLIC;
                _mainWindow.ListOfBedUpDowns[i].udSystolicUp.AlarmValue = (int) DefaultSettings.UPPER_SYSTOLIC;




                _mainWindow.ListOfBedUpDowns[i].udBreathingDown.ValueChanged += new EventHandler(limitsChanged); //if the value of the up down box is changed, the limits changed event is triggered. 
                _mainWindow.ListOfBedUpDowns[i].udPulseDown.ValueChanged += new EventHandler(limitsChanged);
                _mainWindow.ListOfBedUpDowns[i].udTempDown.ValueChanged += new EventHandler(limitsChanged);
                _mainWindow.ListOfBedUpDowns[i].udDiastolicDown.ValueChanged += new EventHandler(limitsChanged);
                _mainWindow.ListOfBedUpDowns[i].udSystolicDown.ValueChanged += new EventHandler(limitsChanged);


                _mainWindow.ListOfBedUpDowns[i].udBreathingDown.ValueChanged += new EventHandler(limitsChanged);
                _mainWindow.ListOfBedUpDowns[i].udPulseUp.ValueChanged += new EventHandler(limitsChanged);
                _mainWindow.ListOfBedUpDowns[i].udTempUp.ValueChanged += new EventHandler(limitsChanged);
                _mainWindow.ListOfBedUpDowns[i].udDiastolicUp.ValueChanged += new EventHandler(limitsChanged);
                _mainWindow.ListOfBedUpDowns[i].udSystolicUp.ValueChanged += new EventHandler(limitsChanged);
            }
        }

        public void limitsChanged(object sender, EventArgs e) //changes the upper and lower limit of the AlarmValue function for each bed
        {
            for (int i = 0; i < 8; i++)
            {
                alarmers[i].BreathingRateTester.LowerLimit = _mainWindow.ListOfBedUpDowns[i].udBreathingDown.AlarmValue; 
                alarmers[i].PulseRateTester.LowerLimit = _mainWindow.ListOfBedUpDowns[i].udPulseDown.AlarmValue;
                alarmers[i].TemperatureTester.LowerLimit = _mainWindow.ListOfBedUpDowns[i].udTempDown.AlarmValue;
                alarmers[i].DiastolicBpTester.LowerLimit = _mainWindow.ListOfBedUpDowns[i].udDiastolicDown.AlarmValue;
                alarmers[i].SystolicBpTester.LowerLimit = _mainWindow.ListOfBedUpDowns[i].udSystolicDown.AlarmValue;

                alarmers[i].BreathingRateTester.UpperLimit = _mainWindow.ListOfBedUpDowns[i].udBreathingUp.AlarmValue;
                alarmers[i].PulseRateTester.UpperLimit = _mainWindow.ListOfBedUpDowns[i].udPulseUp.AlarmValue;
                alarmers[i].TemperatureTester.UpperLimit = _mainWindow.ListOfBedUpDowns[i].udTempUp.AlarmValue;
                alarmers[i].DiastolicBpTester.UpperLimit = _mainWindow.ListOfBedUpDowns[i].udDiastolicUp.AlarmValue;
                alarmers[i].SystolicBpTester.UpperLimit = _mainWindow.ListOfBedUpDowns[i].udSystolicUp.AlarmValue;
            }
        }

        public void setupComponents() //called once when the program starts to set up main events.
        {
            for (int i = 0; i < 8; i++)
            {
                readings[i] = 
                    (PatientData) _patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientData);
                alarmers[i] = 
                    (PatientAlarmer) _patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientAlarmer);

                alarmers[i].BreathingRateAlarm += new EventHandler(soundMutableAlarm); //sounding mutable alarm  when breathingratealarm event is active.
                alarmers[i].DiastolicBloodPressureAlarm += new EventHandler(soundMutableAlarm);
                alarmers[i].PulseRateAlarm += new EventHandler(soundMutableAlarm);
                alarmers[i].SystolicBloodPressureAlarm += new EventHandler(soundMutableAlarm);
                alarmers[i].TemperatureAlarm += new EventHandler(soundMutableAlarm);
            }
            _tickTimer.Interval = TimeSpan.FromMilliseconds(1000); //time between readings changing
            _tickTimer.Start();
            _tickTimer.Tick += updateReadings; //upon tick timer run updateReadings function
            

        }

        public void updateReadings(object sender, EventArgs e)
        {
            PatientData patientReadings;
            for (int i = 0; i < 8; i++)
            {
                _mainWindow.ListOfRectangles[i].Alarming(false); //Settings rectangles to green if bed alarms are not alarming.
            }
            for (int i = 0; i < 8; i++)
            {
                patientReadings = new PatientData(readers[i].getData());     // creates new instance of reader
                if (readings.Count == 8)
                {
                    readings[i] = patientReadings; //assigns temporary readings to readings list.
                }
                else
                {
                    readings.Add(patientReadings); //adds patient readings to patient readings list.
                }
                alarmers[i].ReadingsTest(readings[i], _mainWindow, i); //alarmers function to test if readings are outside of set limits.
            } 
            UpdateAllReadingsToScreen(); //displays all patient readings to screen
        }

        public void UpdateAllReadingsToScreen() // shows the values of patient data on the screen
        {
            for (int i = 0; i < 8; i++)
            {
                _mainWindow.ListOfBedsLabels[i].UpdateReadings(readings[i].BreathingRate, readings[i].PulseRate,
                readings[i].Temperature, readings[i].SystolicBloodPressure, readings[i].DiastolicBloodPressure);
            }
        }

        public void soundMutableAlarm(object sender, EventArgs e)
        {
            if (_alarmMuter.IsChecked == false) //if user hasn't selected to mute alarm, continue to sound alarm
            {
                _mainWindow.soundMutableAlarm(); 
            }
        }

        public void AddtoReadersAlarmers() //Adds instances of patient data, datareader & alarmers to lists.
        {

            for (int i = 0; i < 8; i ++)
            {
                readers.Add(new PatientDataReader(@"..\..\..\bed " + (i + 1) + ".csv"));
                readers[i].Connect(@"..\..\..\bed " + (i + 1) + ".csv");
                readings.Add(new PatientData(readers[i].getData()));
                alarmers.Add(new PatientAlarmer());

            }
            _tickTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _tickTimer.Start();
        }
    }
}
