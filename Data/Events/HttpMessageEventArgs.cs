using System;
using System.Net;

namespace Netvir.Events
{
    class HttpMessageEventArgs : EventArgs
    {
        public bool Authorized;
        public HttpListenerRequest Request;
        public HttpListenerResponse Response;
    }
}
