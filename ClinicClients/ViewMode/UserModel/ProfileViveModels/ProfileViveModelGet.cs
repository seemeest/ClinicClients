using ClinicClients.Data;
using ClinicClients.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClinicClients.ViewMode.UserModel.ProfileViveModels
{
    public partial class ProfileViveModel : DependencyObject
    {
       Doctor doctor { get; set; }
        public ProfileViveModel()
        {
            GetDataList();



        }

     
        private async void GetDataList()
        {
            try
            {
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
                    HttpResponseMessage response = await client.PostAsync($"{AuthData.ServerAddres + AddresList.getUserId}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Получаем JSON-ответ от сервера
                        string responseJson = await response.Content.ReadAsStringAsync();

                        // Преобразуем JSON-ответ в список объектов DataList
                        Doctor Tdata = JsonConvert.DeserializeObject<Doctor>(responseJson);

                        // Заполняем свойство DataList
                        doctor = Tdata;
                        UserName = $" {Tdata.FirstName}{Tdata.LastName}";
                        UserEmail = "нету";
                        UserPhone = Tdata.PhoneNumber;
                        UserMobile = Tdata.mobile_number;
                        UserAddress = Tdata.address;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при получении данных с сервера");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
