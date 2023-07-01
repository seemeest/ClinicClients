using ClinicClients.Data.BaseData;
using ClinicClients.ViewMode.DataBaseModel;
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

namespace ClinicClients.Vive.UsersVive
{
    /// <summary>
    /// Логика взаимодействия для PatientEditVive.xaml
    /// </summary>
    public partial class PatientEditVive : Page
    {
        PatientEditViveModel PEM;
        public PatientEditVive()
        {
            InitializeComponent();

            PEM = new PatientEditViveModel();
            DataContext = PEM;
            PEM.GetDataList();
            dataList.ItemsSource = PEM.TESTGetPatientDataFromServer("2");




        }

        public void _Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получение выбранного элемента ComboBox
            ComboBox comboBox = (ComboBox)sender;
            ComboboxItems selectedItem = (ComboboxItems)comboBox.SelectedItem;

            if (selectedItem != null)
            {
                string selectedId = selectedItem.Id;

                Console.WriteLine(selectedId);

                PEM.GetPatientDataFromServer(selectedId);
            }
        }
    }
}
