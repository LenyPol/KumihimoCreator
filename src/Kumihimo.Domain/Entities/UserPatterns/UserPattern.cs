using System.Drawing;
using Kumihimo.Domain.Exceptions;
using Kumihimo.Domain.ValueObjects;

namespace Kumihimo.Domain.Entities.UserPatterns;

/// <summary>
/// Represents a user-defined color pattern based on a template.
/// Encapsulates a fixed-length list of thread colors assigned to pattern positions.
/// </summary>

public class UserPattern
{
    private static readonly ThreadColor DefaultColor = ThreadColor.FromHex("#000000");
    private readonly List<ThreadColor> _colors = new();

    public Guid Id { get; private set; }
    public string UserId { get; private set; }
    public Guid TemplateId { get; private set; }
    public int PositionCount { get; private set; }
    public string DisplayName { get; private set; }
    public IReadOnlyList<ThreadColor> Colors => _colors;

    protected UserPattern() => (UserId, DisplayName) = (null!, null!);

    /// <summary>
    /// Creates a new <see cref="UserPattern"/> with all positions initialized to the default color.
    /// </summary>
    /// <exception cref="DomainRuleException">Thrown when any parameter is invalid.</exception>
    public UserPattern(string userId, Guid templateId, int positionCount, string? displayName = null)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new DomainRuleException("UserId is required.");
        if (templateId == Guid.Empty)
            throw new DomainRuleException("TemplateId is required.");
        if (positionCount <= 0)
            throw new DomainRuleException("PositionCount must be positive.");

        Id = Guid.NewGuid();
        UserId = userId.Trim();
        TemplateId = templateId;
        PositionCount = positionCount;

        var name = displayName?.Trim();
        DisplayName = string.IsNullOrWhiteSpace(name)
            ? "New pattern"
            : name;

        _colors = new List<ThreadColor>(Enumerable.Repeat(DefaultColor, positionCount));
    }

    /// <summary>Renames the pattern. Throws if name is null or whitespace.</summary>
    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainRuleException("Display name cannot be empty.");
        DisplayName = name.Trim();
    }

    /// <summary>Sets the thread color at the specified position.</summary>
    public void SetColor(int position, string hexColor)
    {
        var pos = new TamaPosition(position, _colors.Count);
        var color = ThreadColor.FromHex(hexColor);
        _colors[pos.Value] = color;
    }

    /// <summary>Replaces all thread colors. Count must match <see cref="PositionCount"/>.</summary>
    /// <exception cref="DomainRuleException">Thrown when colors is null or count does not match.</exception>
    public void SetColors(IReadOnlyList<string> colors)
    {
        if (colors is null)
            throw new DomainRuleException("Colors cannot be null.");
        if (colors.Count != _colors.Count)
            throw new DomainRuleException($"Colors count must be exactly {_colors.Count}.");
        for (int i = 0; i < colors.Count; i++)
            _colors[i] = ThreadColor.FromHex(colors[i]);
    }
}