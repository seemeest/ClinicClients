using ClinicClients.Data;
using ClinicClients.Model;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace ClinicClients.ViewMode.UserModel.ProfileViveModels
{
    public partial class ProfileViveModel : DependencyObject , INotifyPropertyChanged
    {
       Doctor doctor { get; set; }

     
        public ProfileViveModel()
        {
            GetDataList();

            SendImageCommand = new RelayCommand(SendImage);
            GetImageFromServerAsync();
        }

        private void SendImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|Все файлы (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                string format = selectedFilePath.Remove(0, selectedFilePath.LastIndexOf('.') + 1);

                UploadImage(selectedFilePath);

            }
        }

     

        public static async Task UploadImage(string filePath)
        {
            try
            {
                string apiUrl = $"{AuthData.ServerAddres + AddresList.SetUserImage}";

                using (var httpClient = new HttpClient())
                {
                    // Создаем поток запроса
                    using (var requestStream = new MemoryStream())
                    {
                        // Открываем файл для чтения
                        using (var fileStream = File.OpenRead(filePath))
                        {
                            // Копируем данные файла в поток запроса
                            await fileStream.CopyToAsync(requestStream);
                        }

                        // Устанавливаем позицию потока запроса в начало
                        requestStream.Position = 0;

                        // Создаем объект запроса
                        var request = new HttpRequestMessage(HttpMethod.Post, apiUrl)
                        {
                            Content = new StreamContent(requestStream)
                        };

                        // Устанавливаем заголовки запроса
                        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        request.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = "file",
                            FileName = Path.GetFileName(filePath)
                        };

                        // Добавляем авторизацию
                        string username = AuthData.Login;
                        string password = AuthData.password;
                        string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                        // Отправляем запрос и получаем ответ
                        var response = await httpClient.SendAsync(request);

                        if (response.IsSuccessStatusCode)
                        {
                            // Файл успешно отправлен на сервер
                            Console.WriteLine("Файл успешно отправлен на сервер.");
                        }
                        else
                        {
                            // Ошибка при отправке файла
                            Console.WriteLine("Ошибка при отправке файла на сервер.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
        }

        //public async void GetImageFromServerAsync()
        //{
        //    string url = $"{AuthData.ServerAddres + AddresList.GetUserImage}";
        //    ImageBrush imageBrush = new ImageBrush();

        //    string username = AuthData.Login;
        //    string password = AuthData.password;
        //    string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

        //        HttpResponseMessage response = await client.GetAsync(url);

        //        // Запись данных в файл лога
        //        string logFilePath = "request.log"; // Путь к файлу лога

        //        // Создаем или открываем файл лога для записи
        //        using (StreamWriter logWriter = new StreamWriter(logFilePath, true))
        //        {
        //            // Записываем данные в файл лога
        //            logWriter.WriteLine($"URL: {url}");
        //            logWriter.WriteLine($"Username: {username}");
        //            logWriter.WriteLine($"Password: {password}");

        //            // Записываем содержимое ответа сервера
        //            logWriter.WriteLine("Response:");
        //            logWriter.WriteLine(response);
        //            logWriter.WriteLine(await response.Content.ReadAsStringAsync());
        //        }

        //        if (response.IsSuccessStatusCode)
        //        {
        //            using (Stream imageStream = await response.Content.ReadAsStreamAsync())
        //            {
        //                using (MemoryStream memoryStream = new MemoryStream())
        //                {

        //                    await imageStream.CopyToAsync(memoryStream);
        //                    memoryStream.Position = 0;

        //                    BitmapImage bitmapImage = new BitmapImage();
        //                    bitmapImage.BeginInit();
        //                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        //                    bitmapImage.StreamSource = memoryStream;
        //                    bitmapImage.EndInit();

        //                    imageBrush.ImageSource = bitmapImage;
        //                }
        //            }
        //        }
        //    }

        //    ProfilAvatar = imageBrush;
        //}


        public async void GetImageFromServerAsync()
        {
            string url = $"{AuthData.ServerAddres + AddresList.GetUserImage}";

            string username = AuthData.Login;
            string password = AuthData.password;
            string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                HttpResponseMessage response = await client.GetAsync(url);

                // Запись данных в файл лога
                string logFilePath = "request.log"; // Путь к файлу лога
                string imageFilePath = "image"; // Путь для сохранения изображения

                // Создаем или открываем файл лога для записи
                using (StreamWriter logWriter = new StreamWriter(logFilePath, true))
                {
                    // Записываем данные в файл лога
                    logWriter.WriteLine($"URL: {url}");
                    logWriter.WriteLine($"Username: {username}");
                    logWriter.WriteLine($"Password: {password}");

                    // Записываем содержимое ответа сервера
                    logWriter.WriteLine("Response:");
                    logWriter.WriteLine(response);
                    logWriter.WriteLine(await response.Content.ReadAsStringAsync());
                }

                if (response.IsSuccessStatusCode)
                {
                    using (Stream imageStream = await response.Content.ReadAsStreamAsync())
                    {
                        using (FileStream fileStream = new FileStream(imageFilePath, FileMode.Create))
                        {
                            await imageStream.CopyToAsync(fileStream);
                        }
                    }
                }
            }
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
                    var content = new System.Net.Http.StringContent(jsonData, Encoding.UTF8, "application/json");

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

        private static void OnProfilAvatarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = (ProfileViveModel)d;
            model.NotifyPropertyChanged(nameof(ProfilAvatar));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
