using LocalFocusTimeTracker.Dtos;
using LocalFocusTimeTracker.Helpers;
using Microsoft.Web.WebView2.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace LocalFocusTimeTracker.Forms
{
    public partial class StatisticsWindow : UserControl
    {
        public StatisticsWindow()
        {
            InitializeComponent();
            LoadData();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string userDataFolder = PathHelper.GetWebView2DemoUserDataPath();

            try
            {
                var env = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

                await MyWebView.EnsureCoreWebView2Async(env);

                var htmlPath = PathHelper.GetIndexHtmlPath();

                if (!File.Exists(htmlPath))
                {
                    File.WriteAllText(htmlPath, "<html><body><h1>Brak danych</h1></body></html>");
                }

                var fixedPath = "file:///" + htmlPath.Replace("\\", "/");
                MyWebView.Source = new Uri(fixedPath);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error WebView2: " + ex.Message);
            }
        }

        private void LoadData()
        {
            var trackPath = PathHelper.GetTrackPath();

            if (!File.Exists(trackPath))
            {
                return;
            }

            var json = File.ReadAllText(trackPath);
            var data = JsonSerializer.Deserialize<Dictionary<string, TimeEntryDto>>(json);

            string today = DateTime.Now.ToString("yyyy-MM-dd");

            var todayEntries = data?
                .Where(x => x.Value.Date == today)
                .ToList();

            if (todayEntries == null || todayEntries.Count == 0)
            {
                return;
            }

            var totalSeconds = todayEntries.Sum(x => x.Value.Seconds);

            var displayList = todayEntries
                .Select(x => new TimeEntryDisplayDto
                {
                    Name    = x.Key,
                    Time    = TimeSpan.FromSeconds(x.Value.Seconds).ToString(@"hh\:mm\:ss"),
                    Percent = totalSeconds > 0 ? (int)Math.Round(x.Value.Seconds * 100.0 / totalSeconds) : 0
                })
                .OrderByDescending(x => x.Percent)
                .ToList();

            var html      = HtmlHelper.CreateProjectStatsTemplate(displayList);
            var indexPath = PathHelper.GetIndexHtmlPath();

            File.WriteAllText(indexPath, html);
        }
    }
}
