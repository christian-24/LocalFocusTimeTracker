using System.IO;

namespace LocalFocusTimeTracker.Helpers
{
    internal class PathHelper
    {
        internal static string GetIndexHtmlPath()
        {
            return Path.Combine(GetBaseFilePath(), "index.html");
        }

        internal static string GetTrackPath()
        {
            return Path.Combine(GetBaseFilePath(), "track.json");
        }

        internal static string GetWebView2DemoUserDataPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WebView2DemoUserData");
        }

        internal static string GetBaseFilePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nameof(LocalFocusTimeTracker));
        }
    }
}
