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
    /// Логика взаимодействия для TypeOfAttractionPage.xaml
    /// </summary>
    public partial class TypeOfAttractionPage : Page
    {
        private AmusementParkEntities context = new AmusementParkEntities();

        public TypeOfAttractionPage()
        {
            InitializeComponent();
            PositionsGrid.ItemsSource = context.AttractionType.ToList();

            TypeCB.ItemsSource = context.Attractions.ToList();
            TypeCB.DisplayMemberPath = "attraction_name";

        }
        private void PositionsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PositionsGrid.SelectedItem != null)
            {
                PositionNameBox.Text = (PositionsGrid.SelectedItem as AttractionType).attraction_type_name.ToString();
                TypeCB.SelectedItem = (PositionsGrid.SelectedItem as AttractionType).Attractions;
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AttractionType position = new AttractionType();
            if (PositionNameBox.Text != null && TypeCB.SelectedItem != null)
            {
                if (!ContainsEmojis(PositionNameBox.Text))
                {
                    position.attraction_type_name = PositionNameBox.Text;
                    position.attraction_id = (TypeCB.SelectedItem as Attractions).attraction_id;

                    context.AttractionType.Add(position);
                    context.SaveChanges();
                    PositionsGrid.ItemsSource = context.AttractionType.ToList();
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
                var position = PositionsGrid.SelectedItem as AttractionType;

                context.AttractionType.Remove(position);
                context.SaveChanges();

                PositionsGrid.ItemsSource = context.AttractionType.ToList();
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
                if (PositionNameBox.Text != null && TypeCB.SelectedItem != null)
                {
                    if (!ContainsEmojis(PositionNameBox.Text))
                    {

                        var position = PositionsGrid.SelectedItem as AttractionType;

                        position.attraction_type_name = PositionNameBox.Text;
                        position.attraction_id = (TypeCB.SelectedItem as Attractions).attraction_id;

                        context.SaveChanges();
                        PositionsGrid.ItemsSource = context.AttractionType.ToList();
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
