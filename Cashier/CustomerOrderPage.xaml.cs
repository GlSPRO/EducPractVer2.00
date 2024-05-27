using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.IO;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для CustomerOrderPage.xaml
    /// </summary>
    public partial class CustomerOrderPage : Page
    {
        private AmusementParkEntities context = new AmusementParkEntities();
        static public List<Attractions> products = new List<Attractions>();
        static decimal? totalsumm;
        static AuthorizationData account = new AuthorizationData();

        public CustomerOrderPage(AuthorizationData user)
        {
            InitializeComponent();
            ProductsGrid.ItemsSource = context.Attractions.ToList();

            DiscountCB.ItemsSource = context.Discounts.ToList();
            DiscountCB.DisplayMemberPath = "discount_size";
            CashierCB.ItemsSource = context.Cashiers.ToList();
            account = user;
            totalsumm = 0;
            products.Clear();
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            decimal discount = 0;
            if (DiscountCB.SelectedItem != null)
            {
                discount = (decimal)(DiscountCB.SelectedItem as Discounts).discount_size;
            }
            if (ProductsGrid.SelectedItem != null)
            {
                products.Add(ProductsGrid.SelectedItem as Attractions);
                totalsumm += (ProductsGrid.SelectedItem as Attractions).price;

                TotalBox.Content = " Товары в чеке      Сумма: " + (totalsumm - totalsumm * discount);
                ChoiceGrid.ItemsSource = null;
                ChoiceGrid.ItemsSource = products;
            }
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            decimal discount = 0;
            if (DiscountCB.SelectedItem != null)
            {
                discount = (decimal)(DiscountCB.SelectedItem as Discounts).discount_size;
            }
            if (ProductsGrid.SelectedItem != null)
            {
                if (products.Contains(ProductsGrid.SelectedItem as Attractions))
                {
                    products.Remove(ProductsGrid.SelectedItem as Attractions);
                    totalsumm -= (ProductsGrid.SelectedItem as Attractions).price;

                    TotalBox.Content = " Товары в чеке      Сумма: " + (totalsumm - totalsumm * discount);
                    ChoiceGrid.ItemsSource = null;
                    ChoiceGrid.ItemsSource = products;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (products.Count != 0 && CashierCB.SelectedItem != null)
            {

                Orders order = new Orders();

                decimal discount = 0;
                if (DiscountCB.SelectedItem != null)
                {
                    discount = (decimal)(DiscountCB.SelectedItem as Discounts).discount_size;
                }
                order.purchase_date = DateTime.Now;
                order.price = (totalsumm - totalsumm * discount);
                order.cashier_id = (CashierCB.SelectedItem as Cashiers).cashier_id;
                if(DiscountCB.SelectedItem != null)
                {
                    order.discount_id = (DiscountCB.SelectedItem as Discounts).discount_id;
                }

                context.Orders.Add(order);
                context.SaveChanges();

                string goods = "";
                decimal? total =0;
                foreach (Attractions product in products)
                {
                    Receipt check = new Receipt();
                    check.attraction_id = product.attraction_id;
                    check.order_id = order.order_id;
                    context.Receipt.Add(check);
                    context.SaveChanges();


                    total += product.price;
                    goods += "\n\t" + product.attraction_name + "\t-\t" + product.price;

                }

                var dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;
                var result = dialog.ShowDialog();
                if (result == CommonFileDialogResult.Ok)
                {
                    var path = dialog.FileName + "\\Check" + order.order_id.ToString() + ".txt";
                    string checkdesc = "\t   Amusement Park\r\n\t  Кассовый чек №" + order.order_id + "\r\n\r" + goods + "\n\r\nИтого к оплате: " + total + "\r\nС вычетом чкидки: " + order.price + "";
                    File.WriteAllText(path, checkdesc);
                }
                    
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }

        private void DiscountCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            decimal discount = 0;
            if (DiscountCB.SelectedItem != null)
            {
                discount = (decimal)(DiscountCB.SelectedItem as Discounts).discount_size;
                TotalBox.Content = " Товары в чеке      Сумма: " + (totalsumm - totalsumm * discount);
            }
        }
    }
}
