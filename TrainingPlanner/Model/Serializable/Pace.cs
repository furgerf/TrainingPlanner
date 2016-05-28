using System;
using System.Runtime.Serialization;

namespace TrainingPlanner.Model.Serializable
{
  [DataContract(Name = "Pace")]
  public sealed class Pace
  {
    [IgnoreDataMember]
    public const string PaceFormat = "mm':'ss";

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

    public TimeSpan GetPace(PaceNames name)
    {
      switch (name)
      {
        case PaceNames.Easy:
          return Easy;
        case PaceNames.Base:
          return Base;
        case PaceNames.Steady:
          return Steady;
        case PaceNames.Marathon:
          return Marathon;
        case PaceNames.Halfmarathon:
          return Halfmarathon;
        case PaceNames.Threshold:
          return Threshold;
        case PaceNames.TenK:
          return TenK;
        case PaceNames.FiveK:
          return FiveK;
        default:
          throw new ArgumentOutOfRangeException("pace");
      }
    }

    public void SetPace(PaceNames name, TimeSpan value)
    {
      switch (name)
      {
        case PaceNames.Easy:
          Easy = value;
          break;
        case PaceNames.Base:
          Base = value;
          break;
        case PaceNames.Steady:
          Steady = value;
          break;
        case PaceNames.Marathon:
          Marathon = value;
          break;
        case PaceNames.Halfmarathon:
          Halfmarathon = value;
          break;
        case PaceNames.Threshold:
          Threshold = value;
          break;
        case PaceNames.TenK:
          TenK = value;
          break;
        case PaceNames.FiveK:
          FiveK = value;
          break;
        default:
          throw new ArgumentOutOfRangeException("pace");
      }
    }
  }
}