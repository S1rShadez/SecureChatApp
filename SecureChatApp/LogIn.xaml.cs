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
using static SecureChatApp.Logic.CheckLogin;
using static SecureChatApp.Logic.Encryption;

namespace SecureLoginApp
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : UserControl
    {
        public LogIn()
        {
            InitializeComponent();
        }

        //Log in button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(encrypted);
            if (CLogin(UNtxt.Text, PWtxt.Password))
            {
                EncryptFile("abc123");
                DecryptFile("abc123", "EncryptedData.txt");
                this.Content = new Dashboard();
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
