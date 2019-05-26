using ServiceXF.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ServiceXF
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LongRunningPage : ContentPage
	{
		public LongRunningPage ()
		{
			InitializeComponent ();

            //Wire up XAML buttons
            longRunningTask.Clicked += (s, e) =>
            {
                var message = new StartLongRunningTaskMessage();
                MessagingCenter.Send(message, "StartLongRunningTaskMessage");
            };

            stopLongRunningTask.Clicked += (s, e) =>
            {
                var message = new StopLongRunningTaskMessage();
                MessagingCenter.Send(message, "StopLongRunningTaskMessage");
            };

            HandleReceiveMessage();
        }

        private void HandleReceiveMessage()//Recibimos el conteo como string
        {
            MessagingCenter.Subscribe<TickedMessage>(this, "TickedMessage", message => 
            {
                Device.BeginInvokeOnMainThread(() => 
                {
                    ticker.Text = message.Message;
                });
            });
            //MessagingCenter.Subscribe<CancelledMessage>(this, "CancelledMessage", message =>
            //{
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        ticker.Text = "Cancelled";
            //    });
            //});
        }
    }
}