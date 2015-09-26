using System;
using FluentAssertions;
using TrainingPlanner.Model;
using TrainingPlanner.Model.Serializable;
using Xunit;

namespace TrainingPlannerTest
{
  public class StepTest
  {
    [Fact]
    public void TestConstructorException()
    {
      Assert.Throws<ArgumentNullException>(() => new Step("foo", TimeSpan.Zero, TimeSpan.Zero));

      "foo".Should().BeEmpty();
    }
  }
}