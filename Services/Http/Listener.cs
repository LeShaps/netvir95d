using System;
using System.Net;
using System.Threading;

using Netvir.Events;

namespace Netvir.Services
{
    class Listener
    {
        private readonly Thread _listenerThread;
        private HttpListener _listener;

        public event EventHandler<HttpMessageEventArgs> OnMessageReceived;
        public event EventHandler OnStartListening;
        public event EventHandler OnStopListening;

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

                OnMessage(new HttpMessageEventArgs
                {
                    Authorized = true,
                    Request = Request,
                    Response = Response
                });
            }
        }

        protected virtual void OnMessage(HttpMessageEventArgs e)
        {
            OnMessageReceived?.Invoke(this, e);
        }

        protected virtual void OnStart(EventArgs e)
        {
            OnStartListening?.Invoke(this, e);
        }

        protected virtual void OnStop(EventArgs e)
        {
            OnStopListening?.Invoke(this, e);
        }
    }
}
