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
using static SecureChatApp.Logic.User;

namespace SecureChatApp
{
    /// <summary>
    /// Interaction logic for RegisterUser.xaml
    /// </summary>
    public partial class RegisterUser : UserControl
    {
        //TODO: Create proper user account creation 
        public RegisterUser()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (RPWtxt.Password == RPW2txt.Password && !string.IsNullOrEmpty(RPWtxt.Password) && !string.IsNullOrEmpty(RUNtxt.Text))
            {
                var user = CreateUser(RUNtxt.Text, RPWtxt.Password);
                this.Content = new Dashboard(user);
            }
            else
            {
                badInputPSW.Visibility = Visibility.Visible;
            }
        }
    }
}
