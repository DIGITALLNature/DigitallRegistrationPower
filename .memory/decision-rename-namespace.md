# Decision: Rename package from `dgt.registration` to `Digitall.Plugins.Registration`

## Date
2026-05-29

## Context
The original package was published as `dgt.registration` with the root namespace `dgt.registration`. This did not follow the DIGITALL Nature naming conventions and made the package harder to discover.

## Decision
Rename everything to `Digitall.Plugins.Registration`:

| Artifact | Before | After |
|---|---|---|
| NuGet package ID | `dgt.registration` | `Digitall.Plugins.Registration` |
| Root namespace | `dgt.registration` | `Digitall.Plugins.Registration` |
| Assembly name | `dgt.registration` | `Digitall.Plugins.Registration` |
| Project folder | `src/dgt.registration/` | `src/Digitall.Plugins.Registration/` |
| `.csproj` filename | `dgt.registration.csproj` | `Digitall.Plugins.Registration.csproj` |

## Changes made
- All 7 `.cs` source files: namespace updated
- `.csproj`: `<AssemblyName>`, `<RootNamespace>`, `<PackageId>` added/set
- `.sln`: project reference path and display name updated
- `README.md`: all references to old package name updated

## Impact on consumers
Consumers of the old `dgt.registration` NuGet package will need to:
1. Update the package reference to `Digitall.Plugins.Registration`
2. Update `using dgt.registration;` → `using Digitall.Plugins.Registration;`



## Follow-up fixed
`package.json` `projectPath` was also updated from `src/dgt.registration/dgt.registration.csproj` to `src/Digitall.Plugins.Registration/Digitall.Plugins.Registration.csproj` on the same day.
