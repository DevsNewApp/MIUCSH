using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MIUCSH
{
    public partial class MyPlanEstudio : ContentPage
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
        private string titulo = "";
        private List<CargaClass> cargas;
        private int ma = 2027;
        private int mp = 1;
        private PeriodosClass periodo;
        public MyPlanEstudio(string Durl,string nmat, List<CargaClass> carga, PeriodosClass per, string tit, int a, int p)
    {
            ma = a;
            mp = p;
            titulo = tit;
            Aurl = Durl;
            cargas = carga;
            periodo = per;
            anyo = Int32.Parse(per.anyo);
            sem = Int32.Parse(per.sem);
            Url = "http://" + Aurl + ":8020/pestudio?nmat=" + nmat;
           
            
            InitializeComponent();
      
            BindingContext = this;
        }
        protected override async void OnAppearing()
        {
            string content = "";
            Titulos.Text = titulo;
            try
            {
                content = await client.GetStringAsync(Url);
                Planes = JsonConvert.DeserializeObject<List<PlanEstudioClass>>(content);
            }
            catch (Exception y) { }
            Oferta = new List<PlanEstudioClass>();// parseInt
            for (var rw = 0; rw < Planes.Count; rw++)
            {
                if (Planes[rw].anyo == anyo.ToString() && Planes[rw].periodo == sem.ToString())
                {
                    if (Planes[rw].situacion == "AP") Planes[rw].situacion = "APROBADO";
                    if (Planes[rw].situacion == "RR") Planes[rw].situacion = "REPROBADO";
                    int indi = 0;
                    for(int ru=0; ru< cargas.Count; ru++)
                    {
                        if (Planes[rw].codigo == cargas[ru].asig) indi = 1;
                    }
                    if (indi==1) Planes[rw].situacion = "CURSANDO";
                    if (Planes[rw].situacion == "[object Object]")
                    {
                        Planes[rw].situacion = "NO CURSADA";
                        Planes[rw].nota = "  ";
                    }
                    Oferta.Add(Planes[rw]);
                    
                }
            }
            Plan.ItemsSource = Oferta;
            DiaSem.Text = anyo.ToString() + "-" + sem.ToString();
            base.OnAppearing();
        }
        void refresca()
        {
            DiaSem.Text = anyo.ToString() + "-" + sem.ToString();
            Oferta = new List<PlanEstudioClass>();// parseInt
            for (var rw = 0; rw < Planes.Count; rw++)
            {
                if (Planes[rw].anyo == anyo.ToString() && Planes[rw].periodo == sem.ToString())
                {
                    if (Planes[rw].situacion == "AP") Planes[rw].situacion = "APROBADO";
                    if (Planes[rw].situacion == "RR") Planes[rw].situacion = "REPROBADO";
                    int indi = 0;
                    for (int ru = 0; ru < cargas.Count; ru++)
                    {
                        if (Planes[rw].codigo == cargas[ru].asig) indi = 1;
                    }
                    if (indi == 1) Planes[rw].situacion = "CURSANDO";
                    if (Planes[rw].situacion == "[object Object]")
                    {
                        Planes[rw].situacion = "NO CURSADA";
                        Planes[rw].nota = "  ";
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
            if (anyo > ma)
            {
                if (sem == 1) { sem = 2; anyo--; }
                else
                if (sem == 2) { sem = 1; }
            }
            if (anyo == ma && sem > mp)
            {
                sem = 1;
            }
           
            this.refresca();
        }

        void ImageButton_Clicked_1(System.Object sender, System.EventArgs e)
        {
            if (anyo < 2020)
            {
                if (sem == 1) sem++;
                else
                if (sem == 2) { sem = 1;anyo++; }
            }
            this.refresca();

        }
    }
}
