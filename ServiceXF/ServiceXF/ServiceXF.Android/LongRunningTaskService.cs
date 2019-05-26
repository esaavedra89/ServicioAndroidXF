using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ServiceXF.Messages;
using Xamarin.Forms;

namespace ServiceXF.Droid
{
    [Service]
    public class LongRunningTaskService : Service
    {
        CancellationTokenSource _cts;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        [return: GeneratedEnum] 
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            _cts = new CancellationTokenSource();

            Task.Run(() => 
            {
                try
                {
                    //Invoke Shared code
                    var counter = new TaskCounter();
                    counter.RunCounter(_cts.Token).Wait();

                }
                catch (Android.OS.OperationCanceledException)
                {

                }
                finally
                {
                    if (_cts.IsCancellationRequested)
                    {
                        var message = new CancelledMessage();
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            MessagingCenter.Send(message, "CancelledMessage");
                        });
                    }
                }
            }, _cts.Token);

            //return base.OnStartCommand(intent, flags, startId);
            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Token.ThrowIfCancellationRequested();

                _cts.Cancel();
            }
            base.OnDestroy();
        }
    }
}