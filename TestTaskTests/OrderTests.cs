using EffectiveMobileTestTask;

namespace TestTaskTests;

public class OrderTests
{
    private string _rawId = "00000000-0000-0000-0000-000000000000";
    private string _defaltDateTime = "1900-01-01 00:00:00";
    private Guid _testIdGuid;

    [SetUp]
    public void Setup()
    {
        _testIdGuid = Guid.Parse(_rawId);
    }

    [Test]
    public void ThrowArgumentExceptionWhenWeightNegative()
    {
        Assert.Throws<ArgumentException>(() => { new Order(_testIdGuid, -0.00001, "", _defaltDateTime); });
    }

    [TestCase("2021-1-1 0:0:0")]
    [TestCase("21-01-01 00:00:00")]
    [TestCase("2101-01-01 00:00:00")]
    [TestCase("1899-01-01 00:00:00")]
    [TestCase("2022-00-01 00:00:00")]
    [TestCase("2022-13-01 00:00:00")]
    [TestCase("2022-01-00 00:00:00")]
    [TestCase("2022-01-32 00:00:00")]
    [TestCase("2022-01-01 24:00:00")]
    [TestCase("2022-01-01 -1:00:00")]
    [TestCase("2022-01-01 00:60:00")]
    [TestCase("2022-01-01 00:-1:00")]
    [TestCase("2022-01-01 00:00:60")]
    [TestCase("2022-01-01 00:00:-1")]
    [TestCase("")]
    [TestCase("2022-01-01000:00:00")]
    [TestCase("asdf-we-6s 8f:j4:7a")]
    [TestCase("sadfj238mn%@")]
    [TestCase("asdkfj;lkh363n1;lk0823h\nqweriyas3")]
    public void ThrowArgumentExceptionWhenDateTimeFormatInvalid(string dateTime)
    {
        Assert.Throws<ArgumentException>(() => { new Order(_testIdGuid, 1, "", ""); });
    }

    [Test]
    public void DoesNotThrowExceptionWhenCorrectData()
    {
        Assert.DoesNotThrow(() => { new Order(_testIdGuid, 1, "Moscow City", _defaltDateTime); });
    }

    [Test]
    public void OrdersShouldBeEqualsWithEqualsId()
    {
        Order current = new(Guid.Parse(_rawId), 1, "Moscow City", _defaltDateTime);
        Order other = new(Guid.Parse(_rawId), 12.83, "Moscow City", _defaltDateTime);

        Assert.That(current, Is.EqualTo(other));
    }

    [Test]
    public void OrdersHashCodeShouldBeEqualsWithEqualsId()
    {
        Order current = new(Guid.Parse(_rawId), 1, "Moscow City", _defaltDateTime);
        Order other = new(Guid.Parse(_rawId), 1, "Chelyabinsk Oblast", _defaltDateTime);

        Assert.That(current.GetHashCode(), Is.EqualTo(other.GetHashCode()));
    }

    [TestCase(1, "Moscow City")]
    [TestCase(14, "Chelyabinsk Oblast")]
    public void OrderShouldBeInFormat(double weight, string districtName)
    {
        string expected = $"Id: {_rawId}\tWeight: {weight,-10:F5}\tDistrict: {districtName,-25}\tDeliveryTime: {_defaltDateTime}";

        string actual = new Order(Guid.Parse(_rawId), weight, districtName, _defaltDateTime).ToString();

        Assert.That(actual, Is.EqualTo(expected));
    }
}