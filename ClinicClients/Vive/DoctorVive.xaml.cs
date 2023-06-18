using ClinicClients.Data;
using ClinicClients.Model;
using ClinicClients.ViewMode;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
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
    /// Логика взаимодействия для DoctorVive.xaml
    /// </summary>
    public partial class DoctorVive : Page
    {
        public DoctorVive()
        {
            InitializeComponent();
            DataContext = new DoctorViveModel(dataGrid);
        }

       
    }
}
