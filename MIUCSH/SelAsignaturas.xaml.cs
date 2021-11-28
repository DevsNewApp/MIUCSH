using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MIUCSH
{
    public partial class SelAsignaturas : ContentPage
    {
        public List<string> Asignaturas;
        private string nmat = "0";
        private string anyo = "2020";
        private string sem = "1";
        private List<CargaClass> carga;
        private List<CargaClass> cargaAcad;
        private PeriodosClass periodo;
        private string Aurl = "127.0.0.1";
        public SelAsignaturas(string Durl, List<CargaClass> cargaAca, PeriodosClass period, string numat)
        {
            Aurl = Durl;
            cargaAcad = new List<CargaClass>();
            cargaAcad = cargaAca;
            InitializeComponent();
            Asignaturas = new List<string>();
            anyo = period.anyo;
            sem = period.sem;
            nmat = numat;
            carga = new List<CargaClass>();
            periodo = period;

            for (int ty = 0; ty < cargaAca.Count; ty++)
            {
                string contenido = cargaAca[ty].asig +" " + cargaAca[ty].desc_asig;
               if ( cargaAca[ty].cod_func == "01")
                {
                    CargaClass tempora = new CargaClass();
                    tempora.asig = cargaAca[ty].asig + "-" + cargaAca[ty].seccion;
                    tempora.cod_func = cargaAca[ty].cod_func;
                    tempora.desc_asig = cargaAca[ty].desc_asig;
                    tempora.docente = cargaAca[ty].docente;
                    tempora.funcion = cargaAca[ty].funcion;
                    tempora.grupo = cargaAca[ty].grupo;
                    tempora.seccion = cargaAca[ty].seccion;
                    tempora.ticr = cargaAca[ty].ticr;
                    tempora.version = cargaAca[ty].version;
                    carga.Add(tempora);
                 Asignaturas.Add(contenido);
               }
            }

            picker.ItemsSource = Asignaturas;
            Sela.ItemsSource = carga;
            
        }
        protected override void OnAppearing()
        {
            string an = periodo.anyo;
            string se = periodo.sem;
            string tit = "NOTAS (" + an + " - " + se + ")";
            titulo.Text = tit;
            base.OnAppearing();
        }

        async void picker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            if (picker.SelectedItem != null)
            {
                String bus = picker.SelectedItem.ToString();
                bus = bus.Substring(bus.IndexOf(" "));
                await Navigation.PushModalAsync(new CalificacionPage(Aurl, bus, cargaAcad, periodo, nmat));
            }
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if (picker.SelectedItem != null)
            {
                string bus = picker.SelectedItem.ToString();
                
                await Navigation.PushModalAsync(new CalificacionPage( Aurl, bus, cargaAcad, periodo, nmat));
            }
        }

        async void Sela_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            CargaClass bus = (CargaClass) Sela.SelectedItem;
            await Navigation.PushModalAsync(new CalificacionPage(Aurl, bus.desc_asig, cargaAcad, periodo, nmat));
        }
    }
}
