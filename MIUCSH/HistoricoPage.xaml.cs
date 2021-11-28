using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MIUCSH
{
    public partial class HistoricoPage : ContentPage
    {
        private ObservableCollection<Academica> _plan;
        public IList<PlanEstudioClass> PlanE { get; private set; }
        public List<PlanEstudioClass> Planes;
        public List<PlanEstudioClass> Oferta;
        public List<HistoricoNotasClass> historico;
        private string Aurl = "127.0.0.1";
        private string Url;
        private readonly HttpClient client = new HttpClient();
        private int anyo = 0;
        private int anyoActual = 0;
        private int sem = 0;
        private string titulo = "";
        private List<CargaClass> cargas;
        private string nmat;
        private int ma = 2027;
        private int mp = 1;
        private int semo = 1;
        private PeriodosClass periodo;
        public HistoricoPage(string Durl, string mat, List<CargaClass> carga, PeriodosClass per, string tit, int a, int p)
        {
            ma = a;
            mp = p;
            nmat = mat;
            titulo = tit;
            Aurl = Durl;
            cargas = carga;
            periodo = per;
            anyo = Int32.Parse(per.anyo);
            sem = Int32.Parse(per.sem);
            semo = sem;
            anyoActual = anyo;
            Url = "http://" + Aurl + ":8020/historicoNotas?nmat=" + nmat+ "&tipo=1";


            InitializeComponent();

            BindingContext = this;
        }
        protected override async void OnAppearing()
        {
            Oferta = new List<PlanEstudioClass>();
            Titulos.Text = titulo;
            try
            {
                string URL = Url;
                string content2 = await client.GetStringAsync(URL);
                historico = JsonConvert.DeserializeObject<List<HistoricoNotasClass>>(content2);
                
            }
            catch (Exception y) { }
            
            for (var rw = 0; rw < historico.Count; rw++)
            {
                PlanEstudioClass plani = new PlanEstudioClass();
                if (historico[rw].vAgno==anyo.ToString() && historico[rw].vVepe == sem.ToString())
                {
                    plani.tipoCredito = "Black";
                    plani.situacion = historico[rw].vDescF;
                    plani.codigo = historico[rw].vAsig +"-"+ historico[rw].vSecc;
                    plani.actividad = historico[rw].vDesc;
                    plani.credito = historico[rw].vCRed;
                    plani.nota = historico[rw].vNotaF;
                    if (plani.situacion == "") plani.situacion = "CURSANDO";
                    if (plani.nota == "undefined") plani.nota = "-" ;
                    if (plani.situacion.Trim() == "REPROBADO") plani.tipoCredito = "Red";
                    Oferta.Add(plani);

                }
                

                
                

              
            }
            Plan.ItemsSource = Oferta;
            DiaSem.Text = anyo.ToString() + "-" + sem.ToString();
           base.OnAppearing();
        }
        void refresca()
        {

            Oferta = new List<PlanEstudioClass>();
            DiaSem.Text = anyo.ToString() + "-" + sem.ToString();

            for (var rw = 0; rw < historico.Count; rw++)
            {
              
                   
              
                PlanEstudioClass plani = new PlanEstudioClass();
                if (historico[rw].vAgno == anyo.ToString() && historico[rw].vVepe == sem.ToString())
                {
                    plani.tipoCredito = "Black";
                    plani.situacion = historico[rw].vDescF;
                    plani.codigo = historico[rw].vAsig + "-" + historico[rw].vSecc;
                    plani.actividad = historico[rw].vDesc;
                    plani.credito = historico[rw].vCRed;
                    plani.nota = historico[rw].vNotaF;
                    if (plani.situacion == "") plani.situacion = "CURSANDO";
                    if (plani.nota == "undefined") plani.nota = "-" ;
                    if (plani.situacion.Trim() == "REPROBADO") plani.tipoCredito = "Red";
                    Oferta.Add(plani);
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
            if (anyo <= anyoActual)
            {
                if (sem == 1) sem++;
                else
                if (sem == 2 && anyo != anyoActual) { sem = 1; anyo++; }
                if (anyo == anyoActual && sem > semo) sem = 1;
                
            }
            this.refresca();

        }
    
    }
}
