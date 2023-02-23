using System;

namespace DGT.Registrations
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
