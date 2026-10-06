# Decision: Property-based custom data provider registration

## Context and rationale

The former attribute identified a virtual table using a positional string. The new contract identifies a provider through its data-source configuration table, independently of virtual tables.

Reusing the old constructor would allow existing code to compile with a different meaning. The agreed approach is a clean breaking change to the existing attribute type, not a replacement type or an obsolete compatibility constructor. Named properties were preferred for explicit call sites and extensibility without constructor changes.

## Implemented library changes

- Removed the `(string entityName, DataProviderEvent eventRegistration)` constructor and `EntityName`.
- Added a constructor requiring `DataSourceSchemaName`, `Event`, and `ProviderName`; these properties are read-only and remain available to reflection.
- Added writable `DataSourceDisplayName`, `DataSourcePluralName`, and `Description` properties.
- Added `Unspecified = -1`, preserving operation values `Retrieve = 0` through `Delete = 4`.
- Retained class targeting and multiple declarations per class.
- Updated README.md with usage, migration, and the registration-tool contract.

Schema name, event, and provider name are required by the constructor on each declaration. Attribute construction itself performs no validation.

## Consumer coordination

dgtp implementation is outside this repository and has not been completed here. It must read the public attribute properties and reject legacy declarations with actionable migration errors.

The provider name is supplied on every declaration, so the consumer must merge explicit metadata and reject conflicting values. The consumer must also reject different classes claiming the same provider operation before writes. Provider lookup uses the configuration table's resolved logical name and must reject ambiguous matches.

Optional metadata omission preserves existing values; defaults apply only during creation. The provider name is a required constructor argument on every declaration and sets/updates the provider name. Undeclared operation handlers should be preserved, not implicitly removed.

Provider handlers must be assigned on EntityDataProvider records rather than deployed as ordinary SDK steps. Dry-run, reconciliation, and assembly upgrades must account for provider references and platform-owned steps.

First-time data-source table provisioning remains unverified: confirm its special metadata and creation/reuse lifecycle against PRT before implementing it. Connection-specific records, custom configuration columns, virtual tables, and mappings remain outside scope.

## Validation and release

The Release build succeeded with local signing disabled; compiled public API inspection confirmed constructor removal, writable properties, defaults, stable enum values, and attribute multiplicity.

Release as a breaking change using a Conventional Commit with `!` or a `BREAKING CHANGE:` footer. No commit or release was created during this implementation.
