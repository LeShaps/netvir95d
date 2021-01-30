using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Netvir.Events;

namespace Netvir.Services
{
    class Listener
    {
        private readonly Thread _listenerThread;
        private HttpListener _listener;

        public event Func<HttpMessageEventArgs, Task> OnMessageReceived;
        public event Func<Task> OnStartListening;
        public event Func<Task> OnStopListening;

        public Listener(params string[] WatchDomains)
        {
            _listener = new HttpListener();
            // Todo: Check Watchdomains
            foreach (string S in WatchDomains)
            {
                _listener.Prefixes.Add(S);
            }

            _listenerThread = new Thread(new ThreadStart(Loop));
        }

        public void StartListening()
        {
            _listener.Start();
            _listenerThread.Start();
            OnStart(EventArgs.Empty);
        }

        public void StopListening()
        {
            _listener.Stop();
            OnStop(EventArgs.Empty);
        }

        private async void Loop()
        {
            _listener.Start();

            while (_listener.IsListening)
            {
                HttpListenerContext Context = await _listener.GetContextAsync();
                HttpListenerRequest Request = Context.Request;
                HttpListenerResponse Response = Context.Response;

                _ = OnMessageAsync(new HttpMessageEventArgs
                {
                    Authorized = true,
                    Request = Request,
                    Response = Response
                }).ConfigureAwait(false);
            }
        }

        protected virtual async Task OnMessageAsync(HttpMessageEventArgs e)
        {
            await OnMessageReceived?.Invoke(e);
        }

        protected virtual void OnStart(EventArgs e)
        {
            OnStartListening?.Invoke();
        }

        protected virtual void OnStop(EventArgs e)
        {
            OnStopListening?.Invoke();
        }
    }
}
