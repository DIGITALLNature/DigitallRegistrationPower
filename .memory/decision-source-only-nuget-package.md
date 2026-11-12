# Decision: Publish registration attributes as source

The repository will publish `Digitall.Plugins.Registration.Source` alongside the existing `Digitall.Plugins.Registration` package. Both packages use the same semantic-release version, while the existing package remains the binary distribution.

The source package packs the existing attribute and enum `.cs` files under `contentFiles/cs/any/Digitall.Plugins.Registration/` with `buildAction="Compile"`; it does not ship a compiled registration assembly or runtime package dependencies. This path groups the linked source files in a virtual project folder. Both packages are packed and published in the same release using the version selected by semantic-release; the source package is packed separately without symbols.

Consumers should choose only one package because both expose identical fully qualified types. Projects that repackage the source package should use `PrivateAssets="all"` to prevent its transitive flow.
