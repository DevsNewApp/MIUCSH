using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MIUCSH
{
    public partial class NoiciasPage : ContentPage
    {
        public NoiciasPage()
        {
            InitializeComponent();
            webView.Source = "http://ww3.ucsh.cl/noticias-estudiantes/";
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
