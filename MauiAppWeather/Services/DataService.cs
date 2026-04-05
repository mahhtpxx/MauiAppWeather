using MauiAppWeather.Models;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net;

namespace MauiAppWeather.Services
{
    public class DataService
    {
        public static async Task<Tempo?> Getprevisao(string cidade)
        {
            Tempo? t = null;

            string chave = "6135072afe7f6cec1537d5cb08a5a1a2";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&units=metric&appid={chave}";

            using (HttpClient client = new HttpClient())
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
                    }; //fecha o obj do tempo (tudo que pegamos do api)
                }//fecha o if se o status do servidor foi = sucesso
                else if (resp.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Cidade não encontrada. Verifique o nome digitado.");
                } //verifica se o status do servidor foi = cidade não encontrada
                else
                {
                    throw new Exception($"Erro ao consultar a API. Código: {(int)resp.StatusCode}");
                }//se o status do servidor foi diferente de sucesso ou cidade não encontrada, lança uma exceção com o código do erro
            } //fecha laço using


            return t;
        }
    }
}
