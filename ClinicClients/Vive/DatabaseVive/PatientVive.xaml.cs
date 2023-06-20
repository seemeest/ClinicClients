using ClinicClients.Data;
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
    /// Логика взаимодействия для PatientVive.xaml
    /// </summary>
    public partial class PatientVive : Page
    {
        public PatientVive()
        {
            InitializeComponent();
            DataContext = new PatientViveModel(dataGrid, AddresList.getPatient, AddresList.setPatients);
        }
    }
}
