using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.Model
{
  public class DataPersistence
  {
    #region Paths
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
    #endregion

    public DataPersistence(Data data)
    {
      // TODO: Move handlers into separate methods (with console output!)
      data.PaceChanged += (s, e) => SavePaceToSettings(e.ModifiedPace, e.NewPace);
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
      data.TrainingPlanChanged += (s, e) => WriteJsonFile(data.TrainingPlan, TrainingPlanFile);

      Console.WriteLine("DataPersistence instantiated");
    }

    #region Public access - Loading data
    public IEnumerable<WorkoutCategory> LoadCategories()
    {
      Console.WriteLine("Loading workout categories");
      return
        Directory.GetFiles(WorkoutCategoriesDirectory, "*.json")
          .Select(ParseJsonFile<WorkoutCategory>);
    }

    public IEnumerable<Workout> LoadWorkouts()
    {
      Console.WriteLine("Loading workouts");
      return Directory.GetFiles(WorkoutsDirectory, "*.json").Select(ParseJsonFile<Workout>);
    }

    public TrainingPlan LoadPlan()
    {
      Console.WriteLine("Loading training plan");
      return File.Exists(TrainingPlanFile)
        ? ParseJsonFile<TrainingPlan>(TrainingPlanFile)
        : TrainingPlan.NewTrainingPlan;
    }

    #endregion

    #region Private access - Saving data
    private static string GetWorkoutPath(Workout workout)
    {
      return WorkoutsDirectory + Path.DirectorySeparatorChar + workout.Name.ToLower().Replace(' ', '-') + ".json";
    }

    private static string GetWorkoutCategoryPath(WorkoutCategory category)
    {
      return WorkoutCategoriesDirectory + Path.DirectorySeparatorChar + category.Name.ToLower().Replace(' ', '-') +
             ".json";
    }

    private static void SavePaceToSettings(Pace pace, TimeSpan value)
    {
      Console.WriteLine("Saving pace {0} with new value {1}", pace, value);

      switch (pace)
      {
        case Pace.Easy:
          Paces.Default.Easy = value;
          break;
        case Pace.Long:
          Paces.Default.Long = value;
          break;
        case Pace.Marathon:
          Paces.Default.Marathon = value;
          break;
        case Pace.Halfmarathon:
          Paces.Default.Halfmarathon = value;
          break;
        case Pace.Threshold:
          Paces.Default.Threshold = value;
          break;
        case Pace.Tenk:
          Paces.Default.TenK = value;
          break;
        case Pace.Fivek:
          Paces.Default.FiveK = value;
          break;
        default:
          throw new ArgumentOutOfRangeException("pace");
      }

      Paces.Default.Save();
    }
    #endregion

    #region (De-)serialization
    private static T ParseJsonFile<T>(string path)
    {
      using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        return (T) new DataContractJsonSerializer(typeof(T)).ReadObject(fs);
      }
    }

    private static void WriteJsonFile<T>(T data, string path)
    {
      using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
      {
        new DataContractJsonSerializer(typeof(T)).WriteObject(fs, data);
      }
    }
    #endregion
  }
}