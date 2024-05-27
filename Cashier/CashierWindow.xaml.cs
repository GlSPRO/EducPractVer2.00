using EducPractVer2._00.Admin;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace EducPractVer2._00.Cashier
{
    /// <summary>
    /// Логика взаимодействия для CashierWindow.xaml
    /// </summary>
    public partial class CashierWindow : Window
    {
        static private SolidColorBrush backLightDef = new SolidColorBrush { Color = Color.FromRgb(168, 129, 115) };
        static private SolidColorBrush backMidDef = new SolidColorBrush { Color = Color.FromRgb(121, 85, 72) };
        static private SolidColorBrush backDarkDef = new SolidColorBrush { Color = Color.FromRgb(75, 44, 32) };
        static private SolidColorBrush foreDef = new SolidColorBrush { Color = Color.FromRgb(233, 209, 182) };
        static private SolidColorBrush backAct = new SolidColorBrush { Color = Color.FromRgb(255, 171, 0) };
        static private SolidColorBrush foreAct = new SolidColorBrush { Color = Color.FromRgb(49, 29, 13) };
        private AuthorizationData account;
        public CashierWindow(AuthorizationData account)
        {
            InitializeComponent();
            this.account = account;
        }

        private void DiscountButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new DiscountPage();
            DiscountButton.Background = backAct;
            OrdersButton.Background = backMidDef;
            ChecksButton.Background = backDarkDef;

            DiscountButton.Foreground = foreAct;
            OrdersButton.Foreground = foreDef;
            ChecksButton.Foreground = foreDef;
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new CustomerOrderPage(account);

            DiscountButton.Background = backLightDef;
            OrdersButton.Background = backAct;
            ChecksButton.Background = backDarkDef;

            DiscountButton.Foreground = foreDef;
            OrdersButton.Foreground = foreAct;
            ChecksButton.Foreground = foreDef;
        }

        private void ChecksButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ChekPage();

            DiscountButton.Background = backLightDef;
            OrdersButton.Background = backMidDef;
            ChecksButton.Background = backAct;

            DiscountButton.Foreground = foreDef;
            OrdersButton.Foreground = foreDef;
            ChecksButton.Foreground = foreAct;
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
