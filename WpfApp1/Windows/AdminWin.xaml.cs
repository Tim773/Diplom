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
            var datasourse = entities.Doctors.Where(i => i.valuable == 1).ToList();
            RowAll = datasourse.Count();
            datasourse = datasourse.Skip(Page * 10).Take(10).ToList();
            var doctors = GetDoctors();
            adminList.ItemsSource = datasourse;
            var dataRegList = entities.Sector.ToList();
            listReg.ItemsSource = dataRegList;
            var dataSepList = entities.Separation.ToList();
            listSeparation.ItemsSource = dataSepList;
            var dataDrugList = entities.Drug.ToList();
            listDrug.ItemsSource = dataDrugList;
            var dataSpecList = entities.Specialization.ToList();
            listSpec.ItemsSource = dataSpecList;
            var dataResList = entities.Research.ToList();
            listResearch.ItemsSource= dataResList;
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

        private void WorkersRadio_Click(object sender, RoutedEventArgs e)
        {
            {

                Page = 0;
                adminList.Visibility = Visibility.Visible;
                listReg.Visibility = Visibility.Collapsed;
                FamSearch.Visibility = Visibility.Visible;
                PatrSearch.Visibility = Visibility.Visible;
                listSeparation.Visibility = Visibility.Collapsed;
                listDrug.Visibility = Visibility.Collapsed;
                listResearch.Visibility = Visibility.Collapsed;
                listSpec.Visibility = Visibility.Collapsed;
                nextList.Visibility = Visibility.Visible;
                backList.Visibility = Visibility.Visible;
                name.Content = "Имя";
                Update();

            }
        }

        private void RegRadio_Click(object sender, RoutedEventArgs e)
        {

            Page = 0;
            adminList.Visibility = Visibility.Collapsed;
            listSeparation.Visibility = Visibility.Collapsed;
            listReg.Visibility = Visibility.Visible;
            listDrug.Visibility = Visibility.Collapsed;
            listResearch.Visibility = Visibility.Collapsed;
            listSpec.Visibility = Visibility.Collapsed;
            FamSearch.Visibility = Visibility.Collapsed;
            PatrSearch.Visibility = Visibility.Collapsed;
            nextList.Visibility= Visibility.Hidden;
            backList.Visibility= Visibility.Hidden;
            name.Content = "Район";
            Update();

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
           
            if (MessageBox.Show("Вы действительно хотите выйти из учётной записи?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
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
            if (WorkerRadio.IsChecked == true)
            {
                var tabb = entities.Doctors.Where(i => i.valuable == 1).ToList();
                if (FamSearch1.Text.Length == 0 && ImySearch1.Text.Length == 0)
                {
                    adminList.ItemsSource = tabb.ToList();
                    return;
                }
                else
                {
                    var res = tabb.Where(i => i.Surname.Contains(FamSearch1.Text) &&
                                         i.Name.Contains(ImySearch1.Text)
                                         ).ToList();
                    if (res.Count() != 0)
                    {
                        adminList.ItemsSource = res;
                    }
                    else
                        MessageBox.Show(" Не найдено!", "Внимание!",
                            MessageBoxButton.OK, MessageBoxImage.Warning);


                }

            }
            else if (RegRadio.IsChecked == true)
            {
                var tabb = entities.Sector.ToList();
                if (ImySearch1.Text.Length == 0)
                {
                    listReg.ItemsSource = tabb.ToList();
                    return;
                }
                else
                {
                    var res = tabb.Where(i => i.Sector1.Contains(ImySearch1.Text)
                                         ).ToList();
                    if (res.Count() != 0)
                    {

                        listReg.ItemsSource = res;
                    }
                    else
                        MessageBox.Show("Внимание!", " Не найдено!",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void adminList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DepRadio_Click(object sender, RoutedEventArgs e)
        {
            Page = 0;
            adminList.Visibility = Visibility.Collapsed;
            listReg.Visibility = Visibility.Collapsed;
            listSeparation.Visibility = Visibility.Visible;
            FamSearch.Visibility = Visibility.Collapsed;
            PatrSearch.Visibility = Visibility.Collapsed;
            listDrug.Visibility = Visibility.Collapsed;
            listResearch.Visibility = Visibility.Collapsed;
            listSpec.Visibility = Visibility.Collapsed;
            name.Content = "Отделение";
            nextList.Visibility = Visibility.Hidden;
            backList.Visibility = Visibility.Hidden;
            Update();
        }

        private void PrepRadio_Click(object sender, RoutedEventArgs e)
        {
            Page = 0;
            adminList.Visibility = Visibility.Collapsed;
            listSeparation.Visibility = Visibility.Collapsed;
            listReg.Visibility = Visibility.Collapsed;
            listDrug.Visibility = Visibility.Visible;
            listResearch.Visibility = Visibility.Collapsed;
            listSpec.Visibility = Visibility.Collapsed;
            FamSearch.Visibility = Visibility.Collapsed;
            PatrSearch.Visibility = Visibility.Collapsed;
            nextList.Visibility = Visibility.Hidden;
            backList.Visibility = Visibility.Hidden;
            name.Content = "Препарат";
            Update();
        }

        private void SpecRadio_Click(object sender, RoutedEventArgs e)
        {
            Page = 0;
            adminList.Visibility = Visibility.Collapsed;
            listSeparation.Visibility = Visibility.Collapsed;
            listReg.Visibility = Visibility.Collapsed;
            listDrug.Visibility = Visibility.Collapsed;
            listResearch.Visibility = Visibility.Collapsed;
            listSpec.Visibility = Visibility.Visible;
            FamSearch.Visibility = Visibility.Collapsed;
            PatrSearch.Visibility = Visibility.Collapsed;
            nextList.Visibility = Visibility.Hidden;
            backList.Visibility = Visibility.Hidden;
            name.Content = "Специальность";
            Update();
        }

        private void SerRadio_Click(object sender, RoutedEventArgs e)
        {
            Page = 0;
            adminList.Visibility = Visibility.Collapsed;
            listSeparation.Visibility = Visibility.Collapsed;
            listReg.Visibility = Visibility.Collapsed;
            listDrug.Visibility = Visibility.Collapsed;
            listResearch.Visibility = Visibility.Visible;
            listSpec.Visibility = Visibility.Collapsed;
            FamSearch.Visibility = Visibility.Collapsed;
            PatrSearch.Visibility = Visibility.Collapsed;
            nextList.Visibility = Visibility.Hidden;
            backList.Visibility = Visibility.Hidden;
            name.Content = "Услуга";
            Update();
        }
    }
}
