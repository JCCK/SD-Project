using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Windows.Controls;

namespace PatientMonitor
{
    class Bays
    {
        public string PatientName { get; set; }
        public string Temperature { get; set; }
        public string HeartRate { get; set; }
        public string BreathingRate { get; set; }
        public string DiastolicBloodPressure { get; set; }
        public string SystolicBloodPressure { get; set; }
        public float TemperatureLower { get; set; }
        public float HeartRateLower { get; set; }
        public float BreathingRateLower { get; set; }
        public float DiastolicBloodPressureLower { get; set; }
        public float SystolicBloodPressureLower { get; set; }
        public float TemperatureUpper { get; set; }
        public float HeartRateUpper { get; set; }
        public float BreathingRateUpper { get; set; }
        public float DiastolicBloodPressureUpper { get; set; }
        public float SystolicBloodPressureUpper { get; set; }

        public Bays(string patientName,
        string temperature,
        string heartRate,
        string breathingRate,
        string diastolicBreathingRate,
        string systolicBreathingRate,
        float temperatureLower,
        float heartRateLower,
        float breathingRateLower,
        float diastolicBloodPressureLower,
        float systolicBloodPressureLower,
        float temperatureUpper,
        float heartRateUpper,
        float breathingRateUpper,
        float diastolicBloodPressureUpper,
        float systolicBloodPressureUpper)
        {
            PatientName = patientName;
            Temperature = temperature;
            HeartRate = heartRate;
            BreathingRate = breathingRate;
            DiastolicBloodPressure = diastolicBreathingRate;
            SystolicBloodPressure = systolicBreathingRate;
            TemperatureLower = temperatureLower;
            HeartRateLower = heartRateLower;
            BreathingRateLower = breathingRateLower;
            DiastolicBloodPressureLower = diastolicBloodPressureLower;
            SystolicBloodPressureLower = systolicBloodPressureLower;
            TemperatureUpper = temperatureUpper;
            HeartRateUpper = heartRateUpper;
            BreathingRateUpper= breathingRateUpper;
            DiastolicBloodPressureUpper = diastolicBloodPressureUpper;
            SystolicBloodPressureUpper = systolicBloodPressureUpper

        }
    }
}
