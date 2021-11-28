using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MIUCSH
{
    public partial class PopupNewTaskView
    {
        private string Rtitulo = "Hola";
        private string Rmessage = "Hola";
        public PopupNewTaskView(string titulo, string message)
        {

          Rtitulo = titulo;
          Rmessage = message;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            title.Text = Rtitulo;
            body.Text = Rmessage;
            base.OnAppearing();
        }

        async void  Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
