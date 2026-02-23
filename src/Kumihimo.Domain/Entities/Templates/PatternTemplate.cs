namespace Kumihimo.Domain.Entities.Templates;

/// <summary>
/// Abstract base for all kumihimo pattern templates.
/// Defines shared identity and structure across all loom types.
/// </summary>

public abstract class PatternTemplate
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; protected set; } = string.Empty;
    public string Description { get; protected set; } = string.Empty;
    public int PositionCount { get; protected set; }

    // Required by EF Core
    protected PatternTemplate() { }
}
