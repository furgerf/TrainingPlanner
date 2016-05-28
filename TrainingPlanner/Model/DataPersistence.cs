using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using TrainingPlanner.Model.EventArgs;
using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.Model
{
  /// <summary>
  /// Handles the persistence of a `Data` instance.
  /// </summary>
  public class DataPersistence
  {
    // application root directory
    private const string ApplicationDataDirectoryWindows = @"D:\data\training-planner-data";
    private const string ApplicationDataDirectoryLinux = "/data/data/training-planner-data";
    private static readonly bool IsLinux = !Environment.OSVersion.Platform.ToString().ToLower().StartsWith("win");

    private static readonly string ApplicationDataDirectory = IsLinux
      ? ApplicationDataDirectoryLinux
      : ApplicationDataDirectoryWindows;

    // application sub-directories
    private const string LogFileName = "training-planner.log";

    private const string WorkoutsDirectoryName = "workouts";
    private const string WorkoutCategoriesDirectoryName = "workout-categories";
    private const string TrainingPlanFileName = "plan.json";
    private const string PacesFileName = "paces.json";

    // data to persist - contains the training plan's name
    private readonly Data _data;

    // ...
    private const int DefaultTrainingWeeks = 16;

    public static string LogFile
    {
      get { return ApplicationDataDirectory + Path.DirectorySeparatorChar + LogFileName; }
    }

    private string WorkoutsDirectory
    {
      get
      {
        return ApplicationDataDirectory + Path.DirectorySeparatorChar + _data.PlanName + Path.DirectorySeparatorChar +
               WorkoutsDirectoryName;
      }
    }

    private string WorkoutCategoriesDirectory
    {
      get
      {
        return ApplicationDataDirectory + Path.DirectorySeparatorChar + _data.PlanName + Path.DirectorySeparatorChar +
               WorkoutCategoriesDirectoryName;
      }
    }

    private string TrainingPlanFile
    {
      get
      {
        return ApplicationDataDirectory + Path.DirectorySeparatorChar + _data.PlanName + Path.DirectorySeparatorChar +
               TrainingPlanFileName;
      }
    }
    private string PacesFile
    {
      get
      {
        return ApplicationDataDirectory + Path.DirectorySeparatorChar + _data.PlanName + Path.DirectorySeparatorChar +
               PacesFileName;
      }
    }

    /// <summary>
    /// Creates a new persistence for the provided `Data` instance.
    /// </summary>
    /// <param name="data">Data to persist.</param>
    public DataPersistence(Data data)
    {
      _data = data;

      _data.PaceChanged += (s, e) => SavePace();
      _data.WorkoutChanged += (s, e) => OnWorkoutChanged(e);
      _data.CategoryChanged += (s, e) => OnCategoryChanged(e);
      _data.TrainingPlanModified += (s, e) => OnTrainingPlanChanged(data.TrainingPlan);
      _data.TrainingPlanLoaded += (s, e) => OnTrainingPlanLoaded();

      Logger.Info("DataPersistence instantiated");
    }

    private string GetWorkoutPath(Workout workout)
    {
      return WorkoutsDirectory + Path.DirectorySeparatorChar + workout.CategoryName + Path.AltDirectorySeparatorChar +
             workout.Name.ToLower().Replace(' ', '-') + ".json";
    }

    private string GetWorkoutCategoryPath(WorkoutCategory category)
    {
      return WorkoutCategoriesDirectory + Path.DirectorySeparatorChar + category.Name.ToLower().Replace(' ', '-') +
             ".json";
    }

    private void OnWorkoutChanged(WorkoutChangedEventArgs e)
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

    private void OnCategoryChanged(WorkoutCategoryChangedEventArgs e)
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

    private void OnTrainingPlanChanged(TrainingPlan plan)
    {
      Logger.Info("Training plan saved");
      WriteJsonFile(plan, TrainingPlanFile);
    }

    private void OnTrainingPlanLoaded()
    {
      _data.TrainingPlan.NameChanged += (s, e) =>
      {
        throw new NotImplementedException();
        /*
        var oldName = e;
        var newName = ((TrainingPlan) s).Name;

        File.Move(TrainingPlanFile(oldName), TrainingPlanFile(newName));
        */
      };
    }

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
      return
        Directory.GetDirectories(WorkoutsDirectory)
          .SelectMany(d => Directory.GetFiles(d, "*.json").Select(ParseJsonFile<Workout>));
    }

    public TrainingPlan LoadPlan()
    {
      Logger.InfoFormat("Loading training plan {0}", _data.PlanName);
      return File.Exists(TrainingPlanFile)
        ? ParseJsonFile<TrainingPlan>(TrainingPlanFile)
        : TrainingPlan.NewTrainingPlan(DefaultTrainingWeeks);
    }

    public Pace LoadPace()
    {
      Logger.Info("Loading paces from file");
      return ParseJsonFile<Pace>(PacesFile);
    }

    private void SavePace()
    {
      Logger.Info("Saving paces to file");
      WriteJsonFile(_data.Pace, PacesFile);
    }

    private static T ParseJsonFile<T>(string path)
    {
      using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        return (T) new DataContractJsonSerializer(typeof (T)).ReadObject(fs);
      }
    }

    private static void WriteJsonFile<T>(T data, string path)
    {
      using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
      {
        new DataContractJsonSerializer(typeof (T)).WriteObject(fs, data);
      }
    }
  }
}