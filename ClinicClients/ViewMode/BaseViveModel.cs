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
using ClinicClients.Data;
using ClinicClients.Model;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ClinicClients.ViewMode
{
    public abstract class BaseViveModel<T> : DependencyObject
    {
        public ObservableCollection<T> DataList
        {
            get { return (ObservableCollection<T>)GetValue(DataListProperty); }
            set { SetValue(DataListProperty, value); }
        }

        public static readonly DependencyProperty DataListProperty =
            DependencyProperty.Register("DataList", typeof(ObservableCollection<T>), typeof(BaseViveModel<T>), new PropertyMetadata(null));

        public ICommand GetDataCommand { get; }
        public ICommand SendDataCommand { get; }
        public ICommand DeleteCommand { get; }

        protected DataGrid dataGrid;
        string _Getaddres;
        string _Setaddres;
       

        //СДЕЛАТЬ!!
        private void DeleteRow(T item)
        {
            if (DataList.Contains(item))
            {
                DataList.Remove(item);
            }
        }
        protected BaseViveModel(DataGrid dataGrid, string Getaddres, string Setaddres)
        {
            this.dataGrid = dataGrid;
            GetDataCommand = new RelayCommand(GetDataList);
            SendDataCommand = new RelayCommand(SendDataToServer);
            _Getaddres = Getaddres;
            _Setaddres = Setaddres;
            GetDataList();
            DeleteCommand = new RelayCommand<T>(DeleteRow);
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
                    HttpResponseMessage response = await client.PostAsync($"{AuthData.ServerAddres + this._Getaddres}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Получаем JSON-ответ от сервера
                        string responseJson = await response.Content.ReadAsStringAsync();

                        // Преобразуем JSON-ответ в список объектов DataList
                        List<T> Tdata = JsonConvert.DeserializeObject<List<T>>(responseJson);

                        // Заполняем свойство DataList
                        DataList = new ObservableCollection<T>(Tdata);
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

                    string jsonData = JsonConvert.SerializeObject(DataList);

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                    HttpResponseMessage response = await client.PostAsync($"{AuthData.ServerAddres+_Setaddres}", content);


                    if (response.IsSuccessStatusCode)
                    {
                        string responseJson = await response.Content.ReadAsStringAsync();
                        List<T> updatedData = JsonConvert.DeserializeObject<List<T>>(responseJson);

                        DataList = new ObservableCollection<T>(updatedData);
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
