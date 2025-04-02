using MauiAppTempoAgora2.Models;
using MauiAppTempoAgora2.Services;

namespace MauiAppTempoAgora2
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
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = $"Latitude: {t.lat}\n" +
                                                $"Longitude: {t.lon}\n" +
                                                $"Descrição: {t.description}\n" +
                                                $"Velocidade do Vento: {t.speed} m/s\n" +
                                                $"Visibilidade: {t.visibility} m\n" +
                                                $"Nascer do Sol: {t.sunrise}\n" +
                                                $"Pôr do Sol: {t.sunset}\n" +
                                                $"Temp Máx: {t.temp_max} °C\n" +
                                                $"Temp Min: {t.temp_min} °C\n";

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        lbl_res.Text = "Sem dados de previsão.";
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha o nome da cidade.";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        // Método para alternar o modo offline
        private void ToggleOfflineMode(object sender, EventArgs e)
        {
            DataService.SimulateOfflineMode = !DataService.SimulateOfflineMode;
            string status = DataService.SimulateOfflineMode ? "ativado" : "desativado";
            DisplayAlert("Modo Offline", $"Modo offline {status}.", "OK");
        }
    }
}
