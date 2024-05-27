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

namespace EducPractVer2._00.Admin
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        static private SolidColorBrush backLightDef = new SolidColorBrush{ Color = Color.FromRgb(168, 129, 115) };
        static private SolidColorBrush backMidDef = new SolidColorBrush { Color = Color.FromRgb(121, 85, 72) };
        static private SolidColorBrush backDarkDef = new SolidColorBrush { Color = Color.FromRgb(75, 44, 32) };
        static private SolidColorBrush foreDef = new SolidColorBrush { Color = Color.FromRgb(233, 209, 182) };
        static private SolidColorBrush backAct = new SolidColorBrush { Color = Color.FromRgb(255, 171, 0) };
        static private SolidColorBrush foreAct = new SolidColorBrush { Color = Color.FromRgb(49, 29, 13) };

        public AdminWindow()
        {
            InitializeComponent();
        }
        private void PositionsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new RolesPagexaml();
            PositionsButton.Background = backAct;
            StaffButton.Background = backMidDef;
            UsersButton.Background = backDarkDef;

            PositionsButton.Foreground = foreAct;
            StaffButton.Foreground = foreDef;
            UsersButton.Foreground = foreDef;

            AttractionsButton.Background = backLightDef;
            FearButton.Background = backMidDef;
            KassaButton.Background = backDarkDef;
            TypeButton.Background = backMidDef;

            AttractionsButton.Foreground = foreDef;
            FearButton.Foreground = foreDef;
            KassaButton.Foreground = foreDef;
            TypeButton.Foreground = foreDef;

        }

        private void StaffButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new CahiersPage();

            PositionsButton.Background = backLightDef;
            StaffButton.Background = backAct;
            UsersButton.Background = backDarkDef;

            PositionsButton.Foreground = foreDef;
            StaffButton.Foreground = foreAct;
            UsersButton.Foreground = foreDef;

            AttractionsButton.Background = backLightDef;
            FearButton.Background = backMidDef;
            KassaButton.Background = backDarkDef;
            TypeButton.Background = backMidDef;

            AttractionsButton.Foreground = foreDef;
            FearButton.Foreground = foreDef;
            KassaButton.Foreground = foreDef;
            TypeButton.Foreground = foreDef;
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new AuthorizationPage();

            PositionsButton.Background = backLightDef;
            StaffButton.Background = backMidDef;
            UsersButton.Background = backAct;

            PositionsButton.Foreground = foreDef;
            StaffButton.Foreground = foreDef;
            UsersButton.Foreground = foreAct;

            AttractionsButton.Background = backLightDef;
            FearButton.Background = backMidDef;
            KassaButton.Background = backDarkDef;
            TypeButton.Background = backMidDef;

            AttractionsButton.Foreground = foreDef;
            FearButton.Foreground = foreDef;
            KassaButton.Foreground = foreDef;
            TypeButton.Foreground = foreDef;

        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void AttractionsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new AttractionsPage();

            PositionsButton.Background = backLightDef;
            StaffButton.Background = backMidDef;
            UsersButton.Background = backDarkDef;

            PositionsButton.Foreground = foreDef;
            StaffButton.Foreground = foreDef;
            UsersButton.Foreground = foreDef;

            AttractionsButton.Background = backAct;
            FearButton.Background = backMidDef;
            KassaButton.Background = backDarkDef;
            TypeButton.Background = backMidDef;

            AttractionsButton.Foreground = foreAct;
            FearButton.Foreground = foreDef;
            KassaButton.Foreground = foreDef;
            TypeButton.Foreground = foreDef;
        }

        private void FearButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new FearLevelPage();

            PositionsButton.Background = backLightDef;
            StaffButton.Background = backMidDef;
            UsersButton.Background = backDarkDef;

            PositionsButton.Foreground = foreDef;
            StaffButton.Foreground = foreDef;
            UsersButton.Foreground = foreDef;

            AttractionsButton.Background = backLightDef;
            FearButton.Background = backAct;
            KassaButton.Background = backDarkDef;
            TypeButton.Background = backMidDef;

            AttractionsButton.Foreground = foreDef;
            FearButton.Foreground = foreAct;
            KassaButton.Foreground = foreDef;
            TypeButton.Foreground = foreDef;
        }

        private void KassaButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new KassaNumberPage();

            PositionsButton.Background = backLightDef;
            StaffButton.Background = backMidDef;
            UsersButton.Background = backDarkDef;

            PositionsButton.Foreground = foreDef;
            StaffButton.Foreground = foreDef;
            UsersButton.Foreground = foreDef;

            AttractionsButton.Background = backLightDef;
            FearButton.Background = backMidDef;
            KassaButton.Background = backAct;
            TypeButton.Background = backMidDef;

            AttractionsButton.Foreground = foreDef;
            FearButton.Foreground = foreDef;
            KassaButton.Foreground = foreAct;
            TypeButton.Foreground = foreDef;
        }

        private void TypeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new TypeOfAttractionPage();

            PositionsButton.Background = backLightDef;
            StaffButton.Background = backMidDef;
            UsersButton.Background = backDarkDef;

            PositionsButton.Foreground = foreDef;
            StaffButton.Foreground = foreDef;
            UsersButton.Foreground = foreDef;

            AttractionsButton.Background = backLightDef;
            FearButton.Background = backMidDef;
            KassaButton.Background = backDarkDef;
            TypeButton.Background = backAct;

            AttractionsButton.Foreground = foreDef;
            FearButton.Foreground = foreDef;
            KassaButton.Foreground = foreDef;
            TypeButton.Foreground = foreAct;
        }
    }
}
