using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IP
{
    internal class ServerConnection
    {
        public string GetServerAddress()
        {
            try
            {
                string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                string jsonFilePath = Path.Combine(appFolder, "lineinfo.json");

                if (!File.Exists(jsonFilePath))
                {
                    MessageBox.Show("Файл lineinfo.json не найден", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                string jsonContent = File.ReadAllText(jsonFilePath);
                JsonDocument jsonDoc = JsonDocument.Parse(jsonContent);
                JsonElement root = jsonDoc.RootElement;

                return root.GetProperty("Server").GetString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка чтения адреса сервера: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public async Task<string> GetLinesJsonAsync()
        {
            string server = GetServerAddress();

            if (string.IsNullOrEmpty(server))
            {
                return null;
            }

            string urlAllLinesInfo = $"http://{server}/operators/get-lines";
            return await GetJSONFromURL(urlAllLinesInfo);
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
