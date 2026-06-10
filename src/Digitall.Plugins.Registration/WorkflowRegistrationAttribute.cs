// Copyright (c) DIGITALL Nature. All rights reserved.
// This code is licensed under the Microsoft Public License (MS-PL). See LICENSE.md in the project root for license information.

using System;

namespace Digitall.Plugins.Registration
{

    [AttributeUsage(AttributeTargets.Class)]
    public class WorkflowRegistrationAttribute : Attribute
    {
        public WorkflowRegistrationAttribute(string name, string group = "DGT", bool includeVersion = false)
        {
            Name = name;
            Group = group;
            IncludeVersion = includeVersion;
        }

        public string Name { get; }
        public string Group { get; }
        public bool IncludeVersion { get; }
    }
}
