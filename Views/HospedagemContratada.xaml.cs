
using MauiAppHotel.Models;
using System.Globalization;

namespace MauiAppHotel.Views;

public partial class HospedagemContratada : ContentPage
{
    public HospedagemContratada(
        Quarto quarto,
        int adultos,
        int criancas,
        DateTime checkin,
        DateTime checkout)
    {
        InitializeComponent();

        int estadia = (checkout - checkin).Days;

        double valorTotal =
            (adultos * quarto.ValorDiariaAdulto +
             criancas * quarto.ValorDiariaCrianca) * estadia;

        lbl_quarto.Text = quarto.Descricao;
        lbl_adultos.Text = adultos.ToString();
        lbl_criancas.Text = criancas.ToString();

        lbl_checkin.Text = checkin.ToString("dd/MM/yyyy");
        lbl_checkout.Text = checkout.ToString("dd/MM/yyyy");
        lbl_estadia.Text = estadia + " dias";

        lbl_valor_total.Text =
            valorTotal.ToString("C", new CultureInfo("pt-BR"));
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
