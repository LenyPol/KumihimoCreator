using System.Text.RegularExpressions;
using Kumihimo.Domain.Exceptions;

namespace Kumihimo.Domain.ValueObjects;

public sealed class ThreadColor : IEquatable<ThreadColor>
{
    private static readonly Regex HexRegex = new("^#[0-9A-Fa-f]{6}$");

    public string Hex { get; }

    private ThreadColor(string hex) => Hex = hex;

    public static ThreadColor FromHex(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            throw new DomainRuleException("Thread color cannot be empty.");

        hex = hex.Trim();

        if (!HexRegex.IsMatch(hex))
            throw new DomainRuleException("Thread color must be in format #RRGGBB.");

        return new ThreadColor(hex.ToUpperInvariant());
    }

    public override string ToString() => Hex;

    public bool Equals(ThreadColor? other) => other is not null && Hex == other.Hex;
    public override bool Equals(object? obj) => obj is ThreadColor other && Equals(other);
    public override int GetHashCode() => Hex.GetHashCode(StringComparison.Ordinal);
}
