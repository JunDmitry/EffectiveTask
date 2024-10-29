namespace EffectiveMobileTestTask.Filtering;

public abstract class BinaryRule<T> : IRule<T>
{
    protected readonly IRule<T> _left;

    protected readonly IRule<T> _right;

    public BinaryRule(Predicate<T> left, Predicate<T> right)
        : this(RulesFactory.UnaryRule(left), RulesFactory.UnaryRule(right))
    { }

    public BinaryRule(Predicate<T> left, IRule<T> right)
        : this(RulesFactory.UnaryRule(left), right)
    { }

    public BinaryRule(IRule<T> left, Predicate<T> right)
        : this(left, RulesFactory.UnaryRule(right))
    { }

    public BinaryRule(IRule<T> left, IRule<T> right)
    {
        _left = left;
        _right = right;
    }

    public abstract bool Should(T value);
}