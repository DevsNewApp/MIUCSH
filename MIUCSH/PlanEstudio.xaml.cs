using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MIUCSH
{
    public partial class PlanEstudio : ContentPage
    {
        private ObservableCollection<Academica> _plan;
        public IList<PlanEstudioClass> PlanE { get; private set; }
        public List<PlanEstudioClass> Planes;
        public List<PlanEstudioClass> Oferta;
        private string Aurl = "127.0.0.1";
        private string Url;
        private readonly HttpClient client = new HttpClient();
        private int anyo = 0;
        private int sem = 0;
        private List<CargaClass> cargas;
        private string titulo = "Plan de Estudios";
        private PeriodosClass periodo;
        public PlanEstudio(string Durl, string nmat,string se, string tit, List<CargaClass> carga )
        {
            Aurl = Durl;
            titulo = tit;
            cargas = carga;
            sem = Int32.Parse(se);
            Url = "http://" + Aurl + ":8020/pestudio?nmat=" + nmat;
            InitializeComponent();
            
        }
        protected override  void OnAppearing()
        {
            Titulos.Text = titulo;
            Planes = new List<PlanEstudioClass>();
            string content= "";
            try
            {
                Device.BeginInvokeOnMainThread(async () => {
                    content = await client.GetStringAsync(Url);
               
                Planes = JsonConvert.DeserializeObject<List<PlanEstudioClass>>(content);
             
            DiaSem.Text = "Semestre " + sem.ToString();
            Oferta = new List<PlanEstudioClass>();// parseInt
            for (var rw = 0; rw < Planes.Count; rw++)
            {
                if (Planes[rw].nivel == sem.ToString())
                {
                            //  Planes[rw].codigo = Planes[rw].codigo + "-" + Planes[rw].s
                            Planes[rw].tipoCredito = "Black";
                         if (Planes[rw].situacion == "AP") Planes[rw].situacion = "APROBADO";
                            if (Planes[rw].situacion == "AD") Planes[rw].situacion = "APROBADO";
                            if (Planes[rw].situacion == "RR")
                            {
                                Planes[rw].situacion = "REPROBADO";
                                Planes[rw].tipoCredito = "Red";
                            }
                            int indi = 0;
                            for (int ru = 0; ru < cargas.Count; ru++)
                            {
                                if (Planes[rw].codigo == cargas[ru].asig) indi = 1;
                            }
                            if (indi == 1) Planes[rw].situacion = "CURSANDO";
                            if (Planes[rw].situacion == "[object Object]")
                            {
                                Planes[rw].nota = "  ";
                                Planes[rw].situacion = "NO CURSADA";
                            }
                            Oferta.Add(Planes[rw]);
                }
            }
            Plan.ItemsSource = Oferta;
            base.OnAppearing();
                });
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    await DisplayAlert("Error", "Problemas " + e.ToString(), "OK");

                });

            }
        }
        void refresca()
        {
            DiaSem.Text = "Semestre " + sem.ToString();
            Titulos.Text = titulo;
            Oferta = new List<PlanEstudioClass>();// parseInt
            for (var rw = 0; rw < Planes.Count; rw++)
            {
                if (Planes[rw].nivel == sem.ToString())
                {
                    Planes[rw].tipoCredito = "Black";
                    if (Planes[rw].situacion == "AP") Planes[rw].situacion = "APROBADO";
                    if (Planes[rw].situacion == "AD") Planes[rw].situacion = "APROBADO";
                    if (Planes[rw].situacion == "RR")
                    {
                        Planes[rw].situacion = "REPROBADO";
                        Planes[rw].tipoCredito = "Red";
                    }
                    int indi = 0;
                    for (int ru = 0; ru < cargas.Count; ru++)
                    {
                        if (Planes[rw].codigo == cargas[ru].asig) indi = 1;
                    }
                    if (indi == 1) Planes[rw].situacion = "CURSANDO";
                    if (Planes[rw].situacion == "[object Object]")
                    {
                        Planes[rw].nota = "  ";
                        Planes[rw].situacion = "NO CURSADA";
                    }
                    Oferta.Add(Planes[rw]);
                }
            }
            Plan.ItemsSource = Oferta;

        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            
        }

        void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
           
                if (sem > 1) { sem--; }
                else
                if (sem == 1) { sem = 10; }
            
            this.refresca();
        }

        void ImageButton_Clicked_1(System.Object sender, System.EventArgs e)
        {
            
                if (sem < 10) sem++;
                else
                if (sem == 10) { sem = 1; }
            
            this.refresca();

        }
    }
}
