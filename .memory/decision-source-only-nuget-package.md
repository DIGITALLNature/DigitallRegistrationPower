# Decision: Publish registration attributes as source

The repository will publish `Digitall.Plugins.Registration.Source` alongside the existing `Digitall.Plugins.Registration` package. Both packages use the same semantic-release version, while the existing package remains the binary distribution.

The source package packs the existing attribute and enum `.cs` files as `contentFiles/cs/any` with `buildAction="Compile"`; it does not ship a compiled registration assembly or runtime package dependencies. The release plugin receives both project paths in one `projectPath` array so both packages are packed and published in the same release.

Consumers should choose only one package because both expose identical fully qualified types. Projects that repackage the source package should use `PrivateAssets="all"` to prevent its transitive flow.
