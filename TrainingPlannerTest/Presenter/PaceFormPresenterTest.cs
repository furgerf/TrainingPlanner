using System;
using Moq;
using TrainingPlanner.Model;
using TrainingPlanner.Model.Serializable;
using TrainingPlanner.Presenter;
using TrainingPlanner.View.Interfaces;
using Xunit;

namespace TrainingPlannerTest.Presenter
{
  public class PaceFormPresenterTest
  {
    private readonly Mock<IPaceForm> _viewMock;
    private readonly Mock<IData> _dataMock;
    private readonly PaceFormPresenter _testee;

    public PaceFormPresenterTest()
    {
      _viewMock = new Mock<IPaceForm>();
      _dataMock = new Mock<IData>();

      _viewMock
        .SetupSequence(view => view.ChangedPaces)
        .Returns(new Tuple<Pace.Names, TimeSpan>[0])
        .Returns(new[] {new Tuple<Pace.Names, TimeSpan>(Pace.Names.Base, TimeSpan.FromMinutes(5))})
        .Returns(new[]
        {
          new Tuple<Pace.Names, TimeSpan>(Pace.Names.Base, TimeSpan.FromMinutes(5)),
          new Tuple<Pace.Names, TimeSpan>(Pace.Names.Marathon, TimeSpan.FromSeconds(1234))
        });

      _testee = new PaceFormPresenter(_viewMock.Object, _dataMock.Object);
    }

    [Fact]
    public void TestOnSaveButtonClick()
    {
      _viewMock.Raise(form => form.SaveChangesButtonClick += null, null, null);
      _dataMock.Verify(a => a.ChangePaces(new Pace.Names[0], new TimeSpan[0]));
      _viewMock.Verify(v => v.Close());

      _viewMock.Raise(form => form.SaveChangesButtonClick += null, null, null);
      _dataMock.Verify(a => a.ChangePaces(new[] {Pace.Names.Base}, new[] {TimeSpan.FromMinutes(5)}));
      _viewMock.Verify(v => v.Close()); // this test passes anyway because it was called previously already

      _viewMock.Raise(form => form.SaveChangesButtonClick += null, null, null);
      _dataMock.Verify(
        a => a.ChangePaces(
          new[] {Pace.Names.Base, Pace.Names.Marathon},
          new[] {TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(1234)}
          ));
      _viewMock.Verify(v => v.Close()); // this test passes anyway because it was called previously already
    }

    [Fact]
    public void TestDiscardChangesButtonClick()
    {
      _viewMock.Raise(form => form.DiscardChangesButtonClick += null, null, null);
      _viewMock.Verify(v => v.Close());
    }
  }
}