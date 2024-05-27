using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace EducPractVer2._00.Cashier
{
    /// <summary>
    /// Логика взаимодействия для DiscountPage.xaml
    /// </summary>
    public partial class DiscountPage : Page
    {
        private AmusementParkEntities context = new AmusementParkEntities();

        public DiscountPage()
        {
            InitializeComponent();
            PositionsGrid.ItemsSource = context.Discounts.ToList();

        }
        private void PositionsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PositionsGrid.SelectedItem != null)
            {
                PositionNameBox.Text = (PositionsGrid.SelectedItem as Discounts).discount_size.ToString();
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Discounts position = new Discounts();
            if (PositionNameBox.Text != null)
            {
                if (Decimal.TryParse(PositionNameBox.Text, out decimal discount))
                {
                    position.discount_size = Math.Round(discount * 100) / 100;

                    context.Discounts.Add(position);
                    context.SaveChanges();
                    PositionsGrid.ItemsSource = context.Discounts.ToList();
                }
                else
                {
                    MessageBox.Show("Скидка должна быть числом");
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
                var position = PositionsGrid.SelectedItem as Discounts;
                if (!context.Orders.ToList().Any(x => x.discount_id == position.discount_id))
                {
                    context.Discounts.Remove(position);
                    context.SaveChanges();

                    PositionsGrid.ItemsSource = context.Discounts.ToList();
                }
                else
                {
                    MessageBox.Show("Существует заказ с такой скидкой, удаление невозможно");
                }
            }
            else
            {
                MessageBox.Show("Выберете элемент для удаления");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (PositionNameBox.Text != null)
            {
                if (Decimal.TryParse(PositionNameBox.Text, out decimal discount))
                {
                    var position = PositionsGrid.SelectedItem as Discounts;

                    position.discount_size = discount;

                    context.SaveChanges();
                    PositionsGrid.ItemsSource = context.Discounts.ToList();
                }
                else
                {
                    MessageBox.Show("Скидка должна быть числом");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
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
