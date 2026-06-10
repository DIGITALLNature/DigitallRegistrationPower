// Copyright (c) DIGITALL Nature. All rights reserved.
// This code is licensed under the Microsoft Public License (MS-PL). See LICENSE.md in the project root for license information.

using System;

namespace Digitall.Plugins.Registration
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CustomApiRegistrationAttribute : Attribute
    {
        public CustomApiRegistrationAttribute(string messageName)
        {
            MessageName = messageName;
        }

        /// <summary>
        /// Message of plugin for this registration
        /// </summary>
        public string MessageName { get; }
    }
}
