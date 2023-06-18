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

namespace ClinicClients.ViewMode
{
    public class PatientViveModel : DependencyObject
    {

        public PatientViveModel() { GetPatientsList(); }
        public List<Patient> PatientsList
        {
            get { return (List<Patient>)GetValue(PatientListProperty); }
            set
            {
                SetValue(PatientListProperty, value);
            }
        }


            // Using a DependencyProperty as the backing store for AppointmentsList.
            // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PatientListProperty =
            DependencyProperty.Register("PatientsList", typeof(List<Patient>), typeof(PatientViveModel), new PropertyMetadata(null));
        private async Task GetPatientsList()
        {
            try
            {
                await Console.Out.WriteLineAsync("PatientsList");
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
                    HttpResponseMessage response = await client.PostAsync("http://localhost:8000/getPatient", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Получаем JSON-ответ от сервера
                        string responseJson = await response.Content.ReadAsStringAsync();

                        // Преобразуем JSON-ответ в список объектов Appointment
                        List<Patient>  patients = JsonConvert.DeserializeObject<List<Patient>>(responseJson);
                      

                        // Заполняем свойство AppointmentsList
                        PatientsList = patients;
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

    }

}
    

