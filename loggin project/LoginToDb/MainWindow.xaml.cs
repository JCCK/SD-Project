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
using System.Data.OleDb;
using System.Data;

namespace LoginToDb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source =C:\Users\Andrew\Desktop\loggin project\SDProject.accdb";
        OleDbConnection con = new OleDbConnection(connectionString);


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text != "" && txtPassword.Text != "")
            {
                string sql = "SELECT * FROM tblStaff WHERE staffID = '" + txtUsername.Text + "' AND password = '" + txtPassword.Text + "';";
                con.ConnectionString = connectionString;
                OleDbDataAdapter login = new OleDbDataAdapter(sql, con);
                DataTable dt = new DataTable();
                login.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    MessageBox.Show("Login", "Login", MessageBoxButton.OK);
                    // FormName form = new FormName();
                    // form.Show();

                }
                else
                {
                    MessageBox.Show("Login Failed", "Incorrect Username or Password", MessageBoxButton.OK);
                }
            }
        }


    }
}
