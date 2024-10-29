namespace EffectiveMobileTestTask.Filtering;

public static partial class OrderRules
{
    public static IRule<Order> Unary(this Predicate<Order> predicate) => RulesFactory.UnaryRule(predicate);

    public static IRule<Order> AndBetweenWeight(this IRule<Order> rule, double from, double to)
    {
        return rule.AndRule(BetweenWeight(from, to));
    }

    public static IRule<Order> OrBetweenWeight(this IRule<Order> rule, double from, double to)
    {
        return rule.OrRule(BetweenWeight(from, to));
    }

    public static IRule<Order> XorBetweenWeight(this IRule<Order> rule, double from, double to)
    {
        return rule.XorRule(BetweenWeight(from, to));
    }

    public static IRule<Order> BetweenWeight(double from, double to)
    {
        return RulesFactory.BetweenRule<Order, double>(
            v => v.Weight, from, to);
    }

    public static IRule<Order> AndBetweenDeliveryTime(this IRule<Order> rule, string from, string to)
    {
        return rule.AndRule(BetweenDeliveryTime(from, to));
    }

    public static IRule<Order> OrBetweenDeliveryTime(this IRule<Order> rule, string from, string to)
    {
        return rule.OrRule(BetweenDeliveryTime(from, to));
    }

    public static IRule<Order> XorBetweenDeliveryTime(this IRule<Order> rule, string from, string to)
    {
        return rule.XorRule(BetweenDeliveryTime(from, to));
    }

    public static IRule<Order> BetweenDeliveryTime(string from, string to)
    {
        return RulesFactory.BetweenRule<Order, string>(
            v => v.DeliveryTime, from, to);
    }
}