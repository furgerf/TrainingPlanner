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
    private const string WorkoutsDirectoryName = "workouts";
    private const string WorkoutCategoriesDirectoryName = "workout-categories";
    private const string LogFileName = "training-planner.log";

    private static readonly bool IsLinux = !Environment.OSVersion.Platform.ToString().ToLower().StartsWith("win");

    private static readonly string ApplicationDataDirectory = IsLinux
      ? ApplicationDataDirectoryLinux
      : ApplicationDataDirectoryWindows;

    private const int DefaultTrainingWeeks = 12;

    private static string WorkoutsDirectory
    {
      get { return ApplicationDataDirectory + Path.DirectorySeparatorChar + WorkoutsDirectoryName; }
    }
    private static string WorkoutCategoriesDirectory
    {
      get { return ApplicationDataDirectory + Path.DirectorySeparatorChar + WorkoutCategoriesDirectoryName; }
    }
    public static string LogFile
    {
      get { return ApplicationDataDirectory + Path.DirectorySeparatorChar + LogFileName; }
    }
    #endregion

    private readonly Data _data;

    #region Constructor
    public DataPersistence(Data data)
    {
      this._data = data;

      this._data.PaceChanged += (s, e) => SavePaceToSettings(e.ModifiedPace, e.NewPace);
      this._data.WorkoutChanged += (s, e) => OnWorkoutChanged(e);
      this._data.CategoryChanged += (s, e) => OnCategoryChanged(e);
      this._data.TrainingPlanModified += (s, e) => OnTrainingPlanChanged(data.TrainingPlan);
      this._data.TrainingPlanLoaded += (s, e) => OnTrainingPlanLoaded();

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
      WriteJsonFile(plan, TrainingPlanFile(plan.Name));
    }

    private void OnTrainingPlanLoaded()
    {
      this._data.TrainingPlan.NameChanged += (s, e) =>
      {
        var oldName = e;
        var newName = ((TrainingPlan) s).Name;

        File.Move(TrainingPlanFile(oldName), TrainingPlanFile(newName));
      };
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

    public TrainingPlan LoadPlan(string planName)
    {
      Logger.InfoFormat("Loading training plan {0}", planName);
      return File.Exists(TrainingPlanFile(planName))
        ? ParseJsonFile<TrainingPlan>(TrainingPlanFile(planName))
        : TrainingPlan.NewTrainingPlan(DefaultTrainingWeeks);
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

    private static string TrainingPlanFile(string planName)
    {
      return ApplicationDataDirectory + Path.DirectorySeparatorChar + planName.Replace(' ', '-').ToLower() + ".json";
    }

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