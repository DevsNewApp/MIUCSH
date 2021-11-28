using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MIUCSH
{
    public partial class CalendarioAcadPage : ContentPage
    {

        private List<CalendarioClass> Calendario;
        private List<CalenAcadClass> actividades;
        private string Url = "http://localhost:8020/datos";
        private readonly HttpClient client = new HttpClient();
        private PeriodosClass period;
        private string Mes;
        private string mesUpd = "1";
        private string Aurl;
        private string[] meses = new string[] {"Error","Enero","Febrero","Marzo","Abril","Mayo",
            "Junio","Julio","Agosto","Septiembre","Octubre","Noviembre","Diciembre"};
        public CalendarioAcadPage(PeriodosClass periodo, string mes, string Url)
        {

            period = periodo;
            Aurl = Url;
            Mes = mes;
            InitializeComponent();
            DateTime fechaActual = DateTime.Today;
           
            mesUpd = fechaActual.Month.ToString();
        }
        protected override async void OnAppearing()
        {
            Url = "http://" + Aurl + ":8020/calendario?anop=" + period.anyo;
            int y = Int32.Parse(mesUpd);
               
           if (y>2) Mesver.Text = meses[y];
            else
            {
                int anio = Int32.Parse(period.anyo);
                anio = anio + 1;

                string mesi = meses[y] + " " + anio.ToString();
            }
                 
            String LasNotas = await client.GetStringAsync(Url);
            actividades = JsonConvert.DeserializeObject<List<CalenAcadClass>>(LasNotas);
            Calendario = new List<CalendarioClass>();
            for (int ty = 0; ty < actividades.Count; ty++)
            {
                DateTime parsedDatei = DateTime.Parse(actividades[ty].caa_fini);
                DateTime parsedDatef = DateTime.Parse(actividades[ty].caa_fter);
                string inou = parsedDatei.ToString("dd-MM-yyyy");
                string ouin = parsedDatef.ToString("dd-MM-yyyy");
                if (actividades[ty].mes == mesUpd)
                    Calendario.Add(new CalendarioClass
                    {
                        actividad = actividades[ty].caa_texto,
                        area = actividades[ty].caa_resp,
                        mes = actividades[ty].mes,
                        ini = inou,
                        fin = ouin
                    });
            }
            CalendarioAcademico.ItemsSource = Calendario;
            base.OnAppearing();
        }
        void updateData()
        {
            Calendario = new List<CalendarioClass>();
            for (int ty = 0; ty < actividades.Count; ty++)
            {
                DateTime parsedDatei = DateTime.Parse(actividades[ty].caa_fini);
                DateTime parsedDatef = DateTime.Parse(actividades[ty].caa_fter);
                string inou = parsedDatei.ToString("dd-MM-yyyy");
                string ouin = parsedDatef.ToString("dd-MM-yyyy");
                if (actividades[ty].mes == mesUpd)
                    Calendario.Add(new CalendarioClass
                    {
                        actividad = actividades[ty].caa_texto,
                        area = actividades[ty].caa_resp,
                        mes = actividades[ty].mes,
                        ini = inou,
                        fin = ouin
                    });
            }
            CalendarioAcademico.ItemsSource = Calendario;
        }
        void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            int y = 0;
            y = Int32.Parse(mesUpd);
            y--;
            if (y == 0) y = 12;
            mesUpd = y.ToString();
            if (y > 2) Mesver.Text = meses[y];
            else
            {
                int anio = Int32.Parse(period.anyo);
                anio = anio + 1;

                string mesi = meses[y] + " " + anio.ToString();
                Mesver.Text = mesi;
            }
            updateData();
        }

        void ImageButton_Clicked_1(System.Object sender, System.EventArgs e)
        {
            int y = 0;
            y = Int32.Parse(mesUpd);
            y++;
            if (y == 13) y = 1;
            mesUpd = y.ToString();
            if (y > 2) Mesver.Text = meses[y];
            else
            {
                int anio = Int32.Parse(period.anyo);
                anio = anio + 1;

                string mesi = meses[y] + " " + anio.ToString();
                Mesver.Text = mesi;
            }
            updateData();
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await Plugin.Media.CrossMedia.Current.Initialize();
            if (!Plugin.Media.CrossMedia.Current.IsCameraAvailable || !Plugin.Media.CrossMedia.Current.IsTakePhotoSupported)
            {
                string titulo = "Interesante";
                string cuerpo = "No puede tomar las fotos!";
                msgAlert(titulo, cuerpo);
               // await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));

                return;
            }

            var file = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 20000,
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front

            });

          //  var photo =
          //      await Plugin.Media.CrossMedia.Current
          //          .TakePhotoAsync(new StoreCameraMediaOptions());
            if (file == null)
            {
                string titulo = "Interesante";
                string cuerpo = "No grabo nada de la foto!";
                msgAlert(titulo, cuerpo);
                // Photo.Source = ImageSource.FromStream(() =>
                // {
                //    return photo.GetStream();
                // });
            }


        }

        async private void msgAlert(string titulo, string msg)
        {
           await DisplayAlert(titulo, msg, "Ok");


        }
    }
}
