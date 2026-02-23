using Kumihimo.Domain.Entities.Templates;
using Kumihimo.Domain.Exceptions;

namespace Kumihimo.Domain.Entities.Templates.Marudai;

/// <summary>
/// Abstract base for all Marudai-based patterns.
/// Marudai patterns share position-based validation but differ in weaving logic.
/// </summary>
public abstract class MarudaiPatternTemplate : PatternTemplate
{
    public MarudaiPatternType Type { get; private set; }

    protected MarudaiPatternTemplate() { } // Required by EF Core

    protected MarudaiPatternTemplate(string name, string? description, MarudaiPatternType type, int positionCount)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainRuleException("Name cannot be null or empty.");

        Name = name.Trim();
        Description = description?.Trim() ?? string.Empty;
        Type = type;
        PositionCount = ValidatePositionCount(type, positionCount);

    }

/// <summary>
/// Override in derived classes to implement pattern-specific validation rules.
/// Called after full object initialization via the Create factory method.
/// </summary>
    protected abstract void Validate();

/// <summary>
/// Factory method that creates a fully initialized and validated pattern instance....
/// Ensures Validate() is called only after the object is completely constructed.
/// </summary>
    protected static TPattern Create<TPattern>(Func<TPattern> factory)
        where TPattern : MarudaiPatternTemplate
    {
        var pattern = factory();
        pattern.Validate(); 
        return pattern;
    }

/// <summary>
/// Checks whether the given position count is valid for the specified pattern type.
/// Throws DomainRuleException if the count is not in the list of allowed values.
/// </summary>
    private static int ValidatePositionCount(MarudaiPatternType type, int positionCount)
    {
        var allowed = type.AllowedPositionCounts();
        if (!allowed.Contains(positionCount))
            throw new DomainRuleException(
                $"Pattern type {type} does not support {positionCount} positions. " +
                $"Allowed: {string.Join(", ", allowed)}");
        return positionCount;
    }
}

