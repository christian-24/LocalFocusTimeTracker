using EnvDTE;
using LocalFocusTimeTracker.Dtos;
using LocalFocusTimeTracker.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LocalFocusTimeTracker.Storage
{
    public static class TimeStorage
    {
        private static readonly string FolderPath = PathHelper.GetBaseFilePath();
        private static readonly string FilePath   = PathHelper.GetTrackPath();

        private static string GetToday() => DateTime.Now.ToString("yyyy-MM-dd");

        private static string GetSolutionName()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var dte  = (DTE)Package.GetGlobalService(typeof(DTE));
            var path = dte?.Solution?.FullName;

            return string.IsNullOrEmpty(path) ? "NoSolution" : Path.GetFileNameWithoutExtension(path);
        }

        private static Dictionary<string, TimeEntryDto> LoadAll()
        {
            if (!File.Exists(FilePath))
            {
                return new();
            }

            var json = File.ReadAllText(FilePath);

            return JsonSerializer.Deserialize<Dictionary<string, TimeEntryDto>>(json) ?? new();
        }

        private static void SaveAll(Dictionary<string, TimeEntryDto> data)
        {
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(FilePath, json);
        }

        public static int LoadTime()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var all    = LoadAll();
            string key = GetSolutionName();

            if (all.TryGetValue(key, out var entry) && entry.Date == GetToday())
            {
                return entry.Seconds;
            }

            return 0;
        }

        public static void SaveTime(int seconds)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var all    = LoadAll();
            string key = GetSolutionName();

            all[key] = new TimeEntryDto
            {
                Date    = GetToday(),
                Seconds = seconds
            };

            SaveAll(all);
        }


        public static void ClearOutdated()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var all      = LoadAll();
            string today = GetToday();

            bool changed = false;

            var keysToRemove = all
                .Where(kvp => kvp.Value.Date != today)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var key in keysToRemove)
            {
                all.Remove(key);
                changed = true;
            }

            if (changed)
            {
                SaveAll(all);
            }
        }
    }
}
