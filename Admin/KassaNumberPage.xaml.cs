using System;
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
    /// Логика взаимодействия для KassaNumberPage.xaml
    /// </summary>
    public partial class KassaNumberPage : Page
    {
        private AmusementParkEntities context = new AmusementParkEntities();

        public KassaNumberPage()
        {
            InitializeComponent();
            PositionsGrid.ItemsSource = context.CashRegisterNumbers.ToList();

        }
        private void PositionsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PositionsGrid.SelectedItem != null)
            {
                PositionNameBox.Text = (PositionsGrid.SelectedItem as CashRegisterNumbers).register_number.ToString();
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CashRegisterNumbers position = new CashRegisterNumbers();
            if (PositionNameBox.Text != null)
            {
                if (PositionNameBox.Text.All(ch => char.IsLetterOrDigit(ch)) && !ContainsEmojis(PositionNameBox.Text))
                {
                    position.register_number = PositionNameBox.Text;

                    context.CashRegisterNumbers.Add(position);
                    context.SaveChanges();
                    PositionsGrid.ItemsSource = context.CashRegisterNumbers.ToList();
                }
                else
                {
                    MessageBox.Show("Номер кассы не должен содержать специальные символы");
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
                var position = PositionsGrid.SelectedItem as CashRegisterNumbers;
                if (!context.Cashiers.ToList().Any(x => x.register_number_id == position.register_number_id))
                {
                    context.CashRegisterNumbers.Remove(position);
                    context.SaveChanges();

                    PositionsGrid.ItemsSource = context.CashRegisterNumbers.ToList();
                }
                else
                {
                    MessageBox.Show("Существует кассир, работающий за этой кассой, удаление невозможно");
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
                        var position = PositionsGrid.SelectedItem as CashRegisterNumbers;
                        position.register_number = PositionNameBox.Text;

                        context.SaveChanges();
                        PositionsGrid.ItemsSource = context.CashRegisterNumbers.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Номер кассы не должен содержать специальные символы");
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
