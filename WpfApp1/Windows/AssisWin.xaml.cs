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
using static WpfApp1.AppData;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Логика взаимодействия для AssisWin.xaml
    /// </summary>
    public partial class AssisWin : Window
    {
        public AssisWin()
        {
            InitializeComponent();
            patientList.ItemsSource = entities.Patients.ToList();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из приложения?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void patientList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (patientList.SelectedItem is Patients patients)
            {
                AppWin appWin = new AppWin(patients);
                appWin.ShowDialog();
            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddPatient windowAddPatient = new WindowAddPatient();
            this.Close();
            windowAddPatient.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из учётной записи?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                Close();
                mainWindow.ShowDialog();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tabb = entities.Patients.ToList();
            if (FamSearch1.Text.Length == 0 && ImySearch1.Text.Length == 0)
            {
                patientList.ItemsSource = tabb.ToList();
                return;
            }
            else
            {
                var res = tabb.Where(i => i.Surname.Contains(FamSearch1.Text) &&
                                     i.Name.Contains(ImySearch1.Text)
                                     ).ToList();
                if (res.Count() != 0)
                {
                    patientList.ItemsSource = res;
                }
                else
                    MessageBox.Show(" Не найдено!", "Внимание!",
                        MessageBoxButton.OK, MessageBoxImage.Warning);


            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var tabb = entities.Patients.ToList();
            patientList.ItemsSource = tabb.ToList();
            return;
        }

        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            if (patientList.SelectedItem is Patients selectedPatient)
            {
                WindowAddPatient windowAddPatient = new WindowAddPatient(selectedPatient);
                this.Close();
                windowAddPatient.ShowDialog();
            }
        }
    }
}
