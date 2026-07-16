namespace HRMS.Shared.Kernel.Exceptions;

public class BusinessRuleValidationException : Exception
{
    public string RuleName { get; }
    public IReadOnlyList<string> BrokenRules { get; }

    public BusinessRuleValidationException(string ruleName, string message)
        : base(message)
    {
        RuleName = ruleName;
        BrokenRules = new[] { message };
    }

    public BusinessRuleValidationException(string ruleName, IReadOnlyList<string> brokenRules)
        : base($"Business rule '{ruleName}' was violated: {string.Join("; ", brokenRules)}")
    {
        RuleName = ruleName;
        BrokenRules = brokenRules;
    }
}
