# Project Summary — Digitall.Plugins.Registration

## Status

Current releases: **main `2.0.0`** (`v2.0.0`), **beta `3.0.0-beta.2`** (`v3.0.0-beta.2`)
NuGet package: [`Digitall.Plugins.Registration`](https://www.nuget.org/packages/Digitall.Plugins.Registration)

## What this project is

A lightweight `.NET Standard 2.0` NuGet package providing C# attributes for annotating Microsoft Dataverse plugin assemblies and workflow activities. An external registration tool reads these attributes to automatically register the classes in Dataverse.

## Project structure

```
DigitallRegistrationPower/
├── src/
│   └── Digitall.Plugins.Registration/       ← single project (formerly dgt.registration)
│       ├── Digitall.Plugins.Registration.csproj
│       ├── PluginRegistrationAttribute.cs
│       ├── CustomApiRegistrationAttribute.cs
│       ├── CustomDataProviderRegistrationAttribute.cs
│       ├── WorkflowRegistrationAttribute.cs
│       ├── PluginExecutionMode.cs
│       ├── PluginExecutionStage.cs
│       └── DataProviderEvent.cs
├── Digitall.Plugins.Registration.slnx
├── AGENTS.md                                 ← agent rules, code conventions, commit format
├── README.md                                 ← public docs, included as NuGet PackageReadme
├── package.json                              ← semantic-release + commitlint config
└── .github/workflows/
    ├── build.yml                             ← CI build + semantic-release
    ├── codeql.yml
    └── qodana_code_quality.yml
```

## Where to find what

| Topic | Location |
|---|---|
| Code conventions, naming, copyright | `AGENTS.md` → Code Conventions |
| Commit format and version bump rules | `AGENTS.md` → Commit Messages |
| Documentation update rules | `AGENTS.md` → Documentation Maintenance |
| Public API reference | `README.md` |
| Release pipeline, secrets, signing | `.memory/guide-release-process.md` |
| How to add a new attribute | `.memory/guide-adding-attributes.md` |
| Non-obvious C# / build caveats | `.memory/research-project-conventions.md` |
| Rename decision (dgt.registration → Digitall.Plugins.Registration) | `.memory/decision-rename-namespace.md` |
| Property-based provider registration and dgtp coordination | `.memory/decision-provider-registration-contract.md` |
| Removal of ManagedIdentityRegistrationAttribute | `.memory/decision-remove-managed-identity-attribute.md` |

## Active tasks

The property-based custom data provider API is implemented in the registration library.
Corresponding dgtp consumer support is still required; see README.md for the contract and migration.
First-time data-source table provisioning remains subject to verification of Dataverse's special table metadata.
