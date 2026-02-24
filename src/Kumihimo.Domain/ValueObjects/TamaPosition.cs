using Kumihimo.Domain.Exceptions;

namespace Kumihimo.Domain.ValueObjects;

/// <summary>
/// Represents the position of a tama (bobbin) on the kumihimo disk or stand.
/// This is an immutable value object that ensures the position is always valid
/// within the given range of available positions.
/// </summary>
public readonly struct TamaPosition : IEquatable<TamaPosition>
{
    public int Value { get; }

    private TamaPosition(int value)
    {
        Value = value;
    }

    public static TamaPosition Create(int value, int positionCount)
    {
        if (positionCount <= 0)
            throw new DomainRuleException("Position count must be positive.");

        if (value < 0 || value >= positionCount)
            throw new DomainRuleException($"Position must be between 0 and {positionCount - 1}.");

        return new TamaPosition(value);
    }

    public override string ToString() => Value.ToString();

    public bool Equals(TamaPosition other) => Value == other.Value;
    public override bool Equals(object? obj) => obj is TamaPosition other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(Value);

    public static bool operator ==(TamaPosition left, TamaPosition right) => left.Equals(right);
    public static bool operator !=(TamaPosition left, TamaPosition right) => !(left == right);
}