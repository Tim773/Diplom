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
using static WpfApp1.AppData;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var doctors = GetDoctors();
        }
        

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void inter_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text != String.Empty && pbPassword.Password != String.Empty)
            {
                try
                {                    
                    var user = entities.Doctors.Where(i => i.Login == tbLogin.Text && i.Password == pbPassword.Password).FirstOrDefault();
                    if (user != null && user.IDSpecialization == 16)
                    {
                        tbLogin.Text = string.Empty;
                        pbPassword.Password = string.Empty;
                        Windows.AssisWin assisWin = new Windows.AssisWin();
                        Close();
                        assisWin.ShowDialog();
                        
                    }
                    else if (user != null && user.IDSpecialization == 15)
                    {
                        tbLogin.Text = string.Empty;
                        pbPassword.Password = string.Empty;
                        Windows.AdminWin adminWin = new Windows.AdminWin();
                        Close();
                        adminWin.ShowDialog();
                        
                       
                    }
                    else if (user != null && user.IDSpecialization > 1 && user.IDSpecialization < 15)
                    {
                        tbLogin.Text = string.Empty;
                        pbPassword.Password = string.Empty;
                        Windows.DocWin docWin = new Windows.DocWin();
                        Close();
                        docWin.ShowDialog();
                        
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден! Проверьте правильность введённых данных.", "Авторизация пользователя", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                    throw;
                }
            }
            else
            {
                MessageBox.Show("Поле логина или пароля не заполнено!", "Авторизация пользователя", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
