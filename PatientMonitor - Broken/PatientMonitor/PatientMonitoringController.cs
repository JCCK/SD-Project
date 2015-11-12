using System;
using System.Collections.Generic;
using System.Data.Common;
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
        public static Bays[] bayArray;
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


       /*     bayArray[1].Module1. = _mainWindow.lblBed1Mod1.Content;
            bayArray[1].Module2 = _mainWindow.lblBed1Mod2;
            bayArray[1].Module3 = _mainWindow.lblBed1Mod3;
            bayArray[1].Module4 = _mainWindow.lblBed1Mod4;
            //_alarmMuter1 = _mainWindow.AlarmMute;*/




        }

        public void RunMonitor()
        {
            

            PatientUpload(_dataReader1,1);
            PatientUpload(_dataReader2, 2);
            PatientUpload(_dataReader3, 3);
            PatientUpload(_dataReader4, 4);
            PatientUpload(_dataReader5, 5);
            PatientUpload(_dataReader6, 6);
            PatientUpload(_dataReader7, 7);
            PatientUpload(_dataReader8, 8);

            SetupComponents(_patientData1, _dataReader1, _alarmer1,1);
            SetupComponents(_patientData2, _dataReader2, _alarmer2,2);
            SetupComponents(_patientData3, _dataReader3, _alarmer3,3);
            SetupComponents(_patientData4, _dataReader4, _alarmer4,4);
            SetupComponents(_patientData5, _dataReader5, _alarmer5,5);
            SetupComponents(_patientData6, _dataReader6, _alarmer6,6);
            SetupComponents(_patientData7, _dataReader7, _alarmer7,7);
            SetupComponents(_patientData8, _dataReader8, _alarmer8,8);
        

           LimitsUpdated(_mainWindow.moduleLimitUpper.bay,_mainWindow.moduleLimitUpper.module);
           LimitsUpdated(_mainWindow.ModuleLimitLower.bay, _mainWindow.ModuleLimitLower.module);
        }

        private void LimitsUpdated(int bay, string module)
        {


            switch (_mainWindow.moduleLimitUpper.bay)
            {
                case 1:
                    switch (_mainWindow.moduleLimitUpper.module)
                    {
                        case 
                    }
                    _mainWindow.moduleLimitUpper.ValueChanged += new EventHandler(limitsChanged);
                    _mainWindow.ModuleLimitLower.ValueChanged += new EventHandler(limitsChanged);

                    break;

                case 2:
                    break;

                case 3:
                    break;

                case 4:
                    break;

                case 5:
                    break;

                case 6:
                    break;

                case 7:
                    break;

                case 8:
                    break;

                default:
                {
                }
                    break;


            }
           

            _mainWindow.moduleLimitUpper.ValueChanged += new EventHandler(limitsChanged);


            _mainWindow.ModuleLimitLower.ValueChanged += new EventHandler(limitsChanged);
   
        }

        private void limitsChanged(object sender, EventArgs e)
        {
        /*    _alarmer1.PulseRateTester.LowerLimit = _mainWindow.heartRateLower.AlarmValue;
            _alarmer1.BreathingRateTester.LowerLimit = _mainWindow.breathingRateLower.AlarmValue;
            _alarmer1.TemperatureTester.LowerLimit = _mainWindow.temperatureLower.AlarmValue;
            _alarmer1.SystolicBpTester.LowerLimit = _mainWindow.systolicLower.AlarmValue;
            _alarmer1.DiastolicBpTester.LowerLimit = _mainWindow.diastolicLower.AlarmValue;

            _alarmer1.PulseRateTester.UpperLimit = _mainWindow.heartRateUpper.AlarmValue;
            _alarmer1.BreathingRateTester.UpperLimit = _mainWindow.breathingRateUpper.AlarmValue;
            _alarmer1.TemperatureTester.UpperLimit = _mainWindow.temperatureUpper.AlarmValue;
            _alarmer1.SystolicBpTester.UpperLimit = _mainWindow.systolicUpper.AlarmValue;
            _alarmer1.DiastolicBpTester.UpperLimit = _mainWindow.diastolicUpper.AlarmValue;*/
        }

        private void SetupComponents(PatientData _patientData ,PatientDataReader _patientDataReader ,PatientAlarmer _patientAlarmer, int bay)
        {
            _patientData = (PatientData) _patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientData);
            _patientDataReader = (PatientDataReader) _patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientDataReader);
            _patientAlarmer = (PatientAlarmer) _patientFactory.CreateandReturnObj(PatientClassesEnumeration.PatientAlarmer);
             
            _patientAlarmer.BreathingRateAlarm += new EventHandler(soundMutableAlarm);
            _patientAlarmer.DiastolicBloodPressureAlarm += new EventHandler(soundMutableAlarm);
            _patientAlarmer.PulseRateAlarm += new EventHandler(soundMutableAlarm);
            _patientAlarmer.SystolicBloodPressureAlarm += new EventHandler(soundMutableAlarm);
            _patientAlarmer.TemperatureAlarm += new EventHandler(soundMutableAlarm);
            _tickTimer.Stop();
            _tickTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _tickTimer.Tick += new EventHandler((sender, e) => updateReadings(sender, e, bay));

        }

        private void updateReadings(object sender, EventArgs e, int bay)
        {
            _patientData1.SetPatientData(_dataReader1.GetData());
            bayArray[bay].Module1 = module;


                    _mainWindow.lblBed1Mod1.Content = _patientData1.PulseRate;

                    _mainWindow.lblBed1Mod1.Content = _patientData1.BreathingRate;

                    _mainWindow.lblBed1Mod1.Content = _patientData1.Temperature;

                    _mainWindow.lblBed1Mod1.Content = _patientData1.DiastolicBloodPressure;

                    _mainWindow.lblBed1Mod1.Content = _patientData1.SystolicBloodPressure;


           }


        private void PatientUpload(PatientDataReader dataReader, int index)
        {

            _tickTimer.Stop();
                string fileName = @"..\..\..\" + "bed " + index + ".csv";
                dataReader.Connect(fileName);
            dataReader.Connect(fileName);
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
