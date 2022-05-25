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
    /// Логика взаимодействия для DocWin.xaml
    /// </summary>
    public partial class DocWin : Window
    {
        public DocWin(Doctors authDoc)
        {
            InitializeComponent();
            Update(authDoc);
        }

        private void Update(Doctors authDoc)
        {
            var patients = entities.Appointment.Where(i => i.IDDoc == authDoc.IDDoc).ToList();
            patientList.ItemsSource = patients;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {

        }

        private void patientList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void patientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void nextList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
