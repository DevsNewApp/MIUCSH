using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MIUCSH
{
    public partial class HorariosPage : ContentPage
    {
        private List<CargaClass> cargaAca;
        private List<HorariosClases> horarios;
        private string Aurl = "127.0.0.1";
        private PeriodosClass period;
        private int lun, mar, jue, vie, sab, mie = 0;
        public HorariosPage(string Durl, List<HorariosClases> horas, List<CargaClass> carga, PeriodosClass per)
        {
            Aurl = Durl;
            period = per;
            horarios = horas;
            cargaAca = carga;
            lun = 0; mar = 0; mie = 0; jue = 0; vie = 0; sab = 0;
            for (int r =0;r<horas.Count;r++)
            {
                if (horas[r].lunes != "") lun++;
                if (horas[r].martes != "") mar++;
                if (horas[r].miercoles != "") mie++;
                if (horas[r].jueves != "") jue++;
                if (horas[r].viernes != "") vie++;
                if (horas[r].sabado != "") sab++;
            }
            
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (lun == 0) lunes.BorderColor = Xamarin.Forms.Color.Red;
            if (mar == 0) martes.BorderColor = Xamarin.Forms.Color.Red;
            if (mie == 0) miercoles.BorderColor = Xamarin.Forms.Color.Red;
            if (jue == 0) jueves.BorderColor = Xamarin.Forms.Color.Red;
            if (vie == 0) viernes.BorderColor = Xamarin.Forms.Color.Red;
            if (sab == 0) sabado.BorderColor = Xamarin.Forms.Color.Red;
            string an = period.anyo;
            string se = period.sem;
            string tit= "HORARIOS ("+ an + " - " + se+")";
            titulo.Text = tit;
            base.OnAppearing();
        }
        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new MiCalendario(Aurl,horarios,cargaAca,"LUNES", period));
        }

        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new MiCalendario(Aurl, horarios, cargaAca, "MARTES", period));
        }

        async void Button_Clicked_2(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new MiCalendario(Aurl, horarios, cargaAca, "MIERCOLES", period));
        }

        async void Button_Clicked_3(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new MiCalendario(Aurl, horarios, cargaAca, "JUEVES", period));
        }

        async void Button_Clicked_4(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new MiCalendario(Aurl,horarios, cargaAca, "VIERNES", period));
        }

        async void Button_Clicked_5(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new MiCalendario(Aurl,horarios, cargaAca, "SABADO", period));
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
