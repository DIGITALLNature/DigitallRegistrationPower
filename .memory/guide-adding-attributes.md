# Guide: Adding a New Registration Attribute

> Documentation update rules are in AGENTS.md (Documentation Maintenance section).
> README.md structure is in place — follow the existing pattern when adding sections.

## Steps

### 1. Create the attribute class

File: `src/Digitall.Plugins.Registration/<Name>RegistrationAttribute.cs`

```csharp
// Copyright (c) DIGITALL Nature. All rights reserved.
// This code is licensed under the Microsoft Public License (MS-PL). See LICENSE.md in the project root for license information.

using System;

namespace Digitall.Plugins.Registration
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MyRegistrationAttribute : Attribute
    {
        public MyRegistrationAttribute(string mandatoryParam)
        {
            MandatoryParam = mandatoryParam;
        }

        public string MandatoryParam { get; }
        public string OptionalParam { get; set; }
    }
}
```

### 2. Add a new enum (if needed)

File: `src/Digitall.Plugins.Registration/<EnumName>.cs`

```csharp
// Copyright (c) DIGITALL Nature. All rights reserved.
// This code is licensed under the Microsoft Public License (MS-PL). See LICENSE.md in the project root for license information.

namespace Digitall.Plugins.Registration
{
    public enum MyEnum
    {
        ValueA,
        ValueB,
    }
}
```

### 3. Update README.md

- New `###` section under the relevant `##` block with a parameter table and code example
- New row in the **Attributes** table (API Reference)
- New enum block in the **Enums** section if applicable

### 4. Commit

```
feat: add MyRegistration attribute
```

## Design rules

- `AllowMultiple = true` by default — only use `false` for attributes that are logically singular (e.g. `WorkflowRegistrationAttribute`)
- Mandatory parameters → constructor; optional parameters → settable properties
- Integer-backed enums: the underlying `int` values are part of the public contract consumed by the registration tool — document them in README
- Zero runtime dependencies — never add a `<PackageReference>` without `PrivateAssets=all`
