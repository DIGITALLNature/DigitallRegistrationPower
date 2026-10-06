# [3.0.0-beta.3](https://github.com/DIGITALLNature/DigitallRegistrationPower/compare/v3.0.0-beta.2...v3.0.0-beta.3) (2026-10-06)


### Bug Fixes

* address open repository issues ([#11](https://github.com/DIGITALLNature/DigitallRegistrationPower/issues/11)) ([b9a52c5](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/b9a52c5f9e4cc37dfcef85e78bf50dbe2ec2115c))

# [3.0.0-beta.2](https://github.com/DIGITALLNature/DigitallRegistrationPower/compare/v3.0.0-beta.1...v3.0.0-beta.2) (2026-10-06)


* feat!: use named properties for custom data provider registration ([#10](https://github.com/DIGITALLNature/DigitallRegistrationPower/issues/10)) ([f351885](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/f351885a4271f6424100ac8baf73b11758633a79))


### BREAKING CHANGES

* remove the entity-name constructor and EntityName property.
Declarations must use DataSourceSchemaName and Event, with ProviderName supplied once
per provider group.

# [3.0.0-beta.1](https://github.com/DIGITALLNature/DigitallRegistrationPower/compare/v2.0.0...v3.0.0-beta.1) (2026-10-06)


* feat!: remove Configuration property from PluginRegistrationAttribute ([#7](https://github.com/DIGITALLNature/DigitallRegistrationPower/issues/7)) ([4b46481](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/4b464813e58fcbd84bc5f1c81b50457b698b17ae))
* refactor!: rename project and namespace to Digitall.Plugins.Registration ([87048b0](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/87048b0125f1727dd81b0e199411eee9a5586ce1))


### Bug Fixes

* grammar error in xmldoc ([5109069](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/51090697022d136734fc6a4b16751088bba264f1))
* malformed xmldoc ([1727119](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/172711967ed9658e66f72bb3fd0b7f667c7d6ee8))


### Features

* add ManagedIdentityRegistrationAttribute ([2103d70](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/2103d70606e1db41c1ee4d8e5171e57473011730))


### BREAKING CHANGES

* PluginRegistrationAttribute no longer has a
Configuration property. Migrate to Environment Variables or another
runtime configuration mechanism.

Co-authored-by: Junie <junie@jetbrains.com>
* package ID, assembly name, and root namespace changed
from dgt.registration to Digitall.Plugins.Registration.

Consumers must update their package reference and replace
'using dgt.registration;' with 'using Digitall.Plugins.Registration;'.

Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>

# [2.0.0-beta.3](https://github.com/DIGITALLNature/DigitallRegistrationPower/compare/v2.0.0-beta.2...v2.0.0-beta.3) (2026-10-06)


* feat!: remove Configuration property from PluginRegistrationAttribute ([#7](https://github.com/DIGITALLNature/DigitallRegistrationPower/issues/7)) ([4b46481](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/4b464813e58fcbd84bc5f1c81b50457b698b17ae))


### BREAKING CHANGES

* PluginRegistrationAttribute no longer has a
Configuration property. Migrate to Environment Variables or another
runtime configuration mechanism.

Co-authored-by: Junie <junie@jetbrains.com>

# [2.0.0](https://github.com/DIGITALLNature/DigitallRegistrationPower/compare/v1.0.1...v2.0.0) (2026-06-10)


* refactor!: rename project and namespace to Digitall.Plugins.Registration ([8ca3a6c](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/8ca3a6c7e458dc90699ef7e4a03dd65905f1dff9))


### Bug Fixes

* grammar error in xmldoc ([b797427](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/b797427541b0b8e04bd52306885d929fc5e5a3d8))
* malformed xmldoc ([8112011](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/81120115040a707392d4b64089e6eedd5eba6fbd))


### Features

* add ManagedIdentityRegistrationAttribute ([4075977](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/40759777fdc5099b7c249943fcebd91c532156f8))

### BREAKING CHANGES

* package ID, assembly name, and root namespace changed
from dgt.registration to Digitall.Plugins.Registration.

Consumers must update their package reference and replace
'using dgt.registration;' with 'using Digitall.Plugins.Registration;'.

Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>

# [2.0.0-beta.2](https://github.com/DIGITALLNature/DigitallRegistrationPower/compare/v2.0.0-beta.1...v2.0.0-beta.2) (2026-06-09)


### Bug Fixes

* grammar error in xmldoc ([5109069](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/51090697022d136734fc6a4b16751088bba264f1))
* malformed xmldoc ([1727119](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/172711967ed9658e66f72bb3fd0b7f667c7d6ee8))

# [2.0.0-beta.1](https://github.com/DIGITALLNature/DigitallRegistrationPower/compare/v1.0.1...v2.0.0-beta.1) (2026-05-29)


* refactor!: rename project and namespace to Digitall.Plugins.Registration ([87048b0](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/87048b0125f1727dd81b0e199411eee9a5586ce1))


### Features

* add ManagedIdentityRegistrationAttribute ([2103d70](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/2103d70606e1db41c1ee4d8e5171e57473011730))


### BREAKING CHANGES

* package ID, assembly name, and root namespace changed
from dgt.registration to Digitall.Plugins.Registration.

Consumers must update their package reference and replace
'using dgt.registration;' with 'using Digitall.Plugins.Registration;'.

Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>

## [1.0.1](https://github.com/DIGITALLNature/DigitallRegistrationPower/compare/v1.0.0...v1.0.1) (2024-03-04)


### Bug Fixes

* **license:** fix typo in license hint in nuget package ([ef0e9f5](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/ef0e9f5a572e9c0a9ee3248941f4cc082a6991b3))

# 1.0.0 (2023-02-23)


### Features

* initial set of Registrations: ([998703c](https://github.com/DIGITALLNature/DigitallRegistrationPower/commit/998703c56c54efd4ada51325c91f7bf595d7e71c))
