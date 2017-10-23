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
        public bool IsEditable = false;
        private void Button_AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (IsEditable)
            {
                if (ListBox_Admins.SelectedIndex >= 0)
                {
                    User SelectedAdmin = (User)ListBox_Admins.SelectedItem;

                    SelectedAdmin.Name = TextBox_UserName.Text;
                    SelectedAdmin.Email = TextBox_UserEmail.Text;

                    ListBox_Admins.Items.Refresh();
                }
                else if (ListBox_Users.SelectedIndex >= 0)
                {
                    User SelectedUser = (User)ListBox_Users.SelectedItem;

                    SelectedUser.Name = TextBox_UserName.Text;
                    SelectedUser.Email = TextBox_UserEmail.Text;

                    ListBox_Users.Items.Refresh();
                }
            }
            else
            {
                ListBox_Users.Items.Add(new User(TextBox_UserName.Text, TextBox_UserEmail.Text));
            }

            TextBox_UserName.Clear();
            TextBox_UserEmail.Clear();
            // Input already validated at this point.

        }

        private void ListBox_Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox_Admins.SelectedIndex = -1;

            if (ListBox_Users.SelectedIndex >= 0)
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

            UpdateInfoBox(ListBox_Users);
        }

        private void ListBox_Admins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox_Users.SelectedIndex = -1;
            if (ListBox_Admins.SelectedIndex >= 0)
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

            UpdateInfoBox(ListBox_Admins);
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

        private void Button_RemoveUser_Click(object sender, RoutedEventArgs e)
        {

            if (ListBox_Users.SelectedItem is User)
            {
                ListBox_Users.Items.Remove(ListBox_Users.SelectedItem);
            }

            if (ListBox_Admins.SelectedItem is User)
            {
                ListBox_Admins.Items.Remove(ListBox_Admins.SelectedItem);
            }
        }

        private void Button_EditUser_Click(object sender, RoutedEventArgs e)
        {
            Button_AddUser.Content = "Apply";

            IsEditable = true;

            if (ListBox_Admins.SelectedIndex >= 0)
            {
                User SelectedAdmin = (User)ListBox_Admins.SelectedItem;

                TextBox_UserName.Text = SelectedAdmin.Name;
                TextBox_UserEmail.Text = SelectedAdmin.Email;
            }
            else if (ListBox_Users.SelectedIndex >= 0)
            {
                User SelectedUser = (User)ListBox_Users.SelectedItem;

                TextBox_UserName.Text = SelectedUser.Name;
                TextBox_UserEmail.Text = SelectedUser.Email;

            }
        }

        public void UpdateInfoBox(ListBox listBox)
        {
            if (listBox.SelectedItem is User)
            {
                User CurrentSelected = (User)listBox.SelectedItem;
                Info_box.Content =
                    $"Name: {CurrentSelected.Name}" +
                    $"\nE-Mail: {CurrentSelected.Email}" +
                    $"\nStatus: {CurrentSelected.Privilege}";
            }
            else
            {
                Info_box.Content = "Name:\nE-Mail:\nStatus:";
            }
        }
    }
}
