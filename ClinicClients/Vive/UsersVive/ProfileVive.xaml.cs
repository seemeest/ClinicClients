using ClinicClients.Data;
using ClinicClients.ViewMode.UserModel.ProfileViveModels;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.IO;
using ClinicClients.Model;
using System.Text;

namespace ClinicClients.Vive.UsersVive
{
    /// <summary>
    /// Логика взаимодействия для ProfileVive.xaml
    /// </summary>
    public partial class ProfileVive : Page
    {
        public ProfileVive()
        {
            InitializeComponent();
           
            DataContext = new ProfileViveModel();
        }

   

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {

            Front.Visibility = Visibility.Visible;


            //DoubleAnimation doubleAnimation = new DoubleAnimation();
            //doubleAnimation.From = 20;
            //doubleAnimation.To = 100;
            //doubleAnimation.Duration = TimeSpan.FromMilliseconds(200);
            //Front.BeginAnimation(HeightProperty, doubleAnimation);


            ThicknessAnimation animation = new ThicknessAnimation();
            animation.From = new Thickness(-50);
            animation.To = new Thickness(0, 0, 0, 0);
            animation.Duration = TimeSpan.FromMilliseconds(200);

            FrontAddImage.BeginAnimation(StackPanel.MarginProperty, animation);

        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Front.Visibility = Visibility.Hidden;
        }

        private void FrontStackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(sender is StackPanel stack)
            {
                foreach (var el in stack.Children )
                {
                    if( el is TextBlock block)
                    {
                        block.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6BC1E3"));
                    }
                    if( el is PackIcon icon)
                    {
                        icon.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6BC1E3"));
                    }
                }
            }
        }

        private void FrontStackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is StackPanel stack)
            {
                foreach (var el in stack.Children)
                {
                    if (el is TextBlock block)
                    {
                        block.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000"));
                    }
                    if (el is PackIcon icon)
                    {
                        icon.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000"));
                    }
                }
            }
        }
      
    }
}
