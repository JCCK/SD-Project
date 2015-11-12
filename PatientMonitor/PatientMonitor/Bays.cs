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
        public Label PatientName { get; set; }
        public Label Module1 { get; set; }
        public Label Module2 { get; set; }
        public Label Module3 { get; set; }
        public Label Module4 { get; set; }
        public float Module1Lower { get; set; }
        public float Module2Lower { get; set; }
        public float Module3Lower { get; set; }
        public float Module4Lower { get; set; }
        public float Module1Upper { get; set; }
        public float Module2Upper { get; set; }
        public float Module3Upper { get; set; }
        public float Module4Upper { get; set; }

        public Bays(Label patientName, Label module1, Label module2, Label module3,Label module4,
            float module1Lower, float module2Lower, float module3Lower, float module4Lower,
            float module1Upper, float module2Upper, float module3Upper, float module4Upper)
        {
            PatientName = patientName;
            Module1 = module1;
            Module2 = module2;
            Module3 = module3;
            Module4 = module4;
            Module1Lower = module1Lower;
            Module2Lower = module2Lower;
            Module3Lower = Module3Lower;
            Module4Lower = module4Lower;
            Module1Upper = module1Upper;
            Module2Upper = module2Upper;
            Module3Upper = Module3Upper;
            Module4Upper = module4Upper;
        }
    }
}
