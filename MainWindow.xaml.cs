using EducPractVer2._00.Admin;
using EducPractVer2._00.Cashier;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EducPractVer2._00
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AmusementParkEntities context = new AmusementParkEntities();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            bool show1 = true;
            var allogins = context.AuthorizationData.ToList();
            foreach (var account in allogins)
            {
                if (account.logins == LoginBox.Text && account.passwords == PasswordBX.Password)
                {
                    show1 = false;
                    switch (account.role_id)
                    {
                        case 1:
                            AdminWindow adminWindow = new AdminWindow();
                            adminWindow.Show();
                            this.Close();
                            break;
                        case 2:
                            CashierWindow cashier = new CashierWindow(account);
                            cashier.Show();
                            this.Close();
                            break;
                        default:
                            MessageBox.Show("Такое окно ещё не разработано");
                            break;
                    }
                }
            }
            if (show1)
            {
                MessageBox.Show("Неверный логин или пароль");
            }

        }
    }
}
