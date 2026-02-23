namespace Kumihimo.Domain.Exceptions;

/// <summary>
/// Exception thrown when a domain business rule is violated.
/// </summary>
[Serializable]
public sealed class DomainRuleException : Exception
{
    public string? RuleName { get; }

    public DomainRuleException(string message) 
        : base(message) { }

    public DomainRuleException(string message, string ruleName) 
        : base(message)
    {
        RuleName = ruleName;
    }

    public DomainRuleException(string message, Exception? innerException)
        : base(message, innerException) { }
}
