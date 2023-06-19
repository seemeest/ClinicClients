using ClinicClients.Data;
using ClinicClients.Model;
using ClinicClients.Vive;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
namespace ClinicClients.ViewMode
{
    public class AppointmentsViveModel : DependencyObject
    {

    
        public ObservableCollection<Appointment> AppointmentsList
        {
            get { return (ObservableCollection<Appointment>)GetValue(AppointmentsListProperty); }
            set { SetValue(AppointmentsListProperty, value); }
        }

        public ICommand GetDataCommand { get; }
        public ICommand SendDataCommand { get; }
        DataGrid dataGrid;
        public AppointmentsViveModel(DataGrid dataGrid)
        {
            this.dataGrid = dataGrid;
            GetDataCommand = new RelayCommand(GetAppointmentsList);
            SendDataCommand = new RelayCommand(SendDataToServer);
            GetAppointmentsList();
        }

        // Using a DependencyProperty as the backing store for AppointmentsList.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AppointmentsListProperty =
            DependencyProperty.Register("AppointmentsList", typeof(ObservableCollection<Appointment>), typeof(AppointmentsViveModel), new PropertyMetadata(null));

        private async void GetAppointmentsList()
        {
            try
            {
                await Console.Out.WriteLineAsync("AppointmentsList");
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
                    HttpResponseMessage response = await client.PostAsync($"{AuthData.ServerAddres}getAppointments", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Получаем JSON-ответ от сервера
                        string responseJson = await response.Content.ReadAsStringAsync();

                        // Преобразуем JSON-ответ в список объектов Appointment
                        ObservableCollection<Appointment> updatedAppointments = JsonConvert.DeserializeObject<ObservableCollection<Appointment>>(responseJson);

                        // Заполняем свойство AppointmentsList
                        AppointmentsList = new ObservableCollection<Appointment>(updatedAppointments);
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

                    string jsonData = JsonConvert.SerializeObject(AppointmentsList);

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                    HttpResponseMessage response = await client.PostAsync($"{AuthData.ServerAddres}setAppointments", content);


                    if (response.IsSuccessStatusCode)
                    {
                        string responseJson = await response.Content.ReadAsStringAsync();
                        List<Appointment> updatedDoctors = JsonConvert.DeserializeObject<List<Appointment>>(responseJson);

                        AppointmentsList = new ObservableCollection<Appointment>(updatedDoctors);
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


