using System;
using System.Runtime.Serialization;

namespace TrainingPlanner.Model.Serializable
{
  /// <summary>
  /// Describes the set of different paces that are used in the trainings.
  /// </summary>
  [DataContract(Name = "Pace")]
  public sealed class Pace
  {
    /// <summary>
    /// The different paces at which to run.
    /// </summary>
    public enum Names
    {
      Easy,
      Base,
      Steady,
      Marathon,
      Threshold,
      Halfmarathon,
      TenK,
      FiveK
    }

    [IgnoreDataMember]
    public const string PaceFormat = @"mm\:ss";

    [DataMember(Name = "Easy", IsRequired = true)]
    public TimeSpan Easy { get; set; }
     
    [DataMember(Name = "Base", IsRequired = true)]
    public TimeSpan Base { get; set; }
     
    [DataMember(Name = "Steady", IsRequired = true)]
    public TimeSpan Steady { get; set; }
     
    [DataMember(Name = "Marathon", IsRequired = true)]
    public TimeSpan Marathon { get; set; }
     
    [DataMember(Name = "Threshold", IsRequired = true)]
    public TimeSpan Threshold { get; set; }
     
    [DataMember(Name = "Halfmarathon", IsRequired = true)]
    public TimeSpan Halfmarathon { get; set; }
     
    [DataMember(Name = "TenK", IsRequired = true)]
    public TimeSpan TenK { get; set; }
     
    [DataMember(Name = "FiveK", IsRequired = true)]
    public TimeSpan FiveK { get; set; }

    public void SetPace(Names name, TimeSpan value)
    {
      switch (name)
      {
        case Names.Easy:
          Easy = value;
          break;
        case Names.Base:
          Base = value;
          break;
        case Names.Steady:
          Steady = value;
          break;
        case Names.Marathon:
          Marathon = value;
          break;
        case Names.Halfmarathon:
          Halfmarathon = value;
          break;
        case Names.Threshold:
          Threshold = value;
          break;
        case Names.TenK:
          TenK = value;
          break;
        case Names.FiveK:
          FiveK = value;
          break;
        default:
          throw new ArgumentOutOfRangeException("name");
      }
    }
  }
}