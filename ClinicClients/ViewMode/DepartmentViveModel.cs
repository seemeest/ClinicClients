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
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace ClinicClients.ViewMode
{
    //Department
    internal class DepartmentViveModel : DependencyObject
    {


        public ObservableCollection<Department> DepartmentList
        {
            get { return (ObservableCollection<Department>)GetValue(DepartmentListProperty); }
            set { SetValue(DepartmentListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AppointmentsList.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DepartmentListProperty =
            DependencyProperty.Register("DepartmentList", typeof(ObservableCollection<Department>), typeof(DepartmentViveModel), new PropertyMetadata(null));

        public ICommand GetDataCommand { get; }
        public ICommand SendDataCommand { get; }
        DataGrid dataGrid;

        public DepartmentViveModel(DataGrid dataGrid)
        {
            this.dataGrid = dataGrid;
            GetDataCommand = new RelayCommand(GetDepartmentList);
            SendDataCommand = new RelayCommand(SendDataToServer);
            GetDepartmentList();
        }
        private void ConfigureDataGridColumns()
        {
            // Находим столбец DoctorId по его индексу
            DataGridColumn doctorIdColumn = dataGrid.Columns[0];

            // Устанавливаем IsReadOnly для столбца DoctorId в true
            doctorIdColumn.IsReadOnly = true;
        }
        private async void  GetDepartmentList()
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

                        List<Department> departments = JsonConvert.DeserializeObject<List<Department>>(responseJson);

                        DepartmentList = new ObservableCollection<Department>(departments);
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

                    string jsonData = JsonConvert.SerializeObject(DepartmentList);

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                    HttpResponseMessage response = await client.PostAsync($"{AuthData.ServerAddres}setDepartments", content);


                    if (response.IsSuccessStatusCode)
                    {
                        string responseJson = await response.Content.ReadAsStringAsync();
                        List<Department> updatedDepartment = JsonConvert.DeserializeObject<List<Department>>(responseJson);

                        DepartmentList = new ObservableCollection<Department>(updatedDepartment);
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
