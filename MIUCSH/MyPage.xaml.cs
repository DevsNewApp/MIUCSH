using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MIUCSH
{
    public partial class MyPage : ContentPage
    {
        private ObservableCollection<Asistencias> _asis;
        public IList<Asistencias> Asis { get; private set; }
        private string Aurl = "127.0.0.1";
        private List<CargaClass> cargaAca;
        private readonly HttpClient client = new HttpClient();
        private PeriodosClass period;
        private string nmat = "";
        public MyPage(string Durl, string n, List<CargaClass> carga, PeriodosClass p)
        {
            Aurl = Durl;
            period = p;
            cargaAca = carga;
            nmat = n;
        InitializeComponent();
      
       //     BindingContext = this;

        }
        protected override  void OnAppearing()
        {
            List<string> anombre = new List<string>();
            string valor = "ASISTENCIAS ("+period.anyo + " - " + period.sem+")";
            titulos.Text = valor;
            Asis = new List<Asistencias>();
            List<Asistencias> Asis2 = new List<Asistencias>();
            List<Asistencias> Asis3 = new List<Asistencias>();
         
            Asis2.Add(new Asistencias
            {
                Materia = "CLINICAS JURIDICAS Y TECNICAS DE LITIGACION II",
                Asiste = "20 Días Asistencia",
                Noasiste = "4 Días Inasistencia",
                Porceng = "84%",
                Imagen = "Orange"
            });
            Asis2.Add(new Asistencias
            {
                Materia = "DERECHO PROCESAL LITIGIOS III",
                Asiste = "24 Días Asistencia",
                Noasiste = "0 Días Inasistencia",
                Porceng = "100%",
                Imagen = "Green"
            });
            // http://app.ucsh.cl:8020/asisten?anoa=
            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    for (int uy = 0; uy < cargaAca.Count; uy++)
                    {
                        string content = "";
                        string URL = "http://" + Aurl + ":8020/asisten?anoa=" + period.anyo + "&tipe=S&vepe="+ period.sem+"&asig=" + cargaAca[uy].asig + "&secc=" + cargaAca[uy].seccion +
                    "&nmat=" + nmat + "&vers=0&func="+ cargaAca[uy].cod_func+ "&nrgr=0";
                     //   notifica("Esta es URL", URL);

                        try
                        {
                          
                            content = await client.GetStringAsync(URL);
                            List<AsisteClass> asisten = JsonConvert.DeserializeObject <List<AsisteClass>>(content);
                          for (int kl = 0; kl < asisten.Count; kl++)
                            {
                                int r = 0;
                                for (int ro = 0; ro < anombre.Count; ro++) if (anombre[ro] == cargaAca[uy].desc_asig) r = 1;
                                if (asisten[kl].curs_sasi == "NO" || r ==1)
                                {
                                    if (r != 1) anombre.Add(cargaAca[uy].desc_asig);
                                    string nombreAs = cargaAca[uy].desc_asig;
                                    if (cargaAca[uy].cod_func == "02") nombreAs= "AYUDANTÍA "+nombreAs;
                                    if (cargaAca[uy].cod_func == "03") nombreAs = "LABORATORIO " + nombreAs;
                                    if (cargaAca[uy].cod_func == "04") nombreAs = "TALLER " + nombreAs;
                                    if (cargaAca[uy].cod_func == "05") nombreAs = "CLINICA " + nombreAs;
                                    if (cargaAca[uy].cod_func == "06") nombreAs = "PRACTICA " + nombreAs;
                                    if (cargaAca[uy].cod_func == "07") nombreAs = "SIMULACIÓN " + nombreAs;
                                    nombreAs = cargaAca[uy].asig + "-" + cargaAca[uy].seccion +" "+ nombreAs;
                                    Asis3.Add(new Asistencias
                                    {
                                        Materia = nombreAs,
                                        Asiste = "SIN ",
                                        Noasiste = "ASISTENCIA ",
                                        Porceng = "N/A",
                                        Imagen = "White"
                                    });
                                }
                                else
                                {
                                    string nombreAs = cargaAca[uy].desc_asig;
                                    if (cargaAca[uy].cod_func == "02") nombreAs = "AYUDANTÍA " + nombreAs;
                                    if (cargaAca[uy].cod_func == "03") nombreAs = "LABORATORIO " + nombreAs;
                                    if (cargaAca[uy].cod_func == "04") nombreAs = "TALLER " + nombreAs;
                                    if (cargaAca[uy].cod_func == "05") nombreAs = "CLINICA " + nombreAs;
                                    if (cargaAca[uy].cod_func == "06") nombreAs = "PRACTICA " + nombreAs;
                                    if (cargaAca[uy].cod_func == "07") nombreAs = "SIMULACIÓN " + nombreAs;
                                    nombreAs = cargaAca[uy].asig + "-" + cargaAca[uy].seccion + " " + nombreAs;
                                    string reque = asisten[kl].asis_req;
                                    string inasi = asisten[kl].asis_ina;
                                    string inge = asisten[kl].asis_ing;
                                    if (reque == "undefined") reque = "0.0";
                                    if (inasi == "undefined") inasi = "0.0";
                                    if (inge == "undefined") inge = "0.0";
                                    Asis3.Add(new Asistencias
                                    {
                                        Materia = nombreAs,
                                        Asiste = reque + " % Requerido",
                                        Noasiste = inasi + " % Inasistencia",
                                        Porceng = inge+" %",
                                        Imagen = "Green"
                                    });
                                }
                            }
                        } catch (Exception f)
                        {
                           notifica("ERROR", content);
                        }
                    }

                    Asistencias.ItemsSource = Asis3;
                });
            } catch(Exception f)
            {

            }
          
            base.OnAppearing();
        }
        async void notifica(string titulos, string cuerpos)
        {
            string titulo = titulos;
            string cuerpo = cuerpos;
            await DisplayAlert(titulo, cuerpo, "Ok");
           
        }
        async void OkButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
