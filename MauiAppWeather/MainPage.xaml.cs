using MauiAppWeather.Models;
using MauiAppWeather.Services;
using System.Threading.Tasks;

namespace MauiAppWeather
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.Getprevisao(txt_cidade.Text);

                    if (t != null) 
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat}\n" +
                                         $"Longitude: {t.lon}\n" +
                                         $"Nascer do sol: {t.sunrise}\n" +
                                         $"Pôr do sol: {t.sunset}\n" +
                                         $"Temp Máx: {t.temp_max} \n" +
                                         $"Temp Mín: {t.temp_min} \n";

                    } else
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
                DisplayAlert("Ops...", ex.Message, "OK");
            }
        }
    }

}
