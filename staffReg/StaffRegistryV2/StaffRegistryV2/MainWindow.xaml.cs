using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Threading;

namespace StaffRegistryV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //string staffFile = @"..\..\..\GitHub\SD-Project/staffFile.txt";//
        string staffFile = @"H:\My Documents\Books.bin";
        public List<StaffDetails> staffDetails = new List<StaffDetails>();
        bool hasBeenClicked = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            StaffDetails temp = new StaffDetails(staffIdtxtbox.Text, txtstaffFirstNametxtbox.Text, staffLastNametxtbox.Text, ContactNumbertxtbox.Text, emailTxtBox.Text);
         
            staffDetails.Add(temp);
            SaveFile();
            MessageBox.Show("Details registered");
        }


        private void BtnClock_in_Click(object sender, RoutedEventArgs e)
        {
            string.Format("{0:HH:mm:ss tt}", DateTime.Now);
        }

        private void BtnClock_out_Click(object sender, RoutedEventArgs e)
        {
            string.Format("{0:HH:mm:ss tt}", DateTime.Now);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SaveFile()
        {
            using (Stream stream = File.Open(staffFile, FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, staffDetails);
                stream.Flush();


            }
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            using (Stream stream = File.Open(staffFile, FileMode.OpenOrCreate))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                if (stream.Length > 0)
                    staffDetails = (List<StaffDetails>)binaryFormatter.Deserialize(stream);
            }
        }

        private void staffIdtxtbox_TextInput(object sender, TextCompositionEventArgs e)
        {
            Console.Clear();
        }

        
    }
}

