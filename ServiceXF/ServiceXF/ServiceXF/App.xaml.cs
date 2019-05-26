using ServiceXF.Messages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ServiceXF
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LongRunningPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps

            //Iniciamos el servicio al salir de la App
            var message = new StartLongRunningTaskMessage();
            MessagingCenter.Send(message, "StartLongRunningTaskMessage");

        }

        protected override void OnResume()
        {
            // Handle when your app resumes

            //Finalizamos el servicio al regresar a la App
            var message = new StopLongRunningTaskMessage();
            MessagingCenter.Send(message, "StopLongRunningTaskMessage");
        }
    }
}
