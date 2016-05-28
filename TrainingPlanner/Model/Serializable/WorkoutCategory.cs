using System.Drawing;
using System.Runtime.Serialization;

namespace TrainingPlanner.Model.Serializable
{
  /// <summary>
  /// Describes a category of workouts.
  /// </summary>
  [DataContract(Name = "WorkoutCategory")]
  public class WorkoutCategory
  {
    /// <summary>
    /// Name of the category.
    /// </summary>
    [DataMember(Name = "Name", IsRequired = true)]
    public readonly string Name;

    /// <summary>
    /// Color with which to highlight the category.
    /// </summary>
    [DataMember(Name = "CategoryColor", IsRequired = true)]
    public readonly Color CategoryColor;

    public WorkoutCategory(string name, Color categoryColor )
    {
      Name = name;
      CategoryColor = categoryColor;
    }

    public override string ToString()
    {
      return string.Format("Workout category {0}", Name);
    }
  }
}