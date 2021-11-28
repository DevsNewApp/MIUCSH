using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MIUCSH
{
    public partial class ProgramasPage : ContentPage
    {
        private ObservableCollection<ProgramasClass> _programa;
        public List<ProgramasClass> Programa;
        private string Aurl = "127.0.0.1";
        private string escuela = "";
        private string Url = "http://127.0.0.1:8020/programas?escu=370&anoa=2020&vepe=1&tipo=PR";
        private readonly HttpClient client = new HttpClient();
        private string captio = "";
        public ProgramasPage(string Durl, string escuel, string optio)
        {
            captio = optio;
            escuela = escuel;
            Aurl = Durl;
            Url = "http://" + Aurl + ":8020/programas?escu="+escuela+"&anoa=2020&vepe=1&tipo=PR";
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            string content = await client.GetStringAsync(Url);
            Programa = JsonConvert.DeserializeObject<List<ProgramasClass>>(content);
           // Temp.Text = escuela;
           for(int i=0;i<Programa.Count;i++)
            {
                if (Programa[i].jornada == "[object Object]")
                    Programa[i].jornada = "DIURNA";
            }
            Programas.ItemsSource = Programa;
            Caption.Text = captio;
            base.OnAppearing();
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
           
        }

        async void Programas_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            string selec = Programas.SelectedItem.ToString();
            string cod = "0";
            for (int r = 0; r < Programa.Count; r++)
            {
                if (selec.Equals(Programa[r].programa)) cod = Programa[r].codigo;
            }
            string home = captio ;
            Page p = new OfertaPage(Aurl, cod,home );
            await Navigation.PushModalAsync(p);
        }
    }
}
