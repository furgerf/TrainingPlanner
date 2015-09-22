using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace TrainingPlanner.Model
{
  [DataContract]
  public class WorkoutCategory
  {
    [DataMember]
    public readonly string Name;

    [DataMember]
    public readonly Color CategoryColor;

    public WorkoutCategory(string name, Color categoryColor )
    {
      this.Name = name;
      this.CategoryColor = categoryColor;
    }

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