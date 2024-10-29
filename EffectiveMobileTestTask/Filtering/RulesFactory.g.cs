#nullable enable

using EffectiveMobileTestTask.Logging;

namespace EffectiveMobileTestTask.Filtering;

public static partial class RulesFactory
{
			
		public static IRule<T> AndRule<T>(this Predicate<T> left, Predicate<T> right)
		{
			return new AndRule<T>(UnaryRule(left), UnaryRule(right));
		}

		public static IRule<T> AndRule<T>(this Predicate<T> left, IRule<T> right)
		{
			return new AndRule<T>(UnaryRule(left), right);
		}

		public static IRule<T> AndRule<T>(this IRule<T> left, Predicate<T> right)
		{
			return new AndRule<T>(left, UnaryRule(right));
		}

		public static IRule<T> AndRule<T>(this IRule<T> left, IRule<T> right)
		{
			return new AndRule<T>(left, right);
		}
		
			
		public static IRule<T> OrRule<T>(this Predicate<T> left, Predicate<T> right)
		{
			return new OrRule<T>(UnaryRule(left), UnaryRule(right));
		}

		public static IRule<T> OrRule<T>(this Predicate<T> left, IRule<T> right)
		{
			return new OrRule<T>(UnaryRule(left), right);
		}

		public static IRule<T> OrRule<T>(this IRule<T> left, Predicate<T> right)
		{
			return new OrRule<T>(left, UnaryRule(right));
		}

		public static IRule<T> OrRule<T>(this IRule<T> left, IRule<T> right)
		{
			return new OrRule<T>(left, right);
		}
		
			
		public static IRule<T> XorRule<T>(this Predicate<T> left, Predicate<T> right)
		{
			return new XorRule<T>(UnaryRule(left), UnaryRule(right));
		}

		public static IRule<T> XorRule<T>(this Predicate<T> left, IRule<T> right)
		{
			return new XorRule<T>(UnaryRule(left), right);
		}

		public static IRule<T> XorRule<T>(this IRule<T> left, Predicate<T> right)
		{
			return new XorRule<T>(left, UnaryRule(right));
		}

		public static IRule<T> XorRule<T>(this IRule<T> left, IRule<T> right)
		{
			return new XorRule<T>(left, right);
		}
		
	}