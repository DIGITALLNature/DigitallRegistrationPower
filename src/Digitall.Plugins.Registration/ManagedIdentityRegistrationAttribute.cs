// Copyright (c) DIGITALL Nature. All rights reserved.
// This code is licensed under the Microsoft Public License (MS-PL). See LICENSE.md in the project root for license information.

using System;

namespace Digitall.Plugins.Registration
{
    /// <summary>
    /// Registers a managed identity in Dataverse and associates it with the plugin assembly / package.
    /// </summary>
    /// <remarks>
    /// This attribute just handles the registration in Dataverse. You still need to set up the managed identity in Azure and take care of signing the assembly / package. Please follow the instructions provided by Microsoft.
    /// </remarks>
    /// <see href="https://learn.microsoft.com/en-us/power-platform/admin/managed-identity-overview"/>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ManagedIdentityRegistrationAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedIdentityRegistrationAttribute"/> class.
        /// </summary>
        /// <param name="clientId">The client id of the managed identity.</param>
        public ManagedIdentityRegistrationAttribute(string clientId)
        {
            ClientId = clientId;
        }

        /// <summary>
        /// Gets the client id of the managed identity.
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// Tenant id of the managed identity. Defaults to the current tenant if not provided.
        /// </summary>
        public string TenantId { get; set; }
    }
}
