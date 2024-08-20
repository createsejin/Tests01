using Prime.Services;
using MyProgram.test001;

namespace Prime.UnitTests.Services;

[TestFixture]
public class PrimeService_IsPrimeShould
{
  private PrimeService _primeService;

  [SetUp]
  public void Setup()
  {
    _primeService = new PrimeService();
  }

  [Test]
  public void IsPrime_InputIs1_ReturnFalse()
  {
    var result = _primeService.IsPrime(1);
    Assert.That(result, Is.False, "1 should not be prime");
  }

  [TestCase(-1)]
  [TestCase(0)]
  [TestCase(1)]
  public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
  {
    var result = _primeService?.IsPrime(value);
    Assert.That(result, Is.False, $"{value} should not be prime");
  }

  [Test]
  public void TestAddFunc()
  {
    var result = MyClass.Add(2, 4);
    Assert.That(result, Is.EqualTo(6));
  }
}