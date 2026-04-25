namespace Soenneker.Quark.Gen.Presets;

/// <summary>
/// Options for the QuarkGenPresets generator. Extend as needed for your generator.
/// </summary>
public sealed class GeneratorOptions
{
    public static readonly GeneratorOptions Default = new();

    public bool EmitDiagnostics { get; set; }
}
