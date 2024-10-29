#nullable enable

namespace EffectiveMobileTestTask.Filtering;

public static partial class OrderRules 
{
	
		
			public static IRule<Order> AndWithId(this IRule<Order> rule, Guid value)
			{
				return rule.AndRule(v => v.Id == value);
			}

		
			public static IRule<Order> OrWithId(this IRule<Order> rule, Guid value)
			{
				return rule.OrRule(v => v.Id == value);
			}

		
			public static IRule<Order> XorWithId(this IRule<Order> rule, Guid value)
			{
				return rule.XorRule(v => v.Id == value);
			}

		
	
		
			public static IRule<Order> AndWithWeight(this IRule<Order> rule, double value)
			{
				return rule.AndRule(v => v.Weight == value);
			}

		
			public static IRule<Order> OrWithWeight(this IRule<Order> rule, double value)
			{
				return rule.OrRule(v => v.Weight == value);
			}

		
			public static IRule<Order> XorWithWeight(this IRule<Order> rule, double value)
			{
				return rule.XorRule(v => v.Weight == value);
			}

		
	
		
			public static IRule<Order> AndWithDistrictName(this IRule<Order> rule, string value)
			{
				return rule.AndRule(v => v.DistrictName == value);
			}

		
			public static IRule<Order> OrWithDistrictName(this IRule<Order> rule, string value)
			{
				return rule.OrRule(v => v.DistrictName == value);
			}

		
			public static IRule<Order> XorWithDistrictName(this IRule<Order> rule, string value)
			{
				return rule.XorRule(v => v.DistrictName == value);
			}

		
	
		
			public static IRule<Order> AndWithDeliveryTime(this IRule<Order> rule, string value)
			{
				return rule.AndRule(v => v.DeliveryTime == value);
			}

		
			public static IRule<Order> OrWithDeliveryTime(this IRule<Order> rule, string value)
			{
				return rule.OrRule(v => v.DeliveryTime == value);
			}

		
			public static IRule<Order> XorWithDeliveryTime(this IRule<Order> rule, string value)
			{
				return rule.XorRule(v => v.DeliveryTime == value);
			}

		
	}