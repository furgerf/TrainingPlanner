using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace TrainingPlanner
{
  [DataContract]
  public struct WeeklyPlan
  {
    [DataMember]
    private string[] _workouts;

    [DataMember]
    public DateTime WeekStart { get; set; }

    [DataMember]
    public string Notes { get; set; }

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
