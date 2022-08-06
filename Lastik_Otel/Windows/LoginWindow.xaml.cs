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

namespace Lastik_Otel
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            Hide();

            StartWindow s = new StartWindow();
            s.ShowDialog();

            Show();

            txtUser.Focus();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Login()
        {
            if (txtUser.Text.Trim().ToUpper() == "FATİH" && txtPassword.Password == "1234")
            {
                AnaEkran ana = new AnaEkran();
                ana.Show();

                Hide();
                txtPassword.Password = "";
                txtUser.Text = "";
                lblHata.Content = "";
            }
            else
            {
                lblHata.Content = "Kullanıcı Adı veya Parola Yanlış.";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    Login();
                }
            }
            catch (Exception ex)
            {
                Loglama.Write(ex.ToString());
            }
        }
    }
}
