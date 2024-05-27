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
    /// Логика взаимодействия для CahiersPage.xaml
    /// </summary>
    public partial class CahiersPage : Page
    {
        private AmusementParkEntities context = new AmusementParkEntities();

        public CahiersPage()
        {
            InitializeComponent();
            StaffGrid.ItemsSource = context.Cashiers.ToList();

            PositionCB.ItemsSource = context.CashierRoles.ToList();
            PositionCB.DisplayMemberPath = "role_name";
            CassaCB.ItemsSource = context.CashRegisterNumbers.ToList();
            CassaCB.DisplayMemberPath = "register_number";

        }

        private void StaffGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StaffGrid.SelectedItem != null)
            {
                SurnameBox.Text = (StaffGrid.SelectedItem as Cashiers).last_name.ToString();
                NameBox.Text = (StaffGrid.SelectedItem as Cashiers).first_name.ToString();
                PositionCB.SelectedItem = (StaffGrid.SelectedItem as Cashiers).CashierRoles;
                CassaCB.SelectedItem = (StaffGrid.SelectedItem as Cashiers).CashRegisterNumbers;
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Cashiers employee = new Cashiers();
            if (SurnameBox.Text != null && NameBox.Text != null
                && CassaCB.SelectedItem != null && PositionCB.SelectedItem != null)
            {
                if (SurnameBox.Text.All(ch => char.IsLetter(ch))
                    && NameBox.Text.All(ch => char.IsLetter(ch)) && !ContainsEmojis(SurnameBox.Text) && !ContainsEmojis(NameBox.Text))
                {

                    employee.last_name = SurnameBox.Text;
                    employee.first_name = NameBox.Text;
                    employee.role_id = (PositionCB.SelectedItem as CashierRoles).role_id;
                    employee.register_number_id = (CassaCB.SelectedItem as CashRegisterNumbers).register_number_id;
                    context.Cashiers.Add(employee);
                    context.SaveChanges();
                    StaffGrid.ItemsSource = context.Cashiers.ToList();
                }
                else
                {
                    MessageBox.Show("ФИО должно состоять только из букв");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (StaffGrid.SelectedItem != null)
            {
                var employee = StaffGrid.SelectedItem as Cashiers;
                if ( !context.Orders.ToList().Any(x => x.cashier_id == employee.cashier_id))
                {
                    context.Cashiers.Remove(employee);
                    context.SaveChanges();

                    StaffGrid.ItemsSource = context.Cashiers.ToList();
                }
                else
                {
                    MessageBox.Show("Данный сотрудник связан с другими данныйми, удаление невозможно\n(см. Заказы)");
                }
            }
            else
            {
                MessageBox.Show("Выберете элемент для удаления");
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (StaffGrid.SelectedItem != null)
            {
                if (SurnameBox.Text != null && NameBox.Text != null
                    && CassaCB.SelectedItem != null && PositionCB.SelectedItem != null)
                {
                    if (SurnameBox.Text.All(ch => char.IsLetter(ch))
                        && NameBox.Text.All(ch => char.IsLetter(ch)) && !ContainsEmojis(SurnameBox.Text) && !ContainsEmojis(NameBox.Text))
                    {
                        var employee = StaffGrid.SelectedItem as Cashiers;

                        employee.last_name = SurnameBox.Text;
                        employee.first_name = NameBox.Text;
                        employee.role_id = (PositionCB.SelectedItem as CashierRoles).role_id;
                        employee.register_number_id = (CassaCB.SelectedItem as CashRegisterNumbers).register_number_id;
                        context.SaveChanges();
                        StaffGrid.ItemsSource = context.Cashiers.ToList();
                    }
                    else
                    {
                        MessageBox.Show("ФИО должно состоять только из букв");
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
