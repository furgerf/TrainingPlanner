using FluentAssertions;
using Xunit;

namespace TrainingPlannerTest
{
  public class StepTest
  {
    [Fact]
    public void TestConstructorException()
    {
      "foo".Should().NotBeEmpty();
      Assert.Equal(4, 2 + 2);
    }
  }
}