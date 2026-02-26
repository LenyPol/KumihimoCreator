using System.Text.RegularExpressions;
using Kumihimo.Domain.Exceptions;

namespace Kumihimo.Domain.ValueObjects;

public sealed class ThreadColor : IEquatable<ThreadColor>
{
    private static readonly Regex HexRegex = new("^#[0-9A-Fa-f]{6}$", RegexOptions.Compiled);

    public string Hex { get; }

    private ThreadColor(string hex) => Hex = hex;

    public static ThreadColor FromHex(string hex)
    {
        // Trim first, then validate emptiness
        hex = hex?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(hex))
            throw new DomainRuleException("Thread color cannot be empty.");

        // Regex accepts both cases, we normalize to uppercase
        if (!HexRegex.IsMatch(hex))
            throw new DomainRuleException("Thread color must be in format #RRGGBB.");

        return new ThreadColor(hex.ToUpperInvariant());
    }

    public override string ToString() => Hex;

    public bool Equals(ThreadColor? other) => other is not null && Hex == other.Hex;
    public override bool Equals(object? obj) => obj is ThreadColor other && Equals(other);
    public override int GetHashCode() => Hex.GetHashCode(StringComparison.Ordinal);

    public static bool operator ==(ThreadColor? left, ThreadColor? right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(ThreadColor? left, ThreadColor? right) =>
        !(left == right);
}