using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MIUCSH
{
    public partial class EscuelasPage : ContentPage
    {
        private ObservableCollection<FacultadesClass> _escuela;
        public List<EscuelaClass> Escuela;
        private string Aurl = "127.0.0.1";
        private  string Url = "http://127.0.0.1:8020/escuelas?facu=FS&tipo=01";
        private readonly HttpClient client = new HttpClient();
        private string captio = "";
        public EscuelasPage(string Durl, string facul, string selec)
        {
            Aurl = Durl;
            captio = selec;
           
            Url = "http://" + Aurl + ":8020/escuelas?facu="+facul+"&tipo=01";
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            string content = await client.GetStringAsync(Url);
            Escuela = JsonConvert.DeserializeObject<List<EscuelaClass>>(content);
            Escuelas.ItemsSource = Escuela;
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

        async void Escuelas_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            string selec = Escuelas.SelectedItem.ToString();
            string cod = "0";
            for(int r=0; r< Escuela.Count; r++) 
            {
                if (selec.Equals(Escuela[r].escuela)) cod = Escuela[r].codigo;
            }
            string hacer = captio + "/" + Escuelas.SelectedItem.ToString();
            Page p = new ProgramasPage(Aurl, cod,hacer);
            await Navigation.PushModalAsync(p);
        }
    }
}
