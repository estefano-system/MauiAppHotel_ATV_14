
using MauiAppHotel.Models;

namespace MauiAppHotel.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    App PropriedadesApp;

    public ContratacaoHospedagem()
    {
        InitializeComponent();

        PropriedadesApp = (App)Application.Current!;

        pck_quarto.ItemsSource = PropriedadesApp.lista_quartos;

        dtpck_checkin.MinimumDate = DateTime.Today;
        dtpck_checkin.MaximumDate = DateTime.Today.AddMonths(1);

        DateTime dataCheckin = dtpck_checkin.Date ?? DateTime.Today;

        dtpck_checkout.MinimumDate = dataCheckin.AddDays(1);
        dtpck_checkout.MaximumDate = dataCheckin.AddMonths(6);
        dtpck_checkout.Date = dataCheckin.AddDays(1);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            int adultos = (int)stp_adultos.Value;
            int criancas = (int)stp_criancas.Value;

            Quarto? quartoSelecionado = pck_quarto.SelectedItem as Quarto;

            DateTime checkin = dtpck_checkin.Date ?? DateTime.Today;
            DateTime checkout = dtpck_checkout.Date ?? DateTime.Today;

            if (adultos + criancas == 0)
            {
                await DisplayAlertAsync("Atenção", "Informe pelo menos um hóspede.", "OK");
                return;
            }

            if (quartoSelecionado == null)
            {
                await DisplayAlertAsync("Atenção", "Selecione uma acomodação.", "OK");
                return;
            }

            if (checkout <= checkin)
            {
                await DisplayAlertAsync("Atenção", "A data de checkout deve ser posterior ao check-in.", "OK");
                return;
            }

            await Navigation.PushAsync(
                new HospedagemContratada(
                    quartoSelecionado,
                    adultos,
                    criancas,
                    checkin,
                    checkout
                )
            );
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "OK");
        }
    }

    private void dtpck_checkin_DateSelected(object sender, DateChangedEventArgs e)
    {
        DateTime dataSelecionada = e.NewDate ?? DateTime.Today;

        dtpck_checkout.MinimumDate = dataSelecionada.AddDays(1);
        dtpck_checkout.MaximumDate = dataSelecionada.AddMonths(6);
        dtpck_checkout.Date = dataSelecionada.AddDays(1);
    }
}
