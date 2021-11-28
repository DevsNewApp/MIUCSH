using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;


namespace MIUCSH
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private string Aurl = "app.ucsh.cl";  // "192.168.1.129";  "app.ucsh.cl"
        private string Url = "http://app.ucsh.cl:8020/valida";
       
        private readonly HttpClient client = new HttpClient();
        public MainPage()
        {                           
            Url = Aurl;
            InitializeComponent();
           
        }

         async private void msgAlert(string titulo, string msg)
        {
           //     await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, msg));
            //  await PopupNavigation.PushAsync(new PopupNewTaskView(titulo, msg));
           // await Navigation.PushModalAsync(new PopupNewTaskView(titulo, msg));
            await DisplayAlert(titulo, msg, "Ok");


        }
        async void OkButtonClicked(object sender, EventArgs e)
        {
            string usua = usuario.Text;
            string pass = password.Text;
            // usua = "14199342";
            if (usua.ToCharArray().All(Char.IsLetter) || usua.Length > 8)
            {
                string titulo = "Error Login";
                string cuerpo = "El usuario es la parte numerica de su Rut sin el numero digito";
                msgAlert(titulo, cuerpo);

            }
            else
            {
                string Test = "http://" + Url + ":8020/valida?rut=" + usua + "&password=" + pass     ;
               // string Test = "http://" + Url + ":8020/datos?rut=" + usua ;
                try
                {
                 //   msgAlert("Antes LLamado", "Esto es previo al llamado");
                 //   msgAlert("Url del llamado", Test);
                    string content = await client.GetStringAsync(Test);
                    ValidaClass datos = JsonConvert.DeserializeObject<ValidaClass>(content);
                  //  List<datosPersonales> datos = JsonConvert.DeserializeObject<List<datosPersonales>>(content);
                    //   msgAlert("Antes LLamado", "Despues del llamado respuesta datos");
                   // notifica("Esto es lo que lee", content);
                    if (datos.val != "OK")  //(datos.Count() < 1 || datos[0].numMat.Equals("0"))
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                         {
                             string titulo = "Error Login..";
                             string cuerpo = "Usuario o password errado "+ usua + "-"+ pass;
                             // string cuerpo = "Usted no tiene acceso a este sistema con el rut: " + usua;
                             msgAlert(titulo, cuerpo);
                          //   await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
                           // await this.DisplayAlert("Notificacion", "Rut o password incorrectos", "OK");
                       });
                    }
                    else
                    {
                     /*   string Url2 = "http://" + Aurl + ":8020/periodo";
                        String periodos = await client.GetStringAsync(Url2);
                        PeriodosClass period = JsonConvert.DeserializeObject<PeriodosClass>(periodos);
                        period.sem = "1";   // TOMAS 
                        string alfa = datos[0].numMat;
                        string URL = "http://" + Aurl + ":8020/carga?codigo=" + alfa + "&anyo=" + period.anyo + "&sem=" + period.sem;
                        //  notifica("Carga", URL);
                        string content2 = await client.GetStringAsync(URL);
                       
                        List<CargaClass> cargaAca = JsonConvert.DeserializeObject<List<CargaClass>>(content2);

                        if (cargaAca.Count > 0)
                        { */

                            Page p = new MenuPage(Aurl, usua);

                            await Navigation.PushModalAsync(p);
                      /*  }
                        else
                        {
                            notifica("Atención", "Estudiante no se encuentra regular en periodo vigente");
                        } */
                    }
                }
                catch (Exception t)
                {
                    string titulo = "Conexion";
                    string cuerpo = "El servidor no responde posible problema de conexion";
                    // cuerpo = t.ToString();
                    msgAlert(titulo, cuerpo);
                 /*   Device.BeginInvokeOnMainThread(async () =>
                    {
                        string titulo = "Conexion";
                        string cuerpo = "El servidor no responde posible problema de conexion";
                    
                         await this.DisplayAlert("Conexión", "En este momento tienes problemas de conectividad, intentalo mas tarde." + t.ToString(), "OK");
                    });*/

                }
            }
        }

        void notifica(string titulos, string cuerpos)
        {
            string titulo = titulos;
            string cuerpo = cuerpos;
            msgAlert(titulo, cuerpo);
            

        }
        void CancelButtonClicked(object sender, EventArgs e)
        {
            
        }
    }
}
