using ClinicClients.Control;
using ClinicClients.Data;
using ClinicClients.Model;
using ClinicClients.ViewMode;
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

namespace ClinicClients.Vive
{
    /// <summary>
    /// Логика взаимодействия для LogIn.xaml
    /// </summary>
    public partial class LogIn : Page
    {
        public LogIn()
        {
            InitializeComponent();
            //LogInViewMode viewModel = new LogInViewMode();
            //this.DataContext = viewModel;
            DataContext= new LogInViewMode();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            if (LoginTb.Text == string.Empty)
            {
                Error.AuthError = "Введите Логин";
                return;
            }
            if (PasswordTB.Password == string.Empty)
            {
                Error.AuthError = "Введите Пароль";
                return;
            }
            AuthData.Login = LoginTb.Text;
            AuthData.password = PasswordTB.Password;

            Auth.auth();

        }
    }
}
