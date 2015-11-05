using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatientMonitor
{
    class Modules
    {
        string Heartrate { get; set; }

        public Modules(string heartrate)
        {
            Heartrate = heartrate;
        }
    }
}
