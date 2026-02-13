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
    internal class Lines
    {
        public async Task AllLinesAsync()
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
                    JArray jsonArray = JArray.Parse(jsonResponse);

                    List<string> namesList = new List<string>();
                    List<int> idList = new List<int>();

                    foreach (JObject item in jsonArray)
                    {
                        int id = (int)item["id"];
                        string name = item["name"]?.ToString();

                        if (!string.IsNullOrEmpty(name) && id > 0)
                        {
                            namesList.Add(name);
                            idList.Add(id);
                        }
                    }

                    string[] namesArray = namesList.ToArray();
                    int[] idArray = idList.ToArray();


                    foreach (string name in namesArray)
                    {
                        MessageBox.Show(name);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
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
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return responseBody;
                    }
                    else
                    {
                        Console.WriteLine($"HTTP Error: {response.StatusCode}");
                        return null;
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Ошибка HTTP: {e.Message}");
                    return null;
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("Таймаут запроса к серверу");
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Общая ошибка: {ex.Message}");
                    return null;
                }
            }
        }

        public string[] linesName = { "Боссар 1", "Боссар 2", "Боссар 3", "Боссар 4", "Боссар 5", "Боссар 6",
                "Боссар 7", "Боссар 8", "Боссар 9", "Боссар 10", "Боссар 11",
                "Боссар 12", "Боссар 13", "Волпак 1", "Волпак 2", "Волпак 3",
                "Волпак 4", "ЛМ 1", "ЛМ 2", "ЛМ 4", "ЛК 3", "Меспак", "Стеклобанка" };

        public static int GetLineIdFromName(string lineName)
        {
            if (string.IsNullOrEmpty(lineName))
                return 0;

            var lineMappings = new Dictionary<string, int>
    {
        { "Боссар 1", 1 },
        { "Боссар 2", 2 },
        { "Боссар 3", 3 },
        { "Боссар 5", 4 },
        { "Боссар 8", 5 },
        { "Боссар 9", 6 },
        { "Боссар 10", 7 },
        { "Боссар 11", 8 },
        { "Боссар 13", 9 },
        { "Волпак 3", 10 },
        { "Боссар 6", 11 },
        { "Боссар 7", 12 },
        { "Боссар 12", 13 },
        { "Меспак", 14 },
        { "Волпак 2", 15 },
        { "Волпак 1", 16 },
        { "Волпак 4", 17 },
        { "Боссар 4", 18 },
        { "Стеклобанка", 19 },
        { "ЛМ 1", 20 },
        { "ЛМ 3", 21 },
        { "ЛМ 4", 22 },
        { "ЛК 3", 23 }
    };

            return lineMappings.TryGetValue(lineName, out int lineId) ? lineId : 0;
        }
    }
}