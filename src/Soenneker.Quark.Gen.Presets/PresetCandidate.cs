using System;

namespace Soenneker.Quark.Gen.Presets;

internal readonly struct PresetCandidate : IEquatable<PresetCandidate>
{
    public PresetCandidate(string typeName, string tokenName, string memberName)
    {
        TypeName = typeName;
        TokenName = tokenName;
        MemberName = memberName;
    }

    public string TypeName { get; }
    public string TokenName { get; }
    public string MemberName { get; }

    public bool Equals(PresetCandidate other) =>
        string.Equals(TypeName, other.TypeName, StringComparison.Ordinal) &&
        string.Equals(TokenName, other.TokenName, StringComparison.Ordinal) &&
        string.Equals(MemberName, other.MemberName, StringComparison.Ordinal);

    public override bool Equals(object? obj) => obj is PresetCandidate other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;
            hash = (hash * 31) + StringComparer.Ordinal.GetHashCode(TypeName);
            hash = (hash * 31) + StringComparer.Ordinal.GetHashCode(TokenName);
            hash = (hash * 31) + StringComparer.Ordinal.GetHashCode(MemberName);
            return hash;
        }
    }
}
