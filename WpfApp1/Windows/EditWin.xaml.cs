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
using static WpfApp1.ValidationClass;
using static WpfApp1.AppData;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditWin.xaml
    /// </summary>
    public partial class EditWin : Window
    {
        Doctors selectedDoc;

        public EditWin()
        {
            InitializeComponent();
        }
        public EditWin(Doctors doctors)
        {
            InitializeComponent();
            selectedDoc = doctors;
            tbName.Text = doctors.Name;
            tbLname.Text = doctors.Surname;
            tbPatronymic.Text = doctors.Patronymic;
            cbSpecialization.SelectedItem = doctors.IDSpecialization;
            cbSpecialization.SelectedIndex = selectedDoc.IDSpecialization -1;
            cbSector.SelectedIndex = selectedDoc.IDSector -1;
            cbSeparation.SelectedIndex = selectedDoc.IDSeparation -1;

        }
        private void editSub_Click(object sender, RoutedEventArgs e)
        {
            if (tbLname.Text == string.Empty ||
                tbName.Text == string.Empty ||
                tbPatronymic.Text == string.Empty ||
               cbSpecialization.SelectedItem == null||
               cbSector.SelectedItem == null ||
               cbSeparation.SelectedItem == null)
            {
                MessageBox.Show("Вы указали не все данные", "Регистрация абоента", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (ValidateFIO(tbName.Text, tbLname.Text) == false)
            {
                MessageBox.Show("Имя или фамилия содержат недопустимые символы (В этих полях может присутсвовать только кирилица)", "Регистрация абоента", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }           
            else if (selectedDoc.Name == tbName.Text &&
                        selectedDoc.Surname == tbLname.Text &&
                        selectedDoc.Patronymic == tbPatronymic.Text &&
                        selectedDoc.IDSpecialization == cbSpecialization.SelectedIndex +1 &&
                        selectedDoc.IDSector == cbSector.SelectedIndex +1 &&
                        selectedDoc.IDSeparation == cbSeparation.SelectedIndex +1)
            {
                MessageBox.Show("Изменения не были внесены", "Редактирование абоента", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                try
                {
                    selectedDoc.Name = tbName.Text;
                    selectedDoc.Surname = tbLname.Text;
                    selectedDoc.Patronymic = tbPatronymic.Text;
                    selectedDoc.IDSpecialization = cbSpecialization.SelectedIndex +1;
                    selectedDoc.IDSector = cbSector.SelectedIndex +1;
                    selectedDoc.IDSeparation = cbSeparation.SelectedIndex +1;
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                tbLname.Text = string.Empty;
                tbName.Text = string.Empty;
                tbPatronymic.Text = string.Empty;
                cbSpecialization.SelectedIndex = -1;
                cbSector.SelectedIndex = -1;
                cbSeparation.SelectedIndex = -1;
                entities.SaveChanges();
                MessageBox.Show("Обновление данных абонента прошло успешно", "Редактирование абоента", MessageBoxButton.OK);
                AdminWin adminWin = new AdminWin();
                Close();
                adminWin.ShowDialog();
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из приложения?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void editCan_Click(object sender, RoutedEventArgs e)
        {
            AdminWin adminWin = new AdminWin();
            Close();
            adminWin.ShowDialog();
        }

        private void cbSpecial_Loaded(object sender, RoutedEventArgs e)
        {
            var specializations = entities.Specialization;
            cbSpecialization.ItemsSource = specializations.ToList();
        }

        private void cbSector_Loaded(object sender, RoutedEventArgs e)
        {
            var sector = entities.Sector;
            cbSector.ItemsSource = sector.ToList();
        }

        private void cbSeparation_Loaded(object sender, RoutedEventArgs e)
        {
            var separation = entities.Separation;
            cbSeparation.ItemsSource = separation.ToList();
        }
    }
}
