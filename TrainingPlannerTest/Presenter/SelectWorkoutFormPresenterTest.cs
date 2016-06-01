using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using TrainingPlanner.Model;
using TrainingPlanner.Presenter;
using TrainingPlanner.View.Interfaces;
using Xunit;

namespace TrainingPlannerTest.Presenter
{
  public class SelectWorkoutFormPresenterTest
  {
    private readonly Mock<ISelectWorkoutForm> _viewMock;
    private readonly Mock<IData> _dataMock;
    private readonly SelectWorkoutFormPresenter _testee;

    private readonly List<string> _selectedWorkouts = new List<string>();

    public SelectWorkoutFormPresenterTest()
    {
      _viewMock = new Mock<ISelectWorkoutForm>();
      _dataMock = new Mock<IData>();

      _testee = new SelectWorkoutFormPresenter(_viewMock.Object, _dataMock.Object);
      _testee.WorkoutSelected += (s, e) => _selectedWorkouts.Add(e);
    }

    [Fact]
    public void TestWorkoutSelected()
    {
      _viewMock.Raise(form => form.WorkoutSelected += null, null, "foo");
      _selectedWorkouts.Should().HaveCount(1);
      _selectedWorkouts.Last().Should().Be("foo");

      _viewMock.Raise(form => form.WorkoutSelected += null, null, "bar");
      _selectedWorkouts.Should().HaveCount(2);
      _selectedWorkouts.Last().Should().Be("bar");
    }
  }
}