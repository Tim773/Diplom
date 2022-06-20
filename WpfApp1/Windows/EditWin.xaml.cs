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
            StPAuthData.Visibility = Visibility.Visible;

        }
        public EditWin(Doctors doctors)
        {
            InitializeComponent();
            selectedDoc = doctors;
            tbName.Text = selectedDoc.Name;
            tbLname.Text = selectedDoc.Surname;
            tbPatronymic.Text = selectedDoc.Patronymic;
            cbSpecialization.SelectedItem = selectedDoc.IDSpecialization;
            cbSpecialization.SelectedIndex = selectedDoc.IDSpecialization - 1;
            cbSector.SelectedIndex = selectedDoc.IDSector - 1;
            cbSeparation.SelectedIndex = selectedDoc.IDSeparation - 1;

        }
        private void editSub_Click(object sender, RoutedEventArgs e)
        {
            if (tbLname.Text == string.Empty ||
                tbName.Text == string.Empty ||
                tbPatronymic.Text == string.Empty ||
               cbSpecialization.SelectedItem == null ||
               cbSector.SelectedItem == null ||
               cbSeparation.SelectedItem == null)
            {
                MessageBox.Show("Вы указали не все данные", "Добавление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (ValidateFIO(tbName.Text, tbLname.Text) == false)
            {
                MessageBox.Show("Имя или фамилия содержат недопустимые символы (В этих полях может присутсвовать только кирилица)", "Добавление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (selectedDoc != null &&
                        selectedDoc.Name == tbName.Text &&
                        selectedDoc.Surname == tbLname.Text &&
                        selectedDoc.Patronymic == tbPatronymic.Text &&
                        selectedDoc.IDSpecialization == cbSpecialization.SelectedIndex + 1 &&
                        selectedDoc.IDSector == cbSector.SelectedIndex + 1 &&
                        selectedDoc.IDSeparation == cbSeparation.SelectedIndex + 1)
            {
                MessageBox.Show("Изменения не были внесены", "Редактирование", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (selectedDoc != null && ValidateNumber(tbCabin.Text) == true)
                {
                    try
                    {
                        selectedDoc.Name = tbName.Text;
                        selectedDoc.Surname = tbLname.Text;
                        selectedDoc.Patronymic = tbPatronymic.Text;
                        selectedDoc.IDSpecialization = cbSpecialization.SelectedIndex + 1;
                        selectedDoc.IDSector = cbSector.SelectedIndex + 1;
                        selectedDoc.IDSeparation = cbSeparation.SelectedIndex + 1;
                        selectedDoc.Cabinet = Convert.ToInt32(tbCabin.Text);

                        entities.SaveChanges();
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
                    tbCabin.Text = string.Empty;
                    
                    MessageBox.Show("Обновление данных прошло успешно", "Редактирование", MessageBoxButton.OK);
                    AdminWin adminWin = new AdminWin();
                    Close();
                    adminWin.ShowDialog();
                }
                else
                {
                    try
                    {
                        entities.Doctors.Add(new Doctors
                        {

                            Name = tbName.Text,
                            Surname = tbLname.Text,
                            Patronymic = tbPatronymic.Text,
                            IDSpecialization = cbSpecialization.SelectedIndex + 1,
                            IDSector = cbSector.SelectedIndex + 1,
                            IDSeparation = cbSeparation.SelectedIndex + 1,
                            Cabinet = Convert.ToInt32(tbCabin.Text),
                            Login = tbLogin.Text,
                            Password = pbReristr.Password,
                            valuable = 1
                        });
                        entities.SaveChanges();
                        tbLname.Text = string.Empty;
                        tbName.Text = string.Empty;
                        tbPatronymic.Text = string.Empty;
                        cbSpecialization.SelectedIndex = -1;
                        cbSector.SelectedIndex = -1;
                        cbSeparation.SelectedIndex = -1;
                        tbCabin.Text = string.Empty;
                        tbLogin.Text = string.Empty;
                        pbReristr.Password = string.Empty;
                        MessageBox.Show("Добавление прошло успешно", "Добавление", MessageBoxButton.OK);
                        AdminWin adminWin = new AdminWin();
                        Close();
                        adminWin.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(@ex.Message);
                        throw;
                    }


                }

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
