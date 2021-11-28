using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace MIUCSH
{
    public partial class SemestresPage : ContentPage
    {
        private string Aurl = "127.0.0.1";
        private string Url;
        private readonly HttpClient client = new HttpClient();
        private int anyo = 0;
        private int sem = 1;
        private string nmat = "";
        private List<CargaClass> cargas;
        private List<datosPersonales> datpe;
        private string titulo = "Plan de Estudios";
        private PeriodosClass periodo;
        public SemestresPage(string Durl, string rnmat, List<CargaClass> carga, PeriodosClass per, List<datosPersonales> datp)
        {
            Aurl = Durl;
            periodo = per;
            cargas = carga;
            nmat = rnmat;
            datpe = datp;
            
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            
            Semestre.Text = datpe[0].alum_nive;
            Planestu.Text = datpe[0].alum_plan;
            Anoaplan.Text = datpe[0].anoa_plan;
            base.OnAppearing();
        }
            async void Button_Clicked(System.Object sender, System.EventArgs e)
        {

            await Navigation.PushModalAsync(new PlanEstudio(Aurl, nmat, "1", titulo, cargas));
        }

        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new PlanEstudio(Aurl, nmat, "2", titulo, cargas));
        }

        async void Button_Clicked_2(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new PlanEstudio(Aurl, nmat, "3", titulo, cargas));
        }

        async void Button_Clicked_3(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new PlanEstudio(Aurl, nmat, "4", titulo, cargas));
        }

        async void Button_Clicked_4(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new PlanEstudio(Aurl, nmat, "5", titulo, cargas));
        }

        async void Button_Clicked_5(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new PlanEstudio(Aurl, nmat, "6", titulo, cargas));
        }

        async void Button_Clicked_6(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new PlanEstudio(Aurl, nmat, "7", titulo , cargas));
        }

        async void Button_Clicked_7(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new PlanEstudio(Aurl, nmat, "8", titulo, cargas));
        }

        async void Button_Clicked_8(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new PlanEstudio(Aurl, nmat, "9", titulo, cargas));
        }

        async void Button_Clicked_9(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new PlanEstudio(Aurl, nmat, "10", titulo, cargas));
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
