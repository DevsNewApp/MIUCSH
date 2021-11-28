using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MIUCSH
{
    public partial class OfertaPage : ContentPage
    {
    
      


        private ObservableCollection<Oferta> _oferta;
        public List<OfertasClass> Oferta;
        private string Aurl = "127.0.0.1";
        private string program = "";
        private string Url = "http://127.0.0.1:8020/escuelas?facu=FS&tipo=01";
        private readonly HttpClient client = new HttpClient();
        private string captio = "";
        public OfertaPage(string Durl,  string programa, string capto)
        {
            Aurl = Durl;
            captio = capto;
            program = programa;
            Url = "http://" + Aurl + ":8020/ofertas?prog="+programa+"&anoa=2020&vepe=1";


            InitializeComponent();
          
        }
        protected override async void OnAppearing()
        {
            string content = await client.GetStringAsync(Url);
            Oferta = JsonConvert.DeserializeObject<List<OfertasClass>>(content);
            List<OfertasClass> oferta;
            oferta = new List<OfertasClass>();
            for (int t = 0; t < Oferta.Count; t++)
            {
                int r = 0;
                for (int y = 0; y < oferta.Count; y++)
                {
                    if (oferta[y].asig_desc == Oferta[t].asig_desc)
                    {
                        r = 1;
                        int yu = Int32.Parse(oferta[y].asig_ticr);
                        yu = yu + 1;
                        oferta[y].asig_ticr = yu.ToString();
                    }
                }
                if (r == 0)
                {
                    Oferta[t].asig_ticr = "1";
                    oferta.Add(Oferta[t]);
                   
                }
            }
            Notas.ItemsSource = oferta;
            Caption.Text = captio;
            base.OnAppearing();
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Notas_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            OfertasClass selec = (OfertasClass) Notas.SelectedItem;
            string home = captio ;
            Page p = new OfertaDetallePage(Aurl, selec.asig_codi, program, home);
            await Navigation.PushModalAsync(p);
        }
    }
}
