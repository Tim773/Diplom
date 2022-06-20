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
using static WpfApp1.ValidationClass;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowAddPatient.xaml
    /// </summary>
    public partial class WindowAddPatient : Window
    {
        Patients SelectedPatient;
        public WindowAddPatient()
        {
            InitializeComponent();
        }
        public WindowAddPatient(Patients selectedPatient)
        {
            InitializeComponent();
            tbName.Text = selectedPatient.Name;
            tbLname.Text = selectedPatient.Surname;
            tbPatronymic.Text = selectedPatient.Patronymic;
            dbPatientDate.DisplayDate = selectedPatient.Birthday;
            cbGender.SelectedItem = selectedPatient.Gender;
            tbPolis.Text = selectedPatient.Polis.ToString();
            tbNumber.Text = selectedPatient.Phone;
            tbAddress.Text = selectedPatient.Adress;
            cbSector.SelectedItem = selectedPatient.Sector;
            SelectedPatient = selectedPatient;
        }

        private void cbGender_Loaded(object sender, RoutedEventArgs e)
        {
            cbGender.ItemsSource = entities.Gender.ToList() ;
        }

        private void cbSector_Loaded(object sender, RoutedEventArgs e)
        {
            cbSector.ItemsSource = entities.Sector.ToList();
        }

        private void addPatient_Click(object sender, RoutedEventArgs e)
        {
            if (tbLname.Text == string.Empty ||
                tbName.Text == string.Empty ||
                tbPatronymic.Text == string.Empty ||
               cbGender.SelectedItem == null ||
               cbSector.SelectedItem == null ||
               dbPatientDate.Text == null ||
               tbPolis.Text == null ||
               tbNumber.Text == null ||
               cbGender.SelectedItem == null ||
               tbAddress.Text == null)
            {
                MessageBox.Show("Вы указали не все данные", "Добавление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (ValidateFIO(tbName.Text, tbLname.Text) == false)
            {
                MessageBox.Show("Имя или фамилия содержат недопустимые символы (В этих полях может присутсвовать только кирилица)", "Добавление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (SelectedPatient != null &&
                        SelectedPatient.Name == tbName.Text &&
                        SelectedPatient.Surname == tbLname.Text &&
                        SelectedPatient.Patronymic == tbPatronymic.Text &&
                        SelectedPatient.IDGender == cbGender.SelectedIndex + 1 &&
                        SelectedPatient.IDSector == cbSector.SelectedIndex + 1 &&
                        SelectedPatient.Phone == tbNumber.Text &&
                        (SelectedPatient.Birthday == dbPatientDate.DisplayDate) == true &&
                        SelectedPatient.Polis.ToString() == tbPolis.Text &&
                        SelectedPatient.Adress == tbAddress.Text)
            {
                MessageBox.Show("Изменения не были внесены", "Редактирование", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (SelectedPatient != null && ValidateNumber(tbPolis.Text) == true)
                {
                    try
                    {
                        SelectedPatient.Name = tbName.Text;
                        SelectedPatient.Surname = tbLname.Text;
                        SelectedPatient.Patronymic = tbPatronymic.Text;
                        SelectedPatient.IDGender = cbGender.SelectedIndex + 1;
                        SelectedPatient.IDSector = cbSector.SelectedIndex + 1;
                        SelectedPatient.Birthday = dbPatientDate.DisplayDate;
                        SelectedPatient.Phone = tbNumber.Text;
                        SelectedPatient.Polis = Convert.ToInt64(tbPolis.Text);
                        SelectedPatient.Adress = tbAddress.Text;

                        entities.SaveChanges();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                    tbLname.Text = string.Empty;
                    tbName.Text = string.Empty;
                    tbPatronymic.Text = string.Empty;
                    cbGender.SelectedIndex = -1;
                    cbSector.SelectedIndex = -1;
                    dbPatientDate.DisplayDate = DateTime.Now;
                    tbNumber.Text = string.Empty;
                    tbPolis.Text = string.Empty;
                    tbAddress.Text = string.Empty;

                    MessageBox.Show("Обновление данных прошло успешно", "Редактирование", MessageBoxButton.OK);
                    AdminWin adminWin = new AdminWin();
                    Close();
                    adminWin.ShowDialog();
                }
                else
                {
                    try
                    {
                        entities.Patients.Add(new Patients
                        {

                            Name = tbName.Text,
                            Surname = tbLname.Text,
                            Patronymic = tbPatronymic.Text,
                            IDGender = cbGender.SelectedIndex + 1,
                            IDSector = cbSector.SelectedIndex + 1,
                            Birthday = dbPatientDate.DisplayDate,
                            Phone = tbNumber.Text,
                            Polis = Convert.ToInt64(tbPolis.Text),
                            Adress = tbAddress.Text
                            

                        });
                        entities.SaveChanges();
                        tbLname.Text = string.Empty;
                        tbName.Text = string.Empty;
                        tbPatronymic.Text = string.Empty;
                        cbGender.SelectedIndex = -1;
                        cbSector.SelectedIndex = -1;
                        dbPatientDate.DisplayDate = DateTime.Now;
                        tbNumber.Text = string.Empty;
                        tbPolis.Text = string.Empty;
                        tbAddress.Text = string.Empty;
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

        private void addCan_Click(object sender, RoutedEventArgs e) //АТМИНЭТ
        {
            AssisWin assisWin = new AssisWin(); 
            this.Close();
            assisWin.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e) //ВИХАД
        {
            if (MessageBox.Show("Вы действительно хотите выйти из приложения?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}
