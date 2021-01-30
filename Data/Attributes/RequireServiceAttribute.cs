using System;
using System.Threading.Tasks;

using Discord.Commands;

using Netvir.Data;
using Netvir.Exceptions;

namespace Netvir.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    class RequireServiceAttribute : PreconditionAttribute
    {
        public string RequiredServiceName;

        public RequireServiceAttribute(string Name)
        {
            RequiredServiceName = Name;
        }

        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            if (!Globals.ServiceStatus.ContainsKey(RequiredServiceName))
                return Task.FromResult(PreconditionResult.FromError(
                    new UnavailableServiceException("Something went wrong: this service is not registered")
                    ));

            if (!Globals.ServiceStatus[RequiredServiceName])
                return Task.FromResult(PreconditionResult.FromError(
                    new UnavailableServiceException(Globals.ServiceThrowReasons[RequiredServiceName])
                    ));

            return Task.FromResult(PreconditionResult.FromSuccess());
        }
    }
}
