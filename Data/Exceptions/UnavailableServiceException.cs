using System;

namespace Netvir.Exceptions
{
    class UnavailableServiceException : Exception
    {
        public UnavailableServiceException()
        { }

        public UnavailableServiceException(string Reason)
            : base(Reason)
        { }
    }
}