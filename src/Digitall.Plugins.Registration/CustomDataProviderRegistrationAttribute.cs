// Copyright (c) DIGITALL Nature. All rights reserved.
// This code is licensed under the Microsoft Public License (MS-PL). See LICENSE.md in the project root for license information.

using System;

namespace Digitall.Plugins.Registration
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CustomDataProviderRegistrationAttribute : Attribute
    {
        private readonly DataProviderEvent _event;

        public CustomDataProviderRegistrationAttribute(string dataSourceSchemaName, DataProviderEvent eventRegistration, string providerName)
        {
            DataSourceSchemaName = dataSourceSchemaName;
            _event = eventRegistration;
            ProviderName = providerName;
        }

        /// <summary>
        /// Publisher-prefixed schema name of the provider's data-source configuration table.
        /// Identifies the provider independently of virtual tables.
        /// </summary>
        public string DataSourceSchemaName { get; }

        /// <summary>
        /// Operation handled by the decorated plugin class. Must be explicitly specified.
        /// </summary>
        public int Event => (int)_event;

        /// <summary>
        /// Provider display name.
        /// </summary>
        public string ProviderName { get; }

        /// <summary>
        /// Optional singular label for the data-source configuration table.
        /// When omitted, preserves the existing label.
        /// </summary>
        public string DataSourceDisplayName { get; set; }

        /// <summary>
        /// Optional plural label for the data-source configuration table.
        /// When omitted, preserves the existing label.
        /// </summary>
        public string DataSourcePluralName { get; set; }

        /// <summary>
        /// Optional provider description. When omitted, preserves the existing description.
        /// </summary>
        public string Description { get; set; }
    }
}
