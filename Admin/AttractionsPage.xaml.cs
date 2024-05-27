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
    /// Логика взаимодействия для AttractionsPage.xaml
    /// </summary>
    public partial class AttractionsPage : Page
    {
        private AmusementParkEntities context = new AmusementParkEntities();

        public AttractionsPage()
        {
            InitializeComponent();
            ProductsGrid.ItemsSource = context.Attractions.ToList();

            CategoryCB.ItemsSource = context.FearLevels.ToList();
            CategoryCB.DisplayMemberPath = "fear";
        }

        private void ProductsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsGrid.SelectedItem != null)
            {
                TitleBox.Text = (ProductsGrid.SelectedItem as Attractions).attraction_name.ToString();
                PriceBox.Text = (ProductsGrid.SelectedItem as Attractions).price.ToString();
                CategoryCB.SelectedItem = (ProductsGrid.SelectedItem as Attractions).FearLevels;
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Attractions product = new Attractions();
            if (TitleBox.Text != null && PriceBox.Text != null
                 && CategoryCB.SelectedItem != null)
            {
                if (Decimal.TryParse(PriceBox.Text, out decimal price) && !ContainsEmojis(TitleBox.Text))
                {
                    if (price > 0 && price < 10000)
                    {
                        if (!context.Attractions.ToList().Any(x => x.attraction_name == TitleBox.Text))
                        {

                            product.attraction_name = TitleBox.Text;
                            product.price = Math.Round(price * 100) / 100;
                            product.fear_level_id = (CategoryCB.SelectedItem as FearLevels).fear_level_id;
                            

                            context.Attractions.Add(product);
                            context.SaveChanges();
                            ProductsGrid.ItemsSource = context.Attractions.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Такой аттракцион уже существует");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Цена должна быть больше нуля и меньше 10 тысяч");
                    }
                }
                else
                {
                    MessageBox.Show("Цена введена в неверном формате, название не должно содеражть спец. сиволы");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem != null)
            {
                var product = ProductsGrid.SelectedItem as Attractions;
                if (!context.Receipt.ToList().Any(x => x.attraction_id == product.attraction_id) && !context.AttractionType.ToList().Any(x => x.attraction_id == product.attraction_id))
                {
                    context.Attractions.Remove(product);
                    context.SaveChanges();

                    ProductsGrid.ItemsSource = context.Attractions.ToList();
                }
                else
                {
                    MessageBox.Show("Данный аттракцион связан с другими данными, удаление невозможно. (см чеки, типы аттракционов)");
                }
            }
            else
            {
                MessageBox.Show("Выберете элемент для удаления");
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem != null)
            {
                if (TitleBox.Text != null && PriceBox.Text != null
                    && CategoryCB.SelectedItem != null)
                {
                    if (Decimal.TryParse(PriceBox.Text, out decimal price) && !ContainsEmojis(TitleBox.Text))
                    {
                        if (price > 0 && price < 10000)
                        {
                            var product = ProductsGrid.SelectedItem as Attractions;
                            if (!context.Attractions.ToList().Any(x => x.attraction_name == TitleBox.Text) || TitleBox.Text == product.attraction_name)
                            {
                                product.attraction_name = TitleBox.Text;
                                product.price = Math.Round(price * 100) / 100;
                                product.fear_level_id = (CategoryCB.SelectedItem as FearLevels).fear_level_id;


                                context.SaveChanges();
                                ProductsGrid.ItemsSource = context.Attractions.ToList();
                            }
                            else
                            {
                                MessageBox.Show("Такой аттракцион уже существует");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Цена должна быть больше нуля и меньше 10 тысяч");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Цена введена в неверном формате, название не должно содеражть спец. сиволы");
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                }
            }
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<AttractionsJSON> products = SerDeser.DeserialiseObject<List<AttractionsJSON>>();
                foreach (var product in products)
                {
                    if (product == null)
                    {
                        MessageBox.Show("Не тот формат");
                        return;
                    }
                    var newproduct = new Attractions();
                    newproduct.attraction_name = product.attraction_Name;
                    newproduct.price = product.price;
                    newproduct.fear_level_id = product.fear_level_id;

                    context.Attractions.Add(newproduct);
                    context.SaveChanges();
                }
                ProductsGrid.ItemsSource = null;
                ProductsGrid.ItemsSource = context.Attractions.ToList();

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
