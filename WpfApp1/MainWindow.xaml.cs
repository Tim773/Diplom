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
                    var user = entities.Doctors.ToList().Where
                        (i => i.Login == tbLogin.Text && i.Password == pbPassword.Password).FirstOrDefault();

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
