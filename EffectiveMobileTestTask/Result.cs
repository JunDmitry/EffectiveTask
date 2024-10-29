namespace EffectiveMobileTestTask;

public class Result<T>
{
    public T? Value { get; init; }

    public Exception? Exception { get; init; }

    public bool IsSuccess { get; init; }
}