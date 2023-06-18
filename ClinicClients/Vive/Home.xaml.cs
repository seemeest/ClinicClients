using ClinicClients.Data;
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
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();

            foreach(var item in Panel.Children) {

                if(item is Border) {

                    ((Border)item).MouseLeave += Home_MouseLeave;
                    ((Border)item).MouseMove += Home_MouseMove;
                    ((Border)item).Margin = new Thickness(40,0,0,0);
                    ((Border)item).Height = 30;
                }
            }


        }

        private void Home_MouseMove(object sender, MouseEventArgs e)
        {
            Brush brush = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            ((Border)sender).Background = brush;
        }

        private void Home_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = null;
        }

        private void Appointments_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new AppointmentVive();
        } 
        private void Department_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new DepartmentVive();
        }

        private void Patient_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new PatientVive();
        }

        private void Diagnosis_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new DiagnosisVive();
        }

        private void Doctor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new DoctorVive();
        }

        private void Prescription_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new PrescriptionVive();
        }
    }
}
