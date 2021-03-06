﻿using System;
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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Authentication
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string ConString = System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            string CmdString = String.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                try
                {
                    con.Open();
                    CmdString = "SELECT * FROM Login where Username='" + txtUsername.Text + "' and Password='" + txtPassword.Password + "' and Role='" + comboRole.Text + "'";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr = cmd.ExecuteReader();

                    int count = 0;
                    // This checks how many times the record is present.
                    while (dr.Read())
                    {
                        count++;
                    }
                    if (count > 1)
                    {
                        MessageBox.Show("Username and password are duplicate!!");
                    }
                    if (count == 1)
                    {
                       // MessageBox.Show("You are authenticated!!");

                        this.Hide();
                        con.Close();
                        Employee empForm = new Employee();
                        //App.Current.MainWindow.Close();
                        empForm.ShowDialog();
                    }
                    if (count < 1)
                    {
                        MessageBox.Show("Username or password is not correct, try again!");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
