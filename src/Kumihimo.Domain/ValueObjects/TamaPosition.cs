using Kumihimo.Domain.Exceptions;

namespace Kumihimo.Domain.ValueObjects;

public readonly struct TamaPosition : IEquatable<TamaPosition>
{
    public int Value { get; }

    public TamaPosition(int value, int positionCount)
    {
        if (positionCount <= 0)
            throw new DomainRuleException("Position count must be positive.");

        if (value < 0 || value >= positionCount)
            throw new DomainRuleException($"Position must be between 0 and {positionCount - 1}.");

        Value = value;
    }

    public override string ToString() => Value.ToString();

    public bool Equals(TamaPosition other) => Value == other.Value;
    public override bool Equals(object? obj) => obj is TamaPosition other && Equals(other);
    public override int GetHashCode() => Value;
}