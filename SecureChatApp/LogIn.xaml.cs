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
using static SecureChatApp.Logic.User;
using static SecureChatApp.Logic.Encryption;
using SecureChatApp.Logic;

namespace SecureChatApp
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : UserControl
    {
        //TODO: Create a proper login test
        public LogIn()
        {
            InitializeComponent();
        }

        //Log in button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(encrypted);
            if (CheckLogin(UNtxt.Text, PWtxt.Password))
            {
                //EncryptFile("testing tekst", "abc123");
                //DecryptFile("abc123");
                this.Content = new Dashboard(CreateUser(UNtxt.Text, PWtxt.Password));
            }
            else
            {
                BadInput.Visibility = Visibility.Visible;
            }
        }

        //Createuser button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Content = new RegisterUser();
        }
    }
}
