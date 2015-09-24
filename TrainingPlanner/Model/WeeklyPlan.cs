using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace TrainingPlanner.Model
{
  /// <summary>
  /// Describes a week's worth of training (up to 14 workouts).
  /// </summary>
  [DataContract]
  public struct WeeklyPlan
  {
    /// <summary>
    /// The names of the workouts of the week.
    /// TODO: Rename...
    /// </summary>
    [DataMember]
    private string[] _workouts;

    /// <summary>
    /// Date of the start of the week (Monday).
    /// </summary>
    [DataMember]
    public DateTime WeekStart { get; set; }

    /// <summary>
    /// Notes for the week.
    /// </summary>
    [DataMember]
    public string Notes { get; set; }

    /// <summary>
    /// Wrapper around the backing field with the sole purpose to ensure that
    /// arrays are assigned with the right dimension.
    /// </summary>
    public string[] Workouts
    {
      get { return _workouts; }
      set
      {
        if (value == null || value.Length != 14)
        {
          throw new ArgumentException("Invalid workout array");
        }
        _workouts = value;
      }
    }

    /// <summary>
    /// Serializes the plan to JSON.
    /// TODO: Move to common class.
    /// </summary>
    public string Json
    {
      get
      {
        var stream = new MemoryStream();
        var serializer = new DataContractJsonSerializer(typeof(WeeklyPlan));
        serializer.WriteObject(stream, this);
        stream.Position = 0;
        var reader = new StreamReader(stream);
        return reader.ReadToEnd();
      }
    }

    /// <summary>
    /// Deserializes the plan from JSON.
    /// TODO: Move to common class.
    /// </summary>
    public static WeeklyPlan FromJson(string json)
    {
      var serializer = new DataContractJsonSerializer(typeof (WeeklyPlan));
      var bytes = new byte[json.Length*sizeof (char)];
      Buffer.BlockCopy(json.ToCharArray(), 0, bytes, 0, bytes.Length);
      using (var ms = new MemoryStream(bytes))
      {
        return (WeeklyPlan) serializer.ReadObject(ms);
      }
    }
  }
}
