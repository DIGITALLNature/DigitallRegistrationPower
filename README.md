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

Use `[CustomDataProviderRegistration]` on classes that implement `IPlugin` to declare operation handlers for a custom data provider. The attribute has a parameterless constructor and uses named properties only. Stack one attribute per operation and use the same data-source schema name to group handlers into one provider.

The provider is identified by its **data-source configuration table**, not by a virtual table. One provider can serve multiple virtual tables.

| Property | Type | Requirement | Description |
|---|---|---|---|
| `DataSourceSchemaName` | `string` | Required on each declaration | Publisher-prefixed schema name of the data-source configuration table; deployment key |
| `Event` | `DataProviderEvent` | Required on each declaration | Operation handled by the decorated class; defaults to `Unspecified`, which is invalid for deployment |
| `ProviderName` | `string` | Required once per grouped provider | Provider display name; explicitly supplied values set/update the name |
| `DataSourceDisplayName` | `string` | Optional | Singular label for the configuration table |
| `DataSourcePluralName` | `string` | Optional | Plural label for the configuration table |
| `Description` | `string` | Optional | Provider description |

```csharp
using Digitall.Plugins.Registration;

[CustomDataProviderRegistration(
    DataSourceSchemaName = "dgt_CrmDataSource",
    ProviderName = "CRM Provider",
    Event = DataProviderEvent.Create)]
[CustomDataProviderRegistration(
    DataSourceSchemaName = "dgt_CrmDataSource",
    Event = DataProviderEvent.Update)]
public class HandleUpsertOnVirtualTable : IPlugin
{
    public void Execute(IServiceProvider serviceProvider)
    {
        // ...
    }
}
```

Provider metadata can be supplied on any declaration in the group; it does not need to be repeated. Optional string properties default to `null`. Omitted metadata preserves existing values: a minimal declaration updates the provider name and handler assignments, but does not change existing table labels or descriptions. For first-time creation only, omitted table labels default to the schema name (singular) and the singular label plus `" Records"` (plural).

**Registration-tool contract:** This package supplies metadata only; it does not perform Dataverse registration or validation. A compatible dgtp release must:

- Validate required values before writes, reject `Unspecified` and unknown events, and reject conflicting metadata or different classes claiming the same provider operation.
- Resolve the schema name to the table's logical name and find the provider by `datasourcelogicalname`; reject ambiguous provider matches.
- Create/update the `EntityDataProvider` record and assign registered plugin-type IDs to its operation fields, rather than creating ordinary SDK message-processing steps.
- Preserve existing handlers for undeclared operations and preserve omitted optional metadata. Omission does not clear a value or remove a handler.

Creating or validating the provider's data-source configuration table belongs to the registration tool. The special table metadata and creation/reuse lifecycle must be verified before first-time provisioning is supported. Data-source records, custom configuration columns, virtual tables, and their mappings are outside this declaration's scope. Credentials must not be stored in attributes.

**Breaking-change migration:** The `(string entityName, DataProviderEvent eventRegistration)` constructor and `EntityName` property have been removed. Replace old declarations such as `[CustomDataProviderRegistration("dgt_virtual_table", DataProviderEvent.Retrieve)]` with named properties as shown above, selecting the provider's data-source schema name rather than copying the virtual-table name. Supply `ProviderName` at least once per provider and rebuild the plugin assembly. This API requires corresponding support in dgtp; legacy declarations must be rejected with migration guidance, not reinterpreted.

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
| `CustomDataProviderRegistrationAttribute` | `class` | ✅ | Declares a provider operation handler keyed by its data-source configuration table |
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

| Value | Int | Description |
|---|---|---|
| `Unspecified` | `-1` | Default sentinel; invalid for deployment |
| `Retrieve` | `0` | Single-record retrieve |
| `RetrieveMultiple` | `1` | Multi-record retrieve |
| `Create` | `2` | Create operation |
| `Update` | `3` | Update operation |
| `Delete` | `4` | Delete operation |

---

## Requirements

- .NET Standard 2.0 or higher

---

## License

Released under the [Microsoft Public License (MS-PL)](LICENSE.md).
