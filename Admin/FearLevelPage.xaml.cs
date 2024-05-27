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
    /// Логика взаимодействия для FearLevelPage.xaml
    /// </summary>
    public partial class FearLevelPage : Page
    {   
        private AmusementParkEntities context = new AmusementParkEntities();

        public FearLevelPage() 
        { 
            InitializeComponent();
            PositionsGrid.ItemsSource = context.FearLevels.ToList();

        }
        private void PositionsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PositionsGrid.SelectedItem != null)
            {
                PositionNameBox.Text = (PositionsGrid.SelectedItem as FearLevels).fear.ToString();
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            FearLevels position = new FearLevels();
            if (PositionNameBox.Text != null)
            {
                if (PositionNameBox.Text.All(ch => char.IsLetterOrDigit(ch)) && !ContainsEmojis(PositionNameBox.Text))
                {
                    position.fear = PositionNameBox.Text;

                    context.FearLevels.Add(position);
                    context.SaveChanges();
                    PositionsGrid.ItemsSource = context.FearLevels.ToList();
                }
                else
                {
                    MessageBox.Show("Уровень страха должен содержать только буквы и цифры");
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
                var position = PositionsGrid.SelectedItem as FearLevels;
                if (!context.Attractions.ToList().Any(x => x.fear_level_id == position.fear_level_id))
                {
                    context.FearLevels.Remove(position);
                    context.SaveChanges();

                    PositionsGrid.ItemsSource = context.FearLevels.ToList();
                }
                else
                {
                    MessageBox.Show("Существует аттракцион с таким уровнем страха, удаление невозможно");
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
                if (PositionNameBox.Text.All(ch => char.IsLetterOrDigit(ch)) && !ContainsEmojis(PositionNameBox.Text))
                {
                    var position = PositionsGrid.SelectedItem as FearLevels;

                        position.fear = PositionNameBox.Text;

                        context.SaveChanges();
                        PositionsGrid.ItemsSource = context.FearLevels.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Уровень страха должен содержать только буквы и цифры");
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
