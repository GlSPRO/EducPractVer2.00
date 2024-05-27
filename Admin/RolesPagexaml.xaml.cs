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

namespace EducPractVer2._00.Admin
{
    /// <summary>
    /// Логика взаимодействия для RolesPagexaml.xaml
    /// </summary>
    public partial class RolesPagexaml : Page
    {
        private AmusementParkEntities context = new AmusementParkEntities();
        public RolesPagexaml()
        {
            InitializeComponent();
            PositionsGrid.ItemsSource = context.CashierRoles.ToList();

        }
        private void PositionsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PositionsGrid.SelectedItem != null)
            {
                PositionNameBox.Text = (PositionsGrid.SelectedItem as CashierRoles).role_name.ToString();
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CashierRoles position = new CashierRoles();
            if (PositionNameBox.Text != null)
            {
                if (PositionNameBox.Text.All(ch => char.IsLetterOrDigit(ch)) && !ContainsEmojis(PositionNameBox.Text))
                {
                    position.role_name = PositionNameBox.Text;

                    context.CashierRoles.Add(position);
                    context.SaveChanges();
                    PositionsGrid.ItemsSource = context.CashierRoles.ToList();                    
                }
                else
                {
                    MessageBox.Show("Название не должно содержать специальных символов");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PositionsGrid.SelectedItem != null)
            {
                var position = PositionsGrid.SelectedItem as CashierRoles;
                if (!context.Cashiers.ToList().Any(x => x.role_id == position.role_id))
                {
                    context.CashierRoles.Remove(position);
                    context.SaveChanges();

                    PositionsGrid.ItemsSource = context.CashierRoles.ToList();
                }
                else
                {
                    MessageBox.Show("Существует сотрудник с данной должностью, удаление невозможно");
                }
            }
            else
            {
                MessageBox.Show("Выберете элемент для удаления");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (PositionsGrid.SelectedItem != null)
            {
                if (PositionNameBox.Text != null)
                {
                    if (PositionNameBox.Text.All(ch => char.IsLetterOrDigit(ch)) && !ContainsEmojis(PositionNameBox.Text))
                    {

                        var position = PositionsGrid.SelectedItem as CashierRoles;

                        position.role_name = PositionNameBox.Text;

                        context.SaveChanges();
                        PositionsGrid.ItemsSource = context.CashierRoles.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Название не должно содержать специальных символов");
                    }

                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                }
            }
            else
            {
                MessageBox.Show("Выберете элемент для изменения");
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<RolesJSON> positions = SerDeser.DeserialiseObject<List<RolesJSON>>();
                foreach (var position in positions)
                {
                    if (position == null) {
                        MessageBox.Show("Не тот формат");
                        return;
                    }
                    var newposition = new CashierRoles();
                    newposition.role_name = position.role_name;

                    context.CashierRoles.Add(newposition);
                    context.SaveChanges();
                    

                }
                PositionsGrid.ItemsSource = null;
                PositionsGrid.ItemsSource = context.CashierRoles.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Не тот формат");
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
