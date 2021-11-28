using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MIUCSH
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleAlumno : ContentPage
    {
        private string Aurl;
        private string Run;
        private string Nmat;
        private int angulo = 0;
        private string uri;
        private FotoClass foto = new FotoClass();
        private readonly HttpClient client = new HttpClient();
        public DetalleAlumno(string Url, string Rut, string Nmats)
        {
            Aurl = Url;
            Run = Rut;
            Nmat = Nmats;

            InitializeComponent();
        }
        protected async override void OnAppearing()
        {

            string Url = "http://" + Aurl + ":8020/aca/datosa";
            Url = Url + "?rut=" + Run + "&nmat=" + Nmat;

            string content = await client.GetStringAsync(Url);
           

            List<EstudianteClass> datosA = JsonConvert.DeserializeObject<List<EstudianteClass>>(content);



            RutA.Text = datosA[0].alum_rut;
            XNmat.Text = Nmat;
            NombreA.Text = datosA[0].nombre;
            CarreraA.Text = datosA[0].carr_desc;
            EmailA.Text = datosA[0].mail_mail;
            if (Preferences.ContainsKey("mi_foto"))
            {
                FotoA.Source = Preferences.Get("mi_foto", "default_value");
           //     Preferences.Remove("mi_angulo");
                if (Preferences.ContainsKey("mi_angulo"))
                   FotoA.Rotation = Int32.Parse(Preferences.Get("mi_angulo", "default_value"));
            }
            else
              if (datosA[0].foto.Length > 8)
                FotoA.Source = ImageSource.FromUri(new Uri(datosA[0].foto));
           
            base.OnAppearing();
        }
        async void Button_Clicked(object sender, EventArgs e)
        {
            await Plugin.Media.CrossMedia.Current.Initialize();
            if (!Plugin.Media.CrossMedia.Current.IsCameraAvailable || !Plugin.Media.CrossMedia.Current.IsTakePhotoSupported)
            {
                string titulo = "Interesante";
                string cuerpo = "No puede tomar las fotos!";
                msgAlert(titulo, cuerpo);

                return;
            }
            uri = Digito(Run) + ".jpg";
            var file = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Mifoto",
                Name = uri,
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                RotateImage = false,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 20000,
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front

            }) ;

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
            else
            {
                try
                {
                    string pathfinder = file.AlbumPath;
                    if (Preferences.ContainsKey("mi_foto"))
                        Preferences.Remove("mi_foto");
                    Preferences.Set("mi_foto", pathfinder);
                    FotoA.Source = ImageSource.FromFile(pathfinder);
                    
                    
                    MemoryStream memoryStream = new MemoryStream();
                    file.GetStream().CopyTo(memoryStream);
                    byte[] buffer = memoryStream.ToArray();
                    // string msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    string msg = Convert.ToBase64String(buffer);
                    foto = new FotoClass();
                    foto.Nombre = uri;
                    foto.Foto = msg;
                    string json = JsonConvert.SerializeObject(foto);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    string URL = "http://" + Aurl + ":8020/upload";// "http://192.168.1.129:8020/upload";
                                                                   //   var content = JsonConvert.SerializeObject(post);
                    await client.PostAsync(URL, content);
                }
                catch (Exception ) { }

            }
        }
        static string Digito(string numero)
        {
            string sol = "0";
            string alfa = numero;
            int num = 2;
            int totaliza = 0;
            while (alfa.Length > 0)
            {
                if (num == 8) num = 2;
                int leng = alfa.Length;
                string alfa2 = alfa.Substring(leng - 1, 1);
                alfa = alfa.Substring(0, leng - 1);
                int resul1 = Int32.Parse(alfa2);
                totaliza = totaliza + (resul1 * num);
                num++;
            }
            int no = totaliza / 11;
            no = no * 11;
            totaliza = 11 - (totaliza - no);
            if (totaliza == 11) totaliza = 0;
            
            if (totaliza == 10) sol = numero + "-" + "K";
            else sol = numero +"-"+totaliza.ToString();
            return sol;
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            Preferences.Set("mi_angulo", angulo.ToString());
            await Navigation.PopModalAsync();
        }

        void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            angulo = angulo - 90;
            FotoA.Rotation = angulo;

        }

        void ImageButton_Clicked_1(System.Object sender, System.EventArgs e)
        {
            angulo = angulo + 90;
            FotoA.Rotation = angulo;
        }
        async private void msgAlert(string titulo, string msg)
        {
            await DisplayAlert(titulo, msg, "Ok");
        }
    }
}
