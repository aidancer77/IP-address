using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IP
{
    internal class Lines
    {
        public async Task<string[]> GetAllLinesIdAsync()
        {
            try
            {
                // Создаем экземпляр ServerConnection
                ServerConnection serverConnection = new ServerConnection();

                // Получаем JSON с сервера
                string jsonResponse = await serverConnection.GetLinesJsonAsync();

                // Проверяем, что ответ не пустой
                if (string.IsNullOrEmpty(jsonResponse))
                {
                    MessageBox.Show("Не удалось получить данные с сервера");
                    return Array.Empty<string>();
                }

                // Парсим JSON
                JArray jsonArray = JArray.Parse(jsonResponse);

                List<string> idList = new List<string>();

                foreach (JObject item in jsonArray)
                {
                    // Безопасное получение id
                    if (item.TryGetValue("id", out JToken idToken))
                    {
                        int id = idToken.ToObject<int>();

                        if (id > 0)
                        {
                            idList.Add(id.ToString());
                        }
                    }
                }

                return idList.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return Array.Empty<string>();
            }
        }

        public async Task<string[]> GetAllLinesNameAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                    string jsonFilePath = Path.Combine(appFolder, "lineinfo.json");

                    string jsonContent = File.ReadAllText(jsonFilePath);

                    JsonDocument jsonDoc = JsonDocument.Parse(jsonContent);
                    JsonElement root = jsonDoc.RootElement;

                    string server = root.GetProperty("Server").GetString();

                    string urlAllLinesInfo = $"http://{server}/operators/get-lines";
                    string jsonResponse = await GetJSONFromURL(urlAllLinesInfo);

                    if (string.IsNullOrEmpty(jsonResponse))
                    {
                        MessageBox.Show("Не удалось получить данные с сервера");
                        return Array.Empty<string>();
                    }

                    JArray jsonArray = JArray.Parse(jsonResponse);

                    List<string> namesList = new List<string>();

                    foreach (JObject item in jsonArray)
                    {
                        string name = item["name"]?.ToString();

                        if (!string.IsNullOrEmpty(name))
                        {
                            namesList.Add(name);
                        }
                    }

                    return namesList.ToArray();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                    return Array.Empty<string>();
                }
            }
        }

        private static async Task<string> GetJSONFromURL(string urlJSON)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    HttpResponseMessage response = await client.GetAsync(urlJSON);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        MessageBox.Show($"HTTP Error: {response.StatusCode}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                catch (HttpRequestException e)
                {
                    MessageBox.Show($"Ошибка HTTP: {e.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                catch (TaskCanceledException)
                {
                    MessageBox.Show("Таймаут запроса к серверу", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Общая ошибка: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }
    }
}