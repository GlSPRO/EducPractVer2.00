using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace EducPractVer2._00.Admin
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        private AmusementParkEntities context = new AmusementParkEntities();   

        public AuthorizationPage()
        {
            InitializeComponent();
            UsersGrid.ItemsSource = context.AuthorizationData.ToList();
            StaffCB.ItemsSource = context.CashierRoles.ToList();
            StaffCB.DisplayMemberPath = "role_name";

        }

        private void UsersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersGrid.SelectedItem != null)
            {
                LoginBox.Text = (UsersGrid.SelectedItem as AuthorizationData).logins.ToString();
                PasswordBox.Password = (UsersGrid.SelectedItem as AuthorizationData).logins.ToString();
                StaffCB.SelectedItem = (UsersGrid.SelectedItem as AuthorizationData).CashierRoles;
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationData user = new AuthorizationData();
            if (LoginBox.Text != null && PasswordBox.Password != null
                && StaffCB.SelectedItem != null)
            {
                if (PasswordBox.Password.Length >= 5 && !ContainsEmojis(LoginBox.Text) && !ContainsEmojis(PasswordBox.Password))
                {
                    if (!context.AuthorizationData.ToList().Any(x => x.logins == LoginBox.Text))
                    {
                        user.logins = LoginBox.Text;
                        user.passwords = PasswordBox.Password;
                        user.role_id = (StaffCB.SelectedItem as CashierRoles).role_id;
                        context.AuthorizationData.Add(user);
                        context.SaveChanges();
                        UsersGrid.ItemsSource = context.AuthorizationData.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Такой логин уже существует");
                    }
                }
                else
                {
                    MessageBox.Show("Пароль должен быть длинее 5, не должно быть специальных символов");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem != null)
            {
                var user = UsersGrid.SelectedItem as AuthorizationData;
                if (!(user.CashierRoles.role_id == 1))
                {
                    context.AuthorizationData.Remove(user);
                    context.SaveChanges();

                    UsersGrid.ItemsSource = context.AuthorizationData.ToList();
                }
                else
                {
                    MessageBox.Show("Удаление пользователей с ролью Менеджер запрещено");
                }
            }
            else
            {
                MessageBox.Show("Выберете элемент для удаления");
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem != null)
            {
                var user = UsersGrid.SelectedItem as AuthorizationData;

                if (LoginBox.Text != null && PasswordBox.Password != null
                && StaffCB.SelectedItem != null)
                {
                    if (PasswordBox.Password.Length >= 5 && !ContainsEmojis(LoginBox.Text) && !ContainsEmojis(PasswordBox.Password))
                    {
                        if (!context.AuthorizationData.ToList().Any(x => x.logins == LoginBox.Text) || LoginBox.Text == user.logins)
                        {

                            user.logins = LoginBox.Text;
                            user.passwords = PasswordBox.Password;
                            user.role_id = (StaffCB.SelectedItem as CashierRoles).role_id;

                            context.SaveChanges();
                            UsersGrid.ItemsSource = context.AuthorizationData.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Такой логин уже существует");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароль должен быть длинее 5");
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                }
            }
        }
        private bool ContainsEmojis(string input)
        {
            Regex rgx = new Regex(@"\p{Cs}");

            if (rgx.IsMatch(input))
            {
                return true;
            }
            return false;
        }
    }
}
