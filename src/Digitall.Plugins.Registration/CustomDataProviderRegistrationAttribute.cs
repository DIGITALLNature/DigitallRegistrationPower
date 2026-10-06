// Copyright (c) DIGITALL Nature. All rights reserved.
// This code is licensed under the Microsoft Public License (MS-PL). See LICENSE.md in the project root for license information.

using System;

namespace Digitall.Plugins.Registration
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CustomDataProviderRegistrationAttribute : Attribute
    {
        /// <summary>
        /// Publisher-prefixed schema name of the provider's data-source configuration table.
        /// Required on each declaration; identifies the provider independently of virtual tables.
        /// </summary>
        public string DataSourceSchemaName { get; set; }

        /// <summary>
        /// Operation handled by the decorated plugin class. Must be explicitly specified.
        /// </summary>
        public DataProviderEvent Event { get; set; } = DataProviderEvent.Unspecified;

        /// <summary>
        /// Provider display name. Required on at least one declaration for the same provider.
        /// </summary>
        public string ProviderName { get; set; }

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
