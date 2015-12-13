using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
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

namespace PatientMonitor
{
    /// <summary>
    /// Interaction logic for alarmRectangle.xaml
    /// </summary>
    public partial class alarmRectangle : UserControl
    {

        public alarmRectangle()
        {
            InitializeComponent();
        }

        public Rectangle RecBox
        {
            get { return recBox; }
            set { recBox = value; }
        }

        public void Alarming(bool state)
        {
            recBox.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0)); // sets the default colour to green

            if (state == true)
            {
                recBox.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0)); // if the state changes to true, set the colour to red
            }

        }
    }
}
