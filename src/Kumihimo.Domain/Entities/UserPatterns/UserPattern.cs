using Kumihimo.Domain.Exceptions;
using Kumihimo.Domain.ValueObjects;

namespace Kumihimo.Domain.Entities.UserPatterns;

public class UserPattern
{
    private readonly List<string> _colors = new();

    public Guid Id { get; private set; } = Guid.NewGuid();

    public string UserId { get; private set; } = default!;
    public Guid TemplateId { get; private set; }

    public string DisplayName { get; private set; } = "";

    public IReadOnlyList<string> Colors => _colors;

    protected UserPattern() { } // EF

    public UserPattern(string userId, Guid templateId, int positionCount, string? displayName = null)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new DomainRuleException("UserId is required.");

        if (templateId == Guid.Empty)
            throw new DomainRuleException("TemplateId is required.");

        if (positionCount <= 0)
            throw new DomainRuleException("PositionCount must be positive.");

        UserId = userId.Trim();
        TemplateId = templateId;
        DisplayName = displayName?.Trim() ?? "";

        for (int i = 0; i < positionCount; i++)
            _colors.Add("#000000"); // default
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainRuleException("Display name cannot be empty.");

        DisplayName = name.Trim();
    }

    public void SetColor(int position, string hexColor)
    {
        var pos = new TamaPosition(position, _colors.Count);
        var color = ThreadColor.FromHex(hexColor);

        _colors[pos.Value] = color.Hex;
    }

    public void SetColors(IReadOnlyList<string> colors)
    {
        if (colors is null)
            throw new DomainRuleException("Colors cannot be null.");

        if (colors.Count != _colors.Count)
            throw new DomainRuleException($"Colors count must be exactly {_colors.Count}.");

        for (int i = 0; i < colors.Count; i++)
            _colors[i] = ThreadColor.FromHex(colors[i]).Hex;
    }
}