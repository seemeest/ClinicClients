using ClinicClients.Data;
using ClinicClients.Model;
using ClinicClients.Vive;
using Microsoft.Xaml.Behaviors.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClinicClients.Control
{
    static class Auth
    {
        public static async  void auth()
        {

            string authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{AuthData.Login}:{AuthData.password}"));
            string authorization = $"Basic {authHeader}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", authorization);


                    HttpResponseMessage response = await client.GetAsync(AuthData.ServerAddres);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();

                        ThisPage.thisPage = new Home();
                        return;
                    }
                    Error.AuthError = "Ошибка авторизации";

                }
            }
            catch
            {
                Error.AuthError = "Ошибка Подключения";
                return;
            }
        }
    }

}
