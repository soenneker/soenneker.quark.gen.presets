namespace Soenneker.Quark.Gen.Presets;

/// <summary>
/// Options for the QuarkGenPresets generator. Extend as needed for your generator.
/// </summary>
public sealed class GeneratorOptions
{
    /// <summary>
    /// The default.
    /// </summary>
    public static readonly GeneratorOptions Default = new();

    /// <summary>
    /// Gets or sets a value indicating whether emit diagnostics.
    /// </summary>
    public bool EmitDiagnostics { get; set; }
}
