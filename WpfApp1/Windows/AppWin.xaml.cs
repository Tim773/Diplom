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
    /// Логика взаимодействия для AppWin.xaml
    /// </summary>
    public partial class AppWin : Window
    {
        Patients selectedPatient;
        

        public AppWin(Patients patient)
        {
            InitializeComponent();
            selectedPatient = patient;
        }
        public int selectedSpec = 0;
      

        private void cbSpecialization_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedSpec = cbSpecialization.SelectedIndex;
            Update();
        }

        private void cbDoctors_Loaded(object sender, RoutedEventArgs e)
        {
            cbDoctors.ItemsSource = entities.Doctors.Where(i => i.IDSpecialization == selectedSpec).ToList();            
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbSpecialization_Loaded(object sender, RoutedEventArgs e)
        {
            var itemsours = entities.Specialization.ToList();
            cbSpecialization.ItemsSource = itemsours;
        }

        private void Update()
        {
            cbDoctors.ItemsSource = entities.Doctors.Where(i => i.IDSector == selectedPatient.IDSector && i.IDSpecialization == selectedSpec).ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            entities.Appointment.Add(new Appointment
            {
                IDDoc = cbDoctors.SelectedIndex,
                IDPatient = selectedPatient.IDPatient,
                DateTime = DateTime.Now
            });
            MessageBox.Show("Запись прошла успешно", "Добавление", MessageBoxButton.OK);
            Close();
            
        }
    }
}
