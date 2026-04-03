using MauiAppWeather.Models;
using MauiAppWeather.Services;

namespace MauiAppWeather
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.Getprevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao =
                            $"Latitude: {t.lat}\n" +
                            $"Longitude: {t.lon}\n" +
                            $"Nascer do sol: {t.sunrise}\n" +
                            $"Pôr do sol: {t.sunset}\n" +
                            $"Temp Máx: {t.temp_max}\n" +
                            $"Temp Mín: {t.temp_min}\n";

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        lbl_res.Text = "Sem dados de previsão do tempo";
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha a cidade para obter a previsão do tempo";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops...", ex.Message, "OK");
            }
        }
    }
}