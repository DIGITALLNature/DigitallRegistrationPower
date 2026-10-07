# Decision: Removal of ManagedIdentityRegistrationAttribute

## Context

`ManagedIdentityRegistrationAttribute` was previously added to allow decorating assemblies with a Managed Identity (User-Assigned Managed Identity / App Registration) Client ID and optional Tenant ID for Dataverse plugin assemblies or packages.

## Problem

Hardcoding or embedding the Managed Identity Client ID at the assembly level results in the exact same app registration / user-assigned managed identity being registered across all target environments and deployment stages (e.g., Development, Testing, Acceptance, Production).

This violates security best practices:
1. Environments must maintain isolation with environment-specific service principals / managed identities with least-privilege scoping.
2. Promoting the same binary across environments should not bind all environments to a single identity.
3. Managed Identity configuration belongs to environment-specific provisioning / ALM deployment processes rather than compile-time assembly metadata.

## Decision

Remove `ManagedIdentityRegistrationAttribute` from the library and update all public documentation accordingly. This is a breaking change and will be published with a major version bump.
