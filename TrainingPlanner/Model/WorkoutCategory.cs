using System.Drawing;
using System.Runtime.Serialization;

namespace TrainingPlanner.Model
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
      this.Name = name;
      this.CategoryColor = categoryColor;
    }
  }
}