using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Threading;

namespace Soenneker.Quark.Gen.Presets.Tests;

public sealed class QuarkGenPresetsTests
{
    [Test]
    public async Task Static_presets_opt_into_freezing_while_dynamic_presets_keep_callbacks(CancellationToken cancellationToken)
    {
        const string source = """
            namespace Soenneker.Quark {
                [System.AttributeUsage(System.AttributeTargets.Class)]
                public sealed class QuarkPresetAttribute : System.Attribute {
                    public QuarkPresetAttribute(string name) { }
                    public bool IsStatic { get; set; }
                }
                [QuarkPreset("fixed", IsStatic = true)] public class FixedPreset { }
                [QuarkPreset("dynamic")] public class DynamicPreset { }
            }
            """;
        var compilation = CSharpCompilation.Create("Presets", [CSharpSyntaxTree.ParseText(source, cancellationToken: cancellationToken)],
            [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new QuarkGenPresetsGenerator());
        var result = driver.RunGenerators(compilation, cancellationToken: cancellationToken).GetRunResult();
        await Assert.That(result.Diagnostics.Any(d => d.Severity == DiagnosticSeverity.Error)).IsFalse();
        string generated = result.Results.Single().GeneratedSources.Single().SourceText.ToString();
        await Assert.That(generated).Contains("Fixed { get; } = global::Soenneker.Quark.QuarkPresetToken.Freeze(\"fixed\"");
        await Assert.That(generated).Contains("Dynamic { get; } = new(\"dynamic\"");
        await Assert.That(generated).Contains("ContainerWrapper { get; } = global::Soenneker.Quark.QuarkPresetToken.Freeze(");
    }
}
