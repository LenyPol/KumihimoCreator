using Kumihimo.Domain.Exceptions;

namespace Kumihimo.Domain.Entities.Templates.Marudai;

/// <summary>
/// Defines pattern types available for the Marudai loom.
/// </summary>
public enum MarudaiPatternType
{
    /// <summary>Supports 32 positions.</summary>
    KikkoGumi,

    /// <summary>Supports 24 or 32 positions.</summary>
    KaraKumi
}

/// <summary>
/// Extension methods for <see cref="MarudaiPatternType"/>.
/// </summary>
public static class MarudaiPatternTypeExtensions
{
    /// <summary>
    /// Returns allowed position counts for the given Marudai pattern type...
    /// </summary>
    /// <exception cref="DomainRuleException">Thrown if pattern type is not supported.</exception>
    public static IReadOnlyList<int> AllowedPositionCounts(this MarudaiPatternType type) => type switch
    {
        MarudaiPatternType.KikkoGumi => [32],
        MarudaiPatternType.KaraKumi => [24, 32],
        _ => throw new DomainRuleException($"Unsupported pattern type: {type}")
    };
}