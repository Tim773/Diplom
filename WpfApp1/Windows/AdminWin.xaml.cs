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
    /// Логика взаимодействия для AdminWin.xaml
    /// </summary>
    public partial class AdminWin : Window
    {

        public AdminWin()
        {
            InitializeComponent();
            Update();
        }
        public int Page { get; set; } = 0;
        public int RowAll { get; set; } = 0;
        

        public void Update()
        {

            var datasourse = entities.Doctors.Where(i => i.valuable ==1).ToList();
            
            RowAll = datasourse.Count();
            datasourse = datasourse.Skip(Page * 10).Take(10).ToList();
            var doctors = GetDoctors();
            adminList.ItemsSource = datasourse;
        }
        private void BackList_Click(object sender, RoutedEventArgs e)
        {
            if (Page > 0)
            {
                Page--;
                Update();
            }
            else
            {
                MessageBox.Show("Открыта первая страница", "Выборка", MessageBoxButton.OK, MessageBoxImage.Information);
            };
        }

        private void NextList_Click(object sender, RoutedEventArgs e)
        {

            Page++;
            if ((10 * Page) < RowAll)
            {
                Update();
            }
            else
            {
                Page--;
                Update();
                MessageBox.Show("Открыта последняя страница", "Выборка", MessageBoxButton.OK, MessageBoxImage.Information);
            };

        }

        private void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (adminList.SelectedItem is Doctors doctors)
            {
                EditWin editWin = new EditWin(doctors);
                Close();
                editWin.ShowDialog();


            }
        }

        private void Tarifs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if (Tarifs.SelectedItem is tarifs ttarifs)
            //{
            //    editTarifWin editTarifWin = new editTarifWin(ttarifs);
            //    editTarifWin.ShowDialog();
            //    Update();
            //}
        }

        private void ClientsRadio_Click(object sender, RoutedEventArgs e)
        {
            //if (AbonentsRadio.IsChecked == true)
            //{
            //    Page = 0;
            //    Abonents.Visibility = Visibility.Visible;
            //    Tarifs.Visibility = Visibility.Collapsed;
            //    btnDel.Content = "Удалить абонента";
            //    btnAdd.Content = "Добавить абонента";
            //    FamSearch.Visibility = Visibility.Visible;
            //    PatrSearch.Visibility = Visibility.Visible;
            //    Update();
            //}
            //else if (AbonentsRadio.IsChecked == false)
            //{
            //    Page = 0;
            //    Abonents.Visibility = Visibility.Visible;
            //    Tarifs.Visibility = Visibility.Collapsed;
            //    btnDel.Content = "Удалить абонента";
            //    btnAdd.Content = "Добавить абонента";
            //    FamSearch.Visibility = Visibility.Visible;
            //    PatrSearch.Visibility = Visibility.Visible;
            //    Update();

            //}
        }

        private void TarifRadio_Click(object sender, RoutedEventArgs e)
        {
            //if (TarifRadio.IsChecked == true)
            //{
            //    Page = 0;
            //    Abonents.Visibility = Visibility.Collapsed;  ////Создать метод, в который будет передаваться интовское или булевое значение, которое определит, какой вариант нужно показать
            //    Tarifs.Visibility = Visibility.Visible;
            //    btnDel.Content = "Удалить тариф";
            //    btnAdd.Content = "Добавить тариф";
            //    FamSearch.Visibility = Visibility.Collapsed;
            //    PatrSearch.Visibility = Visibility.Collapsed;
            //    Update();
            //}
            //else if (TarifRadio.IsChecked == false)
            //{
            //    Page = 0;
            //    Abonents.Visibility = Visibility.Collapsed;
            //    Tarifs.Visibility = Visibility.Visible;
            //    btnDel.Content = "Удалить тариф";
            //    btnAdd.Content = "Добавить тариф";
            //    FamSearch.Visibility = Visibility.Collapsed;
            //    PatrSearch.Visibility = Visibility.Collapsed;
            //    Update();
            //}
        }
        private void Close_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из приложения?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                var itm = adminList.SelectedItem;
                if (itm == null)
                {
                    MessageBox.Show("Выберите запись из таблицы", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information); ;
                }
                else if (MessageBox.Show("Строка абонента будет удалена из таблицы. Желаете продолжить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int del = (adminList.SelectedItem as Doctors).IDDoc;

                    Doctors doctors = entities.Doctors.Where(i => i.IDDoc == del).FirstOrDefault();
                    
                    doctors.valuable = 0;
                    entities.SaveChanges();
                    Update();
                }
                    else return;

                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так!");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            EditWin editWin = new EditWin();
            Close();
            editWin.ShowDialog();

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            if (MessageBox.Show("Вы действительно хотите выйти из учётной записи?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
                mainWindow.ShowDialog();
            }
        }
        private void ResetSearch(object sende, RoutedEventArgs e)
        {
            FamSearch1.Text = "";
            ImySearch1.Text = "";
            PatrSearch1.Text = "";
            Update();
        }
        private void Search(object sender, RoutedEventArgs e)
        {
            //if (AbonentsRadio.IsChecked == true)
            //{
            //    var tabb = entities.abonents.ToList().Where(i => i.avaluable == "1");
            //    if (FamSearch1.Text.Length == 0 && ImySearch1.Text.Length == 0)
            //    {
            //        Abonents.ItemsSource = tabb.ToList();
            //        return;
            //    }
            //    else
            //    {
            //        var res = tabb.Where(i => i.lName.Contains(FamSearch1.Text) &&
            //                             i.name.Contains(ImySearch1.Text) &&
            //                             i.number.Contains(PatrSearch1.Text)
            //                             ).ToList();
            //        if (res.Count() != 0)
            //        {
            //            Abonents.ItemsSource = res;
            //        }
            //        else
            //            MessageBox.Show("Внимание!", " Не найдено!",
            //                MessageBoxButton.OK, MessageBoxImage.Warning);


            //    }

            //}
            //else if (TarifRadio.IsChecked == true)
            //{
            //    var tabb = entities.tarifs.ToList().Where(i => i.avaluable == "1");
            //    if (ImySearch1.Text.Length == 0)
            //    {
            //        Tarifs.ItemsSource = tabb.ToList();
            //        return;
            //    }
            //    else
            //    {
            //        var res = tabb.Where(i => i.nameTarif.Contains(ImySearch1.Text)
            //                             ).ToList();
            //        if (res.Count() != 0)
            //        {

            //            Tarifs.ItemsSource = res;
            //        }
            //        else
            //            MessageBox.Show("Внимание!", " Не найдено!",
            //                MessageBoxButton.OK, MessageBoxImage.Warning);
            //    }

            //}
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из приложения?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void off_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
