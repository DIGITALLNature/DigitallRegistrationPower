using System;

namespace dgt.registration
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CustomDataProviderRegistrationAttribute : Attribute
    {
        public CustomDataProviderRegistrationAttribute(string entityName, DataProviderEvent eventRegistration)
        {
            EntityName = entityName;
            Event = eventRegistration;
        }

        /// <summary>
        /// DataProvider for Entity
        /// </summary>
        public string EntityName { get; }

        /// <summary>
        /// Register Plugin to Event of DataProvider
        /// </summary>
        public DataProviderEvent Event { get; }

    }
}
