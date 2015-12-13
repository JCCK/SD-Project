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
    public partial class LoginWindow : Window
    {

        public LoginWindow()
        {
            InitializeComponent();
        }

        private static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source =C:\Users\Andrew\Desktop\loggin project\SDProject.accdb"; // string connects to database
        OleDbConnection con = new OleDbConnection(connectionString);


        private void btnLogin_Click(object sender, RoutedEventArgs e) //button click for login
        {
            if (txtUsername.Text != "" && pwPassword.Password != "") // if statement to compares password box and ussername box
            { DateTime time = DateTime.Now;
                            
                string sql = "SELECT * FROM tblStaff WHERE staffID = '" + txtUsername.Text + "' AND password = '" + pwPassword.Password + "';"; // checks staffId with database table for staff id and password.
                con.ConnectionString = connectionString;
                if (con.State != ConnectionState.Open) // checks to make sure connection is open, if not opens connection
                {
                    con.Open();
                }


                OleDbDataAdapter login = new OleDbDataAdapter(sql, con); //checks the users and and password.
                DataTable dt = new DataTable();
                login.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    MessageBox.Show("Login", "Login", MessageBoxButton.OK); // returned results match, sucessfull log in.
                    string sql2 = "UPDATE tblStaff SET logginTime = '" + time + "' WHERE staffId = '" + txtUsername.Text + "'"; 
                    OleDbCommand updateTime = new OleDbCommand();
                    updateTime.Connection = con;
                    updateTime.CommandText = sql2;
                    updateTime.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Login Failed", "Incorrect Username or Password", MessageBoxButton.OK); //failed loggin error message
                }
                con.Close(); // close connection 
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e) //fuction for button click to send data to database
        {
            if (txtStaffID.Text == "" || txtEmail.Text == "" || pwPasswordRegister.Password == "" || pwPasswordConfirm.Password == "" || txtName.Text == "" || txtSurname.Text == "" || txtContactNumber.Text == "") //checks that alltext boxes are filled in the new staff regester
            {
                MessageBox.Show("You have to fill in all the fields before you can create a user", "Missing Informaion", MessageBoxButton.OK); // message box for incompelte data entry
            }
            else
            {
                if (pwPasswordRegister.Password != pwPasswordConfirm.Password) //checks password  with confirm password box 
                {
                    MessageBox.Show("Passwords dont match", "retype passwords", MessageBoxButton.OK); // called error when password don't match
                }
                else
                {
                    bool userFound = CheckForUser(txtStaffID.Text);
                    if (userFound)
                    {
                        MessageBox.Show("StaffId already exists", "Login instead", MessageBoxButton.OK); // called error when user already exists
                    }
                    else
                    {


                        con.ConnectionString = connectionString; // conects with data base inserts user information with sql statement
                        string sql = "INSERT INTO tblStaff([staffID], [password], [firstName], [lastName], [contactNumber], [emailAddress]) VALUES('" + txtStaffID.Text + "', '" + pwPasswordRegister.Password + "', '" + txtName.Text + "', '" + txtSurname.Text + "', '" + txtContactNumber.Text + "', '" + txtEmail.Text + "');";
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        OleDbCommand addUser = new OleDbCommand();
                        addUser.Connection = con;
                        addUser.CommandText = sql;

                        try
                        {
                            addUser.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("User Added", "Added", MessageBoxButton.OK); //susscessfull added infomation to database.
                        }
                        catch (Exception ex) //exception for failed infomation
                        {
                            MessageBox.Show("User Not Added", "Not Added", MessageBoxButton.OK);
                            MessageBox.Show(ex.StackTrace, "Not Added", MessageBoxButton.OK);
                            MessageBox.Show(sql, "Not Added", MessageBoxButton.OK);
                            Console.Write(ex.Message);
                            con.Close();
                        }
                    }

                }
            }
        }

        private void btnCheckDb_Click(object sender, RoutedEventArgs e) //button click to check staff id with database
        {
            bool userFound = CheckForUser(txtUsername.Text);
            if (userFound)
            {
                MessageBox.Show("userfound", "userfound", MessageBoxButton.OK);  //suscessfull operation

            }
            else
            {
                MessageBox.Show("User Not found", "User Not found", MessageBoxButton.OK); //failed to find username
            }

            
        }

        private bool CheckForUser(string userName)  //method is used to not dupicating code as is called inmore than one place
        {
            string sql = "SELECT * FROM tblStaff WHERE staffID = '" + userName + "';"; //selects string from username textbox
            con.ConnectionString = connectionString;

            if (con.State != ConnectionState.Open) //opens  connection with database
            {
                con.Open();
            }


            OleDbDataAdapter login = new OleDbDataAdapter(sql, con); //checks with database feild for username
            DataTable dt = new DataTable();
            login.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                con.Close();//closes connection to database  
                return true;

            }
            else
            {
                con.Close();//closes connection to database  
                return false;
            }
              
        }
    }
}


  
