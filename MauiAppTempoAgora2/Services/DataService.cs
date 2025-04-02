using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MauiAppTempoAgora2.Models;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora2.Services
{
    public class DataService
    {
        public static bool SimulateOfflineMode = false;

        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null;

            // Simulação de ambiente offline
            if (SimulateOfflineMode)
            {
                throw new Exception("Simulação: Sem conexão com a internet.");
            }

            string chave = "6135072afe7f6cec1537d5cb08a5a1a2";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&units=metric&appid={chave}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage resp = await client.GetAsync(url);

                    if (resp.IsSuccessStatusCode)
                    {
                        string json = await resp.Content.ReadAsStringAsync();
                        var rascunho = JObject.Parse(json);

                        DateTime time = new();
                        DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                        DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                        t = new()
                        {
                            lat = (double)rascunho["coord"]["lat"],
                            lon = (double)rascunho["coord"]["lon"],
                            description = (string)rascunho["weather"][0]["description"],
                            main = (string)rascunho["weather"][0]["main"],
                            temp_min = (double)rascunho["main"]["temp_min"],
                            temp_max = (double)rascunho["main"]["temp_max"],
                            speed = (double)rascunho["wind"]["speed"],
                            visibility = (int)rascunho["visibility"],
                            sunrise = sunrise.ToString(),
                            sunset = sunset.ToString(),
                        };
                    }
                    else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new Exception("Cidade não encontrada.");
                    }
                    else
                    {
                        throw new Exception("Erro ao buscar os dados. Verifique sua conexão com a internet.");
                    }
                }
                catch (HttpRequestException)
                {
                    throw new Exception("Sem conexão com a internet.");
                }
            }
            return t;
        }
    }
}
