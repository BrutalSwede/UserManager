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
                    User selectedAdmin = (User)ListBox_Admins.SelectedItem;

                    selectedAdmin.Name = TextBox_UserName.Text;
                    selectedAdmin.Email = TextBox_UserEmail.Text;

                    ListBox_Admins.Items.Refresh();
                    UpdateInfoBox(ListBox_Admins);
                }
                else if (ListBox_Users.SelectedIndex >= 0)
                {
                    User selectedUser = (User)ListBox_Users.SelectedItem;

                    selectedUser.Name = TextBox_UserName.Text;
                    selectedUser.Email = TextBox_UserEmail.Text;

                    ListBox_Users.Items.Refresh();
                    UpdateInfoBox(ListBox_Users);
                }
                Button_AddUser.Content = "Add User";
                IsEditable = false;
            }
            else
            {
                ListBox_Users.Items.Add(new User(TextBox_UserName.Text, TextBox_UserEmail.Text));
            }

            TextBox_UserName.Clear();
            TextBox_UserEmail.Clear();
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
            ((User) ListBox_Users.SelectedItem).Privilege = Privilege.Admin;
            ListBox_Admins.Items.Add(ListBox_Users.SelectedItem);
            ListBox_Users.Items.Remove(ListBox_Users.SelectedItem);
        }

        private void Button_DemoteAdmin_Click(object sender, RoutedEventArgs e)
        {
            ((User) ListBox_Admins.SelectedItem).Privilege = Privilege.User;
            ListBox_Users.Items.Add(ListBox_Admins.SelectedItem);
            ListBox_Admins.Items.Remove(ListBox_Admins.SelectedItem);
        }

        private void TextBox_UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Button_AddUser.IsEnabled = ValidateInputs(TextBox_UserName.Text, TextBox_UserEmail.Text);
        }

        private void TextBox_UserEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            Button_AddUser.IsEnabled = ValidateInputs(TextBox_UserName.Text, TextBox_UserEmail.Text);
        }

        private static bool ValidateInputs(string username, string email)
        {
            return !string.IsNullOrWhiteSpace(username) && CheckIfEmail(email);
        }

        private static bool CheckIfEmail(string input)
        {
            const string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return Regex.IsMatch(input, pattern);
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
                User selectedAdmin = (User)ListBox_Admins.SelectedItem;

                TextBox_UserName.Text = selectedAdmin.Name;
                TextBox_UserEmail.Text = selectedAdmin.Email;
            }
            else if (ListBox_Users.SelectedIndex >= 0)
            {
                User selectedUser = (User)ListBox_Users.SelectedItem;

                TextBox_UserName.Text = selectedUser.Name;
                TextBox_UserEmail.Text = selectedUser.Email;

            }
        }

        public void UpdateInfoBox(ListBox listBox)
        {
            if (listBox.SelectedItem is User)
            {
                User currentSelected = (User)listBox.SelectedItem;
                Info_box.Content =
                    $"Name: {currentSelected.Name}" +
                    $"\nE-Mail: {currentSelected.Email}" +
                    $"\nStatus: {currentSelected.Privilege}";
            }
            else
            {
                Info_box.Content = "Name:\nE-Mail:\nStatus:";
            }
        }
    }
}
