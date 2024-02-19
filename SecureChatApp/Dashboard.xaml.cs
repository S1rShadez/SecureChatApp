using SecureChatApp.Logic;
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

namespace SecureChatApp
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        User currentUser { get; set; }
        public Dashboard()
        {
            InitializeComponent();
        }
        public Dashboard(User user)
        {
            currentUser = user;
            InitializeComponent();
            AccName.Content = currentUser.username;
        }
    }
}
