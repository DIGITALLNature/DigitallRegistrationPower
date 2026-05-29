# Guide: Release Process

> Commit message types and version bump rules are documented in AGENTS.md.

## How releases work

This project uses **semantic-release** to fully automate versioning and publishing. No manual version bumps or NuGet pushes needed.

## Trigger

A release is created automatically when a push lands on `main` or `beta` **and** the CI build job succeeds.

- `main` → stable release (e.g. `1.2.0`)
- `beta` → pre-release (e.g. `1.2.0-beta.1`)

## What semantic-release does

1. Analyzes commit messages since last release (Conventional Commits)
2. Determines version bump from commit types (see AGENTS.md)
3. Generates release notes and updates `CHANGELOG.md` (auto-generated — **never edit manually**)
4. Builds and pushes the NuGet package to nuget.org (including symbols, `includeSymbols: true`)
5. Creates a GitHub Release + tag
6. Commits `CHANGELOG.md` back with `chore(release): X.Y.Z [skip ci]`

## Required secrets (GitHub repository settings)

| Secret | Purpose |
|---|---|
| `NUGET_TOKEN` | API key for nuget.org push |
| `CI_GITHUB_TOKEN` | PAT for committing CHANGELOG back (needs `repo` scope) |
| `SIGNING_KEY` | Base64-encoded `.snk` strong-name key for assembly signing |

## Assembly signing at build time

The `.snk` key is materialised from `SIGNING_KEY` via `timheuer/base64-to-file`. The path is passed as the `AssemblyOriginatorKeyFile` env var. Local builds without the key will fail — override `SignAssembly` to `false` locally if needed.

## Local dry-run

```bash
npx semantic-release --dry-run
```
