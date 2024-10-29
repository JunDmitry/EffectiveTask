#nullable enable

using EffectiveMobileTestTask.Logging;

namespace EffectiveMobileTestTask.Filtering;

	
public class AndRule<T> : BinaryRule<T>
{
    public AndRule(Predicate<T> left, Predicate<T> right) : base(left, right)
    {
    }

    public AndRule(Predicate<T> left, IRule<T> right) : base(left, right)
    {
    }

    public AndRule(IRule<T> left, Predicate<T> right) : base(left, right)
    {
    }

    public AndRule(IRule<T> left, IRule<T> right) : base(left, right)
    {
    }

    public override bool Should(T value)
    {
        return _left.Should(value) && _right.Should(value);
    }
}

	
public class OrRule<T> : BinaryRule<T>
{
    public OrRule(Predicate<T> left, Predicate<T> right) : base(left, right)
    {
    }

    public OrRule(Predicate<T> left, IRule<T> right) : base(left, right)
    {
    }

    public OrRule(IRule<T> left, Predicate<T> right) : base(left, right)
    {
    }

    public OrRule(IRule<T> left, IRule<T> right) : base(left, right)
    {
    }

    public override bool Should(T value)
    {
        return _left.Should(value) || _right.Should(value);
    }
}

	
public class XorRule<T> : BinaryRule<T>
{
    public XorRule(Predicate<T> left, Predicate<T> right) : base(left, right)
    {
    }

    public XorRule(Predicate<T> left, IRule<T> right) : base(left, right)
    {
    }

    public XorRule(IRule<T> left, Predicate<T> right) : base(left, right)
    {
    }

    public XorRule(IRule<T> left, IRule<T> right) : base(left, right)
    {
    }

    public override bool Should(T value)
    {
        return _left.Should(value) ^ _right.Should(value);
    }
}

