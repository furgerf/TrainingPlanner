using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace TrainingPlanner.Model
{
  /// <summary>
  /// Describes a category of workouts.
  /// </summary>
  [DataContract]
  public class WorkoutCategory
  {
    /// <summary>
    /// Name of the category.
    /// </summary>
    [DataMember]
    public readonly string Name;

    /// <summary>
    /// Color with which to highlight the category.
    /// </summary>
    [DataMember]
    public readonly Color CategoryColor;

    public WorkoutCategory(string name, Color categoryColor )
    {
      this.Name = name;
      this.CategoryColor = categoryColor;
    }

    /// <summary>
    /// Serializes the category to JSON.
    /// TODO: Move to common class.
    /// </summary>
    public string Json
    {
      get
      {
        var stream = new MemoryStream();
        var serializer = new DataContractJsonSerializer(typeof(WorkoutCategory));
        serializer.WriteObject(stream, this);
        stream.Position = 0;
        var reader = new StreamReader(stream);

        return reader.ReadToEnd();
      }
    }

    /// <summary>
    /// Deserializes the workout category from file containing JSON.
    /// TODO: Move to common class.
    /// </summary>
    public static WorkoutCategory ParseJsonFile(string path)
    {
      var serializer = new DataContractJsonSerializer(typeof(WorkoutCategory));
      using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        return (WorkoutCategory)serializer.ReadObject(fs);
      }
    }
  }
}