using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MIUCSH
{
    public partial class CalificacionPage : ContentPage
    {
        private ObservableCollection<Notas> _nota;
        public IList<Notas> Nota { get; private set; }
        private string Url = "http://localhost:8020/datos";
        private readonly HttpClient client = new HttpClient();
        private List<NotasClass> notas;
        public List<string> Asignaturas;
        private string nmat = "0";
        private string anyo = "2020";
        private string sem = "1";
        private int listing = 0;
        private string busqueda = "";
        private List<CargaClass> carga;
        private List<string> Funciona;
        private string Aurl = "127.0.0.1";
        private PeriodosClass perio;
        private string centro = "";

        public CalificacionPage(string Durl, string bus, List<CargaClass> cargaAca, PeriodosClass period, string numat)
        {

            // picker.Text = bus;
            perio = period;


            Aurl = Durl;
            busqueda = bus;
            carga = new List<CargaClass>();
            notas = new List<NotasClass>();
            Nota = new List<Notas>();
            Funciona = new List<string>();
            carga = cargaAca;
            anyo = period.anyo;
            sem = period.sem;
            nmat = numat;
            Nota.Add(new Notas
            {
                Asignatura = "No registra Notas",
                Fecha = "01-01-2020",
                Nota = "0.0",

                Ponderacion = "0",
                Tipo = "NoEval",
                Visible = "true"
            });
            InitializeComponent();

        }

        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override async void OnAppearing()
        {
            string titulos = " ";
            string cuerpo = " ";
            listing = 0;
            int canti = 0;
            string asig = "";
            string secc = "";
            string version = "";
            string codFuncion = "";
            string grupo = "";
            string validador = "";
            string vali = perio.anyo + "-" + perio.sem;
            Nota = new List<Notas>();
            titulo.Text = "NOTAS (" + vali + ")";
            Mater.Text = busqueda;
            int indi = 0;

            for (var gt = 0; gt < carga.Count; gt++)
            {
                if (carga[gt].desc_asig.Equals(busqueda))
                {
                  
                    if (validador != carga[gt].funcion)
                    {

                        int sw = 0;
                        for (int ki = 0; ki < Funciona.Count; ki++)
                        {
                            if (carga[gt].funcion == Funciona[ki]) sw = 1;
                        }
                        if (sw == 0)
                        {
                            validador = carga[gt].funcion;
                            centro = validador;
                            Funciona.Add(validador);
                            if (indi == 2)
                            {
                                Grilla.IsVisible = true;
                                Boton3.IsVisible = true;
                                Boton3.FontSize = 10;
                                Boton3.Text = validador;
                                indi++;
                            }
                            if (indi == 1)
                            {
                                Grilla.IsVisible = true;
                                Boton2.IsVisible = true;
                                Boton2.FontSize = 10;
                                Boton2.Text = validador;
                                indi++;
                            }
                            if (indi == 0)
                            {
                                Mater.Text = busqueda + "  (" + validador.Trim() + ")";
                                Grilla.IsVisible = true;
                                Boton1.IsVisible = true;
                                Boton1.FontSize = 10;
                                Boton1.Text = validador;
                                indi++;
                            }

                            
                            
                        }

                    }


                    if (String.Equals(busqueda, carga[gt].desc_asig) && carga[gt].funcion == centro)
                    {

                        asig = carga[gt].asig;
                        secc = carga[gt].seccion;
                        version = carga[gt].version;
                        codFuncion = carga[gt].cod_func;
                        grupo = carga[gt].grupo;


                        // Url = "http://"+Aurl+":8020/notas?codigo="+nmat+"1611046&anyo=2019&sem=2&asig=ESDE03&secc=3&version=0&codFuncion=01&grupo=0";
                        Url = "http://" + Aurl + ":8020/notas?codigo=" + nmat + "&anyo=" + anyo + "&sem=" + sem + "&asig=" + asig + "&secc=" + secc + "&version=" + version + "&codFuncion=" + codFuncion + "&grupo=" + grupo;
                        titulos = "Atencion";
                        cuerpo = Url;

                      
                        String LasNotas = await client.GetStringAsync(Url);
                        notas = JsonConvert.DeserializeObject<List<NotasClass>>(LasNotas);

                        listing = notas.Count;

                        canti = 0;
                        for (int yu = 0; yu < notas.Count; yu++)
                        {
                            String valorDia = notas[yu].fecha;
                            if (valorDia.Length > 10)
                            {
                                valorDia = valorDia.Substring(0, 10);
                                DateTime oDate = DateTime.ParseExact(valorDia, "yyyy-MM-dd", null);
                                valorDia = oDate.Day + "-" + oDate.Month + "-" + oDate.Year;
                            }
                            else valorDia = " ";
                            // DateTime oDate = DateTime.ParseExact(iString, "yyyy-MM-dd HH:mm tt", null);
                            // if (notas[yu].fecha.Length >2 ) valorDia = Convert.ToDateTime(notas[yu].fecha);
                            if (valorDia.Length > 5)
                            {
                                canti++;
                                listing = 1;
                                if (notas[yu].nota.Equals("undefined")) notas[yu].nota = "0.0";
                                Nota.Add(new Notas
                                {
                                    Asignatura = notas[yu].asig,
                                    Fecha = valorDia,
                                    Tipo = notas[yu].tino,
                                    Ponderacion = notas[yu].pond,
                                    Nota = notas[yu].nota,
                                    Visible = "true",

                                });
                            }
                            else
                            {
                                Final.IsVisible = true;
                                // etiqueta.IsVisible = true;
                                //  await DisplayAlert("Notificacion", "Este es el valor = " + notas[yu].nota, "OK");
                                if (!notas[yu].nota.Equals("undefined"))
                                {
                                    Final.Text = notas[yu].nota;
                                }
                                else
                                {
                                    Final.Text = "-";
                                    Final.IsVisible = false;
                                    etiqueta.IsVisible = false;
                                }
                            }
                        }
                    }
                }

            }
            if (canti == 0)
            {
                titulos = "Atencion";
                cuerpo = "Sin planificación de notas parciales y prueba integrativa.";
                msgAlert(titulos, cuerpo);
                await Navigation.PopModalAsync();
            }
            // await DisplayAlert("Notificacion", "Todos estan = " + picker.SelectedItem, "OK");
            Notas.ItemsSource = Nota;
            base.OnAppearing();
        }

        void Boton1_Clicked(System.Object sender, System.EventArgs e)
        {
            
            centro = Boton1.Text;
            Mater.Text = busqueda + "  (" + centro.Trim() + ")";
            Actualizador();
        }

        void Boton2_Clicked(System.Object sender, System.EventArgs e)
        {
            centro = Boton2.Text;
            Mater.Text = busqueda + "  (" + centro.Trim() + ")";
            Actualizador();
        }

        void Boton3_Clicked(System.Object sender, System.EventArgs e)
        {
            centro = Boton3.Text;
            Mater.Text = busqueda + "  (" + centro.Trim() + ")";
            Actualizador();
        }
        async void Actualizador()
        {
            Nota = new List<Notas>();
            for (var gt = 0; gt < carga.Count; gt++)
            {
                if (String.Equals(busqueda, carga[gt].desc_asig) && carga[gt].funcion == centro)
                {

                    string asig = carga[gt].asig;
                    string secc = carga[gt].seccion;
                    string version = carga[gt].version;
                    string codFuncion = carga[gt].cod_func;
                    string grupo = carga[gt].grupo;


                    // Url = "http://"+Aurl+":8020/notas?codigo="+nmat+"1611046&anyo=2019&sem=2&asig=ESDE03&secc=3&version=0&codFuncion=01&grupo=0";
                    Url = "http://" + Aurl + ":8020/notas?codigo=" + nmat + "&anyo=" + anyo + "&sem=" + sem + "&asig=" + asig + "&secc=" + secc + "&version=" + version + "&codFuncion=" + codFuncion + "&grupo=" + grupo;
                    //   titulos = "Atencion";
                    //   cuerpo = Url;

                    String LasNotas = await client.GetStringAsync(Url);
                    notas = JsonConvert.DeserializeObject<List<NotasClass>>(LasNotas);

                    listing = notas.Count;

                    int canti = 0;
                    for (int yu = 0; yu < notas.Count; yu++)
                    {
                        String valorDia = notas[yu].fecha;
                        if (valorDia.Length > 10)
                        {
                            valorDia = valorDia.Substring(0, 10);
                            DateTime oDate = DateTime.ParseExact(valorDia, "yyyy-MM-dd", null);
                            valorDia = oDate.Day + "-" + oDate.Month + "-" + oDate.Year;
                        }
                        else valorDia = " ";
                        // DateTime oDate = DateTime.ParseExact(iString, "yyyy-MM-dd HH:mm tt", null);
                        // if (notas[yu].fecha.Length >2 ) valorDia = Convert.ToDateTime(notas[yu].fecha);
                        if (valorDia.Length > 5)
                        {
                            canti++;
                            listing = 1;
                            if (notas[yu].nota.Equals("undefined")) notas[yu].nota = "0.0";
                            Nota.Add(new Notas
                            {
                                Asignatura = notas[yu].asig,
                                Fecha = valorDia,
                                Tipo = notas[yu].tino,
                                Ponderacion = notas[yu].pond,
                                Nota = notas[yu].nota,
                                Visible = "true",

                            });
                        }
                        else
                        {
                            Final.IsVisible = true;
                            // etiqueta.IsVisible = true;
                            //  await DisplayAlert("Notificacion", "Este es el valor = " + notas[yu].nota, "OK");
                            if (!notas[yu].nota.Equals("undefined"))
                            {
                                Final.Text = notas[yu].nota;
                            }
                            else
                            {
                                Final.Text = "-";
                                Final.IsVisible = false;
                                etiqueta.IsVisible = false;
                            }
                        }
                    }
                }
            }
            Notas.ItemsSource = Nota;
        }
        async private void msgAlert(string titulo, string msg)
        {
            await DisplayAlert(titulo, msg, "Ok");
        }

    }
}
