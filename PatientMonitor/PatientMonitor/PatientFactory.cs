using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PatientMonitor
{
    class PatientFactory:IPatientFactory //The patient factory class is where the new alarm limits are set. 
    {
        public Object CreateandReturnObj(PatientClassesEnumeration objectToGet)
        {
            object createdObject = null;
            switch (objectToGet)
            {
                case PatientClassesEnumeration.PatientAlarmer:
                    PatientAlarmer alarmer = new PatientAlarmer(); //Setting alarm values
                    createdObject = alarmer;
                    break;
                case PatientClassesEnumeration.PatientDataReader:
                    PatientDataReader dataReader = new PatientDataReader(); //reads patient information
                    createdObject = dataReader;
                    break;
                case PatientClassesEnumeration.PatientData:
                    PatientData patientData = new PatientData(); //setting patient data
                    createdObject = patientData;
                    break;
                default:
                    throw new ArgumentException("Invalid parameter passed"); //Error Catching.
            }
            return createdObject;
        }
    }
}
