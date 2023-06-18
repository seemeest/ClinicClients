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
    //Department
    internal class DepartmentViveModel : DependencyObject
    {
        public DepartmentViveModel()
        {
            GetDepartmentList();

        }
        public List<Department> DepartmentList
        {
            get { return (List<Department>)GetValue(DepartmentListProperty); }
            set { SetValue(DepartmentListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AppointmentsList.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DepartmentListProperty =
            DependencyProperty.Register("DepartmentList", typeof(List<Department>), typeof(DepartmentViveModel), new PropertyMetadata(null));

        private async Task GetDepartmentList()
        {
            try
            {
                await Console.Out.WriteLineAsync("DepartmentList");
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
                    HttpResponseMessage response = await client.PostAsync("http://localhost:8000/getDepartment", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Получаем JSON-ответ от сервера
                        string responseJson = await response.Content.ReadAsStringAsync();

                        // Преобразуем JSON-ответ в список объектов Appointment
                        List<Department> departments = JsonConvert.DeserializeObject<List<Department>>(responseJson);

                        // Заполняем свойство AppointmentsList
                        DepartmentList = departments;
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
