using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private void Button_AddUser_Click(object sender, RoutedEventArgs e)
        {
            // Input already validated at this point.
            ListBox_Users.Items.Add(new User(TextBox_UserName.Text, TextBox_UserEmail.Text));
            TextBox_UserName.Clear();
            TextBox_UserEmail.Clear();
        }

        private void ListBox_Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox_Admins.SelectedIndex = -1;
            if(ListBox_Users.SelectedIndex >= 0)
            {
                Button_ElevateUser.IsEnabled = true;
                Button_EditUser.IsEnabled = true;
                Button_RemoveUser.IsEnabled = true;
            }
            else
            {
                Button_ElevateUser.IsEnabled = false;
                Button_EditUser.IsEnabled = false;
                Button_RemoveUser.IsEnabled = false;
            }
        }

        private void ListBox_Admins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox_Users.SelectedIndex = -1;
            if(ListBox_Admins.SelectedIndex >= 0)
            {
                Button_DemoteAdmin.IsEnabled = true;
                Button_EditUser.IsEnabled = true;
                Button_RemoveUser.IsEnabled = true;
            }
            else
            {
                Button_DemoteAdmin.IsEnabled = false;
                Button_EditUser.IsEnabled = false;
                Button_RemoveUser.IsEnabled = false;
            }
        }

        private void Button_ElevateUser_Click(object sender, RoutedEventArgs e)
        {
            (ListBox_Users.SelectedItem as User).Privilege = Privilege.Admin;
            ListBox_Admins.Items.Add(ListBox_Users.SelectedItem);
            ListBox_Users.Items.Remove(ListBox_Users.SelectedItem);
        }

        private void Button_DemoteAdmin_Click(object sender, RoutedEventArgs e)
        {
            (ListBox_Admins.SelectedItem as User).Privilege = Privilege.User;
            ListBox_Users.Items.Add(ListBox_Admins.SelectedItem);
            ListBox_Admins.Items.Remove(ListBox_Admins.SelectedItem);
        }

        private void TextBox_UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidateInputs(TextBox_UserName.Text, TextBox_UserEmail.Text))
                Button_AddUser.IsEnabled = true;
            else
                Button_AddUser.IsEnabled = false;
        }

        private void TextBox_UserEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidateInputs(TextBox_UserName.Text, TextBox_UserEmail.Text))
                Button_AddUser.IsEnabled = true;
            else
                Button_AddUser.IsEnabled = false;
        }

        private bool ValidateInputs(string username, string email)
        {
            if (!string.IsNullOrWhiteSpace(username) && CheckIfEmail(email))
                return true;
            else
                return false;
        }

        private bool CheckIfEmail(string input)
        {
            string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            if (Regex.IsMatch(input, pattern))
                return true;
            else
                return false;
        }
    }
}
