using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
namespace MIUCSH
{
    
    public partial class MenuPage : ContentPage
    {
        private string Url = "http://192.168.1.129:8020/datos";
        private string Url2 = "http://192.168.1.129:8020/periodo";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<datosPersonales> _datos;
        private List<HorariosClases> horas;
        private List<CargaClass> cargaAca;
        private List<datosPersonales> datpe;
        private PeriodosClass period;
        private string Run;
        private string nmat;
        private string Aurl = "127.0.0.1";
        private string bus;
        private int anyo = 0;
        private int sem = 0;
        private int ma = 2027;
        private int mp = 1;
        private int mpi = 1;
        private int aanyo = 0;
        private string minombre;

        public MenuPage(string Durl, string run)
        {
            Aurl = Durl;
            Url = "http://" + Aurl + ":8020/datos";
            Url2 = "http://" + Aurl + ":8020/periodo";
            UpdPeriod();
            Run = run;
            Url = Url + "?rut=" + run;
            InitializeComponent();
        }     
            
        async void UpdPeriod()
        {
            String periodos = await client.GetStringAsync(Url2);
            period = JsonConvert.DeserializeObject<PeriodosClass>(periodos);
           //  period.sem = "1";
            aanyo = Int32.Parse(period.anyo);
            mp = Int32.Parse(period.sem);
            mpi = mp;
        }
        protected override async void OnAppearing()
        {
            string ff = "00";
            try
            {
                //    client.Timeout = TimeSpan.FromSeconds(120);

                string content = await client.GetStringAsync(Url);
                ff = "49";
                List<datosPersonales> datos = JsonConvert.DeserializeObject<List<datosPersonales>>(content);
                ff = "51";

                // _msg = new ObservableCollection<Msg>(msgs);
                datpe = datos;
                // var dtas = datos.ToArray();
                minombre = datos[0].nombres + " " + datos[0].apellPat + " " + datos[0].apellMat;
                Nombres.Text = datos[0].nombres + " " + datos[0].apellPat + " " + datos[0].apellMat;
                //   minombre = "Maria José Fuentes Araya";
                //   Nombres.Text = minombre;
                Rut.Text = Run;
                string alfa = datos[0].numMat;
                nmat = alfa;
                Carrera.Text = "(" + datos[0].codCar + ")-" + datos[0].carrera;
                // client.Timeout = TimeSpan.FromSeconds(120);

                //  XXXXXXXX        cambios por pruebas debe ser eliminas las siguientes lineas

                anyo = Int32.Parse(period.anyo);
                sem = Int32.Parse(period.sem);
                ma = anyo;
                mp = sem;
                //   XXXXXXXXX       Pendientes
                string URL = "http://" + Aurl + ":8020/horarios?codigo=" + alfa + "&anyo=" + period.anyo + "&sem=" + period.sem;
                //  client.Timeout = TimeSpan.FromSeconds(120);
                //  notifica("Horarios", URL);
                string content2 = await client.GetStringAsync(URL);
                ff = "70";
                horas = JsonConvert.DeserializeObject<List<HorariosClases>>(content2);
                ff = "72";
                URL = "http://" + Aurl + ":8020/carga?codigo=" + alfa + "&anyo=" + period.anyo + "&sem=" + period.sem;
                //  notifica("Carga", URL);
                content2 = await client.GetStringAsync(URL);
                ff = "75";
                cargaAca = JsonConvert.DeserializeObject<List<CargaClass>>(content2);
                ff = "77";
                DiaSem.Text = anyo.ToString() + "-" + sem.ToString();
                ff = "77.5";
                if (cargaAca.Count > 0)
                { 
                    bus = cargaAca[0].desc_asig;
                    ff = "77.7";
                    string Url3 = "http://" + Aurl + ":8020/pestudio?nmat=" + nmat;
                    ff = "78";
                    content = await client.GetStringAsync(Url3);
                    //    notifica("Plan de estudios", Url3);
                    ff = "79";
                    List<PlanEstudioClass> Planes = JsonConvert.DeserializeObject<List<PlanEstudioClass>>(content);
                    ff = "80";
                    for (var rw = 0; rw < Planes.Count; rw++)
                    {
                        ff = "81";
                        if (Int32.Parse(Planes[rw].anyo) == ma && Int32.Parse(Planes[rw].periodo) < mp)
                        {

                            mp = Int32.Parse(Planes[rw].periodo);
                        }
                        ff = "82";
                        if (Int32.Parse(Planes[rw].anyo) > 0 && Int32.Parse(Planes[rw].anyo) < ma)
                        {
                            ma = Int32.Parse(Planes[rw].anyo);
                            mp = Int32.Parse(Planes[rw].periodo);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    string titulo = "Error 77 -- "  + ff.ToString();
                    string cuerpo = "Se presento un error que no ha podido controlar " + e.ToString();
                    await DisplayAlert(titulo, cuerpo, "Ok");
                  //  await PopupNavigation.Instance.PushAsync(new                                (titulo, cuerpo));
               //     await DisplayAlert("Error", "URL= " + Aurl + "  -Se presento un error que no ha podido controlar = " + e.ToString(), "OK");
                });
               
            }
        //  await DisplayAlert("Notificacion", "Todos estan = ", "OK");
            base.OnAppearing();


        }
        async void Actualiza()
        {

            try
            {
                //    client.Timeout = TimeSpan.FromSeconds(120);
                string content = await client.GetStringAsync(Url);
                List<datosPersonales> datos = JsonConvert.DeserializeObject<List<datosPersonales>>(content);
                // _msg = new ObservableCollection<Msg>(msgs);
                datpe = datos;
                // var dtas = datos.ToArray();
                minombre = datos[0].nombres + " " + datos[0].apellPat + " " + datos[0].apellMat;
                Nombres.Text = datos[0].nombres + " " + datos[0].apellPat + " " + datos[0].apellMat;
              //  minombre = "Maria José Fuentes Araya";
               // Nombres.Text = minombre;
                Rut.Text = Run;
                string alfa = datos[0].numMat;
                nmat = alfa;
                Carrera.Text = "(" + datos[0].codCar + ")-" + datos[0].carrera;
                // client.Timeout = TimeSpan.FromSeconds(120);

                //  XXXXXXXX        cambios por pruebas debe ser eliminas las siguientes lineas

                anyo = Int32.Parse(period.anyo);
                sem = Int32.Parse(period.sem);
                //   XXXXXXXXX       Pendientes
                string URL = "http://" + Aurl + ":8020/horarios?codigo=" + alfa + "&anyo=" + period.anyo + "&sem=" + period.sem;
                //  client.Timeout = TimeSpan.FromSeconds(120);
                string content2 = await client.GetStringAsync(URL);
                horas = JsonConvert.DeserializeObject<List<HorariosClases>>(content2);
                URL = "http://" + Aurl + ":8020/carga?codigo=" + alfa + "&anyo=" + period.anyo + "&sem=" + period.sem;
               // PopUp("Atencion URL", URL);
                content2 = await client.GetStringAsync(URL);
                cargaAca = JsonConvert.DeserializeObject<List<CargaClass>>(content2);
                DiaSem.Text = anyo.ToString() + "-" + sem.ToString();
                if (cargaAca.Count > 0)
                   bus = cargaAca[0].desc_asig;
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    string titulo = "Error 127";
                    string cuerpo = "Se presento un error que no ha podido controlar";
                    await DisplayAlert(titulo, cuerpo, "Ok");
                 //   await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
                    //     await DisplayAlert("Error", "URL= " + Aurl + "  -Se presento un error que no ha podido controlar = " + e.ToString(), "OK");
                });
            }
        }
    //    async void PopUp(string nota, string mensaje)
    //    {
    //        await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(nota, mensaje));
    //    }
        async void CalendaButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new HorariosPage(Aurl,horas, cargaAca, period));
          //  await Navigation.PushModalAsync(new MiCalendario(horas));
        }
        async void AsistenciaButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MyPage(Aurl,nmat,cargaAca,period));
        }
        async void NotificaButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Notifica(Aurl, Run, cargaAca, minombre));
        }
        async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new DetalleAlumno(Aurl, Run, nmat));
            
        }
        async void CalificaButtonClicked(object sender, EventArgs e)
        {
            // await Navigation.PushModalAsync(new CalificacionPage(bus,cargaAca,period,nmat));
            await Navigation.PushModalAsync(new SelAsignaturas(Aurl,cargaAca, period, nmat));
        }
        async void OfertaButtonClicked(object sender, EventArgs e)
        {
            Page p = new FacultadesSel(Aurl);
            await Navigation.PushModalAsync(p);
        }
        async void PlanButtonClicked(object sender, EventArgs e)
        {
          //  await Navigation.PushModalAsync(new MyPlanEstudio(Aurl, nmat, cargaAca, period));
            await Navigation.PushModalAsync(new SemestresPage(Aurl, nmat, cargaAca, period, datpe));
            
        }
         void HistoricoButtonClicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => {
                string t = "Histórico de Notas";
                await Navigation.PushModalAsync(new HistoricoPage(Aurl, nmat, cargaAca, period,t,ma,mp));
                
            });
            
           // await Navigation.PushModalAsync(new SemestresPage(Aurl, nmat, cargaAca, period));

        }
        async void KAsistenciaButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CalendarioAcadPage(period, "1", Aurl));
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
       


        void ImageButton_Clicked_2(System.Object sender, System.EventArgs e)
        {
       
            //if (anyo > ma)
           // {
                if (sem == 1) { sem = 2; anyo--; }
                else
                if (sem == 2) { sem = 1; }
           // }
           // if (anyo == ma && sem > mp)
           // {
           //     sem = 1;
           // }
           
            DiaSem.Text = anyo.ToString() + "-" + sem.ToString();
            period.anyo = anyo.ToString();
            period.sem = sem.ToString();
           // notifica("Actualizando fechas", "Esto deberia generar resultados "+ DiaSem.Text);
            Actualiza();

        }
        async void notifica(string titulos, string cuerpos)
        {
            string titulo = titulos;
            string cuerpo = cuerpos;
            await DisplayAlert(titulo, cuerpo, "Ok");

        //    await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));

        }

        void ImageButton_Clicked_3(System.Object sender, System.EventArgs e)
        {
     
            if (anyo <= aanyo)
            {
                if (sem == 1) sem++;
                else
                   if (sem == 2 && anyo != aanyo) { sem = 1; anyo++; }
                if (anyo == aanyo && sem > mpi) sem = 1;
            }
         //   notifica("Esto es lo que tiene", anyo.ToString() + "-" + sem.ToString() + "-" + aanyo.ToString() + "-" + mp.ToString());
            DiaSem.Text = anyo.ToString() + "-" + sem.ToString();
            period.anyo = anyo.ToString();
            period.sem = sem.ToString();
            Actualiza();
        }

        async void ImageButton_Clicked_1(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NoiciasPage());

        }
    }
}
