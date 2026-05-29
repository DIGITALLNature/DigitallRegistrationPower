# Research: Project Conventions (non-obvious caveats)

> Naming, copyright header, dependency rules, and assembly signing are documented in AGENTS.md.
> This file captures caveats that are NOT obvious from reading AGENTS.md or the csproj.

## Source file encoding

All existing source files start with a UTF-8 BOM (`﻿`). Keep this consistent when adding new files.

## C# language features in practice

- `netstandard2.0` + .NET 6 SDK → C# 10 features compile, but avoid them to stay compatible with older SDK consumers
- No `Nullable` — do **not** enable it without a deliberate decision; it would be a breaking change for consumers
- No `ImplicitUsings` — all `using` statements must be explicit in each file

## packages.lock.json

`RestorePackagesWithLockFile=true` is set in the csproj. The lock file **must** be committed and kept up to date. After changing any `<PackageReference>`, run:

```bash
dotnet restore
```

and commit the updated `packages.lock.json`. CI uses `--locked-mode`, which fails if the lock file is stale.

## CI SDK version

Build and release workflows pin `dotnet-version: 10.0.x`. If the SDK is upgraded, both `build.yml` steps (build job + release job) must be updated together.
