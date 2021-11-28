using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MIUCSH
{
    public partial class OfertaDetallePage : ContentPage
    {

        private ObservableCollection<Oferta> _oferta;
        public List<OfertasClass> Ofertas;
        public List<OfertasClass> Oferta;
        private string Aurl = "127.0.0.1";
        private string Url = "http://127.0.0.1:8020/escuelas?facu=FS&tipo=01";
        private readonly HttpClient client = new HttpClient();
        private string codigo = "";
        private string captio = "";
        public OfertaDetallePage(string Durl, string cod, string programa, string optio)
        {

            Aurl = Durl;
            codigo = cod;
            captio = optio;
            Url = "http://" + Aurl + ":8020/ofertas?prog=" + programa + "&anoa=2020&vepe=1";

            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            string content = await client.GetStringAsync(Url);
            Ofertas = JsonConvert.DeserializeObject<List<OfertasClass>>(content);
            Oferta = new List<OfertasClass>();
            for (var rw=0; rw < Ofertas.Count; rw++)
            {
                 if (Ofertas[rw].asig_codi == codigo)
                {
                    Ofertas[rw].asig_acad = "   " + Ofertas[rw].asig_acad;
                    Ofertas[rw].asig_secc = "Seccion:" + Ofertas[rw].asig_secc + " Cupos:" + Ofertas[rw].asig_cupo;
                    Oferta.Add(Ofertas[rw]);
                }
            }
            Caption.Text = captio;
            Notas.ItemsSource = Oferta;
            base.OnAppearing();
        }

        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        async void HomeButtonClicked(object sender, EventArgs e)
        {
            await Navigation.UnwindModalStackTo<MainPage>();

        }
    }
}
