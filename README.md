[![](https://img.shields.io/nuget/v/soenneker.quark.gen.presets.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.quark.gen.presets/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.quark.gen.presets/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.quark.gen.presets/actions/workflows/publish-package.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.quark.gen.presets/build-and-test.yml?label=Build&style=for-the-badge)](https://github.com/soenneker/soenneker.quark.gen.presets/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/nuget/dt/soenneker.quark.gen.presets.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.quark.gen.presets/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.quark.gen.presets/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.quark.gen.presets/actions/workflows/codeql.yml)

# Soenneker.Quark.Gen.Presets

Generates strongly typed `QuarkPresets` registry members from attributed preset classes.

## Install

```bash
dotnet add package Soenneker.Quark.Gen.Presets
```

## Usage

Define a preset in a project that references the Quark preset APIs:

```csharp
[QuarkPreset("card")]
public sealed class CardPreset : QuarkPreset
{
    public override void Apply(QuarkPresetContext context)
    {
        context.Padding = Padding.Is4;
        context.Rounded = Rounded.IsLg;
        context.Class = "card";
    }
}
```

The generator adds a token named after the class with the `Preset` suffix removed:

```razor
<Div Preset="QuarkPresets.Card" />
```

The attribute value becomes the token’s runtime name (`card` in this example). The generated registry creates the preset lazily and reuses that instance whenever the token is applied.

`QuarkPresets.ContainerWrapper` is always generated as the built-in centered container preset.

## Naming

`CardPreset` generates `QuarkPresets.Card`. Preset class names must produce unique registry member names and cannot produce the reserved name `ContainerWrapper`; the generator reports `QGP001` when names collide.
