using ClinicClients.Data.BaseData;
using ClinicClients.Data;
using ClinicClients.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using ClinicClients.Vive.DatabaseVive;
using System.Windows.Navigation;

namespace ClinicClients.ViewMode.DataBaseModel
{
    public class ComboboxItems
    {
        public string FIO { get; set; }
        public string Id { get; set; }

        public override string ToString()
        {
            return FIO;
        }
    }
    public class PatientEditViveModel : DependencyObject
    {


        public PatientEditViveModel()
        {
            GetDataList();
        }

        public async void _Combobox_SelectionChanged(object selectedValue)
        {
            if (selectedValue != null)
            {
                ComboboxItems selectedItem = (ComboboxItems)((ComboBox)selectedValue).SelectedItem;
                string selectedId = selectedItem.Id;

                Console.WriteLine(selectedId);

                GetPatientDataFromServer(selectedId);
            }
        }


        public ObservableCollection<PatientEditName> DataListInfo
        {
            get { return (ObservableCollection<PatientEditName>)GetValue(DataListInfoProperty); }
            set { SetValue(DataListInfoProperty, value); }
        }

        public static readonly DependencyProperty DataListInfoProperty =
            DependencyProperty.Register("DataListInfo", typeof(ObservableCollection<PatientEditName>), typeof(BaseViveModel<PatientEditName>), new PropertyMetadata(null));

        public ObservableCollection<PatientDiagnos> DataListDiagnos
        {
            get { return (ObservableCollection<PatientDiagnos>)GetValue(DataListDiagnosProperty); }
            set { SetValue(DataListDiagnosProperty, value); }
        }

        public static readonly DependencyProperty DataListDiagnosProperty =
            DependencyProperty.Register("DataListDiagnos", typeof(ObservableCollection<PatientDiagnos>), typeof(BaseViveModel<PatientDiagnos>), new PropertyMetadata(null));

        public ObservableCollection<ComboboxItems> ComboBoxNameItem
        {
            get { return (ObservableCollection<ComboboxItems>)GetValue(ComboBoxNameItemProperty); }
            set { SetValue(ComboBoxNameItemProperty, value); }
        }

        public static readonly DependencyProperty ComboBoxNameItemProperty =
            DependencyProperty.Register("ComboBoxNameItem", typeof(ObservableCollection<ComboboxItems>), typeof(BaseViveModel<ComboboxItems>), new PropertyMetadata(null));

        public ICommand SelectionChanged { get; }

        public ObservableCollection<PatientEditName> TESTGetPatientDataFromServer(string patientId)
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    string username = AuthData.Login;
                    string password = AuthData.password;

                    var data = new { patientId };
                    string jsonData = JsonConvert.SerializeObject(data);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                    HttpResponseMessage response = client.PostAsync($"{AuthData.ServerAddres + AddresList.GetPatientEditName}", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(responseJson);
                        return JsonConvert.DeserializeObject<ObservableCollection<PatientEditName>>(responseJson);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка при получении данных с сервера.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;

        }
        public void GetPatientDataFromServer(string patientId)
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    string username = AuthData.Login;
                    string password = AuthData.password;

                    var data = new { patientId };
                    string jsonData = JsonConvert.SerializeObject(data);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                    HttpResponseMessage response = client.PostAsync($"{AuthData.ServerAddres + AddresList.GetPatientEditName}", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(responseJson);
                        DataListInfo = JsonConvert.DeserializeObject<ObservableCollection<PatientEditName>>(responseJson);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка при получении данных с сервера.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        public async void GetDataList()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string username = AuthData.Login;
                    string password = AuthData.password;

                    var data = new { username, password };
                    string jsonData = JsonConvert.SerializeObject(data);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                    HttpResponseMessage response = await client.PostAsync($"{AuthData.ServerAddres + AddresList.getPatient}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseJson = await response.Content.ReadAsStringAsync();
                        List<Patient> Tdata = JsonConvert.DeserializeObject<List<Patient>>(responseJson);

                        ObservableCollection<ComboboxItems> comboboxItems = new ObservableCollection<ComboboxItems>();
                        foreach (Patient patient in Tdata)
                        {
                            ComboboxItems comboboxItem = new ComboboxItems();
                            comboboxItem.FIO = patient.FirstName + " " + patient.LastName;
                            comboboxItem.Id = patient.PatientId.ToString();
                            comboboxItems.Add(comboboxItem);
                        }

                        ComboBoxNameItem = comboboxItems;


                    }
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
            }
        }


    }
}