using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EffectiveMobileTestTask;

public class Order
{
    #region Fields

    public static readonly Regex DateTimeRegex = new(
        @"[0-9]{4}-(0[1-9]|1[012])-(0[1-9]|1[0-9]|2[0-9]|3[01]) ([0-1]\d|2[0-3])(:[0-5]\d){2}");

    private readonly double _weight;

    private readonly string _deliveryTime = default!;

    #endregion Fields

    #region Properties

    [Required]
    public Guid Id { get; init; }

    public double Weight
    {
        get { return _weight; }
        init
        {
            if (value < 0)
                throw new ArgumentException($"Weight should be more or equals than 0, but was {value}");
            _weight = value;
        }
    }

    public string DistrictName { get; init; }

    /*
     * Спорный тип данных для времени, хотелось бы DateTime.
     * В задании было написано про формат yyyy-MM-dd HH:mm:ss
     * и было не понятно, имеется ввиду, чтобы входная\выходная дата была в этом формате
     * или поле типа строки было такого формата.
     * Не стал переусложнять, так как для DateTime пришлось бы писать JsonConverter
     * для выбранного типа хранения данных.
     * Кастомную сериализацию\десериализацию не видел смысла писать. Будь проще как говорится
     * P.C. Коммент написан для объяснения и недолюбливаю бесмысленные комментарии в коде типа такого
     * хотя и написан для объяснения причины выбора
     */

    public string DeliveryTime
    {
        get { return _deliveryTime; }
        init
        {
            if (!DateTimeRegex.IsMatch(value) || value is null)
                throw new ArgumentException($"Delivery time should be in format: [yyyy-MM-dd HH:mm:ss], but was: {value}");
            _deliveryTime = value;
        }
    }

    #endregion Properties

    public Order(Guid id, double weight, string districtName, string deliveryTime)
    {
        Id = id;
        Weight = weight;
        DistrictName = districtName;
        DeliveryTime = deliveryTime;
    }

    #region Override Object Methods

    public override bool Equals(object? obj)
    {
        if (obj is not Order order)
            return false;
        return Id.Equals(order.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"Id: {Id}\tWeight: {Weight,-10:F5}\tDistrict: {DistrictName,-25}\tDeliveryTime: {DeliveryTime}";
    }

    #endregion Override Object Methods
}