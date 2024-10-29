using System.Runtime.CompilerServices;

namespace EffectiveMobileTestTask.Logging;

[InterpolatedStringHandler]
public struct StructuredLoggingInterpolatedStringHandler
{
    private readonly StringBuilder _template = default!;

    private readonly ArgumentList _arguments = default!;

    public bool IsEnabled { get; }

    public StructuredLoggingInterpolatedStringHandler(
        int literalLength,
        int formattedCount,
        ILogger logger,
        LogLevel logLevel,
        out bool isEnabled)
    {
        IsEnabled = isEnabled = logger.IsEnabled(logLevel);
        if (isEnabled)
        {
            _template = new(literalLength + 20 * formattedCount);
            _arguments = new(formattedCount);
        }
    }

    public void AppendLiteral(string literal)
    {
        if (!IsEnabled)
            return;
        _template.Append(EscapeSymbol(literal, '{', '}'));
    }

    public void AppendFormatted<T>(
        T value,
        [CallerArgumentExpression("value")] string name = "")
    {
        if (!IsEnabled)
            return;

        _arguments.Add(value);
        _template.Append($"{{@{name}}}");
    }

    public (string, object?[]) GetTemplateAndArguments() => (_template.ToString(), _arguments.Arguments);

    private string EscapeSymbol(string s, params char[] symbols)
    {
        HashSet<char> hashSymbols = new(symbols);
        StringBuilder sb = new(s.Length);
        foreach (char ch in s)
            sb.Append(ch, hashSymbols.Contains(ch) ? 2 : 1);

        return sb.ToString();
    }

    private class ArgumentList
    {
        private int _index;

        public object?[] Arguments { get; }

        public ArgumentList(int formattedCount)
        {
            Arguments = new object?[formattedCount];
        }

        public void Add(object? value)
        {
            Arguments[_index++] = value;
        }
    }
}