# Agent Guidelines

Instructions for all AI agents (GitHub Copilot, Claude, Cursor, etc.) working on this repository.

---

## Documentation Maintenance (MANDATORY)

When making changes to this codebase, **you MUST keep the documentation up to date**. This is not optional.

### Rules

1. **README.md must reflect the current state of the project.** After any change that affects the public API, architecture, configuration, project structure, or usage patterns, update the corresponding section(s) in `README.md`.

2. **What requires a README update:**
   - Adding, removing, or renaming public attributes, enums, or their parameters
   - Adding or removing NuGet dependencies
   - Changing the namespace or package ID
   - Adding new folders or restructuring the project
   - Changing build/CI/CD workflows
   - Adding new features or capabilities

3. **What does NOT require a README update:**
   - Internal refactoring that doesn't change the public API
   - Bug fixes that don't change behavior or usage
   - Test-only changes
   - Code style / formatting changes

4. **CHANGELOG.md is auto-generated** by semantic-release. Do NOT edit it manually.

5. **Keep documentation in English.** All documentation in this repository is written in English.

### Documentation Style

- Use concise, technical language
- Include code examples for new public APIs
- Keep the table of contents in sync with the actual sections
- Use tables for listing related items (request fakes, config vars, etc.)
- Architecture diagrams use ASCII art (no external dependencies)

---

## Code Conventions

- **Language:** C# targeting `netstandard2.0` — no explicit `LangVersion`, `Nullable`, or `ImplicitUsings` set; keep it that way
- **Naming:** Follow standard .NET naming conventions (PascalCase for public members, `_camelCase` for private fields)
- **Licensing header:** All source files start with the exact two-line header:
  - `// Copyright (c) DIGITALL Nature. All rights reserved.`
  - `// This code is licensed under the Microsoft Public License (MS-PL). See LICENSE.md in the project root for license information.`
- **No test project** — this package has no automated tests; validate changes by building and inspecting the public API
- **Zero runtime dependencies** — do not add `<PackageReference>` entries that end up in consumers' dependency graph; use `PrivateAssets=all` for build-only tools
- **Assembly signing** — the project is strong-name signed; the `.snk` key is injected via the `SIGNING_KEY` CI secret and must never be committed

---

## Commit Messages

This project uses [Conventional Commits](https://www.conventionalcommits.org/) enforced by commitlint + Husky.

### Format

```
<type>(<scope>): <short description>

[optional body]

[optional footer(s)]
```

### Types

| Type | When to use | Version bump |
|------|-------------|--------------|
| `feat` | New feature or capability | minor |
| `fix` | Bug fix | patch |
| `docs` | Documentation only | none |
| `refactor` | Code change that neither fixes a bug nor adds a feature | none |
| `perf` | Performance improvement | patch |
| `test` | Adding or updating tests only | none |
| `chore` | Tooling, CI, dependencies, config | none |
| `style` | Formatting, white-space, etc. (no logic change) | none |

### Rules

- **Subject line:** imperative mood, lowercase, no period at end, max 100 chars
- **Breaking changes:** Add `!` after type/scope (e.g. `feat!: remove deprecated API`) or add `BREAKING CHANGE:` footer
- **Scope:** optional, use the affected component (e.g. `feat(query): add fiscal year grouping`)

### Examples

```
feat: add BulkDelete registration attribute
fix: correct ExecutionOrder default value
docs: update README with WorkflowRegistration example
refactor: extract attribute validation into helper
chore: bump Microsoft.SourceLink.GitHub to 9.0.0
feat!: remove deprecated PluginImageAttribute
```


## RTK — Token-Optimized CLI

**rtk** is a CLI proxy that filters and compresses command outputs, saving 60-90% tokens.

**Always prefix shell commands with `rtk`** when available. It passes through unchanged if no filter exists — always safe to use.

```bash
rtk git status              # Compact status
rtk git diff                # Compact diff
rtk dotnet build            # Filtered build output
rtk dotnet test             # Failures only
```

Even in command chains:
```bash
rtk git add . && rtk git commit -m "msg" && rtk git push
```

---

## Knowledge Persistence (.memory/)

Agents **must** persist valuable findings, decisions, and context in the `.memory/` directory so that knowledge survives across sessions and is available to other agents and developers.

### Rules

1. **Read first.** Always read `.memory/summary.md` at the start of a task to understand current status and avoid redundant work.
2. **Store findings** in `.memory/` directory. All notes must be in markdown format.
3. **Filename convention:** `.memory/<type>-<title>.md`
   - `<type>` is one of: `research`, `phase`, `guide`, `decision`, `implementation`
   - `<title>` is a short kebab-case descriptor
4. **Exception:** `.memory/summary.md` does not follow the naming convention — it is the index file.
5. **Keep `.memory/summary.md` up to date** with current status, active tasks, and key findings. Prune incorrect or outdated information.
6. **Committed to git.** The `.memory/` directory is shared knowledge — do not add it to `.gitignore`.

### Types

| Type | Purpose |
|------|---------|
| `research` | Investigation results, API behavior findings, library evaluations |
| `phase` | Progress tracking for multi-step work (e.g. migration phases) |
| `guide` | How-to instructions, patterns, and reusable approaches |
| `decision` | Architecture/design decisions with rationale and alternatives considered |
| `implementation` | Implementation plans, technical specs, or post-implementation notes |

### When to Write

- After discovering non-obvious behavior or caveats
- After making a design/architecture decision
- When starting multi-step work that spans sessions
- When findings would save future agents significant research time

### When NOT to Write

- Trivial or self-evident facts already in the code
- Temporary debugging notes (use comments or session memory instead)
- Information already covered in README.md or code comments

### Example Filenames

```
.memory/summary.md
.memory/research-fetchxml-paging-behavior.md
.memory/decision-tunit-over-xunit.md
.memory/phase-net10-migration.md
.memory/guide-bulk-operation-patterns.md
.memory/implementation-audit-export-logic.md
```
