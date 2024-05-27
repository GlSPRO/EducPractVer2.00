using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для ChekPage.xaml
    /// </summary>
    public partial class ChekPage : Page
    {
        private AmusementParkEntities context = new AmusementParkEntities();

        public ChekPage()
        {
            InitializeComponent();
            DateCB.ItemsSource = context.Orders.ToList();
            DateCB.DisplayMemberPath = "purchase_date";
        }

        private void DateCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateCB.SelectedItem != null)
            {
                var order = (DateCB.SelectedItem as Orders);
                List<Attractions> products = new List<Attractions>();
                context.Receipt.ToList().Where(item => item.order_id == order.order_id).ToList().ForEach(item => products.Add(item.Attractions));
                ProductsGrid.ItemsSource = products;

                StaffBox.Content = order.Cashiers.first_name.ToString() + " " + order.Cashiers.last_name.ToString();
                DateBox.Content = order.purchase_date.ToString();
                SummBox.Content = order.price.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DateCB.SelectedItem != null)
            {
                var order = (DateCB.SelectedItem as Orders);
                List<Attractions> products = new List<Attractions>();

                context.Receipt.ToList().Where(item => item.order_id == order.order_id).ToList().ForEach(item => products.Add(item.Attractions));

                string goods = "";
                decimal? total = 0;
                foreach (Attractions product in products)
                {
                    goods += "\n\t" + product.attraction_name + "\t-\t" + product.price;
                    total += product.price;
                }

                var dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;
                var result = dialog.ShowDialog();
                if (result == CommonFileDialogResult.Ok)
                {
                    var path = dialog.FileName + "\\Check" + order.order_id.ToString() + ".txt";
                    string checkdesc = "\t   Amusement Park\r\n\t  Кассовый чек №" + order.order_id + "\r\n\r" + goods + "\n\r\nИтого к оплате: " + total + "\r\nС вычетом скидки: " + order.price;
                    File.WriteAllText(path, checkdesc);
                }
            }
        }
    }
}
