// Copyright (c) DIGITALL Nature. All rights reserved.
// This code is licensed under the Microsoft Public License (MS-PL). See LICENSE.md in the project root for license information.

using System;

namespace Digitall.Plugins.Registration
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
