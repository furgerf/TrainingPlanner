using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace TrainingPlanner.Model
{
  [DataContract]
  public class Workout
  {
    #region Public Fields

    [DataMember]
    public readonly string Name;

    [DataMember]
    public Step[] Steps { get; private set; }

    public TimeSpan Duration { get { return TimeSpan.FromSeconds(Steps.Sum(s => (s.Duration.TotalSeconds + s.Rest.TotalSeconds) * s.Repetitions)); }}

    public double Distance { get { return Steps.Sum(s => s.Distance * s.Repetitions); }}

    public TimeSpan AveragePace { get { return TimeSpan.FromSeconds(Steps.Sum(s => s.Pace.TotalSeconds * s.Distance) / Distance); }}

    public string Description
    {
      get
      {
          var result = Steps.Aggregate("", (current, s) => current + (Environment.NewLine + s.Name + ": " + s));

          return result.TrimStart();
      }
    }

    public string Json
    {
      get
      {
        var stream = new MemoryStream();
        var serializer = new DataContractJsonSerializer(typeof (Workout));
        serializer.WriteObject(stream, this);
        stream.Position = 0;
        var reader = new StreamReader(stream);

        return reader.ReadToEnd();
      }
    }

    #endregion

    #region Constructor

    public Workout(string name, Step[] steps)
    {
      Name = name;
      Steps = steps;
    }

    #endregion

    #region Main Methods

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

    #endregion
  }
}
