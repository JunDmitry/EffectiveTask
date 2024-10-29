namespace EffectiveMobileTestTask.Filtering;

public class UnaryRule<T> : IRule<T>
{
    private readonly Predicate<T> _predicate;

    public UnaryRule(Predicate<T> predicate)
    {
        _predicate = predicate;
    }

    public bool Should(T value)
    {
        return _predicate(value);
    }
}