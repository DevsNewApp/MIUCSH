using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace MIUCSH
{
    public partial class FacultadesSel : ContentPage
    {
        private ObservableCollection<FacultadesClass> _facultad;
        public List<FacultadesClass> Facultad;
        private string Aurl = "127.0.0.1";
        private  string Url = "http://127.0.0.1:8020/facultades?sede=02";
        private readonly HttpClient client = new HttpClient();
        public FacultadesSel(string Durl)
        {
            Aurl = Durl;
            Url = "http://"+Aurl+":8020/facultades?sede=02";
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            string content = await client.GetStringAsync(Url);
            Facultad = JsonConvert.DeserializeObject<List<FacultadesClass>>(content);
            Facultades.ItemsSource = Facultad;
            // Caption.Text = "Facultades";
            base.OnAppearing();
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

       

        async void Facultades_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            string selec = Facultades.SelectedItem.ToString();
            string cod = "0";
            for (int r = 0; r < Facultad.Count; r++)
            {
                if (selec.Equals(Facultad[r].facultad)) cod = Facultad[r].codigo;
            }
            Page p = new EscuelasPage(Aurl, cod,selec );
            await Navigation.PushModalAsync(p);
        }
    }
}
