namespace EffectiveMobileTestTask.Filtering;

public static partial class RulesFactory
{
    public static IRule<T> UnaryRule<T>(Predicate<T> predicate) => new UnaryRule<T>(predicate);

    public static IRule<T> NotRule<T>(this Predicate<T> predicate) => UnaryRule<T>(k => !predicate(k));

    public static IRule<T> NotRule<T>(this IRule<T> rule) => NotRule<T>(rule.Should);

    public static IRule<T> BetweenRule<T, V>(Func<T, V> selector, V from, V to)
        where V : IComparable<V>
    {
        return AndRule(
            UnaryRule<T>(v => selector(v).CompareTo(from) >= 0),
            UnaryRule<T>(v => selector(v).CompareTo(to) <= 0));
    }
}