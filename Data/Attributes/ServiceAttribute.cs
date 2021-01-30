using System;

namespace Netvir.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    class ServiceAttribute : Attribute
    {
        public string Name;
        public bool IsActive;
        public string ThrowReason = "This service has been desactivated by default";

        public ServiceAttribute(string Name)
        {
            this.Name = Name;
            IsActive = true;
        }

        public ServiceAttribute(string Name, bool IsActive)
        {
            this.Name = Name;
            this.IsActive = IsActive;
        }
    }
}
