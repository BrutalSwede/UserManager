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

namespace UserManager
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

        private void UserListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AdminListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ElevateUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DemoteAdmin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_User_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class User
    {
        public string Name { get; private set; }
        public string Email { get; private set; }


        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string GetUserInfo()
        {

            return ($"{Name} + {Email}");
        }


    }
}
