using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace TrainingPlanner.Model
{
  public class DataPersistence
  {
    private const string ApplicationDataDirectoryWindows = @"D:\data\training-planner-data";
    private const string ApplicationDataDirectoryLinux = "/data/data/training-planner-data";
    private const string TrainingPlanFileName = "plan.json";
    private const string WorkoutsDirectoryName = "workouts";
    private const string WorkoutCategoriesDirectoryName = "workout-categories";

    private static readonly bool IsLinux = !Environment.OSVersion.Platform.ToString().ToLower().StartsWith("win");

    private static readonly string ApplicationDataDirectory = IsLinux
      ? ApplicationDataDirectoryLinux
      : ApplicationDataDirectoryWindows;

    private static string WorkoutsDirectory
    {
      get { return ApplicationDataDirectory + Path.DirectorySeparatorChar + WorkoutsDirectoryName; }
    }
    private static string WorkoutCategoriesDirectory
    {
      get { return ApplicationDataDirectory + Path.DirectorySeparatorChar + WorkoutCategoriesDirectoryName; }
    }
    private static string TrainingPlanFile
    {
      get { return ApplicationDataDirectory + Path.DirectorySeparatorChar + TrainingPlanFileName; }
    }

    public DataPersistence(Data data)
    {
      data.PacesChanged += (s, e) => SavePaceToSettings(e.ModifiedPace);
      data.WorkoutChanged += (s, e) =>
      {
        if (e.WorkoutAdded)
        {
          WriteJsonFile(e.Workout, GetWorkoutPath(e.Workout));
        }
        else
        {
          File.Delete(GetWorkoutPath(e.Workout));
        }
      };
      data.CategoryChanged += (s, e) =>
      {
        if (e.CategoryAdded)
        {
          WriteJsonFile(e.WorkoutCategory, GetWorkoutCategoryPath(e.WorkoutCategory));
        }
        else
        {
          File.Delete(GetWorkoutCategoryPath(e.WorkoutCategory));
        }
      };
      data.TrainingPlanChanged +=
        (s, e) => File.WriteAllLines(TrainingPlanFile, data.TrainingPlan.WeeklyPlans.Select(WeeklyPlanToJson));
    }

    private static string GetWorkoutPath(Workout workout)
    {
      return WorkoutsDirectory + Path.DirectorySeparatorChar + workout.Name.ToLower().Replace(' ', '-') + ".json";
    }

    private static string GetWorkoutCategoryPath(WorkoutCategory category)
    {
      return WorkoutCategoriesDirectory + Path.DirectorySeparatorChar + category.Name.ToLower().Replace(' ', '-') +
             ".json";
    }

    private static void SavePaceToSettings(Pace pace)
    {
      switch (pace)
      {
        case Pace.Easy:
          Paces.Default.Easy = Data.Paces[Pace.Easy];
          break;
        case Pace.Long:
          Paces.Default.Long = Data.Paces[Pace.Long];
          break;
        case Pace.Marathon:
          Paces.Default.Marathon = Data.Paces[Pace.Marathon];
          break;
        case Pace.Halfmarathon:
          Paces.Default.Halfmarathon = Data.Paces[Pace.Halfmarathon];
          break;
        case Pace.Threshold:
          Paces.Default.Threshold = Data.Paces[Pace.Threshold];
          break;
        case Pace.Tenk:
          Paces.Default.TenK = Data.Paces[Pace.Tenk];
          break;
        case Pace.Fivek:
          Paces.Default.FiveK = Data.Paces[Pace.Fivek];
          break;
        default:
          throw new ArgumentOutOfRangeException("pace");
      }

      Paces.Default.Save();
    }

    public IEnumerable<WorkoutCategory> LoadCategories()
    {
      return
        Directory.GetFiles(WorkoutCategoriesDirectory, "*.json")
          .Select(wc => (WorkoutCategory)ParseJsonFile(wc, typeof(WorkoutCategory)));
    }

    public IEnumerable<Workout> LoadWorkouts()
    {
      return Directory.GetFiles(WorkoutsDirectory, "*.json").Select(wc => (Workout)ParseJsonFile(wc, typeof(Workout)));
    }

    public TrainingPlan LoadPlan()
    {
      // TODO: Change
      if (File.Exists(TrainingPlanFile))
      {
        //return (TrainingPlan)ParseJsonFile(TrainingPlanFile, typeof(TrainingPlan));

        var weeks = File.ReadAllLines(TrainingPlanFile).Select(ParseWeeklyPlanJson).ToArray();

        return new TrainingPlan {WeeklyPlans = weeks};
      }

      return TrainingPlan.NewTrainingPlan;
    }

    private static object ParseJsonFile(string path, Type type)
    {
      using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        return new DataContractJsonSerializer(type).ReadObject(fs);
      }
    }

    private static WeeklyPlan ParseWeeklyPlanJson(string json)
    {
      // TODO: get rid of
      var bytes = new byte[json.Length*sizeof (char)];
      Buffer.BlockCopy(json.ToCharArray(), 0, bytes, 0, bytes.Length);
      using (var ms = new MemoryStream(bytes))
      {
        return (WeeklyPlan) new DataContractJsonSerializer(typeof (WeeklyPlan)).ReadObject(ms);
      }
    }

    private static string WeeklyPlanToJson(WeeklyPlan plan)
    {
      // TODO: get rid of
      var stream = new MemoryStream();
      var serializer = new DataContractJsonSerializer(typeof (WeeklyPlan));
      serializer.WriteObject(stream, plan);
      stream.Position = 0;
      var reader = new StreamReader(stream);
      return reader.ReadToEnd();
    }


    public void WriteJsonFile(object data, string path)
    {
      using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
      {
        new DataContractJsonSerializer(data.GetType()).WriteObject(fs, data);
      }
      //new DataContractJsonSerializer(data.GetType()).WriteObject(stream, this);
      //stream.Position = 0;
      //return new StreamReader(stream).ReadToEnd();
    }
  }
}