using ClinicClients.Data;
using ClinicClients.Model;
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;


namespace ClinicClients.ViewMode
{
    internal class DoctorViveModel : DependencyObject
    {


        public static readonly DependencyProperty DoctorsListProperty =
           DependencyProperty.Register("DoctorsList", typeof(ObservableCollection<Doctor>), typeof(DoctorViveModel), new PropertyMetadata(null));

        public ObservableCollection<Doctor> DoctorsList
        {
            get { return (ObservableCollection<Doctor>)GetValue(DoctorsListProperty); }
            set { SetValue(DoctorsListProperty, value); }
        }

        public ICommand GetDataCommand { get; }
        public ICommand SendDataCommand { get; }
        DataGrid dataGrid;
        public DoctorViveModel( DataGrid dataGrid)
        {
            this.dataGrid = dataGrid;
            GetDataCommand = new RelayCommand(GetDoctorList);
            SendDataCommand = new RelayCommand(SendDataToServer);
            GetDoctorList();
        }


        private void ConfigureDataGridColumns()
        {
            // Находим столбец DoctorId по его индексу
            DataGridColumn doctorIdColumn = dataGrid.Columns[0];

            // Устанавливаем IsReadOnly для столбца DoctorId в true
            doctorIdColumn.IsReadOnly = true;
        }
        private async void GetDoctorList()
        {
            try
            {
                await Console.Out.WriteLineAsync("DoctorsList");
                using (HttpClient client = new HttpClient())
                {
                    string username = AuthData.Login;
                    string password = AuthData.password;

                    // Создаем объект для отправки данных
                    var data = new { username, password };

                    // Преобразуем данные в JSON-строку
                    string jsonData = JsonConvert.SerializeObject(data);

                    // Создаем контент запроса с JSON-строкой
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // Добавляем заголовок авторизации
                    string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                    // Отправляем POST-запрос на сервер
                    HttpResponseMessage response = await client.PostAsync($"{AuthData.ServerAddres}getDoctors", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Получаем JSON-ответ от сервера
                        string responseJson = await response.Content.ReadAsStringAsync();

                        // Преобразуем JSON-ответ в список объектов Doctor
                        List<Doctor> doctors = JsonConvert.DeserializeObject<List<Doctor>>(responseJson);

                        // Заполняем свойство DoctorsList
                        DoctorsList = new ObservableCollection<Doctor>(doctors);
                        ConfigureDataGridColumns();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при получении данных с сервера");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private async void SendDataToServer()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string username = AuthData.Login;
                    string password = AuthData.password;

                    string jsonData = JsonConvert.SerializeObject(DoctorsList);

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                    HttpResponseMessage response = await client.PostAsync($"{AuthData.ServerAddres}setDoctors", content);


                    if (response.IsSuccessStatusCode)
                    {
                        string responseJson = await response.Content.ReadAsStringAsync();
                        List<Doctor> updatedDoctors = JsonConvert.DeserializeObject<List<Doctor>>(responseJson);

                        DoctorsList = new ObservableCollection<Doctor>(updatedDoctors);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при отправке данных на сервер");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
