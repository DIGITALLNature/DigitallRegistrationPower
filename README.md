# DigitallRegistrationsPower (DRP)
This is a NuGet package containing a library that provides annotations for dataverse assemblies (plugins and workflows) for automated registration.

<br/>
<p align="center">
    <a href="LICENSE" target="_blank">
        <img src="https://img.shields.io/github/license/DIGITALLNature/DigitallRegistrationPower.svg" alt="GitHub license">
    </a>
    <a href="https://github.com/DIGITALLNature/DigitallPower/releases" target="_blank">
        <img src="https://img.shields.io/github/tag/DIGITALLNature/DigitallRegistrationPower.svg" alt="GitHub tag (latest SemVer)">
    </a>
    <a href="https://www.nuget.org/packages/dgt.registration" target="_blank">
        <img src="https://img.shields.io/nuget/v/dgt.registration" alt="Nuget">
    </a>
    <a href="https://github.com/DIGITALLNature/DigitallPower/graphs/contributors" target="_blank">
        <img src="https://img.shields.io/github/contributors-anon/DIGITALLNature/DigitallRegistrationPower.svg" alt="GitHub contributors">
    </a>
</p>
<br/>

# Installation
You can install this package via CLI: `dotnet add package dgt.registration`.

If you want to install it in a project that is in a subdirectory give the path to the directory containing the `csproj` file or the path to the `csproj` like this: `dotnet add src/MyProject package dgt.registration`.

# Usage
The library provides the following annotations

## Plugins
For plug-ins (derived from IPlugin):

### CustomApiRegistration
For custom apis, parameter here is the messagename of the custom api.

```csharp
using dgt.registration;

[CustomApiRegistration("dgt_calc_vacations")]
public class CalcVacationsPlugin : IPlugin
{
        public void Execute(IServiceProvider serviceProvider)
        {
            ..
        }
}
```

### CustomDataProviderRegistration
For custom data providers, parameter here is the tablename of the virtual table filled with this data provider and which events this plugin processes.

```csharp
using dgt.registration;

[CustomDataProviderRegistration("dgt_virtual_table", DataProviderEvent.Create)]
[CustomDataProviderRegistration("dgt_virtual_table", DataProviderEvent.Update)]
public class HandleUpsertOnVirtualTable : IPlugin 
{
    public void Execute(IServiceProvider serviceProvider) 
    {
        ..
    }
}
```

### PluginRegistration
For regular plugins.

The following parameters can be used:
- PluginExecutionMode (Asynchronous or Synchronous) **Mandatory**
- MessageName (Like Create, Update, custom actions etc.) **Mandatory**
- PluginExecutionStage (PreValidation, Pre or Post) **Mandatory**
- PrimaryEntityName
- SecondaryEntityName
- FilterAttributes
- ExecutionOrder
- PreEntityImage
- PreEntityImageAttributes
- PostEntityImage
- PostEntityImageAttributes
- Configuration (sets Unsecure Configuration)

```csharp
using dgt.registration;

[PluginRegistration(PluginExecutionMode.Asynchronous,"Create", PluginExecutionStage.PreOperation, PrimaryEntityName = "account", ExecutionOrder = 10)]
[PluginRegistration(PluginExecutionMode.Synchronous, "Update", PluginExecutionStage.PostOperation, PrimaryEntityName = "account", FilterAttributes = new []{"name"} ,PreEntityImage = true, PreEntityImageAttributes = new []{"name"})]
public class SamplePlugin : IPlugin {
    public void Execute(IServiceProvider serviceProvider) {
        ..
    }
}
```

## Workflowassemblys
For workflow assemblys (derived from CodeActivity):

### WorkflowRegistration
The following parameters can be used:
- Name **Mandatory**
- Group (Defaults to DGT)
- includeVersion (Default of false - hint registration tool to include Version in Name)

```csharp
using dgt.registration;

[WorkflowRegistration("Sample", "SampleGroup")]
public class SampleWorkflow : CodeActivity
{
    [Input(nameof(Email))]
    [RequiredArgument]
    [ReferenceTarget("email")]
    public InArgument<EntityReference> Email { get; set; }

    protected override void Execute(CodeActivityContext context)
    {
        ..
    }
}
```

# ❤️&nbsp; Community and Contributions

The DigitallRegistrationPower is a **community-driven open source project** backed by DIGITALL. We are committed to a fully transparent development process and **highly appreciate any contributions**. Whether you are helping us fixing bugs, proposing new feature, improving our documentation or spreading the word - **we would love to have you as part of the DigitallRegistrationPower community**.


## 📫&nbsp; Have a question? Want to chat? Ran into a problem?

We are happy to answer your questions via [GitHub Discussions](https://github.com/DIGITALLNature/DigitallRegistrationPower/discussions)!


## 🤝&nbsp; Found a bug? Missing a specific feature?

Feel free to **file a new issue** with a respective title and description on the the [DigitallPower](https://github.com/DIGITALLNature/DigitallRegistrationPower/issues) repository. If you already found a solution to your problem, **we would love to review your pull request**! Have a look at our [contribution guidelines](https://github.com/DIGITALLNature/DigitallRegistrationPower/contributing.md) to find out about our coding standards.


## ✅&nbsp; Requirements

DigitallRegistrationPower requires DOTNET Standard 2.o to be used.


## 📘&nbsp; License

DigitallRegistrationPower is released under the under terms of the [MS PL License](LICENSE).