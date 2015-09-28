using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using TrainingPlanner.Model.EventArgs;
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

    #region Constructor
    public DataPersistence(Data data)
    {
      data.PaceChanged += (s, e) => SavePaceToSettings(e.ModifiedPace, e.NewPace);
      data.WorkoutChanged += (s, e) => OnWorkoutChanged(e);
      data.CategoryChanged += (s, e) => OnCategoryChanged(e);
      data.TrainingPlanChanged += (s, e) => OnTrainingPlanChanged(data.TrainingPlan);

      Logger.Info("DataPersistence instantiated");
    }
    #endregion

    #region Event handlers
    private static void OnWorkoutChanged(WorkoutChangedEventArgs e)
    {
      if (e.WorkoutAdded)
      {
        Logger.InfoFormat("Writing workout {0} to file", e.Workout.Name);
        WriteJsonFile(e.Workout, GetWorkoutPath(e.Workout));
      }
      else
      {
        Logger.InfoFormat("Deleting workout {0}", e.Workout.Name);
        File.Delete(GetWorkoutPath(e.Workout));
      }
    }

    private static void OnCategoryChanged(WorkoutCategoryChangedEventArgs e)
    {
      if (e.CategoryAdded)
      {
        Logger.InfoFormat("Writing category {0} to file", e.WorkoutCategory.Name);
        WriteJsonFile(e.WorkoutCategory, GetWorkoutCategoryPath(e.WorkoutCategory));
      }
      else
      {
        Logger.InfoFormat("Deleting category {0}", e.WorkoutCategory.Name);
        File.Delete(GetWorkoutCategoryPath(e.WorkoutCategory));
      }
    }

    private static void OnTrainingPlanChanged(TrainingPlan plan)
    {
      Logger.Info("Training plan saved");
      WriteJsonFile(plan, TrainingPlanFile);
    }
    #endregion

    #region Public access - Loading data
    public IEnumerable<WorkoutCategory> LoadCategories()
    {
      Logger.Info("Loading workout categories");
      return
        Directory.GetFiles(WorkoutCategoriesDirectory, "*.json")
          .Select(ParseJsonFile<WorkoutCategory>);
    }

    public IEnumerable<Workout> LoadWorkouts()
    {
      Logger.Info("Loading workouts");
      return Directory.GetFiles(WorkoutsDirectory, "*.json").Select(ParseJsonFile<Workout>);
    }

    public TrainingPlan LoadPlan()
    {
      Logger.Info("Loading training plan");
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
      Logger.InfoFormat("Saving pace {0} with new value {1}", pace, value);

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