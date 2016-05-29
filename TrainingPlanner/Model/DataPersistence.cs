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

    /// <summary>
    /// The application working directory.
    /// </summary>
    public static readonly string ApplicationDataDirectory = IsLinux
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

    /// <summary>
    /// The path to the application log file.
    /// </summary>
    public static string LogFile
    {
      get { return ApplicationDataDirectory + Path.DirectorySeparatorChar + LogFileName; }
    }

    private string WorkoutsDirectory
    {
      get
      {
        return ApplicationDataDirectory + Path.DirectorySeparatorChar + _data.TrainingPlan.Name +
               Path.DirectorySeparatorChar +
               WorkoutsDirectoryName;
      }
    }

    private string WorkoutCategoriesDirectory
    {
      get
      {
        return ApplicationDataDirectory + Path.DirectorySeparatorChar + _data.TrainingPlan.Name +
               Path.DirectorySeparatorChar +
               WorkoutCategoriesDirectoryName;
      }
    }

    private string TrainingPlanFile(string planName)
    {
      return ApplicationDataDirectory + Path.DirectorySeparatorChar + planName + Path.DirectorySeparatorChar +
             TrainingPlanFileName;
    }

    private string PacesFile
    {
      get
      {
        return ApplicationDataDirectory + Path.DirectorySeparatorChar + _data.TrainingPlan.Name +
               Path.DirectorySeparatorChar +
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
      WriteJsonFile(plan, TrainingPlanFile(plan.Name));
    }

    /// <summary>
    /// Loads all workout categories in the current training plan.
    /// </summary>
    /// <returns>All workout categories.</returns>
    public IEnumerable<WorkoutCategory> LoadCategories()
    {
      Logger.Info("Loading workout categories");
      return
        Directory.GetFiles(WorkoutCategoriesDirectory, "*.json")
          .Select(ParseJsonFile<WorkoutCategory>);
    }

    /// <summary>
    /// Loads all workouts in the current training plan.
    /// </summary>
    /// <returns>All workouts.</returns>
    public IEnumerable<Workout> LoadWorkouts()
    {
      Logger.Info("Loading workouts");
      return
        Directory.GetDirectories(WorkoutsDirectory)
          .SelectMany(d => Directory.GetFiles(d, "*.json").Select(ParseJsonFile<Workout>));
    }

    /// <summary>
    /// Loads the training plan with the given name.
    /// </summary>
    /// <param name="planName">Name of the training plan to load.</param>
    /// <returns>Deserialized TrainingPlan instance.</returns>
    public TrainingPlan LoadPlan(string planName)
    {
      Logger.InfoFormat("Loading training plan {0}", planName);
      return ParseJsonFile<TrainingPlan>(TrainingPlanFile(planName));
    }

    /// <summary>
    /// Loads the paces associated with the current training plan.
    /// </summary>
    /// <returns>Deserialized Pace instance.</returns>
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

    /// <summary>
    /// Writes the data of a training plan to a file that is *NOT* the currently loaded training plan file.
    /// This is intended to be used when creating a new training plan.
    /// </summary>
    /// <param name="plan">New training plan to store.</param>
    public static void CreateNewTrainingPlanFile(TrainingPlan plan)
    {
      WriteJsonFile(plan,
        ApplicationDataDirectory + Path.DirectorySeparatorChar + plan.Name + Path.DirectorySeparatorChar +
        TrainingPlanFileName);
    }

    /// <summary>
    /// Copies data associated with one training plan into the directory of another training plan.
    /// This associated data includes workouts, categories, and paces.
    /// </summary>
    /// <param name="oldPlanName">Name of the plan from where to copy the data.</param>
    /// <param name="newPlanName">Name of the plan where the data should be copied to.</param>
    public static void CopyExistingTrainingPlanDataToNewPlan(string oldPlanName, string newPlanName)
    {
      var oldPlanDirectory = ApplicationDataDirectory + Path.DirectorySeparatorChar + oldPlanName +
                             Path.DirectorySeparatorChar;
      var newPlanDirectory = ApplicationDataDirectory + Path.DirectorySeparatorChar + newPlanName +
                             Path.DirectorySeparatorChar;

      // workouts
      Directory.CreateDirectory(newPlanDirectory + WorkoutsDirectoryName);
      foreach (var category in new DirectoryInfo(oldPlanDirectory + WorkoutsDirectoryName).GetDirectories())
      {
        // create new subdirectory for the category
        Directory.CreateDirectory(newPlanDirectory + WorkoutsDirectoryName + Path.DirectorySeparatorChar + category.Name);

        // copy workouts
        foreach (var workout in category.GetFiles())
        {
          File.Copy(workout.FullName,
            newPlanDirectory + WorkoutsDirectoryName + Path.DirectorySeparatorChar + category.Name +
            Path.DirectorySeparatorChar + workout.Name);
        }
      }

      // categories
      Directory.CreateDirectory(newPlanDirectory + WorkoutCategoriesDirectoryName);
      foreach (var category in new DirectoryInfo(oldPlanDirectory + WorkoutCategoriesDirectoryName).GetFiles())
      {
        File.Copy(category.FullName,
          newPlanDirectory + WorkoutCategoriesDirectoryName + Path.DirectorySeparatorChar + category.Name);
      }

      // paces
      File.Copy(oldPlanDirectory + PacesFileName, newPlanDirectory + PacesFileName);
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