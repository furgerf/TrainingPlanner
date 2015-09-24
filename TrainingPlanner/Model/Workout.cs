using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace TrainingPlanner.Model
{
  /// <summary>
  /// Describes a workout.
  /// </summary>
  [DataContract]
  public class Workout
  {
    /// <summary>
    /// Name of the workout. Used for the file name and to uniquely identify the workout.
    /// </summary>
    [DataMember]
    public readonly string Name;

    /// <summary>
    /// Short name of the workout. Used in the view if provided.
    /// </summary>
    [DataMember]
    public readonly string ShortName;

    /// <summary>
    /// Name of the workout's category.
    /// </summary>
    [DataMember]
    public readonly string CategoryName;

    /// <summary>
    /// Steps of the workout.
    /// </summary>
    [DataMember]
    public Step[] Steps { get; private set; }

    /// <summary>
    /// Total duration of the workout. Calculated from the steps.
    /// </summary>
    public TimeSpan Duration { get { return TimeSpan.FromSeconds(Steps.Sum(s => (s.Duration.TotalSeconds + s.Rest.TotalSeconds) * s.Repetitions)); } }

    /// <summary>
    /// Total distance of the workout. Calculated from the steps.
    /// </summary>
    public double Distance { get { return Steps.Sum(s => s.Distance * s.Repetitions); } }

    /// <summary>
    /// Average pace over the entire workout. Calculated from the steps.
    /// </summary>
    public TimeSpan AveragePace { get { return TimeSpan.FromSeconds(Steps.Sum(s => s.Pace.TotalSeconds * s.Distance) / Distance); } }

    /// <summary>
    /// Description of the workout. Lists all steps separated by newlines.
    /// </summary>
    public string Description
    {
      get
      {
        return Steps.Aggregate("", (current, s) => current + (Environment.NewLine + s.Name + ": " + s)).TrimStart();
      }
    }

    /// <summary>
    /// Serializes the workout to JSON.
    /// TODO: Move to common class.
    /// </summary>
    public string Json
    {
      get
      {
        var stream = new MemoryStream();
        var serializer = new DataContractJsonSerializer(typeof(Workout));
        serializer.WriteObject(stream, this);
        stream.Position = 0;
        var reader = new StreamReader(stream);

        return reader.ReadToEnd();
      }
    }

    /// <summary>
    /// Creates a new workout without a short name.
    /// </summary>
    public Workout(string name, WorkoutCategory category, Step[] steps)
    {
      this.Name = name;
      this.CategoryName = category == null ? null : category.Name;
      this.Steps = steps;
    }

    /// <summary>
    /// Creates a new workout with a short name.
    /// </summary>
    public Workout(string name, string shortName, WorkoutCategory category, Step[] steps)
    {
      this.Name = name;
      this.ShortName = shortName;
      this.CategoryName = category.Name;
      this.Steps = steps;
    }

    /// <summary>
    /// Deserializes the workout from file containing JSON.
    /// TODO: Move to common class.
    /// </summary>
    public static Workout ParseJsonFile(string path)
    {
      var serializer = new DataContractJsonSerializer(typeof(Workout));
      using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        return (Workout)serializer.ReadObject(fs);
      }
    }

    public override string ToString()
    {
      return "Workout: " + Name;
    }
  }
}
