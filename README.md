# Digitall.Plugins.Registration

NuGet package providing C# attributes for automated registration of Microsoft Dataverse plugin assemblies and workflow activities.

<p align="center">
    <a href="LICENSE.md" target="_blank">
        <img src="https://img.shields.io/github/license/DIGITALLNature/DigitallRegistrationPower.svg" alt="GitHub license">
    </a>
    <a href="https://github.com/DIGITALLNature/DigitallRegistrationPower/releases" target="_blank">
        <img src="https://img.shields.io/github/tag/DIGITALLNature/DigitallRegistrationPower.svg" alt="GitHub tag (latest SemVer)">
    </a>
    <a href="https://www.nuget.org/packages/Digitall.Plugins.Registration" target="_blank">
        <img src="https://img.shields.io/nuget/v/Digitall.Plugins.Registration" alt="NuGet">
    </a>
    <a href="https://github.com/DIGITALLNature/DigitallRegistrationPower/graphs/contributors" target="_blank">
        <img src="https://img.shields.io/github/contributors-anon/DIGITALLNature/DigitallRegistrationPower.svg" alt="GitHub contributors">
    </a>
</p>

---

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
  - [Plugin Registration](#pluginregistration)
  - [Custom API Registration](#customapiregistration)
  - [Custom Data Provider Registration](#customdataproviderregistration)
  - [Workflow Registration](#workflowregistration)
  - [Managed Identity Registration](#managedidentityregistration)
- [API Reference](#api-reference)
  - [Attributes](#attributes)
  - [Enums](#enums)
- [Requirements](#requirements)
- [License](#license)

---

## Installation

```bash
dotnet add package Digitall.Plugins.Registration
```

For a project in a subdirectory:

```bash
dotnet add src/MyProject package Digitall.Plugins.Registration
```

---

## Usage

Add `using Digitall.Plugins.Registration;` to your file and decorate your plugin or workflow class with the appropriate attribute. A registration tool can then discover and apply these annotations automatically.

### PluginRegistration

Use `[PluginRegistration]` on classes that implement `IPlugin` for standard plugin step registration. Multiple attributes can be stacked on one class to register multiple steps.

**Constructor parameters (mandatory):**

| Parameter | Type | Description |
|---|---|---|
| `mode` | `PluginExecutionMode` | `Synchronous` or `Asynchronous` |
| `messageName` | `string` | Dataverse message (e.g. `"Create"`, `"Update"`) |
| `stage` | `PluginExecutionStage` | Pipeline stage |

**Optional named parameters:**

| Parameter | Type | Default | Description |
|---|---|---|---|
| `PrimaryEntityName` | `string` | `null` | Logical name of the primary entity |
| `SecondaryEntityName` | `string` | `null` | Logical name of the secondary entity |
| `FilterAttributes` | `string[]` | `null` | Step only fires when at least one of these attributes changed |
| `ExecutionOrder` | `int` | `100` | Rank among steps on the same message/stage |
| `PreEntityImage` | `bool` | `false` | Register a pre-entity image |
| `PreEntityImageAttributes` | `string[]` | `null` | Attributes for the pre-image; `null` = all |
| `PostEntityImage` | `bool` | `false` | Register a post-entity image |
| `PostEntityImageAttributes` | `string[]` | `null` | Attributes for the post-image; `null` = all |
| `Configuration` | `string` | `null` | Unsecure configuration of the plugin step |

```csharp
using Digitall.Plugins.Registration;

[PluginRegistration(PluginExecutionMode.Asynchronous, "Create", PluginExecutionStage.PreOperation,
    PrimaryEntityName = "account", ExecutionOrder = 10)]
[PluginRegistration(PluginExecutionMode.Synchronous, "Update", PluginExecutionStage.PostOperation,
    PrimaryEntityName = "account",
    FilterAttributes = new[] { "name" },
    PreEntityImage = true, PreEntityImageAttributes = new[] { "name" })]
public class SamplePlugin : IPlugin
{
    public void Execute(IServiceProvider serviceProvider)
    {
        // ...
    }
}
```

---

### CustomApiRegistration

Use `[CustomApiRegistration]` on classes that implement `IPlugin` and act as the handler for a Custom API. Multiple attributes can be stacked.

| Parameter | Type | Description |
|---|---|---|
| `messageName` | `string` | Message name of the Custom API |

```csharp
using Digitall.Plugins.Registration;

[CustomApiRegistration("dgt_calc_vacations")]
public class CalcVacationsPlugin : IPlugin
{
    public void Execute(IServiceProvider serviceProvider)
    {
        // ...
    }
}
```

---

### CustomDataProviderRegistration

Use `[CustomDataProviderRegistration]` on classes that implement `IPlugin` and act as a virtual table data provider. Stack one attribute per event you want to handle.

| Parameter | Type | Description |
|---|---|---|
| `entityName` | `string` | Logical name of the virtual table |
| `eventRegistration` | `DataProviderEvent` | The data provider event to handle |

```csharp
using Digitall.Plugins.Registration;

[CustomDataProviderRegistration("dgt_virtual_table", DataProviderEvent.Create)]
[CustomDataProviderRegistration("dgt_virtual_table", DataProviderEvent.Update)]
public class HandleUpsertOnVirtualTable : IPlugin
{
    public void Execute(IServiceProvider serviceProvider)
    {
        // ...
    }
}
```

---

### WorkflowRegistration

Use `[WorkflowRegistration]` on classes that derive from `CodeActivity` for workflow activity registration.

| Parameter | Type | Default | Description |
|---|---|---|---|
| `name` | `string` | — | Display name of the workflow activity |
| `group` | `string` | `"DGT"` | Group/category shown in the workflow designer |
| `includeVersion` | `bool` | `false` | Hint the registration tool to append the assembly version to the name |

```csharp
using Digitall.Plugins.Registration;

[WorkflowRegistration("Sample", "SampleGroup")]
public class SampleWorkflow : CodeActivity
{
    [Input(nameof(Email))]
    [RequiredArgument]
    [ReferenceTarget("email")]
    public InArgument<EntityReference> Email { get; set; }

    protected override void Execute(CodeActivityContext context)
    {
        // ...
    }
}
```

---

### ManagedIdentityRegistration

Use `[ManagedIdentityRegistration]` at **assembly level** to associate a managed identity with the plugin assembly or package. Applied once per assembly.

> **Note:** This attribute only handles the Dataverse-side registration. You still need to set up the managed identity in Azure and sign the assembly/package. See [Microsoft Managed Identity overview](https://learn.microsoft.com/en-us/power-platform/admin/managed-identity-overview).

| Parameter | Type | Default | Description |
|---|---|---|---|
| `clientId` | `string` | — | Client ID of the managed identity |
| `TenantId` | `string` | `null` | Tenant ID; defaults to the current tenant if not set |

```csharp
using Digitall.Plugins.Registration;

[assembly: ManagedIdentityRegistration("00000000-0000-0000-0000-000000000000")]
```

---

## API Reference

### Attributes

| Attribute | Target | AllowMultiple | Description |
|---|---|---|---|
| `PluginRegistrationAttribute` | `class` | ✅ | Registers a plugin step |
| `CustomApiRegistrationAttribute` | `class` | ✅ | Registers a Custom API handler |
| `CustomDataProviderRegistrationAttribute` | `class` | ✅ | Registers a virtual table data provider handler |
| `WorkflowRegistrationAttribute` | `class` | ❌ | Registers a workflow activity |
| `ManagedIdentityRegistrationAttribute` | `assembly` | ❌ | Associates a managed identity with the assembly |

### Enums

#### `PluginExecutionMode`

| Value | Int | Description |
|---|---|---|
| `Synchronous` | `0` | Executes synchronously in the pipeline |
| `Asynchronous` | `1` | Executes asynchronously via the async service |

#### `PluginExecutionStage`

| Value | Int | Description |
|---|---|---|
| `PreValidation` | `10` | Before the main database transaction |
| `PreOperation` | `20` | Within the transaction, before the core operation |
| `MainOperation` | `30` | The core platform operation |
| `PostOperation` | `40` | Within the transaction, after the core operation |

#### `DataProviderEvent`

| Value | Description |
|---|---|
| `Retrieve` | Single-record retrieve |
| `RetrieveMultiple` | Multi-record retrieve |
| `Create` | Create operation |
| `Update` | Update operation |
| `Delete` | Delete operation |

---

## Requirements

- .NET Standard 2.0 or higher

---

## License

Released under the [Microsoft Public License (MS-PL)](LICENSE.md).
